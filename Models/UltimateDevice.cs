using Blazored.Toast.Services;
using UltimateRemote.Interfaces;

// ReSharper disable once CheckNamespace
namespace UltimateRemote.ApiClients;

public sealed partial class UltimateDevice(IHttpClientFactory httpClientFactory, FtpClient ftpClient) : IUltimateDevice, IDisposable
{
    public event EventHandler? DeviceChangedEvent;

    private IToastService? _toastService;

    private readonly HttpClient _heartBeatHttpClient = httpClientFactory.CreateClient(ServiceKeys.HttpClientHeartBeat);
    private HttpClient _httpClient = httpClientFactory.CreateClient(ServiceKeys.HttpClientApi);
    private RegisteredWaitHandle? _waitHandle;
    private uint _heartBeatInterval;

    private static uint _apiClientTimeOut;
    private const uint LongRunningOperationHttpTimeout = 300;

    public bool Current { get; private set; }

    public string Name { get; set; } = default!;

    public string IpAddress { get; set; } = default!;

    public string Version { get; set; } = default!;

    public UltimateDeviceType Type { get; set; }

    public bool Online { get; private set; }

    public void SelectDevice()
    {
        if (!Current)
        {
            Current = true;
            DeviceChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    public void UnSelectDevice() => Current = false;

    public void SetToastService(IToastService toastService)
        => _toastService ??= toastService;

    public void StopConnectivityCheck()
        => _waitHandle?.Unregister(null);

    public void StartConnectivityCheck()
        => _waitHandle = ThreadPoolHelper.RegisterBgTask(CheckConnectivity, TimeSpan.FromSeconds(_heartBeatInterval));

    public void SetHeartbeatInterval(uint interval)
    {
        if (interval == 0)
        {
            StopConnectivityCheck();
            return;
        }

        _heartBeatInterval = interval;
        StopConnectivityCheck();
        StartConnectivityCheck();
    }

    public void SetDefaultApiClientTimeout(uint timeOutInSeconds)
    {
        _apiClientTimeOut = timeOutInSeconds;
        _httpClient.Timeout = TimeSpan.FromSeconds(_apiClientTimeOut);
    }

    public void ChangeApiClientTimeout(uint timeOutInSeconds)
    {
        _apiClientTimeOut = timeOutInSeconds;
        _httpClient = httpClientFactory.CreateClient(ServiceKeys.HttpClientApi);
        _httpClient.Timeout = TimeSpan.FromSeconds(_apiClientTimeOut);
    }

    public void Dispose()
    {
        StopConnectivityCheck();
        _heartBeatHttpClient.Dispose();
        _httpClient.Dispose();
    }
    
}
