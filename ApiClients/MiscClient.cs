using System.Net.Http.Json;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.ApiClients;
public sealed partial class UltimateDevice
{
    public async Task<VersionResponse?> QueryVersion()
    {
        var versionResponse = default(VersionResponse?);

        try
        {
            versionResponse = await _heartBeatHttpClient.GetFromJsonAsync<VersionResponse>(ApiUrls.Version(IpAddress));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }

        return versionResponse;
    }
        
}
