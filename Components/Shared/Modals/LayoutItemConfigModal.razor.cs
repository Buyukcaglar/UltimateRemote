using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using UltimateRemote.Components.Shared.FormInputs;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Modals;

public sealed partial class LayoutItemConfigModal : BaseComponent
{
    [Inject] private JukeboxService JukeboxService { get; set; } = default!;
    
    [Parameter] public IModalReference? Self { get; set; }

    [Parameter] public string ModalTitle { get; set; } = default!;

    [Parameter, EditorRequired] public LayoutItem Item { get; set; } = default!;

    private FileTypeGroup[] FileTypeGroups => new[]
    {
        PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.DiskImages)!,
        PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.CartridgeImage)!,
        PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.SidFile)!,
        PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.ModFile)!,
        PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.Program)!,
    };

    private DeviceLocation[] EnabledLocations =>
        PrefsMgr.EnabledDeviceLocations.ToArray();

    private int _driveBusId;

    // WTF?!?! :((
    private string? DriveBusIdStr
    {
        get => _driveBusId.ToString();
        set
        {
            if (int.TryParse(value, out _driveBusId))
                Item.SetData(new KeyValuePair<string, int>("BusId", _driveBusId));
        }
    }
    
    protected override void OnInitialized()
    {
        InitDriveByBusIdType();
        base.OnInitialized();
    }

    private void InitDriveByBusIdType()
    {
        if (Item.Type == LayoutItemType.DriveByBusId)
        {
            var driveBusId = Item.GetData<KeyValuePair<string, int>>();
            if (!default(KeyValuePair<string, int>).Equals(driveBusId))
            {
                _driveBusId = driveBusId.Value;
            }
        }
    }

    private Task OnContentFileSelect(FileSelectorModel selectedFile)
    {
        if (Item.Type != LayoutItemType.StorageContentFile)
            return Task.CompletedTask;

        var fileInfo = new FileInfo(selectedFile.Path);
        var extension = fileInfo.Extension; // Includes '.'
        var fileName = fileInfo.Name; // Includes file extension
        var name = fileName.Replace(extension, "");

        if (string.IsNullOrWhiteSpace(Item.Name))
            Item.Name = name;
        
        Item.FileName = fileName;
        Item.Extension = extension.TrimStart('.').ToLowerInvariant();
        Item.Path = selectedFile.Path;
        Item.Location = selectedFile.Location;
        
        return Task.CompletedTask;

    }

    private Task OnPlaylistSelect(JukeboxPlaylist playlist)
    {
        Item.SetData<string>(playlist.Id);
        Item.Name = playlist.Name;
        return CloseModal();
    }

    private async Task UploadFile()
    {
        var fileContent = await ExecuteUiBlockingTask(task: FilePickerService.GetFileContentBytes(FilePickerOptions.LayoutItemUploadFileOptions, ToastService),
            blockingMessage: Strings.ContentListManager.BpMsgSelectFile("any D64, G64, D71, G71, D81, PRG, CRT, SID, MOD file"));

        if (fileContent is { ContentBytes.Length: > 0 })
        {
            var fileInfo = new FileInfo(fileContent.FileName);
            var extension = fileInfo.Extension; // Includes '.'
            var fileName = fileInfo.Name; // Includes file extension
            var name = fileName.Replace(extension, "");

            if (string.IsNullOrWhiteSpace(Item.Name))
                Item.Name = name;
        
            Item.FileName = fileName;
            Item.Extension = extension.TrimStart('.').ToLowerInvariant();

            Item.ContentBytes = fileContent.ContentBytes;
        }
    }

    private async Task UploadIcon()
    {
        var fileContent = await ExecuteUiBlockingTask(task: FilePickerService.GetFileContentBytes(FilePickerOptions.LayoutItemIconImageFileOptions, ToastService),
            blockingMessage: Strings.ContentListManager.BpMsgSelectFile("PNG, WEBP or SVG file"));

        if (fileContent is { ContentBytes.Length: > 0 })
        {
            var fileInfo = new FileInfo(fileContent.FileName);
            var extension = fileInfo.Extension.TrimStart('.').ToLowerInvariant();

            var iconString = extension switch
            {
                "png" => ImageHelper.GetDataImageSource(Convert.ToBase64String(fileContent.ContentBytes), ".png"),
                "webp" => ImageHelper.GetDataImageSource(Convert.ToBase64String(fileContent.ContentBytes), ".webp"),
                "svg" => System.Text.Encoding.Default.GetString(fileContent.ContentBytes),
                _ => null
            };
            
            if(string.IsNullOrWhiteSpace(iconString))
                return;

            Item.CustomIcon = iconString;
            Item.IconType = extension switch
            {
                "png" => CustomIconType.Png,
                "webp" => CustomIconType.WebP,
                "svg" => CustomIconType.Svg
            };
        }
    }

    private Task RemoveIcon()
    {
        Item.CustomIcon = null;
        Item.IconType = CustomIconType.None;
        return Task.CompletedTask;
    }


    private Task CloseModal()
    {
        Self?.Close(ModalResult.Ok<LayoutItem>(Item));
        return Task.CompletedTask;
    }
}
