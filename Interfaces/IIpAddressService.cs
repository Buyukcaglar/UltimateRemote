namespace UltimateRemote.Interfaces;
public interface IIpAddressService
{
    string? GetIpAddress();

#if IOS ||MACCATALYST
    
    IEnumerable<string> GetLocalIPv4Addresses();
    
    string? GetIPv4AddressForInterface(string interfaceName);

    void TriggerLocalNetworkPermissionDialog();

#endif
    
}
