﻿using Blazored.Toast.Services;
using MonkeyCache.FileStore;
using UltimateRemote.Interfaces;
using UltimateRemote.Models;

namespace UltimateRemote.Services;
public sealed class DeviceManager
{
    public List<IUltimateDevice> Devices => _devices;
    
    public event EventHandler? DeviceListUpdatedEvent;
    
    private readonly List<IUltimateDevice> _devices;
    private readonly DeviceProvider _deviceProvider;
    private readonly IUltimateDevice _dummyDevice;

    public DeviceManager(DeviceProvider deviceProvider, [FromKeyedServices(ServiceKeys.DummyDevice)] IUltimateDevice dummyDevice)
    {
        _dummyDevice = dummyDevice;
        _deviceProvider = deviceProvider;
        var deviceInfos = Barrel.Current.Get<UltimateDeviceInfo[]>(CacheKeys.MyDevices) ??
                          Array.Empty<UltimateDeviceInfo>();
        _devices = _deviceProvider.GetUltimateDevices(deviceInfos, OnDeviceChanged).ToList();
    }

    private IUltimateDevice CurrentDevice =>
         _devices.FirstOrDefault(device => device.Current) ?? _devices.FirstOrDefault() ?? _dummyDevice;

    public IUltimateDevice GetCurrentDevice(IToastService toastService)
    {
        CurrentDevice.SetToastService(toastService);
        return CurrentDevice;
    }

    public IUltimateDevice? GetDevice(string ipAddress)
        => _devices.FirstOrDefault(device => device.IpAddress == ipAddress);

    public UltimateDeviceInfo[] GetRegisteredDeviceInfos()
        => [.. _devices.ToDeviceInfoList()];

    public void AddDevices(UltimateDeviceInfo[] devices)
    {
        foreach (var device in devices)
        {
            AddDevice(device.Name, device.IpAddress, device.Version, device.Type);
        }
    }

    public void AddDevice(string? name, string ipAddress, string version, UltimateDeviceType type)
    {
        name = string.IsNullOrWhiteSpace(name) ? Strings.Generic.DefaultDeviceName : name;

        var device = _devices.FirstOrDefault(device => device.IpAddress == ipAddress);
        var deviceInfoList = _devices.ToDeviceInfoList();
        var deviceInfo = deviceInfoList.FirstOrDefault(info => info.IpAddress == ipAddress);

        if (device == null)
        {
            var newDeviceInfo = new UltimateDeviceInfo
                { Name = name, IpAddress = ipAddress, Type = type, Version = version };
            
            deviceInfoList.Add(newDeviceInfo);

            // If adding very first device to list
            // automatically mark it as current selected device
            var newDevice = _deviceProvider.GetUltimateDevice(newDeviceInfo, setAsCurrent: _devices.Count == 0, OnDeviceChanged);
            
            _devices.Add(newDevice);
        }
        else
        {
            device.Name = name;
            device.Version = version;
            device.Type = type;

            if (deviceInfo != null)
            {
                deviceInfo.Name = name;
                deviceInfo.Version = version;
                deviceInfo.Type = type;
            }
        }

        PersistDeviceList(deviceInfoList);

        DeviceListUpdatedEvent?.Invoke(this, EventArgs.Empty);

    }

    public void RemoveDevice(string ipAddress)
    {
        var deviceInfoList = _devices.ToDeviceInfoList();
        var deviceInfo = deviceInfoList.FirstOrDefault(info => info.IpAddress == ipAddress);

        if (deviceInfo != null)
        {
            deviceInfoList.Remove(deviceInfo);
            Barrel.Current.Add(CacheKeys.MyDevices, deviceInfoList, TimeSpan.Zero);
        }

        var device = _devices.FirstOrDefault(device => device.IpAddress == ipAddress);
        
        if (device == null) 
            return;
        
        device.DeviceChangedEvent -= OnDeviceChanged;

        _devices.Remove(device);
        device.Dispose();
        
        DeviceListUpdatedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void SelectDevice(string ipAddress)
    {
        var selectedDevice = GetDevice(ipAddress);
        
        if (selectedDevice is not { Current: false }) 
            return;
        
        _devices.SetValue(device => device.UnSelectDevice());
        selectedDevice.SelectDevice();
    }

    /// <summary>
    /// Returns IUltimateDevice for Manual registering.
    /// DeviceChangedEventHandler purposefully not provided,
    /// since this object is just used for API Verison querying.
    /// </summary>
    /// <param name="deviceInfo">DeviceInfo of the requested device</param>
    /// <returns>IUltimateDevice</returns>
    public IUltimateDevice GetDevice(UltimateDeviceInfo deviceInfo)
        => _deviceProvider.GetUltimateDevice(deviceInfo, setAsCurrent: false, deviceChangedEventHandler: null);

    public bool DeviceNameExists(string deviceName)
        => _devices.Any(device => device.Name == deviceName);

    public void InvokeDeviceListUpdatedEvent()
        => DeviceListUpdatedEvent?.Invoke(this, EventArgs.Empty);
    private void OnDeviceChanged(object? sender, EventArgs eventArgs)
    {
        PersistDeviceList(_devices.ToDeviceInfoList());
        DeviceListUpdatedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void StopConnectivityCheck()
        => _devices.SetValue(device => device.StopConnectivityCheck());

    public void StartConnectivityCheck()
        => _devices.SetValue(device => device.StartConnectivityCheck());

    public void ChangeConnectivityCheckInterval(uint interval)
        => _devices.SetValue(device => device.SetHeartbeatInterval(interval));

    public void ChangeRequestTimeOut(uint interval)
        => _devices.SetValue(device => device.ChangeApiClientTimeout(interval));

    private static void PersistDeviceList(List<UltimateDeviceInfo> deviceInfoList)
        => Barrel.Current.Add(CacheKeys.MyDevices, deviceInfoList, TimeSpan.Zero);

}
