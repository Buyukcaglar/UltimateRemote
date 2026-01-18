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
    private Func<UltimateDeviceInfo, string> _labelFunc = deviceInfo =>
        $"{deviceInfo.Name} - {deviceInfo.IpAddress} {GetShortDeviceName(deviceInfo.Type)} v{deviceInfo.Version}";

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

    private static string GetShortDeviceName(UltimateDeviceType deviceType)
        => deviceType switch
        {
            UltimateDeviceType.None => "N/A",
            UltimateDeviceType.Ultimate1541 => "UII",
            UltimateDeviceType.UltimateC64 => "U64",
            _ => "???"
        };
}
