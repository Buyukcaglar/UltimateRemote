using Blazored.Toast.Services;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Interfaces;

public interface IUltimateDevice
{
    event EventHandler? DeviceChangedEvent;
    bool Current { get; }
    string Name { get; set; }
    string IpAddress { get; set; }
    string Version { get; set; }
    UltimateDeviceType Type { get; set; }
    bool Online { get; }
    void SelectDevice();
    void UnSelectDevice();
    void SetToastService(IToastService toastService);
    void StopConnectivityCheck();
    void StartConnectivityCheck();
    void SetHeartbeatInterval(uint interval);
    void SetDefaultApiClientTimeout(uint timeOutInSeconds);
    void ChangeApiClientTimeout(uint timeOutInSeconds);
    Task<VersionResponse?> QueryVersion();
    Task<ApiResponse?> ResetMachine();
    Task<ApiResponse?> RebootMachine();
    Task<ApiResponse?> PauseMachine();
    Task<ApiResponse?> ResumeMachine();
    Task<ApiResponse?> PowerOffMachine();
    Task<WriteMemoryResponse?> WriteMemory(string address, string hexData);
    Task<WriteMemoryResponse?> WriteMemory(string address, byte[] dataContentBytes);
    Task<byte[]?> ReadMemory(string address, int length = 256);
    Task<DebugRegisterResponse?> ReadDebugRegister();
    Task<DebugRegisterResponse?> WriteDebugRegister(string value);
    Task<ApiResponse?> PlayOnDeviceSidFile(string filePath, int songNumber = 1);
    Task<ApiResponse?> PlayUploadedSidFile(byte[] fileContentBytes, string fileName, int songNumber = 1);
    Task<ApiResponse?> PlayOnDeviceModFile(string filePath);
    Task<ApiResponse?> PlayUploadedModFile(byte[] fileContentBytes, string fileName);
    Task<ApiResponse?> LoadPrgFileOnDevice(string filePath);
    Task<ApiResponse?> LoadUploadedPrgFile(byte[] fileContentBytes, string fileName);
    Task<ApiResponse?> RunPrgFileOnDevice(string filePath);
    Task<ApiResponse?> RunUploadedPrgFile(byte[] fileContentBytes, string fileName);
    Task<ApiResponse?> RunCrtFileOnDevice(string filePath);
    Task<ApiResponse?> RunUploadedCrtFile(byte[] fileContentBytes, string fileName);
    Task<ApiResponse?> StartStream(StreamType type, string destinationIp);
    Task<ApiResponse?> StopStream(StreamType type);
    Task<DrivesResponse?> GetDrives();
    Task<MountImageResponse?> MountOnDeviceImage(string drive, string filePath, DiskImageType imageType, DiskMode mode);
    Task<MountImageResponse?> MountUploadedImage(string drive, byte[] imageFileContentBytes, string fileName, DiskImageType imageType, DiskMode mode);
    Task<ApiResponse?> ResetDrive(string drive);
    Task<ApiResponse?> RemoveDrive(string drive);
    Task<ApiResponse?> UnlinkDrive(string drive);
    Task<ApiResponse?> TurnOnDrive(string drive);
    Task<ApiResponse?> TurnOffDrive(string drive);
    Task<ApiResponse?> LoadOnDeviceDriveRom(string drive, string filePath);
    Task<ApiResponse?> LoadUploadedDriveRom(string drive, byte[] romFileContentBytes);
    Task<ApiResponse?> SetDriveMode(string drive, DriveMode mode);
    Task<GetFileInfoResponse?> GetFileInfo(string filePath);
    Task<CreateDiskImageResponse?> CreateDiskImage(ImageFileType imageFileType, string imageFilePath, int tracks, string? diskLabel);
    Task<ConfigsResponse?> GetConfigs();
    Task<T?> GetConfigCategory<T>(string category) where T : IApiResponse;
    Task<T?> GetConfigCategorySection<T>(string category, string section) where T : IApiResponse;
    Task<T?> UpdateConfigCategorySectionValue<T>(string category, string section, string value) where T : IApiResponse;
    Task<ApiResponse?> UpdateConfig<TConfig>(string category, string section, TConfig config) where TConfig : class;
    Task<ApiResponse?> ConfigurationOperation(ConfigOp operation);
    void Dispose();
}