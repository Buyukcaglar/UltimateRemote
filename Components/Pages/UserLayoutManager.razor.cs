using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UltimateRemote.Components.Shared;
using UltimateRemote.Components.Shared.Modals;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Pages;

[Route(Blazor.RouteTemplates.LayoutManager)]
public sealed partial class UserLayoutManager : BaseComponent, IDisposable
{
    [Inject] private LayoutManager LayoutManager { get; set; } = default!;

    private IJSObjectReference? _js;
    private DotNetObjectReference<UserLayoutManager>? _objRef;

    private readonly LayoutItemType[] _configurableTypes = new[]
        { LayoutItemType.StorageContentFile, 
            LayoutItemType.UploadedFile, 
            LayoutItemType.HVSCSIDFile,
            LayoutItemType.JukeboxPlaylist,
            LayoutItemType.DriveByBusId };

    private readonly LayoutItemType[] _customTypes = new[]
        { LayoutItemType.StorageContentFile, 
            LayoutItemType.UploadedFile,
            LayoutItemType.HVSCSIDFile,
            LayoutItemType.JukeboxPlaylist  };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _js ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/Pages/UserLayoutManager.razor.js");
            _objRef ??= DotNetObjectReference.Create(this);
            await _js.InvokeVoidAsync("registerLayoutNameChangeEvents", _objRef);
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private string GetItemName(LayoutItem item)
        => !_customTypes.Contains(item.Type)
            ? item.Type.GetStringValue()!
            : !string.IsNullOrWhiteSpace(item.Name)
                ? $"{item.Type.GetStringValue()!} ({item.Name})"
                : !string.IsNullOrWhiteSpace(item.FileName)
                    ? $"{item.Type.GetStringValue()!} ({item.FileName})"
                    : item.Type.GetStringValue()!;
            

    private ValueTask RemoveLayoutConfirm(int layoutIndex, string layoutName)
        => BlockPageWithConfirmWithParam(Strings.LayoutManager.RemoveLayoutWarning(layoutName), nameof(RemoveLayout),
            layoutIndex, _objRef!);

    [JSInvokable]
    public async Task RemoveLayout(int layoutIndex)
    {
        LayoutManager.RemoveLayout(layoutIndex);
        await InvokeAsync(StateHasChanged);
    }

    private Task AddItem(LayoutItemType itemType, int layoutIndex)
    {
        var item = new LayoutItem(itemType);
        LayoutManager.AddLayoutItem(layoutIndex, item);

        if ((int)item.Type < 4)
        {
            return item.Type == LayoutItemType.HVSCSIDFile
                ? DisplayTuneSearchModal(item, layoutIndex)
                : DisplayItemConfigPopUp(item);
        }

        return Task.CompletedTask;
    }
        

    private void RemoveItem(int layoutIndex, int itemIndex)
        => LayoutManager.RemoveLayoutItem(layoutIndex, itemIndex);

    private async Task DisplayItemConfigPopUp(LayoutItem item)
    {
        var modalParams = new ModalParameters()
        {
            { nameof(LayoutItemConfigModal.Item), item },
            { nameof(LayoutItemConfigModal.ModalTitle), $"{item.Type.GetStringValue()} Settings" }
        };

        var itemConfigModal = ModalService.Show<LayoutItemConfigModal>(title: "", modalParams);

        modalParams.Add(nameof(LayoutItemConfigModal.Self), itemConfigModal);

        var result = await itemConfigModal.Result;
        
        LayoutManager.SaveCurrentLayout();
    }

    private async Task DisplayTuneSearchModal(LayoutItem item, int layoutIndex)
    {
        var modalParams = new ModalParameters()
        {
            { nameof(HvscSidFileSearchModal.MultiSelect), false },
        };

        var itemConfigModal = ModalService.Show<HvscSidFileSearchModal>(title: "", modalParams);

        modalParams.Add(nameof(HvscSidFileSearchModal.Self), itemConfigModal);

        var result = await itemConfigModal.Result;

        if (result is { Confirmed: true, Data: not null })
        {
            var selectedItems = (SidFileInfo[]) result.Data;
            var sidFileInfo = selectedItems[0];
            var fileInfo = new FileInfo(sidFileInfo.FilePath);
            item.SetData<SidFileInfo>(selectedItems[0]);
            item.FileName = fileInfo.Name;
            item.Name = fileInfo.Name;
            await DisplayItemConfigPopUp(item);
            return;
        }

        // If user does not select file ...
        LayoutManager.RemoveLayoutItem(layoutIndex, item);
    }

    [JSInvokable]
    public Task OnLayoutNameChange(string layoutIndex, string? name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Task.CompletedTask;

        LayoutManager.UpdateLayoutName(int.Parse(layoutIndex), name);

        return Task.CompletedTask;
    }

    private static string GetLayoutItemIconCss(LayoutItemType itemType)
        => itemType switch
        {
            LayoutItemType.StorageContentFile => "file ph-duotone",
            LayoutItemType.UploadedFile => "upload ph-duotone",
            LayoutItemType.HVSCSIDFile => "wave-triangle ph-duotone",
            LayoutItemType.JukeboxPlaylist => "playlist ph-duotone",
            LayoutItemType.PlaySidMusic => "wave-triangle ph-duotone",
            LayoutItemType.PlayModMusic => "music-note ph-duotone",
            LayoutItemType.RunLoadProgram => "app-window ph-duotone",
            LayoutItemType.RunCartridge => "circuitry ph-duotone",
            LayoutItemType.ResetMachine => "arrow-clockwise ph-duotone",
            LayoutItemType.RebootMachine => "arrow-counter-clockwise ph-duotone",
            LayoutItemType.ResetRebootStack => "arrows-clockwise ph-duotone",
            LayoutItemType.MachineFunctions => "cpu ph-duotone",
            LayoutItemType.FloppyDrives => "floppy-disk ph-duotone",
            LayoutItemType.NonFloppyDrives => "hard-drives ph-duotone",
            LayoutItemType.DriveByBusId => "floppy-disk ph-duotone",
            LayoutItemType.Streams => "flow-arrow ph-duotone",
            LayoutItemType.CreateDiskImage => "disc ph-duotone",
            LayoutItemType.GetOnDeviceFileInfo => "file-search ph-duotone",
            _ => ""
        };

    public override void Dispose()
    {
        if (_js is IDisposable jsDisposable)
            jsDisposable.Dispose();
        else if (_js != null)
            _ = _js.DisposeAsync().AsTask();
        _objRef?.Dispose();

        base.Dispose();
    }
}
