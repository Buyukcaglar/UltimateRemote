using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using UltimateRemote.Interfaces;

namespace UltimateRemote.Platforms.MacCatalyst.Services;

public sealed class IpAddressService : IIpAddressService
{
    private const int NI_NUMERICHOST = 2;
    private const int NI_MAXHOST = 1025;

    //[StructLayout(LayoutKind.Sequential)]
    //public struct ifaddrs
    //{
    //    public IntPtr ifa_next;
    //    public string ifa_name;
    //    public uint ifa_flags;
    //    public IntPtr ifa_addr;
    //    public IntPtr ifa_netmask;
    //    public IntPtr ifa_dstaddr;
    //    public IntPtr ifa_data;
    //}

    /*
    [DllImport("libc", EntryPoint = "getnameinfo")]
    private static extern int getnameinfo(IntPtr sa, uint salen, byte[] node, uint nodelen, byte[] service, uint servicelen, int flags);

    [DllImport("libc")]
    public static extern int getifaddrs(ref IntPtr ifap);

    [DllImport("libc")]
    public static extern void freeifaddrs(IntPtr ifa);

    public string? GetIpAddress()
    {
        var retVal = default(string?);

        IntPtr addrList = IntPtr.Zero;

        if (getifaddrs(ref addrList) == 0)
        {
            if (addrList != IntPtr.Zero)
            {
                try
                {
                    for (IntPtr cursor = addrList; cursor != IntPtr.Zero; cursor = Marshal.ReadIntPtr(cursor))
                    {
                        var interfaceName = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(cursor, 8));
                        var addrStr = "?";

                        var hostname = new byte[NI_MAXHOST];
                        if (Marshal.ReadIntPtr(cursor, 24) != IntPtr.Zero)
                        {
                            if (getnameinfo(sa: Marshal.ReadIntPtr(cursor, ofs: 24),
                                    salen: (uint)Marshal.ReadIntPtr(cursor, ofs: 24 + IntPtr.Size),
                                    node: hostname,
                                    nodelen: (uint)hostname.Length,
                                    service: Array.Empty<byte>(),
                                    servicelen: 0,
                                    flags: NI_NUMERICHOST) == 0 && hostname[0] != 0)
                            {
                                addrStr = Encoding.ASCII.GetString(hostname);
                                if (interfaceName == "en0")
                                    retVal = addrStr;
                            }
                        }
                    }
                }
                finally
                {
                    freeifaddrs(addrList);
                }
            }
        }

        return !string.IsNullOrWhiteSpace(retVal)
            ? string.Concat(retVal.Where(c => c == '.' || char.IsDigit(c)))
            : null;
    }
    */

    public string? GetIpAddress()
        => Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork && x.ToString() != "127.0.0.1")?
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