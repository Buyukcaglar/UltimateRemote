using System.Net;
using System.Net.Sockets;
// ReSharper disable InconsistentNaming

namespace UltimateRemote.Platforms.iOS.Services;

public class LocalNetworkPermissionService
{
    private const int Port = 12345;
    private readonly List<Socket> _sockets = new List<Socket>();

    public void TriggerDialog()
    {
        var localIPAddresses = GetLocalIPv4Addresses();
        foreach (var ipAddress in localIPAddresses)
        {
            System.Diagnostics.Debug.WriteLine($"ipAddress: {ipAddress}");
            try
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.Connect(ipAddress, Port);
                _sockets.Add(socket);

                // Sending a dummy message to trigger the local network permission dialog on iOS
                byte[] message = System.Text.Encoding.UTF8.GetBytes("Hello Cruel World!");
                socket.Send(message);

                // You might need to handle the socket state and possibly close it after use
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect or send data: {ex.Message}");
            }
            finally
            {
                Cleanup();
            }
        }
    }

    // This returns all IP Addresses on all Interfaces
    private IEnumerable<string> GetLocalIPv4Addresses()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        return host.AddressList
            .Where(address => address.AddressFamily == AddressFamily.InterNetwork)
            .Select(address => address.ToString());
    }

    // Make sure to clean up resources when done
    public void Cleanup()
    {
        foreach (var socket in _sockets)
        {
            socket?.Close();
        }
        _sockets.Clear();
    }
}
