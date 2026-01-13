using MonkeyCache.FileStore;
using SharpCompress.Archives.Rar;
using UltimateRemote.Models;

// ReSharper disable InconsistentNaming

namespace UltimateRemote.Services;

public sealed class JukeboxService(HttpClient httpClient, PreferencesManager prefsMgr)
{
    public event EventHandler<BgTaskNotification>? ImportStatusChangedEvent;

    public JukeboxPlaylist[] Playlists { get; private set; } = Barrel.Current.Get<JukeboxPlaylist[]>(key: CacheKeys.JukeboxPlaylists) ??
                                                               Array.Empty<JukeboxPlaylist>();

    public SidFileInfo[] Library { get; private set; } = Barrel.Current.Get<SidFileInfo[]>(key: CacheKeys.JukeboxLib) ?? Array.Empty<SidFileInfo>();

    public bool HVSCImported => Barrel.Current.Exists(key: CacheKeys.JukeboxLib);
    
    public void StartImport()
        => ThreadPoolHelper.RegisterBgTask(ImportHVSC);

    #region Import HVSC

    private async Task ImportHVSC()
    {
        var hvscArchiveLocation = prefsMgr.HVSCArchiveLocation;
        var songLengthDbFileLocation = prefsMgr.SongLengthDbFileLocation;
        var fileInfoArchive = new FileInfo(hvscArchiveLocation);
        var fileInfoSongLengthDb = new FileInfo(songLengthDbFileLocation);

        byte[] hvscArchiveBytes;
        string? songLengthFileContent;

        try
        {
            ImportStatusChangedEvent.SignalStart(Strings.JukeboxManager.DownloadingFile(fileInfoArchive.Name));
            hvscArchiveBytes = await httpClient.GetByteArrayAsync(hvscArchiveLocation);
        }
        catch (Exception ex)
        {
            ImportStatusChangedEvent.SignalFail(Strings.JukeboxManager.DownloadFailed(hvscArchiveLocation, ex.Message), Strings.JukeboxManager.ToastTitleHVSCImportFail);
            return;
        }

        try
        {
            ImportStatusChangedEvent.SignalProgress(Strings.JukeboxManager.DownloadingFile(fileInfoSongLengthDb.Name));
            songLengthFileContent = await httpClient.GetStringAsync(songLengthDbFileLocation);
        }
        catch (Exception ex)
        {
            ImportStatusChangedEvent.SignalFail(Strings.JukeboxManager.DownloadFailed(songLengthDbFileLocation, ex.Message), Strings.JukeboxManager.ToastTitleHVSCImportFail);
            return;
        }

        ImportStatusChangedEvent.SignalProgress(Strings.JukeboxManager.ParsingSongLengthDb);
        var songLengthDict = new Dictionary<string, SidFileInfo>();
        var lineSplits = songLengthFileContent.Split(new[] { '\r', '\n' },
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1..]; // skip first line [Database]

        try
        {
            for (var index = 0; index < lineSplits.Length; index += 2)
            {
                var filePathLine = lineSplits[index];
                var hashLine = lineSplits[index + 1];
                var filePath = filePathLine.TrimStart(';', ' ').TrimEnd('\n', '\r');
                var values = hashLine.Split('=', StringSplitOptions.RemoveEmptyEntries);
                var hashMd5 = values[0];
                var songLengths = values[1].Split(' ');
                var numberOfSongs = songLengths.Length;
                var songLengthTimeSpans = songLengths.Select(sl => TimeSpan.Parse($"0:{sl}")).ToArray();
                var totalLength = new TimeSpan(songLengthTimeSpans.Sum(ts => ts.Ticks));
                songLengthDict.Add(filePath,
                    new SidFileInfo(filePath, hashMd5, StringSearchExtensions.ConvertToSearchableString(filePath),
                        numberOfSongs, totalLength, songLengthTimeSpans));
            }
        }
        catch (Exception ex)
        {
            ImportStatusChangedEvent.SignalFail(Strings.JukeboxManager.ParsingSongLengthDbFailed(ex.Message),
                Strings.JukeboxManager.ToastTitleHVSCImportFail);
            return;
        }

        var jukeboxIndex = new List<SidFileInfo>();

        try
        {
            ImportStatusChangedEvent.SignalProgress(Strings.JukeboxManager.ExtractingFiles(fileInfoArchive.Name));

            await using Stream archiveStream = new MemoryStream(hvscArchiveBytes);
            using var rarArchive = RarArchive.Open(archiveStream);

            var sidFileEntries = rarArchive.Entries
                .Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !entry.IsDirectory && entry.Key.EndsWith(".sid"))
                .ToArray();

            var soFar = 0;
            const int taskGroupSize = 500;
            var sidFileEntryGroups = sidFileEntries.ToList().Partition(taskGroupSize).ToList();
            var sidFileEntryTaskGroups =
                sidFileEntryGroups.Select(list => ExtractFilesBatch(list, songLengthDict, jukeboxIndex));

            foreach (var taskGroup in sidFileEntryTaskGroups)
            {
                await taskGroup;
                soFar += taskGroupSize;
                ImportStatusChangedEvent.SignalProgress(Strings.JukeboxManager.ExtractingFiles(fileInfoArchive.Name, sidFileEntries.Length, soFar));
            }

            var jukeboxLib = jukeboxIndex.OrderBy(item => item.SearchContent).ToArray();
            
            Barrel.Current.Add<SidFileInfo[]>(CacheKeys.JukeboxLib, jukeboxLib, TimeSpan.Zero);
            
            Library = jukeboxLib;

        }
        catch (Exception ex)
        {
            ImportStatusChangedEvent.SignalFail(Strings.JukeboxManager.ExtractFailed(fileInfoArchive.Name, ex.Message), Strings.JukeboxManager.ToastTitleHVSCImportFail);
            return;
        }

        ImportStatusChangedEvent.SignalSuccess(Strings.JukeboxManager.ToastMsgHVSCImportSuccess, Strings.JukeboxManager.ToastTitleHVSCImportSuccess);

    }

    private async Task ExtractFilesBatch(List<RarArchiveEntry> sidFileEntries, Dictionary<string, SidFileInfo> songLengthDict, List<SidFileInfo> jukeboxIndex)
    {
        foreach (var sidFileEntry in sidFileEntries)
        {
            var sidFileName = sidFileEntry.Key!
                .Replace("C64Music", "")
                .Replace("\\", "/");

            if (!songLengthDict.TryGetValue(sidFileName, out var sidInfo))
                continue;

            await using var entryStream = await sidFileEntry.OpenEntryStreamAsync();
            await using var memoryStream = new MemoryStream();
            await entryStream.CopyToAsync(memoryStream);

            Barrel.Current.Add<byte[]>(CacheKeys.JukeboxSidFile(sidInfo.HashMD5), memoryStream.ToArray(), TimeSpan.Zero);

            jukeboxIndex.Add(sidInfo);
        }
    }

    #endregion


    public JukeboxPlaylist? GetPlaylist(string playlistId)
        => Playlists.FirstOrDefault(playlist => playlist.Id == playlistId);

    public void AddPlaylist(JukeboxPlaylist playlist)
    {
        var playlistsList = Playlists.ToList();
        //playlistsList.Insert(0, playlist);
        playlistsList.Add(playlist);
        Playlists = [.. playlistsList];
    }

    public void RemovePlaylist(int playlistIndex)
    {
        var playlistsList = Playlists.ToList();
        playlistsList.RemoveAt(playlistIndex);
        Playlists = [.. playlistsList];
        PersistPlaylists(Playlists);
    }

    public void UpdatePlaylistName(int layoutIndex, string name)
    {
        Playlists[layoutIndex].Name = name;
        PersistPlaylists(Playlists);
    }

    public void UpdatePlaylistName(string playlistId, string name)
    {
        var playList = Playlists.FirstOrDefault(playlist => playlist.Id == playlistId);
        if(null == playList)
            return;
        playList.Name = name;
        PersistPlaylists(Playlists);
    }

    public void AddPlaylistItem(int playlistIndex, SidFileInfo item)
    {
        Playlists[playlistIndex].Items.Add(item);
        PersistPlaylists(Playlists);
    }

    public void AddPlaylistItems(int playlistIndex, SidFileInfo[] items)
    {
        Playlists[playlistIndex].Items.AddRange(items);
        PersistPlaylists(Playlists);
    }

    public void RemovePlaylistItem(int playlistIndex, int itemIndex)
    {
        Playlists[playlistIndex].Items.RemoveAt(itemIndex);
        PersistPlaylists(Playlists);
    }

    public void MovePlaylistItemUp(int layoutIndex, int layoutItemIndex)
    {
        Playlists[layoutIndex].Items.MoveItem(layoutItemIndex, layoutItemIndex - 1);
        PersistPlaylists(Playlists);
    }

    public void MovePlaylistItemDown(int layoutIndex, int layoutItemIndex)
    {
        Playlists[layoutIndex].Items.MoveItem(layoutItemIndex, layoutItemIndex + 1);
        PersistPlaylists(Playlists);
    }

    public static byte[]? GetSidFileContents(SidFileInfo sidFileInfo)
        => Barrel.Current.Get<byte[]>(CacheKeys.JukeboxSidFile(sidFileInfo.HashMD5));

    public void SaveCurrentPlaylists()
        => PersistPlaylists(Playlists);

    private static void PersistPlaylists(JukeboxPlaylist[] playlists)
        => Barrel.Current.Add(key: CacheKeys.JukeboxPlaylists, playlists, expireIn: TimeSpan.Zero);
}
