//using MonkeyCache.FileStore;
//using UltimateRemote.Models;

//namespace UltimateRemote.Services;

//public sealed class LibraryManager
//{
//    public event EventHandler<FileLibrary>? LibraryUpdatedEvent;

//    public List<FileLibrary> Libraries => _libraries;

//    private readonly List<FileLibrary> _libraries;

//    private readonly PreferencesManager _prefsMgr;

//    public LibraryManager(PreferencesManager prefsMgr)
//    {
//        _prefsMgr = prefsMgr;
//        _libraries = Barrel.Current.Get<List<FileLibrary>>(key: CacheKeys.Libraries) ?? DefaultLibraries;
//    }

//    public void AddToRecent(string path)
//        => AddToLibrary(LibraryNames.RecentFiles, path);

//    public void AddToRecent(string fileName, byte[] contentBytes)
//        => AddToLibrary(LibraryNames.RecentFiles, fileName, contentBytes);

//    public void AddToLibrary(string libraryName, string path)
//    {
//        var library = GetLibrary(libraryName);

//        if (null == library)
//            return;

//        if (library.Files.Any(file => file.Path == path))
//        {
//            // Just move previous item to top
//            library.Files.MoveItem(file => file.Path == path, newIndex: 0);
//            library.UpdateTimeStamp();
//            SaveLibraries(library);
//            return;
//        }

//        AddToLibrary(library, path);
//    }

//    public void AddToLibrary(string libraryName, string fileName, byte[] contentBytes)
//    {
//        var library = GetLibrary(libraryName);

//        if (null == library)
//            return;

//        if (library.Files.Any(file => file.Name == fileName && file.ContentBytes == contentBytes))
//        {
//            // Just move previous item to top
//            library.Files.MoveItem(file => file.Name == fileName && file.ContentBytes == contentBytes, newIndex: 0);
//            library.UpdateTimeStamp();
//            SaveLibraries(library);
//            return;
//        }

//        AddToLibrary(library, fileName, contentBytes);
//    }

//    // To remove icon from a file provide Empty ByteArray with LibFileIconType.None
//    public void UpdateIcon(string libraryName, int fileId, byte[] iconBytes, LibFileIconType iconType)
//    {
//        var library = GetLibrary(libraryName);

//        var file = library?.Files.FirstOrDefault(file => file.Id == fileId);

//        if (file == null)
//            return;

//        var iconString = iconType switch
//        {
//            LibFileIconType.Png => ImageHelper.GetDataImageSource(Convert.ToBase64String(iconBytes), ".png"),
//            LibFileIconType.WebP => ImageHelper.GetDataImageSource(Convert.ToBase64String(iconBytes), ".webp"),
//            LibFileIconType.Svg => System.Text.Encoding.Default.GetString(iconBytes),
//            _ => null
//        };

//        file.Icon = iconString;
//        file.IconType = iconType;

//        library?.UpdateTimeStamp();

//        SaveLibraries(library);
//    }

//    public void RemoveFromLibrary(string libraryName, int fileId)
//    {
//        var library = GetLibrary(libraryName);

//        var file = library?.Files.FirstOrDefault(file => file.Id == fileId);

//        if (file == null)
//            return;

//        library?.Files.Remove(file);

//        SaveLibraries(library);
//    }

//    public void AddNewLibrary(string libraryName)
//    {
//        libraryName = libraryName.Trim();
//        var newLibrary = new FileLibrary(libraryName, Enabled: true, BuiltIn: false, Files: new List<LibFile>());
//        Libraries.Add(newLibrary);
//        SaveLibraries(newLibrary);
//    }

//    public void RemoveLibrary(string libraryName)
//    {
//        var library = GetLibrary(libraryName);

//        if (null == library)
//            return;

//        _libraries.Remove(library);

//        SaveLibraries(null);
//    }

//    // Returns enabled library if found
//    public FileLibrary? GetLibrary(string libraryName)
//        => _libraries.FirstOrDefault(library => library.Enabled && library.Name == libraryName);

//    public FileLibrary? GetRecentFilesLibrary
//        => _libraries.FirstOrDefault(library => library is { Enabled: true, Name: LibraryNames.RecentFiles });

//    public List<LibFile>? GetRecentFilesOfType(FileTypeGroup[] typeGroups, LibFileType fileType)
//        => GetRecentFilesLibrary?.Files.Where(file =>
//            file.Type == fileType && typeGroups.SelectMany(typeGroup => typeGroup.Extensions).Distinct()
//                .Contains(file.Extension)).ToList();

//    private void SaveLibraries(FileLibrary? updatedLibrary)
//    {
//        Barrel.Current.Add(key: CacheKeys.Libraries, _libraries, expireIn: TimeSpan.MaxValue);
//        if(null != updatedLibrary)
//            LibraryUpdatedEvent?.Invoke(this, updatedLibrary);
//    }

//    public bool LibNameExists(string libraryName)
//        => _libraries.Any(fileLibrary => fileLibrary.Name == libraryName);

//    private List<FileLibrary> DefaultLibraries
//        => new List<FileLibrary>(
//            new[]
//            {
//                new FileLibrary(Name: LibraryNames.RecentFiles, Enabled: true, BuiltIn: true, Files: new List<LibFile>())
//            });

//    private void AddToLibrary(FileLibrary lib, string path)
//    {
//        var fileInfo = new FileInfo(path);
//        var extension = fileInfo.Extension; // Includes '.'
//        var fileName = fileInfo.Name; // Includes file extension
//        var name = fileName.Replace(extension, "");
//        var fileId = lib.Files.Any() ? lib.Files.Max(file => file.Id) + 1 : 1;
        
//        lib.Files.Insert(index: 0, item: new LibFile(fileId, fileName, extension.TrimStart('.'), LibFileType.StorageContentFile, path,
//            null) { Name = name });

//        // Confrom to history size preference
//        if (lib.Name == LibraryNames.RecentFiles && lib.Files.Count > _prefsMgr.HistorySize())
//        {
//            var historySize = _prefsMgr.HistorySize();
//            var libFiles = lib.Files.Take(historySize).ToList();
//            lib.Files.Clear();
//            lib.Files.AddRange(libFiles);
//        }

//        lib.UpdateTimeStamp();
//        SaveLibraries(lib);
//    }


//    private void AddToLibrary(FileLibrary lib, string fileName, byte[] contentBytes)
//    {
//        var fileInfo = new FileInfo(fileName);
//        var extension = fileInfo.Extension; // Includes '.'
//        var name = fileName.Replace(extension, "");
//        var fileId = lib.Files.Any() ? lib.Files.Max(file => file.Id) + 1 : 1;
        
//        lib.Files.Insert(index: 0, item: new LibFile(fileId, fileName, extension.TrimStart('.'), LibFileType.UploadedFile, null,
//            contentBytes) { Name = name });

//        // Confrom to history size preference
//        if (lib.Name == LibraryNames.RecentFiles && lib.Files.Count > _prefsMgr.HistorySize())
//        {
//            var historySize = _prefsMgr.HistorySize();
//            var libFiles = lib.Files.Take(historySize).ToList();
//            lib.Files.Clear();
//            lib.Files.AddRange(libFiles);
//        }

//        lib.UpdateTimeStamp();
//        SaveLibraries(lib);
//    }

//}
