namespace UltimateRemote.Interfaces;
public interface IIpAddressService
{
    string? GetIpAddress();

#if IOS
    IEnumerable<string> GetLocalIPv4Addresses();
    string? GetIPv4AddressForInterface(string interfaceName);
#endif

#if IOS || MACCATALYST
    void TriggerLocalNetworkPermissionDialog();
#endif
    
}
