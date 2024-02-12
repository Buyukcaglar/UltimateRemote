using MonkeyCache.FileStore;
using UltimateRemote.Models;

namespace UltimateRemote.Extensions;
public static class StorageFileExtensions
{
    public static FileContentSearchResult Search(this StorageFile[] searchSet, string searchPhrase, bool searchInFilenames, int resultSetLimit = int.MaxValue)
    {
        var interMediateResults = new List<StorageFile>();

        var phrase = StringSearchExtensions.ConvertToSearchableString(searchPhrase);

        var phraseSplits = phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < phraseSplits.Length; i++)
        {
            if (interMediateResults.Count == 0)
            {
                interMediateResults = searchInFilenames
                    ? searchSet.Where(storageFile => storageFile.FileNameSearchContent.Contains(phraseSplits[i])).ToList()
                    : searchSet.Where(storageFile => storageFile.SearchContent.Contains(phraseSplits[i])).ToList();
                continue;
            }

            interMediateResults = searchInFilenames
                ? interMediateResults.Where(item => item.FileNameSearchContent.Contains(phraseSplits[i])).ToList()
                : interMediateResults.Where(item => item.SearchContent.Contains(phraseSplits[i])).ToList();
        }

        var resultSet = interMediateResults.Select(item => item.Path).Take(resultSetLimit).ToList();
        var searchResults = new FileContentSearchResult(
            Results: resultSet,
            SearchSetCount: searchSet.Length,
            ResultSetCount: interMediateResults.Count,
            ReturnedSetCount: resultSet.Count,
            Limit: resultSetLimit);

        return searchResults;

    }

    public static StorageFile[] GetCachedFiles(this DeviceStorageFileList fileList, string[]? extensions)
    {
        var retVal = new List<StorageFile>();
        
        if (extensions is { Length: > 0 })
        {
            foreach (var extension in extensions)
            {
                retVal.AddRange(Barrel.Current.Get<StorageFile[]>(CacheKeys.StorageContentListFiles(fileList.Id, extension)) ?? []);
            }
        }
        else
        {
            foreach (var extensionInfo in fileList.Extensions)
            {
                retVal.AddRange(Barrel.Current.Get<StorageFile[]>(CacheKeys.StorageContentListFiles(fileList.Id, extensionInfo.Extension)) ?? []);
            }    
        }
        return [.. retVal];
    }

    /*
     *=> _extensions is not {Length: > 0} ? 
       fileList.Extensions
           .Aggregate(Array.Empty<StorageFile>, 
               (extInfo, prev) => Barrel.Current.Get<StorageFile[]>(CacheKeys.StorageContentListFiles(fileList.Id, extInfo.Extension)))
     */

}
