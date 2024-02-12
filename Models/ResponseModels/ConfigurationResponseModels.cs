using System.Text.Json.Serialization;

namespace UltimateRemote.Models.ResponseModels;

public sealed class ConfigsResponse : ApiResponse
{
    [JsonPropertyName("categories")] public string[]? Categories { get; init; }
}

public sealed class ClockSettingsResponse
{
    [JsonPropertyName("Clock Settings")] public required ClockSettings Settings { get; set; }
}

public sealed record ClockSettings(int Year, string Month, int Day, int Hours, int Minutes, int Seconds, int Correction);

public sealed class AudioOutputSettings : ApiResponse
{
    [JsonPropertyName("Audio Output Settings")] public Dictionary<string, string>? Settings { get; set; }
}

public sealed class NetworkSettingsResponse : ApiResponse
{
    [JsonPropertyName("Network settings")] public Dictionary<string, string>? Settings { get; set; }
}

public sealed class ModemSettingsResponse : ApiResponse
{
    [JsonPropertyName("Modem Settings")] public Dictionary<string, object>? Settings { get; set; }
}

public sealed class SoftIecDriveSettingsResponse : ApiResponse
{
    [JsonPropertyName("SoftIEC Drive Settings")] public Dictionary<string, object>? Settings { get; set; }
}

public sealed class PrinterSettingsResponse : ApiResponse
{
    [JsonPropertyName("Printer Settings")] public Dictionary<string, object>? Settings { get; set; }
}

public sealed class C64AndCartridgeSettingsResponse : ApiResponse
{
    [JsonPropertyName("C64 and Cartridge Settings")] public Dictionary<string, object>? Settings { get; set; }
}

public sealed class UserInterfaceSettingsResponse : ApiResponse
{
    [JsonPropertyName("User Interface Settings")] public Dictionary<string, string>? Settings { get; set; }
}

public sealed class TapeSettingsResponse : ApiResponse
{
    [JsonPropertyName("Tape Settings")] public Dictionary<string, string>? Settings { get; set; }
}
