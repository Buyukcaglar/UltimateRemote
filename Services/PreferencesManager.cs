using MonkeyCache.FileStore;
using UltimateRemote.Models;

namespace UltimateRemote.Services;
public class PreferencesManager
{
    public UserPreferences UserPrefs { get; private set; } =
        Barrel.Current.Get<UserPreferences>(CacheKeys.Preferences) ?? new UserPreferences();

    private void SavePreferences() =>
        Barrel.Current.Add(CacheKeys.Preferences, UserPrefs, TimeSpan.Zero);

    public List<FileTypeGroup> FileTypeGroups =>
        UserPrefs.FileTypeGroups;

    public FileTypeGroup[] EnabledFileTypeGroups =>
        UserPrefs.FileTypeGroups.Where(fileTypeGroup => fileTypeGroup.Enabled).ToArray();

    public List<DeviceLocation> DeviceLocations =>
        UserPrefs.DeviceLocations;

    public List<DeviceLocation> EnabledDeviceLocations =>
        UserPrefs.DeviceLocations.Where(deviceLocation => deviceLocation.Enabled).ToList();

    public DeviceLocation? DefaultDeviceLocation =>
        UserPrefs.DeviceLocations.FirstOrDefault(
            deviceLocation => deviceLocation is { Enabled: true, Default: true });

    private DeviceLocation? GetDeviceLocation(string locationName) =>
        UserPrefs.DeviceLocations.FirstOrDefault(location => location.Name == locationName);

    public string[] EnabledFileExtensions =>
        EnabledFileTypeGroups.SelectMany(fileGroup => fileGroup.Extensions).Distinct().ToArray();

    public bool HistoryEnabled
        => UserPrefs.HistoryEnabled;

    public int HistorySize
        => (int)UserPrefs.HistorySize;

    public bool ApiV01CharLimitEnforcement
        => UserPrefs.ApiV01CharLimitEnforcement;

    public bool DisplayShortcutOptions
        => UserPrefs.DisplayShortcutOptions;

    public string HVSCArchiveLocation
        => UserPrefs.HVSCArchiveLocation;

    public string SongLengthDbFileLocation
        => UserPrefs.SongLengthDbFileLocation;

    public bool DisplayFilepathWhileHVSCPlay => 
        UserPrefs.DisplayFilepathWhileHVSCPlay;

    public void UpdateDefaultDeviceLocation(string locationName)
    {
        var location = GetDeviceLocation(locationName);
        if (location == null)
            return;
        UserPrefs.DeviceLocations.SetValue(updateMethod: deviceLocation => deviceLocation.Default = false);
        location.Default = true;
        SavePreferences();
    }

    public FileTypeGroup? GetFileTypeGroup(string name) =>
        FileTypeGroups.FirstOrDefault(fileTypeGroup => fileTypeGroup.Name == name);

    public uint PopUpTimeout =>
        UserPrefs.SnackDuration;

    public uint ConnectivityCheckInterval
        => UserPrefs.ConnectivityCheckInterval > 0 ? UserPrefs.ConnectivityCheckInterval : 5;

    public uint ApiClientTimeout
        => UserPrefs.ApiClientTimeout > 0 ? UserPrefs.ApiClientTimeout : 5;

    public void PersistPreferences()
        => this.SavePreferences();

    public void Reset()
    {
        Barrel.Current.Empty(CacheKeys.Preferences);
        UserPrefs = new UserPreferences();
    }
}
