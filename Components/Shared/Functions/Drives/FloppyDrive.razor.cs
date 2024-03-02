using Microsoft.AspNetCore.Components;
using UltimateRemote.Components.Shared.FormInputs;
using UltimateRemote.Components.Shared.Modals;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;
using UltimateRemote.Services.D64Reader;

namespace UltimateRemote.Components.Shared.Functions.Drives;
public sealed partial class FloppyDrive : BaseFileFunctionComponent
{
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

    private string DisplayDirectory => _d64Reader?.DirectoryItems is { Count: > 0 } ? "position-absolute" : "d-none";

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

    protected override async Task OnInitializedAsync()
    {
        if (MountedImageFile != "none")
            await TrySetD64Reader(MountedImageFile);
        
        await base.OnInitializedAsync();
    }

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
            DriveTask.Remove => CurrentDevice.RemoveDrive(DriveInfo.Key)
                .ExecOnSuccess2(() =>
                    {
                        _d64Reader = null;
                        return Task.CompletedTask;
                    }),
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

    private async Task DisplayDiskDirectory()
    {
        if (_d64Reader?.DirectoryItems is not { Count: > 0 })
            return;

        var modalParams = new ModalParameters()
        {
            {nameof(DiskDirectoryModal.ModalTitle), "Disk Directory"},
            {nameof(DiskDirectoryModal.D64Reader), _d64Reader!}
        };

        var diskDirectoryModal = ModalService.Show<DiskDirectoryModal>(title: "", modalParams);

        modalParams.Add(nameof(DiskDirectoryModal.Self), diskDirectoryModal);

        var result = await diskDirectoryModal.Result;

        if (result is { Confirmed: true, Data: not null })
        {
            var modalResult = result.GetModalData<(DirectoryItem DirItem, bool Run)?>();
            if (modalResult.HasValue)
                await CurrentDevice.ExecuteKeyboardBuffer(modalResult.Value.Run
                    ? MachineCommands.LoadFileAndRun(DriveInfo.Value.BusId, modalResult.Value.DirItem.Name!)
                    : MachineCommands.LoadFile(DriveInfo.Value.BusId, modalResult.Value.DirItem.Name!));
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

        if (driveSettings == null)
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

                await TrySetD64Reader(selectedFile.LocationPath);

            });

    private async Task MountUploadedImage()
    {
        var fileContent = await ExecuteUiBlockingTask(task: FilePickerService.GetFileContentBytes(FilePickerOptions.DiskImageFileOptions, ToastService),
            blockingMessage: Strings.ContentListManager.BpMsgSelectFile(FileTypeGroupNames.DiskImages));

        if (fileContent is { ContentBytes.Length: > 0 })
        {
            await CurrentDevice.MountUploadedImage(DriveInfo.Key, fileContent.ContentBytes, fileContent.FileName, _imageType, _diskMode)
                .ExecOnSuccess(async (mountImageResponse) =>
                {
                    HistoryManager.Add(fileContent.FileName, fileContent.ContentBytes);
                    DisplaySuccessToast(message: Strings.FloppyDrive.ToastMsgSuccessfulMountResult(mountImageResponse),
                        Strings.FloppyDrive.ToastTitleSuccessfulMountResult);
                    await DriveChangedEvent.InvokeAsync();

                    if (fileContent.FileName.EndsWith(".d64", StringComparison.InvariantCultureIgnoreCase))
                        TrySetD64Reader(fileContent.ContentBytes);

                });
        }
    }

    private Task DiskImageHistoryItemSelected(HistoryItem item)
    {
        var task = item.Type switch
        {
            HistoryItemType.StorageContentFile => CurrentDevice.MountOnDeviceImage(DriveInfo.Key, GetPath(item.Path!), _imageType, _diskMode)
                .ExecOnSuccess2(async (mountImageResponse) =>
                {
                    HistoryManager.Add(item.Path!);
                    DisplaySuccessToast(message: Strings.FloppyDrive.ToastMsgSuccessfulMountResult(mountImageResponse),
                        Strings.FloppyDrive.ToastTitleSuccessfulMountResult);
                    await DriveChangedEvent.InvokeAsync();
                }),
            HistoryItemType.UploadedFile => CurrentDevice.MountUploadedImage(DriveInfo.Key, item.ContentBytes!, item.FileName, _imageType, _diskMode)
                .ExecOnSuccess2(async (mountImageResponse) =>
                {
                    HistoryManager.Add(item.FileName, item.ContentBytes!);
                    DisplaySuccessToast(message: Strings.FloppyDrive.ToastMsgSuccessfulMountResult(mountImageResponse),
                        Strings.FloppyDrive.ToastTitleSuccessfulMountResult);

                    await DriveChangedEvent.InvokeAsync();
                }),
            _ => Task.FromResult(default(MountImageResponse?))
        };

        return task.ExecOnSuccess(() =>
        {
            if (item.Extension == "d64")
                TrySetD64Reader(item.ContentBytes!);
            return Task.CompletedTask;
        });
    }

    #region DriveRom

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

    #endregion

    private async Task TrySetD64Reader(string filePath)
    {
        if (!filePath.EndsWith(".d64", StringComparison.InvariantCultureIgnoreCase))
            return;

        var diskImageBytes = await CurrentDevice.GetFile(filePath);

        TrySetD64Reader(diskImageBytes);
    }

    private void TrySetD64Reader(byte[] diskImageBytes)
    {
        if (diskImageBytes.Length == 0)
            return;

        try
        {
            _d64Reader = new D64Reader(diskImageBytes);
        }
        catch { }
    }
}