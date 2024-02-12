using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Services;
public sealed class DeviceScanner(HttpClient httpClient)
{
    public event EventHandler<IpScanResult>? IpScanCompletedEvent;

    public async Task<DeviceScanResult> ScanDevices(string ipAddress)
    {
        ipAddress = IPAddress.Parse(ipAddress).ToString();

        var ipParts = ipAddress.Split('.', StringSplitOptions.RemoveEmptyEntries);
        var partialIp = string.Join(".", ipParts[..^1]);
        var rnd = new Random(DateTime.Now.Millisecond);
        var localNetAccess = await CheckLocalNetworkAccess($"{partialIp}.{rnd.Next(1, 255)}");

        if (!localNetAccess)
        {
            return new DeviceScanResult(Array.Empty<IpScanResult>())
            {
                Message = Strings.ErrorMessages.CouldNotAccessLocalNetwork
            };
        }

        var scanTasks = Enumerable.Range(0, 255).Select(rng => ScanIp2($"{partialIp}.{rng}"));

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
        var retVal = new IpScanResult(ip);
        var response = default(VersionResponse?);

        try
        {
            var requestUrl = ApiUrls.Version(ip);
            response = await httpClient.GetFromJsonAsync<VersionResponse?>(requestUrl);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }

        if (string.IsNullOrWhiteSpace(response?.Version))
            return retVal;

        retVal.Version = response?.Version;

        return retVal;
    }

    private async Task<IpScanResult> ScanIp2(string ip)
    {
        var retVal = new IpScanResult(ip);
        var response = default(VersionResponse?);
        try
        {
            //var requestUrl = $"http://{ip}/v1/version";
            var requestUrl = ApiUrls.Version(ip);
            var httpResponse = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseContentRead);
            if (httpResponse is { IsSuccessStatusCode: true })
            {
                await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                response = await JsonSerializer.DeserializeAsync<VersionResponse>(responseStream);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }

        if (!string.IsNullOrWhiteSpace(response?.Version))
            retVal.Version = response?.Version;

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
