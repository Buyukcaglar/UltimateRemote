namespace UltimateRemote.Models;

public sealed record IpScanResult(string IpAddress)
{
    public bool Found => !string.IsNullOrWhiteSpace(Version);
    public string? Version { get; set; }
}