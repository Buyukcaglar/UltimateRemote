using System.Web;

namespace UltimateRemote.Constants;
public class ApiUrls
{
    private static string UrlBase(string deviceIp) => $"http://{deviceIp}";
    
    private const string VersionUri = "/v1/version";
    
    public static string Version(string deviceIp) => $"{UrlBase(deviceIp)}{VersionUri}";
    
    private const string RunnersUri = "/v1/runners";
    private const string ConfigsUri = "/v1/configs";
    private const string MachineUri = "/v1/machine";
    private const string DrivesUri = "/v1/drives";
    private const string DataStreamsUri = "/v1/streams";
    private const string FileManipulationsUri = "/v1/files";

    #region Runners

    private static string SidPlayOnDeviceUri(string filePath, int songNumber) => $"{RunnersUri}:sidplay?file={HttpUtility.UrlEncode(filePath)}&songnr={songNumber}";

    private static string SidPlayUploadedFileUri(int songNumber) => $"{RunnersUri}:sidplay?songnr={songNumber}";

    private static string ModPlayOnDeviceUri(string filePath) => $"{RunnersUri}:modplay?file={HttpUtility.UrlEncode(filePath)}";

    private const string ModPlayUploadedFileUri = $"{RunnersUri}:modplay";

    private static string LoadProgramOnDeviceUri(string filePath) => $"{RunnersUri}:load_prg?file={HttpUtility.UrlEncode(filePath)}";

    private const string LoadUploadedProgramUri = $"{RunnersUri}:load_prg";

    private static string RunProgramOnDeviceUri(string filePath) => $"{RunnersUri}:run_prg?file={HttpUtility.UrlEncode(filePath)}";

    private const string RunUploadedProgramUri = $"{RunnersUri}:run_prg";

    private static string RunCartridgeOnDeviceUri(string filePath) => $"{RunnersUri}:run_crt?file={HttpUtility.UrlEncode(filePath)}";

    private const string RunUploadedCartridgeUri = $"{RunnersUri}:run_crt";

    public static string SidPlayOnDevice(string deviceIp, string filePath, int songNumber) =>
        $"{UrlBase(deviceIp)}{SidPlayOnDeviceUri(filePath, songNumber)}";

    public static string SidPlayUploadedFile(string deviceIp, int songNumber) =>
        $"{UrlBase(deviceIp)}{SidPlayUploadedFileUri(songNumber)}";

    public static string ModPlayOnDevice(string deviceIp, string filePath) =>
        $"{UrlBase(deviceIp)}{ModPlayOnDeviceUri(filePath)}";

    public static string ModPlayUploadedFile(string deviceIp) =>
        $"{UrlBase(deviceIp)}{ModPlayUploadedFileUri}";

    public static string LoadProgramOnDevice(string deviceIp, string filePath) =>
        $"{UrlBase(deviceIp)}{LoadProgramOnDeviceUri(filePath)}";

    public static string LoadUploadedProgram(string deviceIp) =>
        $"{UrlBase(deviceIp)}{LoadUploadedProgramUri}";

    public static string RunProgramFileOnDevice(string deviceIp, string filePath) =>
        $"{UrlBase(deviceIp)}{RunProgramOnDeviceUri(filePath)}";

    public static string RunUploadedProgramFile(string deviceIp) =>
        $"{UrlBase(deviceIp)}{RunUploadedProgramUri}";

    public static string RunCartridgeOnDevice(string deviceIp, string filePath) =>
        $"{UrlBase(deviceIp)}{RunCartridgeOnDeviceUri(filePath)}";

    public static string RunUploadedCartridge(string deviceIp) =>
        $"{UrlBase(deviceIp)}{RunUploadedCartridgeUri}";

    #endregion

    #region Configs

    public static string GetConfigurations(string deviceIp) => $"{UrlBase(deviceIp)}{ConfigsUri}";

    public static string GetConfigCategory(string deviceIp, string category)
        => $"{UrlBase(deviceIp)}{ConfigsUri}/{category}";

    public static string GetConfigCategorySection(string deviceIp, string category, string section)
        => $"{UrlBase(deviceIp)}{ConfigsUri}/{category}/{section}";

    public static string UpdateConfigCategorySectionValue(string deviceIp, string category, string section, string value)
        => $"{UrlBase(deviceIp)}{ConfigsUri}/{category}/{section}?value={value}";

    public static string UpdateConfig(string deviceIp) => $"{UrlBase(deviceIp)}{ConfigsUri}";

    public static string LoadConfigFromFlash(string deviceIp) => $"{UrlBase(deviceIp)}{ConfigsUri}:load_from_flash";

    public static string SaveConfigToFlash(string deviceIp) => $"{UrlBase(deviceIp)}{ConfigsUri}:save_to_flash";

    public static string ResetConfigToDefault(string deviceIp) => $"{UrlBase(deviceIp)}{ConfigsUri}:reset_to_default";

    #endregion

    #region Machine

    public static string ResetMachine(string deviceIp) => $"{UrlBase(deviceIp)}{MachineUri}:reset";

    public static string RebootMachine(string deviceIp) => $"{UrlBase(deviceIp)}{MachineUri}:reboot";

    public static string PauseMachine(string deviceIp) => $"{UrlBase(deviceIp)}{MachineUri}:pause";

    public static string ResumeMachine(string deviceIp) => $"{UrlBase(deviceIp)}{MachineUri}:resume";

    public static string PowerOffMachine(string deviceIp) => $"{UrlBase(deviceIp)}{MachineUri}:poweroff";

    public static string WriteMemory(string deviceIp, string address, string data) => $"{UrlBase(deviceIp)}{MachineUri}:writemem?address={address}&data={data}";

    public static string WriteMemory(string deviceIp, string address) => $"{UrlBase(deviceIp)}{MachineUri}:writemem?address={address}";

    public static string ReadMemory(string deviceIp, string address, int length = 256) => $"{UrlBase(deviceIp)}{MachineUri}:readmem?address={address}&length={length}";

    public static string ReadDebugRegister(string deviceIp) => $"{UrlBase(deviceIp)}{MachineUri}:debugreg";

    public static string WriteDebugRegister(string deviceIp, string value) => $"{UrlBase(deviceIp)}{MachineUri}:debugreg?value={value}";

    #endregion

    #region Drives

    public static string GetDrives(string deviceIp) => $"{UrlBase(deviceIp)}{DrivesUri}";

    public static string MountImageOnDevice(string deviceIp, string drive, string filePath, DiskImageType imageType, DiskMode mode)
        => $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:mount?image={HttpUtility.UrlEncode(filePath)}{(imageType != DiskImageType.NotSpecified ? ($"&imageType={imageType.ToString().ToLowerInvariant()}") : null)}{(mode != DiskMode.NotSpecified ? ($"&mode={mode.ToString().ToLowerInvariant()}") : null)}";

    // well this is literally an abomination!
    public static string MountUploadedImage(string deviceIp, string drive, DiskImageType imageType, DiskMode mode)
        => imageType == DiskImageType.NotSpecified && mode == DiskMode.NotSpecified
            ? $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:mount"
            : imageType != DiskImageType.NotSpecified && mode != DiskMode.NotSpecified
                ? $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:mount?type={imageType.ToString().ToLowerInvariant()}&mode={mode.ToString().ToLowerInvariant()}"
                : imageType != DiskImageType.NotSpecified
                    ? $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:mount?type={imageType.ToString().ToLowerInvariant()}"
                    : $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:mount?mode={mode.ToString().ToLowerInvariant()}";

    public static string ResetDrive(string deviceIp, string drive) => $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:reset";

    public static string RemoveDrive(string deviceIp, string drive) => $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:remove";

    public static string UnlinkDrive(string deviceIp, string drive) => $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:unlink";

    public static string TurnOnDrive(string deviceIp, string drive) => $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:on";
    
    public static string TurnOffDrive(string deviceIp, string drive) => $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:off";

    public static string LoadDriveRomOnDevice(string deviceIp, string drive, string filePath) =>
        $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:load_rom?file={HttpUtility.UrlEncode(filePath)}";

    public static string LoadUploadedDriveRom(string deviceIp, string drive) =>
        $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:load_rom";

    public static string SetDriveMode(string deviceIp, string drive, DriveMode mode) =>
        $"{UrlBase(deviceIp)}{DrivesUri}/{drive}:set_mode?mode={(int)mode}";

    #endregion

    #region DataStreams
    
    public static string StartStream(string deviceIp, StreamType type, string destinationIp) => $"{UrlBase(deviceIp)}{DataStreamsUri}/{type.ToString().ToLowerInvariant()}:start?ip={destinationIp}";

    public static string StopStream(string deviceIp, StreamType type) => $"{UrlBase(deviceIp)}{DataStreamsUri}/{type.ToString().ToLowerInvariant()}:stop";

    #endregion

    #region FileManipulations

    public static string GetFileInfoOnDevice(string deviceIp, string filePath) => $"{UrlBase(deviceIp)}{FileManipulationsUri}/{HttpUtility.UrlEncode(filePath)}:info";

    public static string CreateD64(string deviceIp, string filePath, D64Tracks tracks = D64Tracks.Tracks35, string? diskName = null)
        => $"{UrlBase(deviceIp)}{FileManipulationsUri}/{HttpUtility.UrlEncode(filePath)}:create_d64?tracks={(int)tracks}{(!string.IsNullOrWhiteSpace(diskName) ? ($"&diskname={diskName}") : null)}";

    public static string CreateD71(string deviceIp, string filePath, string? diskName = null)
        => $"{UrlBase(deviceIp)}{FileManipulationsUri}/{HttpUtility.UrlEncode(filePath)}:create_d71{(!string.IsNullOrWhiteSpace(diskName) ? ($"?diskname={diskName}") : null)}";

    public static string CreateD81(string deviceIp, string filePath, string? diskName = null)
        => $"{UrlBase(deviceIp)}{FileManipulationsUri}/{HttpUtility.UrlEncode(filePath)}:create_d81{(!string.IsNullOrWhiteSpace(diskName) ? ($"?diskname={diskName}") : null)}";

    public static string CreateDnp(string deviceIp, string filePath, int tracks, string? diskName = null)
        => $"{UrlBase(deviceIp)}{FileManipulationsUri}/{HttpUtility.UrlEncode(filePath)}:create_dnp?tracks={tracks}{(!string.IsNullOrWhiteSpace(diskName) ? ($"&diskname={diskName}") : null)}";

    #endregion
}
