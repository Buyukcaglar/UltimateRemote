using MonkeyCache.FileStore;
using UltimateRemote.Models;

namespace UltimateRemote.Services;

public sealed class HistoryManager
{
    public event EventHandler? HistoryItemsUpdatedEvent;

    public List<HistoryItem> HistoryItems => _historyItems;

    private readonly List<HistoryItem> _historyItems;

    private readonly PreferencesManager _prefsMgr;

    public HistoryManager(PreferencesManager prefsMgr)
    {
        _prefsMgr = prefsMgr;
        _historyItems = Barrel.Current.Get<List<HistoryItem>>(key: CacheKeys.HistoryItems) ?? new List<HistoryItem>();
    }

    public void Add(string path)
    {
        if (_historyItems.Any(file => file.Path == path))
        {
            // Just move previously added item to top
            _historyItems.MoveItem(file => file.Path == path, newIndex: 0);
            SaveHistory();
            return;
        }

        AddToHistory(path);
    }

    public void Add(string fileName, byte[] contentBytes)
    {
        if (_historyItems.Where(historyItem => historyItem.ContentBytes is { Length: > 0 }).Any(historyItem =>
                historyItem.FileName == fileName && contentBytes.SequenceEqual(historyItem.ContentBytes!)))
        {
            // Just move previously added item to top
            _historyItems.MoveItem(
                historyItem => historyItem.FileName == fileName && contentBytes.SequenceEqual(historyItem.ContentBytes!),
                newIndex: 0);
            SaveHistory();
            return;
        }

        AddToHistory(fileName, contentBytes);
    }

    public void RemoveFromHistory(int itemId)
    {
        var historyItem = _historyItems.FirstOrDefault(file => file.Id == itemId);
        if (historyItem == null)
            return;
        _historyItems.Remove(historyItem);
        SaveHistory();
    }

    public List<HistoryItem>? GetRecentFilesOfType(FileTypeGroup[] typeGroups) => _historyItems.Where(historyItem =>
        typeGroups.SelectMany(typeGroup => typeGroup.Extensions).Distinct().Contains(historyItem.Extension)).ToList();

    public List<HistoryItem>? GetRecentFilesOfType(FileTypeGroup[] typeGroups, HistoryItemType fileType)
        => _historyItems.Where(historyItem => historyItem.Type == fileType && typeGroups
            .SelectMany(typeGroup => typeGroup.Extensions).Distinct().Contains(historyItem.Extension)).ToList();

    private void SaveHistory()
    {
        Barrel.Current.Add(key: CacheKeys.HistoryItems, _historyItems, expireIn: TimeSpan.Zero);
        HistoryItemsUpdatedEvent?.Invoke(this, EventArgs.Empty);
    }

    private void AddToHistory(string path)
    {
        var fileInfo = new FileInfo(path);
        var extension = fileInfo.Extension; // Includes '.'
        var fileName = fileInfo.Name; // Includes file extension
        var name = fileName.Replace(extension, "");
        var fileId = _historyItems.Any() ? _historyItems.Max(file => file.Id) + 1 : 1;

        _historyItems.Insert(index: 0, 
            item: new HistoryItem(fileId, name, fileName, extension.TrimStart('.'), HistoryItemType.StorageContentFile, path, null)
            );

        // Confrom to history size preference
        if (_historyItems.Count > _prefsMgr.HistorySize)
        {
            var historySize = _prefsMgr.HistorySize;
            var libFiles = _historyItems.Take(historySize).ToList();
            _historyItems.Clear();
            _historyItems.AddRange(libFiles);
        }

        SaveHistory();
    }


    private void AddToHistory(string fileName, byte[] contentBytes)
    {
        var fileInfo = new FileInfo(fileName);
        var extension = fileInfo.Extension; // Includes '.'
        var name = fileName.Replace(extension, "");
        var fileId = _historyItems.Any() ? _historyItems.Max(file => file.Id) + 1 : 1;

        _historyItems.Insert(index: 0, 
            item: new HistoryItem(fileId, name, fileName, extension.TrimStart('.'), HistoryItemType.UploadedFile, null, contentBytes)
            );

        // Confrom to history size preference
        if (_historyItems.Count > _prefsMgr.HistorySize)
        {
            var historySize = _prefsMgr.HistorySize;
            var libFiles = _historyItems.Take(historySize).ToList();
            _historyItems.Clear();
            _historyItems.AddRange(libFiles);
        }

        SaveHistory();
    }

}
