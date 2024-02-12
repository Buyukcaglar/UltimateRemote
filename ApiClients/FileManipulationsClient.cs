using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.ApiClients;

public sealed partial class UltimateDevice
{
    public Task<GetFileInfoResponse?> GetFileInfo(string filePath)
        => PerformHttpRequest<GetFileInfoResponse>(ApiUrls.GetFileInfoOnDevice(IpAddress, filePath), HttpMethod.Get);

    public Task<CreateDiskImageResponse?> CreateDiskImage(ImageFileType imageFileType, string imageFilePath, int tracks, string? diskLabel)
        => imageFileType switch
        {
            ImageFileType.D64 => PerformHttpRequest<CreateDiskImageResponse>(ApiUrls.CreateD64(IpAddress, imageFilePath, (D64Tracks)tracks, diskLabel), HttpMethod.Put),
            ImageFileType.D71 => PerformHttpRequest<CreateDiskImageResponse>(ApiUrls.CreateD71(IpAddress, imageFilePath, diskLabel), HttpMethod.Put),
            ImageFileType.D81 => PerformHttpRequest<CreateDiskImageResponse>(ApiUrls.CreateD81(IpAddress, imageFilePath, diskLabel), HttpMethod.Put),
            ImageFileType.Dnp => Task.Run(async () =>
            {
                ChangeApiClientTimeout(LongRunningOperationHttpTimeout);
                var retVal = await PerformHttpRequest<CreateDiskImageResponse>(ApiUrls.CreateDnp(IpAddress, imageFilePath, tracks, diskLabel), HttpMethod.Put);
                ChangeApiClientTimeout(_apiClientTimeOut);
                return retVal;
            }),
            _ => throw new ArgumentOutOfRangeException(nameof(imageFileType), imageFileType, null)
        };

}