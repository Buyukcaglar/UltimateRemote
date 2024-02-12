using System.Text.Json.Serialization;

namespace UltimateRemote.Models;

public sealed class UltimateDeviceInfo
{
    public bool Current { get; set; }

    [JsonIgnore] public bool Online { get; set; }

    public string Name { get; set; } = default!;
    
    public string IpAddress { get; set; } = default!;
    
    public string Version { get; set; } = default!;
    
    public UltimateDeviceType Type { get; set; }

}