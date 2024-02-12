// ReSharper disable InconsistentNaming

using System.Text.Json.Serialization;

namespace UltimateRemote.Models;

public record SidFileInfo(
    string FilePath,
    string HashMD5,
    string SearchContent,
    int NumberOfSongs,
    TimeSpan TotalLength,
    TimeSpan[] SongLengths)
{
    [JsonIgnore] public string? FormattedFileName { get; set; }

    [JsonIgnore] public int SongNumber { get; set; } = 1;
}
