namespace UltimateRemote.Constants;
internal static class CacheKeys
{
    public const string StorageContentLists = nameof(StorageContentLists);
    public const string MyDevices = nameof(MyDevices);
    public const string Preferences = nameof(Preferences);
    public const string HistoryItems = nameof(HistoryItems);
    public const string UserLayouts = nameof(UserLayouts);
    public const string JukeboxLib = nameof(JukeboxLib);
    public const string JukeboxPlaylists = nameof(JukeboxPlaylists);
    public const string KeyMacros = nameof(KeyMacros);
    public static string JukeboxSidFile(string hashMd5) => $"jukebox-sid-{hashMd5}";

    public static string StorageContentListFiles(string id, string extension) => $"storage-content-list-{id}-{extension}";

}
