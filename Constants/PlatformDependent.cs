namespace UltimateRemote.Constants;

internal static class PlatformDependent
{
    private static readonly List<DevicePlatform> MobilePlatforms =
        new List<DevicePlatform>(new[] { DevicePlatform.Android, DevicePlatform.iOS });

    private static readonly List<DevicePlatform> ApplePlatforms =
        new List<DevicePlatform>(new[] { DevicePlatform.iOS, DevicePlatform.MacCatalyst, DevicePlatform.macOS, DevicePlatform.tvOS, DevicePlatform.watchOS,  });

    private static readonly List<DevicePlatform> Computers =
        new List<DevicePlatform>(new[] { DevicePlatform.WinUI, DevicePlatform.MacCatalyst, DevicePlatform.macOS });

    public static bool IsMobile => MobilePlatforms.Contains(DeviceInfo.Current.Platform);

    public static bool IsApple => ApplePlatforms.Contains(DeviceInfo.Current.Platform);

    public static string DeviceName
        => Computers.Contains(DeviceInfo.Current.Platform) ? "computer"
            : DeviceInfo.Current.Platform == DevicePlatform.iOS ? "iOs device"
            : DeviceInfo.Current.Platform == DevicePlatform.Android ? "Android device"
            : "device";

    public static string ClickTap => IsMobile ? "tap" : "click";

    public static string ClickingTapping => IsMobile ? "tapping" : "clicking";

}
