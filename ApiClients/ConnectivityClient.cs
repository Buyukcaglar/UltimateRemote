using System.Net.Http.Json;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.ApiClients;
public sealed partial class UltimateDevice
{
    private async Task CheckConnectivity()
    {
        var updated = false;

        try
        {
            var versionInfo = await _heartBeatHttpClient.GetFromJsonAsync<VersionResponse>(ApiUrls.Version(IpAddress));
            if (!string.IsNullOrWhiteSpace(versionInfo?.Version))
            {
                if (Version != versionInfo.Version)
                {
                    Version = versionInfo.Version;
                    updated = true;
                }

                if (!Online)
                {
                    Online = true;
                    updated = true;
                }
            }
        }
        catch
        {
            if (Online)
            {
                Online = false;
                updated = true;
            }
        }

        if (updated)
        {
            DeviceChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
