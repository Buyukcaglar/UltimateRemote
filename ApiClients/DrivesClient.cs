using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.ApiClients;

public sealed partial class UltimateDevice
{
    public Task<DrivesResponse?> GetDrives()
        => PerformHttpRequest<DrivesResponse>(ApiUrls.GetDrives(IpAddress), HttpMethod.Get);

    public Task<MountImageResponse?> MountOnDeviceImage(string drive, string filePath, DiskImageType imageType, DiskMode mode)
        => PerformHttpRequest<MountImageResponse>(ApiUrls.MountImageOnDevice(IpAddress, drive, filePath, imageType, mode), HttpMethod.Put);

    public Task<MountImageResponse?> MountUploadedImage(string drive, byte[] imageFileContentBytes, string fileName, DiskImageType imageType, DiskMode mode)
        => PerformMultipartFormDataRequest<MountImageResponse>(ApiUrls.MountUploadedImage(IpAddress, drive, imageType, mode), imageFileContentBytes, fileName, HttpMethod.Post);

    public Task<ApiResponse?> ResetDrive(string drive)
        => PerformHttpRequest<ApiResponse>(ApiUrls.ResetDrive(IpAddress, drive), HttpMethod.Put);

    public Task<ApiResponse?> RemoveDrive(string drive)
        => PerformHttpRequest<ApiResponse>(ApiUrls.RemoveDrive(IpAddress, drive), HttpMethod.Put);

    public Task<ApiResponse?> UnlinkDrive(string drive)
        => PerformHttpRequest<ApiResponse>(ApiUrls.UnlinkDrive(IpAddress, drive), HttpMethod.Put);

    public Task<ApiResponse?> TurnOnDrive(string drive)
        => PerformHttpRequest<ApiResponse>(ApiUrls.TurnOnDrive(IpAddress, drive), HttpMethod.Put);

    public Task<ApiResponse?> TurnOffDrive(string drive)
        => PerformHttpRequest<ApiResponse>(ApiUrls.TurnOffDrive(IpAddress, drive), HttpMethod.Put);

    public Task<ApiResponse?> LoadOnDeviceDriveRom(string drive, string filePath)
        => PerformHttpRequest<ApiResponse>(ApiUrls.LoadDriveRomOnDevice(IpAddress, drive, filePath), HttpMethod.Put);

    public Task<ApiResponse?> LoadUploadedDriveRom(string drive, byte[] romFileContentBytes)
        => PerformFileUploadRequest<ApiResponse>(ApiUrls.LoadUploadedDriveRom(IpAddress, drive), romFileContentBytes, HttpMethod.Post);

    public Task<ApiResponse?> SetDriveMode(string drive, DriveMode mode)
        => PerformHttpRequest<ApiResponse>(ApiUrls.SetDriveMode(IpAddress, drive, mode), HttpMethod.Put);

}
