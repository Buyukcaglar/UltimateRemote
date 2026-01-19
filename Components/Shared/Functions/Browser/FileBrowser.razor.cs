using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions.Browser;

public sealed partial class FileBrowser : BaseComponent
{
    private FtpListItem[] _files = [];

    [Inject] private EventService EventService { get; set; } = default!;

    private string? CurrentDeviceIp { get; set; }

    protected override void OnInitialized()
    {
        EventService.SignalDeviceListUpdateEvent -= OnDeviceListUpdate;
        EventService.SignalDeviceListUpdateEvent += OnDeviceListUpdate;
        CurrentDeviceIp = CurrentDevice.IpAddress;
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

    public async Task OnItemClick(FtpListItem item)
    {
        if (item.IsDirectory)
        {
            await RefreshFiles(item.Path);
            return;
        }
    }

    private async Task RefreshFiles(string path = "/")
    {
        _files = await CurrentDevice.ListFolder(path);
        await InvokeAsync(StateHasChanged);
    }

    private async void OnDeviceListUpdate(object? sender, EventArgs args)
    {
        if (!CurrentDevice.Online || (_files.Length != 0 && CurrentDeviceIp == CurrentDevice.IpAddress)) 
            return;
        
        await RefreshFiles();
        await InvokeAsync(StateHasChanged);
    }

}
