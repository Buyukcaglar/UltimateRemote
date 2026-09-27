using System.Net;
using System.Text.Json;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Services;
public sealed class DeviceScanner(HttpClient httpClient)
{
    public event EventHandler<IpScanResult>? IpScanCompletedEvent;

    public async Task<DeviceScanResult>  ScanDevices(string ipAddress)
    {
        ipAddress = IPAddress.Parse(ipAddress).ToString();

        var ipParts = ipAddress.Split('.', StringSplitOptions.RemoveEmptyEntries);
        var partialIp = string.Join(".", ipParts[..^1]);
        var rnd = new Random(DateTime.Now.Millisecond);

#if !ANDROID
        var localNetAccess = await CheckLocalNetworkAccess($"{partialIp}.{rnd.Next(1, 255)}");

        if (!localNetAccess)
        {
            return new DeviceScanResult(Array.Empty<IpScanResult>())
            {
                Message = Strings.ErrorMessages.CouldNotAccessLocalNetwork
            };
        }
#endif
        var scanTasks = Enumerable.Range(0, 255).Select(rng => ScanIp($"{partialIp}.{rng}"));

        var scanResults = await Task.WhenAll(scanTasks).ConfigureAwait(false);

        if (scanResults.Any(result => result.Found))
        {
            var foundDevices = scanResults.Where(result => result.Found).ToArray();
            return new DeviceScanResult(foundDevices);
        }

        return new DeviceScanResult(Array.Empty<IpScanResult>())
        {
            Message = Strings.ErrorMessages.NoDeviceFound(partialIp)
        };
    }

    private async Task<IpScanResult> ScanIp(string ip)
    {
        var retVal = IpScanResult.Empty(ip);
        var apiVersionResponse = default(VersionResponse?);
        var deviceInfoResponse = default(InfoResponse?);

        try
        {
            var apiVersionHttpResponse = await httpClient.GetAsync(ApiUrls.Version(ip), HttpCompletionOption.ResponseContentRead);
            var deviceInfoHttpResponse = await httpClient.GetAsync(ApiUrls.Info(ip), HttpCompletionOption.ResponseContentRead);

            if (apiVersionHttpResponse is { IsSuccessStatusCode: true })
            {
                await using var responseStream = await apiVersionHttpResponse.Content.ReadAsStreamAsync();
                apiVersionResponse = await JsonSerializer.DeserializeAsync<VersionResponse>(responseStream);
            }

            if (deviceInfoHttpResponse is { IsSuccessStatusCode: true })
            {
                await using var responseStream = await deviceInfoHttpResponse.Content.ReadAsStreamAsync();
                deviceInfoResponse = await JsonSerializer.DeserializeAsync<InfoResponse>(responseStream);
            }

        }
        catch { }

        if (!string.IsNullOrWhiteSpace(apiVersionResponse?.Version))
            retVal.ApiVersion = apiVersionResponse.Version;

        if (null != deviceInfoResponse)
        {
            retVal.DeviceType = deviceInfoResponse.DeviceType;
            retVal.ProductName = deviceInfoResponse.Product;
            retVal.FirmwareVersion = deviceInfoResponse.FirmwareVersion;
            retVal.FpgaVersion = deviceInfoResponse.FpgaVersion;
            retVal.CoreVersion = deviceInfoResponse.CoreVersion;
            retVal.Hostname = deviceInfoResponse.Hostname;
            retVal.UniqueId = deviceInfoResponse.UniqueId;
        }

        IpScanCompletedEvent?.Invoke(this, retVal);

        return retVal;
    }

    private async Task<bool> CheckLocalNetworkAccess(string ip)
    {
        var retVal = false;

        try
        {
            var requestUrl = ApiUrls.Version(ip);
            var httpResponse = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseContentRead);
            retVal = true;
        }
        catch (HttpRequestException httpEx)
        {
            if (httpEx.StatusCode != null)
                retVal = true;

            // The method of determining if device is able to access local network is abysmal .... :(
            if (!string.IsNullOrWhiteSpace(httpEx.InnerException?.Message))
            {
                // iOs
                if (httpEx.InnerException.Message.Contains("Code=-1004"))
                    retVal = true;

                if (httpEx.InnerException.Message.Contains("Code=-1009"))
                    retVal = false;

                // Android (No permission exception means good to go!) ...
                if (httpEx.InnerException.Message.Contains($"Failed to connect to /{ip}:80"))
                    retVal = true;
            }

            System.Diagnostics.Debug.WriteLine($"CheckLocalNetworkAccess HttpRequestException:\r\n {httpEx}");
        }
        catch (TaskCanceledException taskCanceledException)
        {
            // Timed-out is ok also ...
            retVal = true;
            System.Diagnostics.Debug.WriteLine($"CheckLocalNetworkAccess TaskCanceledException:\r\n {taskCanceledException}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"CheckLocalNetworkAccess Exception:\r\n {ex}");
        }

        return retVal;
    }

}
