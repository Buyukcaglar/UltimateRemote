using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.ApiClients;

public sealed partial class UltimateDevice
{
    public Task<ApiResponse?> StartStream(StreamType type, string destinationIp)
        => PerformHttpRequest<ApiResponse>(ApiUrls.StartStream(IpAddress, type, destinationIp), HttpMethod.Put);

    public Task<ApiResponse?> StopStream(StreamType type)
        => PerformHttpRequest<ApiResponse>(ApiUrls.StopStream(IpAddress, type), HttpMethod.Put);
}