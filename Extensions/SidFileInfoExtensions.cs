using UltimateRemote.Models;

namespace UltimateRemote.Extensions;
public static class SidFileInfoExtensions
{
    public static SidFileInfo[] Search(this SidFileInfo[] library, string searchPhrase, int resultSetLimit)
    {
        var interMediateResults = new List<SidFileInfo>();
        var phrase = StringSearchExtensions.ConvertToSearchableString(searchPhrase);
        var phraseSplits = phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 0; i < phraseSplits.Length; i++)
        {
            if (interMediateResults.Count == 0)
            {
                interMediateResults = library.Where(sidFileInfo => sidFileInfo.SearchContent.Contains(phraseSplits[i])).ToList();
                continue;
            }

            interMediateResults =
                interMediateResults.Where(sidFileInfo => sidFileInfo.SearchContent.Contains(phraseSplits[i])).ToList();
        }

        var resultSet = interMediateResults.Take(resultSetLimit).ToArray();
        return resultSet;
    }

    public static string FileName(this SidFileInfo sidFileInfo)
    {
        var fileInfo = new FileInfo(sidFileInfo.FilePath);
        return fileInfo.Name;
    }

    public static string FileNameWithoutExtension(this SidFileInfo sidFileInfo)
    {
        var fileInfo = new FileInfo(sidFileInfo.FilePath);
        return fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
    }

    public static string FileNameWithSongCount(this SidFileInfo sidFileInfo)
    {
        var fileInfo = new FileInfo(sidFileInfo.FilePath);
        return sidFileInfo.NumberOfSongs > 1 ? $"{fileInfo.Name} ({sidFileInfo.NumberOfSongs})" : fileInfo.Name;
    }

}
