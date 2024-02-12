namespace UltimateRemote.Models;

public sealed record DeviceScanResult(IpScanResult[] DeviceAddresses)
{
    public string? Message { get; set; }

    public bool Found => DeviceAddresses.Length > 0;
}