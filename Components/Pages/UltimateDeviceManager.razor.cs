using Microsoft.AspNetCore.Components;
using System.Net;
using UltimateRemote.Components.Shared;
using UltimateRemote.Enums;
using UltimateRemote.Interfaces;
using UltimateRemote.Models;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace UltimateRemote.Components.Pages;

[Route(Blazor.RouteTemplates.DeviceManager)]
public sealed partial class UltimateDeviceManager : BaseComponent
{
    [Inject] private DeviceScanner DeviceScanner { get; set; } = default!;

    [Inject] private IIpAddressService IpAddressService { get; set; } = default!;

    private int _scannedSoFar;
    private int _found;
    private const int TotalScan = 256;
    private UltimateDeviceInfo[]? _devicesToBeRegistered;
    private readonly UltimateDeviceType[] _deviceTypes 
        = UltimateDeviceType.Ultimate1541.GetEnumValues()[1..];

    private UltimateDeviceInfo _manualRegisterDevice = new ();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
#if IOS
            var status = await Permissions.RequestAsync<LocalNetworkPermission>();
            if (status != PermissionStatus.Granted)
            {
                DisplayWarningToast(Strings.ErrorMessages.CouldNotAccessLocalNetwork, title: Strings.DeviceManager.ToastTitlePermissionNotGranted);
            }
#endif
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    protected override void OnInitialized()
    {
        SetUpEvents();
        base.OnInitialized();
    }

    private void SetUpEvents()
    {
        DeviceScanner.IpScanCompletedEvent -= OnIpScanCompleted;
        DeviceScanner.IpScanCompletedEvent += OnIpScanCompleted;

        DeviceManager.DeviceListUpdatedEvent -= OnDeviceListUpdated;
        DeviceManager.DeviceListUpdatedEvent += OnDeviceListUpdated;

#if MACCATALYST
        IpAddressService.TriggerLocalNetworkPermissionDialog();
#endif

    }

    private Task Scan()
    {
        ResetScanResults();

        var deviceIp = IpAddressService.GetIpAddress();
        
        if (!string.IsNullOrWhiteSpace(deviceIp)) 
            return ScanIp(deviceIp);
        
        DisplayErrorToast(message: Strings.ErrorMessages.CouldNotObtainLocalIpAddress, 
            title: Strings.DeviceManager.ToastTitleCouldNotObtainLocalIp);
        return Task.CompletedTask;
    }

    private async Task ScanIp(string? ipAddress)
    {
        ResetScanResults();
        
        // <Not neccessary>
        if (string.IsNullOrWhiteSpace(ipAddress))
            return;
        
        ipAddress = ipAddress.Trim();
        // </Not neccessary>

        var scanResult = await ExecuteUiBlockingTask(task: DeviceScanner.ScanDevices(ipAddress),
            blockingMessage: Strings.DeviceManager.ScanningDevices(ipAddress));
        
        if (scanResult is not { Found: true })
        {
            var message = !string.IsNullOrWhiteSpace(scanResult?.Message)
                ? scanResult.Message
                : Strings.DeviceManager.ToastMessageDeviceScanResultEmpty;
            DisplayWarningToast(message, title: Strings.DeviceManager.ToastTitleDeviceScanFail);
            return;
        }

        _devicesToBeRegistered = scanResult!.DeviceAddresses.Select(result => new UltimateDeviceInfo
        {
            IpAddress = result.IpAddress,
            Version = !string.IsNullOrWhiteSpace(result.Version) ? result.Version : "0.0"
        }).ToArray();
        
        await ScrollToElementWithId("deviceRegistry");
    }

    private async Task ManualRegister()
    {
        if (!ValidateIpAddress(_manualRegisterDevice.IpAddress))
        {
            DisplayWarningToast(Strings.DeviceManager.EnterValidIpAddress, 
                Strings.DeviceManager.InvalidIpAddress);
            return;
        }

        if (_manualRegisterDevice.Type == UltimateDeviceType.None)
        {
            DisplayWarningToast(Strings.DeviceManager.ToastMsgSelectDeviceType, 
                Strings.DeviceManager.ToastTitleSelectDeviceType);
            return;
        }

        var tempDevice = DeviceManager.GetDevice(_manualRegisterDevice);
        var apiResult = await ExecuteUiBlockingTask(tempDevice.QueryVersion(),
            Strings.DeviceManager.AccessingDevice(tempDevice.IpAddress));
        
        tempDevice.Dispose();
        
        if (string.IsNullOrWhiteSpace(apiResult?.Version))
        {
            DisplayWarningToast(Strings.DeviceManager.ToastMsgUnableToQueryDevice(_manualRegisterDevice.IpAddress),
                Strings.DeviceManager.ToastTitleUnableToQueryDevice);
            return;
        }

        var version = apiResult.Version;
        
        DeviceManager.AddDevice(_manualRegisterDevice.Name, _manualRegisterDevice.IpAddress, version,
            _manualRegisterDevice.Type);

        _manualRegisterDevice = new UltimateDeviceInfo();
    }

    private void ResetScanResults()
    {
        _scannedSoFar = 0;
        _found = 0;
        _devicesToBeRegistered = null;
    }

    private bool ValidateIpAddress(string? ipAddress)
        => !string.IsNullOrWhiteSpace(ipAddress) &&
           System.Text.RegularExpressions.Regex.IsMatch(input: ipAddress, pattern: RegexPatterns.ValidateIp) &&
           IPAddress.TryParse(ipAddress, out _);

    private async void OnIpScanCompleted(object? sender, IpScanResult result)
    {
        _scannedSoFar++;

        if (result.Found)
            _found++;

        await UpdateBlockPageMessage(Strings.DeviceManager.ScanStatus(_scannedSoFar, TotalScan, _found));

        await InvokeAsync(StateHasChanged);
    }

    private Task RegisterDevices()
    {
        if (null != _devicesToBeRegistered)
        {
            if (_devicesToBeRegistered.Any(device => device.Type == UltimateDeviceType.None))
            {
                DisplayWarningToast(Strings.DeviceManager.ToastMsgSelectDeviceType, 
                    Strings.DeviceManager.ToastTitleSelectDeviceType);
                return Task.CompletedTask;
            }

            DeviceManager.AddDevices(_devicesToBeRegistered);
        }
            
        ResetScanResults();
        return Task.CompletedTask;
    }

    private async void OnDeviceListUpdated(object? sender, EventArgs eventArgs)
    {
        await InvokeAsync(StateHasChanged);
    }

    private Task RemoveDevice(string ipAddress)
    {
        DeviceManager.RemoveDevice(ipAddress);
        return Task.CompletedTask;
    }

    

}
