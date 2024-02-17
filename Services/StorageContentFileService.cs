using MonkeyCache.FileStore;
using UltimateRemote.Models;

namespace UltimateRemote.Services;
public sealed class StorageContentFileService : IDisposable, IAsyncDisposable
{
    public event EventHandler<BgTaskNotification>? ImportStatusChangedEvent;

    public List<DeviceStorageFileList> StorageFileLists = new List<DeviceStorageFileList>();
    private Stream? _importFileContent;
    private readonly PreferencesManager _prefsMgr;
    public StorageContentFileService(PreferencesManager prefsMgr)
    {
        _prefsMgr = prefsMgr;
        GetCachedLists();
    }

    private void GetCachedLists()
    {
        if (Barrel.Current.Exists(CacheKeys.StorageContentLists))
        {
            try
            {
                var storageContentLists = Barrel.Current.Get<List<DeviceStorageFileList>>(CacheKeys.StorageContentLists);
                if (storageContentLists != null)
                    StorageFileLists = storageContentLists;
            }
            catch (Exception ex)
            {
                var msg = $"Exception on StorageContentFileService.GetCachedLists:\r\n{ex}";
                System.Diagnostics.Debug.WriteLine(msg);
                Console.WriteLine(msg);
            }
        }
    }

    public DeviceStorageFileList? GetFileList(string listName)
        => StorageFileLists.FirstOrDefault(fileList => fileList.Name == listName);

    public DeviceStorageFileList[] GetFileListsHavingFileTypeGroups(FileTypeGroup[] fileTypeGroups)
        => StorageFileLists.Where(file =>
            fileTypeGroups.SelectMany(fileTypeGroup => fileTypeGroup.Extensions).Distinct()
                .Intersect(file.Extensions.Select(extensionInfo => extensionInfo.Extension)).Any()).ToArray();


    public async Task<FilePickResult> PickImportFile()
    {
        var retVal = new FilePickResult();
        try
        {
            var pickResult = await FilePicker.PickAsync(FilePickerOptions.TextFileOptions);
            if (pickResult != null)
            {
                _importFileContent = await pickResult.OpenReadAsync();
                retVal.FileName = pickResult.FileName;
                retVal.ContentType = pickResult.ContentType;
                retVal.FullPath = pickResult.FullPath;
                retVal.FileStream = _importFileContent;
            }
        }
        catch (Exception ex)
        {
            retVal.Exception = ex;
            FinalizeImport();
        }
        return retVal;
    }

    public void StartImportContentFile()
        => ThreadPoolHelper.RegisterBgTask(ImportContentFile);

    public async Task ImportContentFile()
    {
        try
        {
            if (_importFileContent == null)
                return;
            
            ImportStatusChangedEvent.SignalStart(Strings.ContentFileService.ImportStatusReadingContents);
            
            using var memoryStream = new MemoryStream();
            await _importFileContent.CopyToAsync(memoryStream);
            var fileContentBytes = memoryStream.ToArray();
            
            ImportStatusChangedEvent.SignalProgress(Strings.ContentFileService.ImportStatusConvertToString);

            var fileContentString = System.Text.Encoding.Default.GetString(fileContentBytes);
            
            ImportStatusChangedEvent.SignalProgress(Strings.ContentFileService.ImportStatusConvertToArray);
            
            var enabledFileExtensions = _prefsMgr.EnabledFileExtensions;

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);

            var files = fileContentString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(line => (!line.StartsWith("._") || !ContentFileServiceParams.ExcludesContaining.Any(line.Contains)))
                .Select(file => file.Replace("\r", "").Replace("\n", "").Replace('\\', '/'))
                .Where(file => enabledFileExtensions.Any(extension => file.EndsWith($".{extension}", StringComparison.OrdinalIgnoreCase)))
                .Select(formatted => formatted.Substring(formatted.IndexOf('/'), formatted.Length - formatted.IndexOf('/')))
                .ToArray();
            
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);

            ImportStatusChangedEvent.SignalProgress(Strings.ContentFileService.ImportStatusSplitCompleted(files.Length));

            var contentFile = files.Select(file =>
                new StorageFile(file,
                    new FileInfo(file).Extension.TrimStart('.').ToLowerInvariant(),
                    SearchContent: StringSearchExtensions.ConvertToSearchableString(file),
                    FileNameSearchContent: StringSearchExtensions.ConvertToSearchableString(file.Split('/', StringSplitOptions.RemoveEmptyEntries)[^1])
                )).ToArray();

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);

            ImportStatusChangedEvent.SignalSuccess<StorageFile[]?>(Strings.ContentFileService.ToastMsgFileImportSuccess(contentFile?.Length ?? 0),
                Strings.ContentFileService.ToastTitleFileImportSuccess, nameof(ImportContentFile), contentFile);
        }
        catch (Exception ex)
        {
            ImportStatusChangedEvent.SignalFail(Strings.ContentFileService.ToastMsgFileImportFailed(ex.Message),
                Strings.ContentFileService.ToastTitleFileImportFailed);
        }

        FinalizeImport();
    }

    public void FinalizeImport()
    {
        _importFileContent?.Dispose();
        _importFileContent = null;
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
    }

    public void SaveFileList(string listName, StorageFile[] files)
    {
        var fileExtensionInfos = files.GroupBy(file => file.Extension)
            .Select(grouping => new ExtensionInfo(grouping.Key, grouping.Count()))
            .OrderByDescending(extensionInfo => extensionInfo.Count)
            .ToArray();
        
        var storageFileList = new DeviceStorageFileList(listName/*, files*/, files.Length, fileExtensionInfos, DateTime.Now);
        
        StorageFileLists.Add(storageFileList);

        var sortedExtensionInfos = fileExtensionInfos.OrderBy(info => info.Count);

        foreach (var extensionInfo in sortedExtensionInfos)
        {
            Barrel.Current.Add<IEnumerable<StorageFile>>(CacheKeys.StorageContentListFiles(storageFileList.Id, extensionInfo.Extension),
                files.Where(file => file.Extension == extensionInfo.Extension), TimeSpan.Zero);

            files = files.AsEnumerable().Where(file => file.Extension != extensionInfo.Extension).ToArray();
            
            
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
        }

        Barrel.Current.Add(CacheKeys.StorageContentLists, StorageFileLists, TimeSpan.Zero);
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
    }

    public void DeleteFileList(string listName)
    {
        if (StorageFileLists is { Count: > 0 } && StorageFileLists.Any(list => list.Name == listName))
        {
            StorageFileLists.RemoveAll(fileList => fileList.Name == listName);
            Barrel.Current.Add(CacheKeys.StorageContentLists, StorageFileLists, TimeSpan.Zero);
        }
    }

    // https://stackoverflow.com/questions/6967108/is-idisposable-dispose-called-automatically
    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _importFileContent?.Dispose();
            }
            // Release unmanaged resources.
            _disposed = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_importFileContent != null) await _importFileContent.DisposeAsync();
    }

    ~StorageContentFileService() { Dispose(false); }
}
