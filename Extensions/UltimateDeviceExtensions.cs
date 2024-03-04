using System.Text.Json;
using UltimateRemote.Interfaces;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Extensions;

internal static class UltimateDeviceExtensions
{
    public static UltimateDeviceInfo ToDeviceInfo(this IUltimateDevice device)
        => new UltimateDeviceInfo()
        {
            Current = device.Current,
            Online = device.Online,
            Name = device.Name,
            IpAddress = device.IpAddress,
            Version = device.Version,
            Type = device.Type,
        };

    public static List<UltimateDeviceInfo> ToDeviceInfoList(this List<IUltimateDevice> devices)
        => devices.Select(ToDeviceInfo).ToList();

    #region Device Config Extensions
    // Bugged (returns 404)
    private static readonly string[] ExcludedSections = new[] { "UltiDOS: Allow SetDate" };

    private static Dictionary<string, (int MaxInputLen, Func<string?, bool> Validator)> _textInputSettings =
        new(new[]
        {
            new KeyValuePair<string, (int MaxInputLen, Func<string?, bool> Validator)>("Static IP", (15, Validators.IpValidator)),
            new KeyValuePair<string, (int MaxInputLen, Func<string?, bool> Validator)>("Static Netmask", (15, Validators.IpValidator)),
            new KeyValuePair<string, (int MaxInputLen, Func<string?, bool> Validator)>("Static Gateway", (15, Validators.IpValidator)),
            new KeyValuePair<string, (int MaxInputLen, Func<string?, bool> Validator)>("Listening Port", (5, Validators.PortValidator)),
        });

    // There has to be a much much more elegant way of handling
    // dynamic deserialization of Configuration responses from Ultimate API ...
    // No custom JsonSerializer is not the answer! It quickly became as ugly as the following sh.t!
    public static async Task<List<IConfigCategoryItemResponse>> GetConfigSectionItems(this IUltimateDevice currentDevice,
        string categoryName, ConfigCategoryItem[] configCategoryItems)
    {
        var retVal = new List<IConfigCategoryItemResponse>();

        foreach (var configCategoryItem in configCategoryItems.Where(item => !ExcludedSections.Contains(item.Name)))
        {
            ConfigItemResponse? apiResponse;
            switch (configCategoryItem.ValueKind)
            {
                case JsonValueKind.String:
                    apiResponse = await currentDevice.GetConfigCategorySection<ConfigItemResponse>(categoryName, configCategoryItem.Name);
                    var itemResponseForString = apiResponse?.GetValue<Dictionary<string, ConfigCategoryItemResponse<string>>>(categoryName);
                    var itemResponse = itemResponseForString?.Values.FirstOrDefault();
                    if (null != itemResponse)
                    {
                        itemResponse.ValueKind = configCategoryItem.ValueKind;
                        itemResponse.Section = configCategoryItem.Name;
                        if (_textInputSettings.TryGetValue(configCategoryItem.Name, out var inputSettings))
                        {
                            itemResponse.TextInputMaxLen = inputSettings.MaxInputLen;
                            itemResponse.FuncValidator = inputSettings.Validator;
                        }
                        retVal.Add(itemResponse);
                    }
                    break;
                case JsonValueKind.Number:
                    apiResponse = await currentDevice.GetConfigCategorySection<ConfigItemResponse>(categoryName, configCategoryItem.Name);
                    var itemResponseNumber = apiResponse?.GetValue<Dictionary<string, ConfigCategoryItemResponse<int>>>(categoryName);
                    var itemResponse2 = itemResponseNumber?.Values.FirstOrDefault();
                    if (null != itemResponse2)
                    {
                        itemResponse2.ValueKind = configCategoryItem.ValueKind;
                        itemResponse2.Section = configCategoryItem.Name;
                        retVal.Add(itemResponse2);
                    }
                    break;
                default:
                    apiResponse = await currentDevice.GetConfigCategorySection<ConfigItemResponse>(categoryName, configCategoryItem.Name);
                    var itemResponseObject = apiResponse?.GetValue<Dictionary<string, ConfigCategoryItemResponse<object>>>(categoryName);
                    var itemResponse3 = itemResponseObject?.Values.FirstOrDefault();
                    if (null != itemResponse3)
                    {
                        itemResponse3!.ValueKind = configCategoryItem.ValueKind;
                        itemResponse3.Section = configCategoryItem.Name;
                        retVal.Add(itemResponse3);
                    }
                    break;
            }
        }
        return retVal;
    }
    #endregion

    public static async Task<bool> CheckOnDeviceFileExists(this IUltimateDevice currentDevice, string filePath, string location)
        => await CheckOnDeviceFileExists(currentDevice, FilePathHelper.LocationPath(location, filePath));

    public static async Task<bool> CheckOnDeviceFileExists(this IUltimateDevice currentDevice, string locationFilePath)
    {
        var fileInfoResponse = await currentDevice.GetFileInfo(locationFilePath);
        return fileInfoResponse is { Success: true, FileInfo: not null };
    }

    public static async Task<(string DriveId, int BusId, bool Online)[]> GetFloppyDrives(this IUltimateDevice currentDevice)
    {
        if (currentDevice.Type == UltimateDeviceType.None)
        {
            return Array.Empty<(string DriveId, int BusId, bool Online)>();
        }

        var drivesResponse = await currentDevice.GetDrives();

        if (drivesResponse?.Drives == null || drivesResponse.Drives.Length == 0)
        {
            return Array.Empty<(string DriveId, int BusId, bool Online)>();
        }

        var drives = drivesResponse.Drives
            .SelectMany(dictionary => dictionary.ToArray())
            .Where(keyValuePair => FloppyDrives.DriveTypes.Contains(keyValuePair.Value.Type))
            .Select(keyValuePair => (keyValuePair.Key, keyValuePair.Value.BusId, keyValuePair.Value.Enabled))
            .ToArray();

        return drives;
    }

    // Bad shenanigans ...
    public static async Task ExecuteKeyboardBuffer(this IUltimateDevice currentDevice, string keystrokes)
    {
        // Split by return chars if any
        var splitKeystrokeCommands = keystrokes.Split('\r', StringSplitOptions.RemoveEmptyEntries)
            .Select(ks => keystrokes.Contains('\r') ? $"{ks}\r" : ks)
            .ToList();

        var keystrokeCommands = new List<string>();

        // Check any split has more than 10 chars if yes then split them by 10 chars ...
        if (splitKeystrokeCommands.Any(command => command.Count(ks => ks != '~') > 10))
        {
            foreach (var keystrokeCommand in splitKeystrokeCommands)
            {
                var commandLength = keystrokeCommand.Count(ks => ks != '~');
                if (commandLength > 10)
                {
                    var count = 0;
                    var kStrokes = string.Empty;
                    foreach (var keystroke in keystrokes)
                    {
                        if (keystroke != '~') count++;
                        kStrokes = $"{kStrokes}{keystroke}";
                        if (count > 9)
                        {
                            keystrokeCommands.Add(kStrokes);
                            kStrokes = string.Empty;
                            count = 0;
                        }
                    }

                    if(kStrokes.Length > 0)
                        keystrokeCommands.Add(kStrokes);
                }
                else
                {
                    keystrokeCommands.Add(keystrokeCommand);
                }
            }
        }
        else
        {
            keystrokeCommands = splitKeystrokeCommands;
        }
        
        foreach (var keystrokeCommand in keystrokeCommands)
        {
            await ExecuteKeyboardBufferInternal(currentDevice, keystrokeCommand);
        }
    }

    private static Task ExecuteKeyboardBufferInternal(IUltimateDevice currentDevice, string keystrokes)
    {
        var keyStrokeCount = keystrokes.Count(ks => ks != '~');
        var hexValue = PETSCIICodes.GetHexValue(keystrokes);

        var resultTask = currentDevice.WriteMemory(MemoryAddresses.KeyboardBuffer, hexValue)
            .ExecOnSuccess(() =>
                currentDevice.WriteMemory(MemoryAddresses.ExecKeyBuffer, keyStrokeCount.ToString("X2"))
            );

        return resultTask;
    }

}