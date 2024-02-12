using System.Text.Json;
using UltimateRemote.Interfaces;

namespace UltimateRemote.Models.ResponseModels;

public sealed class ConfigItemResponse : Dictionary<string, object?>, IApiResponse
{
    private string[] _errors => this.Keys.Contains("errors") && null != this["errors"] ? ((JsonElement)this["errors"]!).Deserialize<string[]>()! : Array.Empty<string>();

    public string[] Errors { get => _errors; init { } }

    public bool Success => Errors.Length == 0;

    public T? GetValue<T>(string key) => this.Keys.Contains(key) && null != this[key] ? ((JsonElement)this[key]!).Deserialize<T>() : default(T?);

}

/*
public sealed record DriveSettings(
    [property: JsonPropertyName("Drive")] Status Status,
    [property: JsonPropertyName("Drive Type")] string Type,
    [property: JsonPropertyName("Drive Bus ID")] int BusId,
    [property: JsonPropertyName("ROM for 1541 mode")] string Rom1541Mode,
    [property: JsonPropertyName("ROM for 1571 mode")] string Rom1571Mode,
    [property: JsonPropertyName("ROM for 1581 mode")] string Rom1581Mode,
    [property: JsonPropertyName("ExtraRam")] Status ExtraRam,
    [property: JsonPropertyName("Disk swap delay")] int DiskSwapDelay,
    [property: JsonPropertyName("Resets when C64 resets")] YesNoBool SyncReset,
    [property: JsonPropertyName("Freezes in menu")] YesNoBool FreezesOnMenu,
    [property: JsonPropertyName("GCR Save Align Tracks")] YesNoBool GcrSaveAlignTracks,
    [property: JsonPropertyName("Leave Menu on Mount")] YesNoBool LeaveMenuOnMount
);
*/

/*
public sealed record DriveSettingsResponse(
    [property: JsonPropertyName("Drive"), StringValue("Status")] string Status,
    [property: JsonPropertyName("Drive Type"), StringValue("Type")] string Type,
    [property: JsonPropertyName("Drive Bus ID"), StringValue("Bus Id")] int BusId,
    [property: JsonPropertyName("ROM for 1541 mode"), StringValue("ROM for 1541 Mode")] string RomFor1541Mode,
    [property: JsonPropertyName("ROM for 1571 mode"), StringValue("ROM for 1571 Mode")] string RomFor1571Mode,
    [property: JsonPropertyName("ROM for 1581 mode"), StringValue("ROM for 1581 Mode")] string RomFor1581Mode,
    [property: JsonPropertyName("ExtraRam"), StringValue("Extra Ram")] string ExtraRam,
    [property: JsonPropertyName("Disk swap delay"), StringValue("Disk Swap Delay")] int DiskSwapDelay,
    [property: JsonPropertyName("Resets when C64 resets"), StringValue("Resets when C64 Resets")] string SyncReset,
    [property: JsonPropertyName("Freezes in menu"), StringValue("Freezes in Menu")] string FreezesInMenu,
    [property: JsonPropertyName("GCR Save Align Tracks"), StringValue("GCR Save Align Tracks")] string GcrSaveAlignTracks,
    [property: JsonPropertyName("Leave Menu on Mount"), StringValue("Leave Menu on Mount")] string LeaveMenuOnMount
);
*/