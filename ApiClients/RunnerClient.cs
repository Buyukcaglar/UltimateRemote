using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.ApiClients;

public sealed partial class UltimateDevice
{
    public Task<ApiResponse?> PlayOnDeviceSidFile(string filePath, int songNumber = 1)
        => PerformHttpRequest<ApiResponse>(ApiUrls.SidPlayOnDevice(IpAddress, filePath, songNumber), HttpMethod.Put);

    public Task<ApiResponse?> PlayUploadedSidFile(byte[] fileContentBytes, string fileName, int songNumber = 1)
        => PerformMultipartFormDataRequest<ApiResponse>(ApiUrls.SidPlayUploadedFile(IpAddress, songNumber), fileContentBytes, fileName, HttpMethod.Post);

    public Task<ApiResponse?> PlayOnDeviceModFile(string filePath)
        => PerformHttpRequest<ApiResponse>(ApiUrls.ModPlayOnDevice(IpAddress, filePath), HttpMethod.Put);

    public Task<ApiResponse?> PlayUploadedModFile(byte[] fileContentBytes, string fileName)
        => PerformMultipartFormDataRequest<ApiResponse>(ApiUrls.ModPlayUploadedFile(IpAddress), fileContentBytes, fileName, HttpMethod.Post);

    public Task<ApiResponse?> LoadPrgFileOnDevice(string filePath)
        => PerformHttpRequest<ApiResponse>(ApiUrls.LoadProgramOnDevice(IpAddress, filePath), HttpMethod.Put);

    public Task<ApiResponse?> LoadUploadedPrgFile(byte[] fileContentBytes, string fileName)
        => PerformMultipartFormDataRequest<ApiResponse>(ApiUrls.LoadUploadedProgram(IpAddress), fileContentBytes, fileName, HttpMethod.Post);

    public Task<ApiResponse?> RunPrgFileOnDevice(string filePath)
        => PerformHttpRequest<ApiResponse>(ApiUrls.RunProgramFileOnDevice(IpAddress, filePath), HttpMethod.Put);

    public Task<ApiResponse?> RunUploadedPrgFile(byte[] fileContentBytes, string fileName)
        => PerformMultipartFormDataRequest<ApiResponse>(ApiUrls.RunUploadedProgramFile(IpAddress), fileContentBytes, fileName, HttpMethod.Post);

    public Task<ApiResponse?> RunCrtFileOnDevice(string filePath)
        => PerformHttpRequest<ApiResponse>(ApiUrls.RunCartridgeOnDevice(IpAddress, filePath), HttpMethod.Put);

    public Task<ApiResponse?> RunUploadedCrtFile(byte[] fileContentBytes, string fileName)
        => PerformMultipartFormDataRequest<ApiResponse>(ApiUrls.RunUploadedCartridge(IpAddress), fileContentBytes, fileName, HttpMethod.Post);

}
