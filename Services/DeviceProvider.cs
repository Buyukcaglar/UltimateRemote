using UltimateRemote.Interfaces;
using UltimateRemote.Models;

namespace UltimateRemote.Services;
public sealed class DeviceProvider(IServiceProvider serviceProvider)
{
    public IUltimateDevice GetUltimateDevice(UltimateDeviceInfo deviceInfo, bool setAsCurrent, EventHandler? deviceChangedEventHandler)
    {
        var device = serviceProvider.GetRequiredService<UltimateDevice>();
        var prefsMgr = serviceProvider.GetRequiredService<PreferencesManager>();

        device.Name = deviceInfo.Name;
        device.IpAddress = deviceInfo.IpAddress;
        device.Type = deviceInfo.Type;
        device.Version = deviceInfo.Version;
        device.SetHeartbeatInterval(prefsMgr.ConnectivityCheckInterval);
        device.SetDefaultApiClientTimeout(prefsMgr.ApiClientTimeout);
        
        // There should exist more elegant way doing this ...
        if(!prefsMgr.UserPrefs.ConnectivityCheckEnabled)
            device.StopConnectivityCheck();

        // It is important to set this properly here .....
        if(setAsCurrent)
            device.SelectDevice();

        if(deviceChangedEventHandler != null)
            device.DeviceChangedEvent += deviceChangedEventHandler;

        return device;
    }

    public IUltimateDevice[] GetUltimateDevices(UltimateDeviceInfo[] deviceInfos, EventHandler? deviceChangedEventHandler)
        => deviceInfos.Select(info => GetUltimateDevice(info, setAsCurrent: info.Current, deviceChangedEventHandler)).ToArray();
}
