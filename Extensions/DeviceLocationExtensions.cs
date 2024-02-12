using UltimateRemote.Models;

namespace UltimateRemote.Extensions;
internal static class DeviceLocationExtensions
{
    public static string GetSelectedOrDefaultPath(this DeviceLocation[] locations)
        => GetSelectedOrDefault(locations)?.Path ?? string.Empty;

    public static DeviceLocation? GetSelectedOrDefault(this DeviceLocation[] locations)
        => locations.Length == 0 ? default(DeviceLocation) :
            locations.Any(location => location.Selected) ? locations.First(location => location.Selected) :
            locations.Any(location => location.Default) ? locations.First(location => location.Default) :
            locations.First();
}
