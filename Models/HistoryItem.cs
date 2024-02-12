namespace UltimateRemote.Models;

public sealed record HistoryItem(
    int Id,
    string Name,
    string FileName,
    string Extension,
    HistoryItemType Type,
    string? Path,
    byte[]? ContentBytes);