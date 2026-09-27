using System.Text.Json.Serialization;

namespace UltimateRemote.Models.ResponseModels;

public sealed class InfoResponse : ApiResponse
{
    private static readonly string[] CartridgeTypes = ["Ultimate", "Ultimate-II", "Ultimate II+", "Ultimate II+L"];
    private static readonly string[] ComputerTypes = ["Ultimate 64", "Ultimate 64 Elite", "Ultimate 64-II", "C64 Ultimate"];

    [JsonPropertyName("product")] public required string Product { get; init; }
    [JsonPropertyName("firmware_version")] public required string FirmwareVersion { get; init; }
    [JsonPropertyName("fpga_version")] public required string FpgaVersion { get; init; }
    [JsonPropertyName("core_version")] public string? CoreVersion { get; init; }
    [JsonPropertyName("hostname")] public required string Hostname { get; init; }
    [JsonPropertyName("unique_id")] public string? UniqueId { get; init; }

    public UltimateDeviceType DeviceType => Product switch
    {
        not null when CartridgeTypes.Contains(Product) => UltimateDeviceType.Cartridge,
        not null when ComputerTypes.Contains(Product)  => UltimateDeviceType.Computer,
        _ => UltimateDeviceType.None
    };

    public static InfoResponse NoDeviceResponse()
        => new()
        {
            Errors = [Strings.WarningMessages.NoRegisteredDeviceFound],
            Product = string.Empty,
            FirmwareVersion = string.Empty,
            FpgaVersion = string.Empty,
            Hostname = string.Empty
        };

}