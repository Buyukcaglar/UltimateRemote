using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;

public sealed partial class FileBrowser : BaseComponent
{
    //private FtpListItem[] _files = [];

    [Inject] private EventService EventService { get; set; } = default!;
    [Inject] private DeviceManager DeviceManager { get; set; } = default!;

    private LayoutItem[] _items = [];

    protected override void OnInitialized()
    {
        EventService.SignalDeviceListUpdateEvent -= OnDeviceListUpdate;
        EventService.SignalDeviceListUpdateEvent += OnDeviceListUpdate;

        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && CurrentDevice.Online)
        {
            await RefreshFiles();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task RefreshFiles(string path = "/")
    {
        var files = await CurrentDevice.ListFolder("/");
        _items = files.Select(f => new LayoutItem(LayoutItemType.StorageContentFile)
        {
            Name = f.Name,
            Path = f.Path,
            Extension = f.Extension,
            IconType = CustomIconType.None,
            Location = path
        }).ToArray();
        await InvokeAsync(StateHasChanged);
    }


    private async void OnDeviceListUpdate(object? sender, EventArgs args)
    {
        // ToDo: Check if current device changed-
        await RefreshFiles();
        await InvokeAsync(StateHasChanged);
    }

}
