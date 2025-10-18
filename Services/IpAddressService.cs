using System.Net;
using System.Net.Sockets;

namespace UltimateRemote.Services;

public sealed class IpAddressService
{
    public string? GetIpAddress()
    => Dns.GetHostEntry(Dns.GetHostName())
        .AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork & !x.ToString().Contains("127.0.0.1"))?
        .ToString();

    public void TriggerLocalNetworkPermissionDialog()
    {
        const int port = 12345;

        var socket = default(Socket?);
        var en0IpAddress = GetIpAddress();

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
