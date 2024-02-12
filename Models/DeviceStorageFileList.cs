using System.Text.Json.Serialization;

namespace UltimateRemote.Models;
public sealed record DeviceStorageFileList(string Name/*, StorageFile[] Files*/, int FileCount, ExtensionInfo[] Extensions, DateTime TimeStamp)
{
    [JsonInclude] public string Id { get; private set; } = Guid.NewGuid().ToString("N");
    /*
    public FileContentSearchResult SearchInFileNames(string searchPhrase, int resultSetLimit = int.MaxValue, string[]? searchInExtensions = null)
    {
        var interMediateResults = new List<StorageFile>();

        var phrase = StringSearchExtensions.ConvertToSearchableString(searchPhrase);

        var phraseSplits = phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var searchSet = searchInExtensions is { Length: > 0 }
            ? Files.Where(file => searchInExtensions.Contains(file.Extension)).ToArray() : Files;

        for (int i = 0; i < phraseSplits.Length; i++)
        {
            if (interMediateResults.Count == 0)
            {
                interMediateResults = searchSet.Where(item => item.FileNameSearchContent.Contains(phraseSplits[i])).ToList();
                continue;
            }

            interMediateResults =
                interMediateResults.Where(item => item.FileNameSearchContent.Contains(phraseSplits[i])).ToList();
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

    public FileContentSearchResult SearchInEveryWhere(string searchPhrase, int resultSetLimit = int.MaxValue, string[]? searchInExtensions = null)
    {
        var interMediateResults = new List<StorageFile>();

        var phrase = StringSearchExtensions.ConvertToSearchableString(searchPhrase);

        var phraseSplits = phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var searchSet = searchInExtensions is { Length: > 0 }
            ? Files.Where(file => searchInExtensions.Contains(file.Extension)).ToArray() : Files;

        for (int i = 0; i < phraseSplits.Length; i++)
        {
            if (interMediateResults.Count == 0)
            {
                interMediateResults = searchSet.Where(item => item.SearchContent.Contains(phraseSplits[i])).ToList();
                continue;
            }

            interMediateResults =
                interMediateResults.Where(item => item.SearchContent.Contains(phraseSplits[i])).ToList();
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
    */
}

public sealed record StorageFile(
    string Path,
    string Extension,
    string SearchContent,
    string FileNameSearchContent
    );

public sealed record FileContentSearchResult(
    List<string> Results,
    int SearchSetCount,
    int ResultSetCount,
    int ReturnedSetCount,
    int Limit);

public sealed record ExtensionInfo(string Extension, int Count);