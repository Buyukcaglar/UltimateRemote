using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UltimateRemote.Interfaces;

namespace UltimateRemote.Platforms.iOS.Services;

public sealed class IpAddressService : IIpAddressService
{
    public string? GetIpAddress()
        => GetIPv4AddressForInterface("en0");

    public IEnumerable<string> GetLocalIPv4Addresses()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        return host.AddressList
            .Where(address => address.AddressFamily == AddressFamily.InterNetwork)
            .Select(address => address.ToString());
    }

    public string? GetIPv4AddressForInterface(string interfaceName)
    {
        var networkInterface = NetworkInterface.GetAllNetworkInterfaces()
            .FirstOrDefault(ni => ni.Name == interfaceName);

        if (networkInterface == null)
        {
            Console.WriteLine($"No network interface found with name: {interfaceName}");
            return null;
        }

        var ipProperties = networkInterface.GetIPProperties();
        var ipv4Address = ipProperties.UnicastAddresses
            .FirstOrDefault(ua => ua.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

        return ipv4Address?.Address.ToString();
    }

    public void TriggerLocalNetworkPermissionDialog()
    {
        const int port = 12345;

        var socket = default(Socket?);
        var en0IpAddress = GetIPv4AddressForInterface("en0");

        if (string.IsNullOrWhiteSpace(en0IpAddress))
            return;

        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Connect(en0IpAddress, port);

            // Sending a dummy message to trigger the local network permission dialog on iOS
            byte[] message = System.Text.Encoding.UTF8.GetBytes("Hello Cruel World!");
            socket.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect or send data: {ex.Message}");
        }
        finally
        {
            socket?.Close();
        }
    }

}