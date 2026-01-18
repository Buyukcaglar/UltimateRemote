using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;

public abstract class BaseFileFunctionComponent : BaseComponent
{
    [Inject] private EventService EventService { get; set; } = default!;

    protected abstract FileTypeGroup[] AllowedFileTypeGroups { get; }

    protected DeviceLocation[] EnabledLocations = default!;

    protected override void OnInitialized()
    {
        EnabledLocations = PrefsMgr.EnabledDeviceLocations.ToArray();
        EventService.SignalDeviceListUpdateEvent -= OnDeviceListUpdate;
        EventService.SignalDeviceListUpdateEvent += OnDeviceListUpdate;
        base.OnInitialized();
    }

    protected string GetPath(string path) => $"{EnabledLocations.GetSelectedOrDefault()?.Path}{path}";

    private async void OnDeviceListUpdate(object? sender, EventArgs args)
    {
        EnabledLocations = PrefsMgr.EnabledDeviceLocations.ToArray();
        await InvokeAsync(StateHasChanged);
    }

}
