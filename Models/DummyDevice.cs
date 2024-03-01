using Blazored.Toast.Services;
using UltimateRemote.Interfaces;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Models;
internal class DummyDevice : IUltimateDevice
{
    public event EventHandler? DeviceChangedEvent;
    public bool Current { get; } = true;
    public string Name { get; set; } = "DummyDevice";
    public string IpAddress { get; set; } = "0.0.0.0";
    public string Version { get; set; } = "0.0";
    public UltimateDeviceType Type
    {
        get => UltimateDeviceType.None;
        set { }
    }

    public bool Online { get; } = false;

    private IToastService? _toastService;

    public void SelectDevice() { }

    public void UnSelectDevice() { }

    public void SetToastService(IToastService toastService)
        => _toastService = toastService;

    public void StopConnectivityCheck() { }

    public void StartConnectivityCheck() { }

    public void SetHeartbeatInterval(uint interval) { }
    
    public void SetDefaultApiClientTimeout(uint timeOutInSeconds) {}

    public void ChangeApiClientTimeout(uint timeOutInSeconds) {}

    public Task<VersionResponse?> QueryVersion()
        => NoDeviceResponse<VersionResponse>();

    public Task<ApiResponse?> ResetMachine()
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> RebootMachine()
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> PauseMachine()
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> ResumeMachine()
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> PowerOffMachine()
        => NoDeviceResponse<ApiResponse>();

    public Task<WriteMemoryResponse?> WriteMemory(string address, string hexData)
        => NoDeviceResponse<WriteMemoryResponse>();

    public Task<WriteMemoryResponse?> WriteMemory(string address, byte[] dataContentBytes)
        => NoDeviceResponse<WriteMemoryResponse>();

    public Task<byte[]?> ReadMemory(string address, int length = 256)
        => NoDevice<byte[]>();

    public Task<DebugRegisterResponse?> ReadDebugRegister()
        => NoDeviceResponse<DebugRegisterResponse>();

    public Task<DebugRegisterResponse?> WriteDebugRegister(string value)
        => NoDeviceResponse<DebugRegisterResponse>();

    public Task<ApiResponse?> PlayOnDeviceSidFile(string filePath, int songNumber = 1)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> PlayUploadedSidFile(byte[] fileContentBytes, string fileName, int songNumber = 1)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> PlayOnDeviceModFile(string filePath)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> PlayUploadedModFile(byte[] fileContentBytes, string fileName)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> LoadPrgFileOnDevice(string filePath)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> LoadUploadedPrgFile(byte[] fileContentBytes, string fileName)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> RunPrgFileOnDevice(string filePath)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> RunUploadedPrgFile(byte[] fileContentBytes, string fileName)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> RunCrtFileOnDevice(string filePath)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> RunUploadedCrtFile(byte[] fileContentBytes, string fileName)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> StartStream(StreamType type, string destinationIp)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> StopStream(StreamType type)
        => NoDeviceResponse<ApiResponse>();

    public Task<DrivesResponse?> GetDrives()
        => NoDeviceResponse<DrivesResponse>();

    public Task<MountImageResponse?> MountOnDeviceImage(string drive, string filePath, DiskImageType imageType, DiskMode mode)
        => NoDeviceResponse<MountImageResponse>();

    public Task<MountImageResponse?> MountUploadedImage(string drive, byte[] imageFileContentBytes, string fileName, DiskImageType imageType, DiskMode mode)
        => NoDeviceResponse<MountImageResponse>();

    public Task<ApiResponse?> ResetDrive(string drive)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> RemoveDrive(string drive)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> UnlinkDrive(string drive)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> TurnOnDrive(string drive)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> TurnOffDrive(string drive)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> LoadOnDeviceDriveRom(string drive, string filePath)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> LoadUploadedDriveRom(string drive, byte[] romFileContentBytes)
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> SetDriveMode(string drive, DriveMode mode)
        => NoDeviceResponse<ApiResponse>();

    public Task<GetFileInfoResponse?> GetFileInfo(string filePath)
        => NoDeviceResponse<GetFileInfoResponse>();

    public Task<CreateDiskImageResponse?> CreateDiskImage(ImageFileType imageFileType, string imageFilePath, int tracks, string? diskLabel)
        => NoDeviceResponse<CreateDiskImageResponse>();

    public Task<ConfigsResponse?> GetConfigs()
        => NoDeviceResponse<ConfigsResponse>();

    public Task<T?> GetConfigCategory<T>(string category) where T : IApiResponse
    {
        _toastService?.DisplayWarningToast(message: Strings.WarningMessages.NoRegisteredDeviceFound,
            title: Strings.WarningMessages.NoRegisteredDeviceFoundTitle);
        return Task.FromResult<T?>(default(T?));
    }

    public Task<T?> GetConfigCategorySection<T>(string category, string section) where T : IApiResponse
    {
        _toastService?.DisplayWarningToast(message: Strings.WarningMessages.NoRegisteredDeviceFound,
            title: Strings.WarningMessages.NoRegisteredDeviceFoundTitle);
        return Task.FromResult<T?>(default(T?));
    }

    public Task<T?> UpdateConfigCategorySectionValue<T>(string category, string section, string value) where T : IApiResponse
    {
        _toastService?.DisplayWarningToast(message: Strings.WarningMessages.NoRegisteredDeviceFound,
            title: Strings.WarningMessages.NoRegisteredDeviceFoundTitle);
        return Task.FromResult<T?>(default(T?));
    }

    public Task<ApiResponse?> UpdateConfig<TConfig>(string category, string section, TConfig config) where TConfig : class
        => NoDeviceResponse<ApiResponse>();

    public Task<ApiResponse?> ConfigurationOperation(ConfigOp operation)
        => NoDeviceResponse<ApiResponse>();

    public Task<byte[]> GetFile(string filePath) => Task.FromResult(Array.Empty<byte>());

    private Task<T?> NoDeviceResponse<T>() where T : ApiResponse, new()
    {
        var retVal = new T() { Errors = new[] { Strings.WarningMessages.NoRegisteredDeviceFound } };
        _toastService?.DisplayWarningToast(message: Strings.WarningMessages.NoRegisteredDeviceFound,
            title: Strings.WarningMessages.NoRegisteredDeviceFoundTitle);
        return Task.FromResult<T?>(retVal);
    }

    private Task<T?> NoDevice<T>() where T : class
    {
        _toastService?.DisplayWarningToast(message: Strings.WarningMessages.NoRegisteredDeviceFound,
            title: Strings.WarningMessages.NoRegisteredDeviceFoundTitle);
        return Task.FromResult<T?>(default(T?));
    }

    public void Dispose() { }
}
