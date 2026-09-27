using System.Text.Json.Serialization;

namespace UltimateRemote.Models;

public sealed class UltimateDeviceInfo
{
    public bool Current { get; set; }

    [JsonIgnore] public bool Online { get; set; }

    public string Name { get; set; } = default!;
    
    public string IpAddress { get; set; } = default!;
    
    public string ApiVersion { get; set; } = default!;

    public string ProductName { get; set; } = default!;

    public string FirmwareVersion { get; set; } = default!;

    public string FpgaVersion { get; set; } = default!;

    public string? CoreVersion { get; set; }

    public string Hostname { get; set; } = default!;

    public string? UniqueId { get; set; }

    public UltimateDeviceType Type { get; set; }

}