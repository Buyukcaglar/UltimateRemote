using Microsoft.AspNetCore.Components;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Components.Shared.Functions.Drives;
public sealed partial class Drive : BaseComponent
{
    [Parameter, EditorRequired] public int BusId { get; set; }

    private KeyValuePair<string, DriveInfoResponse>? DriveInfo { get; set; }

    protected override async Task OnInitializedAsync()
    {
        DeviceManager.DeviceListUpdatedEvent -= OnDeviceListUpdated;
        DeviceManager.DeviceListUpdatedEvent += OnDeviceListUpdated;
        
        await GetDriveInfo(false);
        
        await base.OnInitializedAsync();
    }

    private async Task GetDriveInfo(bool invokeStateChange)
    {
        if (CurrentDevice.Type == UltimateDeviceType.None)
        {
            DriveInfo = null;
            return;
        }

        var drivesResponse = await CurrentDevice.GetDrives();

        if (drivesResponse?.Drives == null || drivesResponse.Drives.Length == 0)
        {
            DriveInfo = null;
            return;
        }
        

        DriveInfo = drivesResponse.Drives!
            .SelectMany(dictionary => dictionary.ToArray())
            .FirstOrDefault(keyValuePair => keyValuePair.Value.BusId == BusId);
        
        if (invokeStateChange)
            await InvokeAsync(StateHasChanged);
    }

    private async void OnDeviceListUpdated(object? sender, EventArgs eventArgs)
    {
        await GetDriveInfo(false);
    }
}
