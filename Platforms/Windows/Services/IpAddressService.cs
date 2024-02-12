using System.Net;
using System.Net.Sockets;
using UltimateRemote.Interfaces;

namespace UltimateRemote.Platforms.Windows.Services;
public sealed class IpAddressService : IIpAddressService
{
    public string? GetIpAddress()
    => Dns.GetHostEntry(Dns.GetHostName())
        .AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?
        .ToString();
}
