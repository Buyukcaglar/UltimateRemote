using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class UserFile : BaseComponent
{
    [Parameter, EditorRequired] public LayoutItem Item { get; set; } = default!;

    private string InfoText => Strings.UserFile.FunctionInfo(Item.Type, Item.FileName, Item.Extension, Item.Path, Item.Location);

    private string DefaultIconCss => Item.Extension switch
    {
        "d64" => "floppy-disk",
        "g64" => "floppy-disk",
        "d71" => "floppy-disk",
        "g71" => "floppy-disk",
        "d81" => "floppy-disk",
        "dnp" => "hard-drives",
        "prg" => "app-window",
        "crt" => "circuitry",
        "sid" => "wave-triangle",
        "mod" => "music-note",
        _ => ""
    };

    private string[] _diskImageExtensions = ["d64", "g64", "d71", "g71", "d81"];

    private readonly ActionType[] _actionTypes = ActionType.Run.GetEnumValues();

    private DiskMode _diskMode;
    private ActionType _actionType;
    private string _songNr = "1";

    private (string DriveId, int BusId, bool Online)[] _floppyDrives = [];
    private (string DriveId, int BusId, bool Online) _selectedDrive;

    private DeviceLocation[] EnabledLocations => PrefsMgr.EnabledDeviceLocations.ToArray();
    private DeviceLocation? _selectedLocation;

    private string ItemLocationPath => _selectedLocation != null
        ? FilePathHelper.LocationPath(_selectedLocation?.Path, Item.Path)
        : Item.LocationPath;

    protected override async Task OnInitializedAsync()
    {
        await Init();
        await base.OnInitializedAsync();
    }

    private async Task Init()
    {
        SetUpLocations();
    }

    private void SetUpLocations()
    {
        if (Item.Type == LayoutItemType.StorageContentFile)
        {
            _selectedLocation = EnabledLocations.First(location => location.Path == Item.Location);
        }
    }

    Func<Task> PrgOnSuccessFunction => () =>
    {
        DisplaySuccessToast(message: Strings.LoadPrg.ToastMsgPrgLoaded(Item.FileName!),
            Strings.LoadPrg.ToastTitlePrgLoaded);
        return Task.CompletedTask;
    };

    private Task HandleClick()
        => (Item.Extension, ItemType: Item.Type, ActionType: _actionType) switch
        {
            { Extension: "d64" or "g64" or "d71" or "g71" or "d81" } => MountImage(Item.Type),

            /*
            { Extension: "d64" or "g64" or "d71" or "g71" or "d81", ItemType: LayoutItemType.StorageContentFile } => CurrentDevice.MountOnDeviceImage(_selectedDrive.DriveId, ItemLocationPath, DiskImageType.NotSpecified, _diskMode)
                .ExecOnSuccess((mountImageResponse) =>
                {
                    DisplaySuccessToast(message: Strings.FloppyDrive.ToastMsgSuccessfulMountResult(mountImageResponse),
                        Strings.FloppyDrive.ToastTitleSuccessfulMountResult);
                    DeviceManager.InvokeDeviceListUpdatedEvent();
                    return Task.CompletedTask;
                }),
            { Extension: "d64" or "g64" or "d71" or "g71" or "d81", ItemType: LayoutItemType.UploadedFile } => CurrentDevice.MountUploadedImage(_selectedDrive.DriveId, Item.ContentBytes!, Item.FileName!, DiskImageType.NotSpecified, _diskMode)
                .ExecOnSuccess((mountImageResponse) =>
                {
                    DisplaySuccessToast(message: Strings.FloppyDrive.ToastMsgSuccessfulMountResult(mountImageResponse),
                        Strings.FloppyDrive.ToastTitleSuccessfulMountResult);
                    DeviceManager.InvokeDeviceListUpdatedEvent();
                    return Task.CompletedTask;
                }),
            */
            
            { Extension: "prg", ItemType: LayoutItemType.StorageContentFile, ActionType: ActionType.Run } => CurrentDevice.RunPrgFileOnDevice(ItemLocationPath),
            { Extension: "prg", ItemType: LayoutItemType.StorageContentFile, ActionType: ActionType.Load } => CurrentDevice.LoadPrgFileOnDevice(ItemLocationPath)
                .ExecOnSuccess(PrgOnSuccessFunction),
            
            { Extension: "prg", ItemType: LayoutItemType.UploadedFile, ActionType: ActionType.Run } => CurrentDevice.RunUploadedPrgFile(Item.ContentBytes!, Item.FileName!),
            { Extension: "prg", ItemType: LayoutItemType.UploadedFile, ActionType: ActionType.Load } => CurrentDevice.LoadUploadedPrgFile(Item.ContentBytes!, Item.FileName!)
                .ExecOnSuccess(PrgOnSuccessFunction),
            
            { Extension: "crt", ItemType: LayoutItemType.StorageContentFile } => CurrentDevice.RunCrtFileOnDevice(ItemLocationPath),
            { Extension: "crt", ItemType: LayoutItemType.UploadedFile } => CurrentDevice.RunUploadedCrtFile(Item.ContentBytes!, Item.FileName!),
            
            { Extension: "sid", ItemType: LayoutItemType.StorageContentFile } => CurrentDevice.PlayOnDeviceSidFile(ItemLocationPath, int.Parse(_songNr)),
            { Extension: "sid", ItemType: LayoutItemType.UploadedFile } => CurrentDevice.PlayUploadedSidFile(Item.ContentBytes!, Item.FileName!, int.Parse(_songNr)),
            
            { Extension: "mod", ItemType: LayoutItemType.StorageContentFile } => CurrentDevice.PlayOnDeviceModFile(ItemLocationPath),
            { Extension: "mod", ItemType: LayoutItemType.UploadedFile } => CurrentDevice.PlayUploadedModFile(Item.ContentBytes!, Item.FileName!),
            
            _ => Task.CompletedTask
        };

    private async Task MountImage(LayoutItemType itemType)
    {
        _floppyDrives = await CurrentDevice.GetFloppyDrives();
        var onlineDrive = _floppyDrives.FirstOrDefault(tuple => tuple.Online);

        if (onlineDrive.Online)
        {
            _selectedDrive = onlineDrive;

            Func<MountImageResponse, Task> mountImageSuccessTask = (mountImageResponse) =>
            {
                DisplaySuccessToast(
                    message: Strings.FloppyDrive.ToastMsgSuccessfulMountResult(mountImageResponse),
                    Strings.FloppyDrive.ToastTitleSuccessfulMountResult);
                DeviceManager.InvokeDeviceListUpdatedEvent();
                return Task.CompletedTask;
            };

            switch (itemType)
            {
                case LayoutItemType.StorageContentFile:
                    await CurrentDevice.MountOnDeviceImage(onlineDrive.DriveId, ItemLocationPath, DiskImageType.NotSpecified, _diskMode)
                        .ExecOnSuccess(mountImageSuccessTask);
                    break;
                case LayoutItemType.UploadedFile:
                    await CurrentDevice.MountUploadedImage(onlineDrive.DriveId, Item.ContentBytes!, Item.FileName!,
                            DiskImageType.NotSpecified, _diskMode).ExecOnSuccess(mountImageSuccessTask);
                    break;
            }
        }
    }

    private Task UnlinkDrive()
        => CurrentDevice.UnlinkDrive(_selectedDrive.DriveId);
}