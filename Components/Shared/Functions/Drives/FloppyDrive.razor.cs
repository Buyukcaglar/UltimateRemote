using Microsoft.AspNetCore.Components;
using UltimateRemote.Components.Shared.FormInputs;
using UltimateRemote.Components.Shared.Modals;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;
using UltimateRemote.Services.D64Reader;

namespace UltimateRemote.Components.Shared.Functions.Drives;
public sealed partial class FloppyDrive : BaseFileFunctionComponent
{
    [Inject] private FtpClient FtpClient { get; set; }

    [Parameter, EditorRequired] public KeyValuePair<string, DriveInfoResponse> DriveInfo { get; set; } = default!;

    [Parameter] public EventCallback DriveChangedEvent { get; set; }

    protected override FileTypeGroup[] AllowedFileTypeGroups =>
        new[] { PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.RomFile)! };

    private FileTypeGroup[] DiskImageFileTypeGroups =>
        new[] { PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.DiskImages)! };

    private enum DriveTask { Reset, Remove, Unlink, TurnOn, TurnOff, SetMode }
    
    private string CardTitle => $"Drive {DriveInfo.Value.BusId}";

    private string MountedImageFile => !string.IsNullOrWhiteSpace(DriveInfo.Value.ImageFile)
        ? DriveInfo.Value.ImageFile
        : "none";

    private string MountedImageFilePath => !string.IsNullOrWhiteSpace(DriveInfo.Value.ImagePath)
        ? DriveInfo.Value.ImagePath
        : "none";

    private string CurrentRomFile => !string.IsNullOrWhiteSpace(DriveInfo.Value.Rom)
        ? DriveInfo.Value.Rom
        : "none";

    private string DriveIconClass =>
        $"floppy-disk {(DriveInfo.Value.Enabled ? "indicate-online" : "indicate-offline")}";

    private string DisplayCommands => MountedImageFile != "none" ? "position-absolute" : "d-none";

    private D64Reader? _d64Reader;

    private DiskImageType _imageType;
    private DiskMode _diskMode;
    
    // <Not cool ...>
    private DriveMode _driveMode;
    private DriveMode DriveMode
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(DriveInfo.Value.Type))
                _driveMode = DriveInfo.Value.Type == "Unknown" ? DriveMode.Unknown : Enum.Parse<DriveMode>($"Drive{DriveInfo.Value.Type}");
            return _driveMode;
        }
        set => _driveMode = value;
    }
    // </Not cool ...>

    private Task OnDriveModeSelect(DriveMode mode)
    {
        _driveMode = mode;
        return ExecDriveTask(DriveTask.SetMode);
    }

    private Task ExecDriveTask(DriveTask driveTask)
    {
        Task<ApiResponse?> task = driveTask switch
        {
            DriveTask.Reset => CurrentDevice.ResetDrive(DriveInfo.Key),
            DriveTask.Remove => CurrentDevice.RemoveDrive(DriveInfo.Key),
            DriveTask.Unlink => CurrentDevice.UnlinkDrive(DriveInfo.Key),
            DriveTask.TurnOn => CurrentDevice.TurnOnDrive(DriveInfo.Key),
            DriveTask.TurnOff => CurrentDevice.TurnOffDrive(DriveInfo.Key),
            DriveTask.SetMode => CurrentDevice.SetDriveMode(DriveInfo.Key, _driveMode),
            _ => Task.FromResult(default(ApiResponse?))
        };

        return task.ExecOnSuccess(DriveChangedEvent.InvokeAsync());
    }

    private async Task TogglePower()
    {
        // In case of Drive Info is not up-to-date
        var currentPowerState = DriveInfo.Value.Enabled;
        await DriveChangedEvent.InvokeAsync();
        var updatedPowerState = DriveInfo.Value.Enabled;

        if (currentPowerState == updatedPowerState)
        {
            await ExecDriveTask(updatedPowerState ? DriveTask.TurnOff : DriveTask.TurnOn);
        }
    }

    private Task ExecKeyboardBuffer(MachineCommand command)
        => CurrentDevice.ExecuteKeyboardBuffer(command.CommandFunc.Invoke(DriveInfo.Value.BusId));

    private async Task DisplayConfigurationPopUp()
    {
        var driveId = DriveInfo.Key.ToUpperInvariant();
        var slug = $"Drive {driveId} Settings";

        var driveConfig = await CurrentDevice.GetConfigCategory<ConfigCategoryResponse>(slug);
        var driveSettings = driveConfig?.GetValue<Dictionary<string, System.Text.Json.JsonElement?>>(slug);
        
        if(driveSettings == null)
            return;

        var driveSettingValues = driveSettings.Keys.Where(key => driveSettings[key].HasValue)
            .Select(key => new ConfigCategoryItem(key, driveSettings[key]!.Value.ValueKind)).ToArray();

        var modalParams = new ModalParameters()
        {
            { nameof(FloppyDriveSettingsModal.DriveId), driveId },
            { nameof(FloppyDriveSettingsModal.SettingValues), driveSettingValues },
            { nameof(FloppyDriveSettingsModal.ModalTitle), slug },
            { nameof(FloppyDriveSettingsModal.DriveChangedEvent), DriveChangedEvent }
        };

        var searchFileListModal = ModalService.Show<FloppyDriveSettingsModal>(title: "", modalParams);

        modalParams.Add(nameof(FloppyDriveSettingsModal.Self), searchFileListModal);
        
        await searchFileListModal.Result;

        await DriveChangedEvent.InvokeAsync();
    }

    private Task MountOnDeviceImage(FileSelectorModel selectedFile)
        => CurrentDevice.MountOnDeviceImage(DriveInfo.Key, selectedFile.LocationPath, _imageType, _diskMode)
            .ExecOnSuccess(async (mountImageResponse) =>
            {
                HistoryManager.Add(selectedFile.Path);
                DisplaySuccessToast(message: Strings.FloppyDrive.ToastMsgSuccessfulMountResult(mountImageResponse),
                    Strings.FloppyDrive.ToastTitleSuccessfulMountResult);
                await DriveChangedEvent.InvokeAsync();

                var selectedDiskImage = await FtpClient.GetFile($"ftp://{CurrentDevice.IpAddress}{selectedFile.LocationPath}");
                System.Diagnostics.Debug.WriteLine($"Selected Disk Image File Size: {selectedDiskImage.Length}");


                var d64Reader = new D64Reader(selectedDiskImage);
                foreach (var d64ReaderDirectoryItem in d64Reader.DirectoryItems)
                {
                    System.Diagnostics.Debug.WriteLine(d64ReaderDirectoryItem.Name);
                }

            });

    private async Task MountUploadedImage()
    {
        var fileContent = await ExecuteUiBlockingTask(task: FilePickerService.GetFileContentBytes(FilePickerOptions.DiskImageFileOptions, ToastService),
            blockingMessage: Strings.ContentListManager.BpMsgSelectFile(FileTypeGroupNames.DiskImages));

        if (fileContent is { ContentBytes.Length: > 0 })
        {
            await CurrentDevice.MountUploadedImage(DriveInfo.Key, fileContent.ContentBytes, fileContent.FileName,_imageType, _diskMode)
                .ExecOnSuccess(async (mountImageResponse) =>
                {
                    HistoryManager.Add(fileContent.FileName, fileContent.ContentBytes);
                    DisplaySuccessToast(message: Strings.FloppyDrive.ToastMsgSuccessfulMountResult(mountImageResponse),
                        Strings.FloppyDrive.ToastTitleSuccessfulMountResult);
                    await DriveChangedEvent.InvokeAsync();
                });
        }
    }

    private Task DiskImageHistoryItemSelected(HistoryItem item)
        => item.Type switch
        {
            HistoryItemType.StorageContentFile => CurrentDevice.MountOnDeviceImage(DriveInfo.Key, GetPath(item.Path!), _imageType, _diskMode)
                .ExecOnSuccess(async (mountImageResponse) =>
                {
                    HistoryManager.Add(item.Path!); 
                    DisplaySuccessToast(message: Strings.FloppyDrive.ToastMsgSuccessfulMountResult(mountImageResponse),
                        Strings.FloppyDrive.ToastTitleSuccessfulMountResult);
                    await DriveChangedEvent.InvokeAsync();
                }),
            HistoryItemType.UploadedFile => CurrentDevice.MountUploadedImage(DriveInfo.Key, item.ContentBytes!, item.FileName,_imageType, _diskMode)
                .ExecOnSuccess(async (mountImageResponse) =>
                {
                    HistoryManager.Add(item.FileName, item.ContentBytes!); 
                    DisplaySuccessToast(message: Strings.FloppyDrive.ToastMsgSuccessfulMountResult(mountImageResponse),
                        Strings.FloppyDrive.ToastTitleSuccessfulMountResult);
                    await DriveChangedEvent.InvokeAsync();
                }),
            _ => Task.CompletedTask
        };

    private Task LoadOnDeviceRom(FileSelectorModel selectedFile)
        => CurrentDevice.LoadOnDeviceDriveRom(DriveInfo.Key, selectedFile.LocationPath)
            .ExecOnSuccess(async () =>
            {
                HistoryManager.Add(selectedFile.Path);
                await DriveChangedEvent.InvokeAsync();
            });

    private async Task LoadUploadedRom()
    {
        var fileContent = await ExecuteUiBlockingTask(task: FilePickerService.GetFileContentBytes(FilePickerOptions.RomFileOptions, ToastService),
            blockingMessage: Strings.ContentListManager.BpMsgSelectFile(FileTypeGroupNames.RomFile));

        if (fileContent is { ContentBytes.Length: > 0 })
        {
            await CurrentDevice.LoadUploadedDriveRom(DriveInfo.Key, fileContent.ContentBytes)
                .ExecOnSuccess(async () =>
                {
                    HistoryManager.Add(fileContent.FileName, fileContent.ContentBytes);
                    await DriveChangedEvent.InvokeAsync();
                });
        }
    }

    private Task DriveRomHistoryItemSelected(HistoryItem item)
        => item.Type switch
        {
            HistoryItemType.StorageContentFile => CurrentDevice.LoadOnDeviceDriveRom(DriveInfo.Key, GetPath(item.Path!))
                .ExecOnSuccess(async () => { HistoryManager.Add(item.Path!); await DriveChangedEvent.InvokeAsync(); }),
            HistoryItemType.UploadedFile => CurrentDevice.LoadUploadedDriveRom(DriveInfo.Key, item.ContentBytes!)
                .ExecOnSuccess(async () => { HistoryManager.Add(item.FileName, item.ContentBytes!); await DriveChangedEvent.InvokeAsync(); }),
            _ => Task.CompletedTask
        };
}