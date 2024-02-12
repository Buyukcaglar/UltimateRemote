using System.Net;
using System.Net.Sockets;
using UltimateRemote.Interfaces;

namespace UltimateRemote.Platforms.Android.Services;

public sealed class IpAddressService : IIpAddressService
{
    // ChatGPT converted the following code
    // https://gist.github.com/khannedy/29d1de0321e03668fcb1 (public static String getIPAddress(boolean useIPv4)) to C#
    public string GetIpAddress()
    {
        bool useIPv4 = true;
        try
        {
            var interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (var networkInterface in interfaces)
            {
                var ipAddresses = networkInterface.GetIPProperties().UnicastAddresses.Select(addr => addr.Address);
                foreach (var ipAddress in ipAddresses)
                {
                    if (!IPAddress.IsLoopback(ipAddress))
                    {
                        var ipAddressStr = ipAddress.ToString().ToUpper();
                        var isIPv4 = ipAddress.AddressFamily == AddressFamily.InterNetwork;

                        if (useIPv4)
                        {
                            if (isIPv4)
                                return ipAddressStr;
                        }
                        else
                        {
                            if (!isIPv4)
                            {
                                int delimiter = ipAddressStr.IndexOf('%'); // drop ip6 port suffix
                                return delimiter < 0 ? ipAddressStr : ipAddressStr.Substring(0, delimiter);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex) { } // for now eat exceptions
        return "";
    }


}
