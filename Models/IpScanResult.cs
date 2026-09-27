namespace UltimateRemote.Models;

public sealed record IpScanResult(string IpAddress)
{
    public bool Found => !(string.IsNullOrWhiteSpace(ApiVersion) && string.IsNullOrWhiteSpace(ProductName) && string.IsNullOrWhiteSpace(FirmwareVersion));
    public UltimateDeviceType DeviceType { get; set; }
    public required string ApiVersion { get; set; }
    public required string ProductName { get; set; }
    public required string FirmwareVersion { get; set; }
    public required string FpgaVersion { get; set; }
    public string? CoreVersion { get; set; }
    public required string Hostname { get; set; }
    public string? UniqueId { get; set; }

    public static IpScanResult Empty(string ipAddress)
        => new IpScanResult(ipAddress)
        {
            ApiVersion = string.Empty,
            ProductName = string.Empty,
            FirmwareVersion = string.Empty,
            FpgaVersion = string.Empty,
            Hostname = string.Empty
        };

}