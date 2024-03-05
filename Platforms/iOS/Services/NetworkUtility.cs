using System.Runtime.InteropServices;
using System.Text;

namespace UltimateRemote.Platforms.iOS.Services;

public static class NetworkUtility
{
    [DllImport("libc")]
    public static extern int getifaddrs(ref IntPtr ifap);

    [DllImport("libc")]
    public static extern void freeifaddrs(IntPtr ifa);

    public static void PrintAddresses()
    {
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
                                    salen: (uint)Marshal.ReadIntPtr(cursor, 24 + IntPtr.Size), 
                                    node: hostname, 
                                    nodelen: (uint)hostname.Length, 
                                    service: Array.Empty<byte>(), 
                                    servicelen: 0, 
                                    flags: NI_NUMERICHOST) == 0 && hostname[0] != 0)
                            {
                                addrStr = Encoding.ASCII.GetString(hostname);
                            }
                        }

                        Console.WriteLine($"{interfaceName} {addrStr}");
                        System.Diagnostics.Debug.WriteLine($"{interfaceName} {addrStr}");
                    }
                }
                finally
                {
                    freeifaddrs(addrList);
                }
            }
        }
    }

    private const int NI_NUMERICHOST = 2;
    private const int NI_MAXHOST = 1025;

    [DllImport("libc", EntryPoint = "getnameinfo")]
    private static extern int getnameinfo(IntPtr sa, uint salen, byte[] node, uint nodelen, byte[] service, uint servicelen, int flags);
}

