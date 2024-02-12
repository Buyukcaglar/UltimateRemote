using Microsoft.AspNetCore.Components;
using UltimateRemote.Components.Shared.Modals;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.FormInputs;

public sealed record FileSelectorModel(string Location, string Path)
{
    public string LocationPath => FilePathHelper.LocationPath(Location, Path);
};

public sealed partial class FileSelector : BaseComponent
{
    [Parameter] public string Label { get; set; } = "File Path";

    [Parameter] public bool Enable74CharWarning { get; set; }

    [Parameter] public bool DisableDeviceRegistryCheck { get; set; }

    [Parameter] public string? ContainerCss { get; set; } = "fs-container";

    [Parameter] public string? HeaderLabelContainerCss { get; set; } = "rounded-top";

    [Parameter] public string? ActionButtonCss { get; set; } = "rounded-top-0";

    [Parameter] public string? DeviceLocationButtonCss { get; set; } = "rounded-top-0 rounded-end-0";

    [Parameter, EditorRequired] public string Placeholder { get; set; } = default!;

    [Parameter] public string? FilePath { get; set; }

    [Parameter] public string? ButtonIconCss { get; set; }

    [Parameter, EditorRequired] public string ModalTitleTemplate { get; set; } = default!;

    [Parameter, EditorRequired] public FileTypeGroup[] FileTypeGroups { get; set; } = default!;

    [Parameter, EditorRequired] public DeviceLocation[] DeviceLocations { get; set; } = default!;

    [Parameter] public EventCallback<FileSelectorModel> ButtonClickedEvent { get; set; }

    [Parameter] public EventCallback<string> DeviceLocationSelectedEvent { get; set; }

    [Parameter] public RenderFragment? HeaderContent { get; set; }

    [Parameter] public RenderFragment? InputGroupContent { get; set; }

    private SelectOption[]? FileLists { get; set; }

    private string? _textInputValidationStateStyle;

    private string HeaderLabel =>
        $"{Label}{(
            DeviceLocations.Any(deviceLocation => deviceLocation.Selected) ? $" (Location: '{DeviceLocations.FirstOrDefault(location => location.Selected)?.Path}')" : null
        )}";

    protected override void OnInitialized()
    {
        FileLists = FileService.GetFileListsHavingFileTypeGroups(FileTypeGroups)
            .ToList().ToSelectOptions();
        base.OnInitialized();
    }

    private Task OnDeviceLocationSelect(DeviceLocation location)
    {
        if (DeviceLocations is { Length: > 0 })
        {
            foreach (var deviceLocation in DeviceLocations)
            {
                deviceLocation.Selected = deviceLocation.Path == location.Path;
            }
        }

        return Task.CompletedTask;
    }

    private async Task SearchInList(string listName)
    {
        var fileList = FileService.GetFileList(listName)!;

        var modalParams = new ModalParameters()
        {
            { nameof(StorageContentFileSearch.FileTypeGroups), FileTypeGroups },
            { nameof(StorageContentFileSearch.FileList), fileList },
            { nameof(StorageContentFileSearch.IgnoreApiV0174CharWarning), !Enable74CharWarning },
            { nameof(StorageContentFileSearch.DisableDeviceRegistryCheck), DisableDeviceRegistryCheck },
            { nameof(StorageContentFileSearch.ModalTitle), string.Format(ModalTitleTemplate, fileList.Name) }
        };

        var searchFileListModal = ModalService.Show<StorageContentFileSearch>(title: "", modalParams);

        modalParams.Add(nameof(StorageContentFileSearch.Self), searchFileListModal);

        var result = await searchFileListModal.Result;

        if (result.Confirmed)
        {
            var filePath = Convert.ToString(result.Data);
            FilePath = filePath;
            await ButtonClicked();
        }
    }

    private async Task ButtonClicked()
    {
        if (string.IsNullOrWhiteSpace(FilePath))
        {
            _textInputValidationStateStyle = ValidationState.Invalid.InputStyle();
            return;
        }

        if (!DeviceLocations?.Any(option => option.Selected) ?? true)
        {
            ToastService.DisplayWarningToast(message: Strings.FileSelector.ToastMsgSelectLocation, title: Strings.FileSelector.ToastTitleSelectLocation);
            _textInputValidationStateStyle = null;
            return;
        }

        _textInputValidationStateStyle = null;
        
        var location = DeviceLocations.First(option => option.Selected).Path;
        await ButtonClickedEvent.InvokeAsync(new FileSelectorModel(location, FilePath!));
    }

}