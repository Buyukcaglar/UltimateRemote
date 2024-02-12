using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class NonFloppyDrives : BaseComponent
{
    private KeyValuePair<string, DriveInfoResponse>[]? _driveInfos;

    protected override async Task OnInitializedAsync()
    {
        DeviceManager.DeviceListUpdatedEvent -= OnDeviceListUpdated;
        DeviceManager.DeviceListUpdatedEvent += OnDeviceListUpdated;

        _driveInfos = await GetNonFloppyDrives();
        await base.OnInitializedAsync();
    }

    private async Task<KeyValuePair<string, DriveInfoResponse>[]> GetNonFloppyDrives()
    {
        if (CurrentDevice.Type == UltimateDeviceType.None)
        {
            return Array.Empty<KeyValuePair<string, DriveInfoResponse>>();
        }

        var drivesResponse = await CurrentDevice.GetDrives();

        if (drivesResponse?.Drives == null || drivesResponse.Drives.Length == 0)
        {
            return Array.Empty<KeyValuePair<string, DriveInfoResponse>>();
        }

        return drivesResponse.Drives
            .SelectMany(dictionary => dictionary.ToArray())
            .Where(keyValuePair => !Constants.FloppyDrives.DriveTypes.Contains(keyValuePair.Value.Type))
            .ToArray();
    }


    private async void OnDeviceListUpdated(object? sender, EventArgs eventArgs)
    {
        _driveInfos = await GetNonFloppyDrives();
        await InvokeAsync(StateHasChanged);
    }

}
