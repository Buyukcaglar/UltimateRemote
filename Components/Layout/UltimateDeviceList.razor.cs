using UltimateRemote.Models;

namespace UltimateRemote.Components.Layout;
public sealed partial class UltimateDeviceList
{
    private UltimateDeviceInfo[] RegisteredDevices => DeviceManager.GetRegisteredDeviceInfos();
    private UltimateDeviceInfo? _selectedDevice => RegisteredDevices.FirstOrDefault(device => device.Current);

    // Ugly hack ... :(
    private UltimateDeviceInfo? SelectedDevice
    {
        get => _selectedDevice;
        set{}

    }
    private readonly Func<UltimateDeviceInfo, string> _labelFunc = deviceInfo =>
        $"{deviceInfo.Name} - {deviceInfo.IpAddress}";

    private string DropdownLabel => null != _selectedDevice ? _labelFunc(_selectedDevice) : "Select Device";

    private string OnlineIndicatorCss => null == _selectedDevice ? "" :
        _selectedDevice.Online ? "indicate-online-slim" : "indicate-offline-slim";

    protected override void OnInitialized()
    {
        DeviceManager.DeviceListUpdatedEvent -= OnDeviceListUpdated;
        DeviceManager.DeviceListUpdatedEvent += OnDeviceListUpdated;

        base.OnInitialized();
    }

    private Task OnDeviceSelect(UltimateDeviceInfo selectedDevice)
    {
        return DeviceManager.SelectDevice(selectedDevice.IpAddress);
    }

    private async void OnDeviceListUpdated(object? sender, EventArgs eventArgs)
        => await base.InvokeAsync(StateHasChanged);
}
