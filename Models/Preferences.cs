using System.Text.Json.Serialization;
// ReSharper disable InconsistentNaming

namespace UltimateRemote.Models;
public class UserPreferences
{
    public bool HistoryEnabled { get; set; } = true;

    public uint HistorySize { get; set; } = 100;

    public uint SnackDuration { get; set; } = 5;

    public bool ConnectivityCheckEnabled { get; set; } = true;

    public uint ConnectivityCheckInterval { get; set; } = 5;

    public uint ApiClientTimeout { get; set; } = 15;

    // Won't use for now, may be later ....
    public bool RememberLastSelectedDeviceLocation { get; set; } = true;

    public bool ApiV01CharLimitEnforcement { get; set; } = true;

    public bool DisplayShortcutOptions { get; set; }

    public string HVSCArchiveLocation { get; set; } = "https://hvsc.brona.dk/HVSC/HVSC_83-all-of-them.rar";

    public string SongLengthDbFileLocation { get; set; } = "https://www.hvsc.c64.org/download/C64Music/DOCUMENTS/Songlengths.md5";

    public bool DisplayFilepathWhileHVSCPlay { get; set; }


    public List<FileTypeGroup> FileTypeGroups { get; set; } = new List<FileTypeGroup>(new[]
    {
        new FileTypeGroup(Name: FileTypeGroupNames.DiskImages, Extensions: FileTypeGroupExtensions.DiskImages, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.DiskImages)),
        new FileTypeGroup(Name: FileTypeGroupNames.DiskImages1541, Extensions: FileTypeGroupExtensions.DiskImages1541, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.DiskImages1541)),
        new FileTypeGroup(Name: FileTypeGroupNames.DiskImagesD64, Extensions: FileTypeGroupExtensions.DiskImagesD64, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.DiskImagesD64)),
        new FileTypeGroup(Name: FileTypeGroupNames.DiskImagesG64, Extensions: FileTypeGroupExtensions.DiskImagesG64, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.DiskImagesG64)),
        new FileTypeGroup(Name: FileTypeGroupNames.DiskImagesD71, Extensions: FileTypeGroupExtensions.DiskImagesD71, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.DiskImagesD71)),
        new FileTypeGroup(Name: FileTypeGroupNames.DiskImagesG71, Extensions: FileTypeGroupExtensions.DiskImagesG71, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.DiskImagesG71)),
        new FileTypeGroup(Name: FileTypeGroupNames.DiskImagesD81, Extensions: FileTypeGroupExtensions.DiskImagesD81, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.DiskImagesD81)),
        new FileTypeGroup(Name: FileTypeGroupNames.DiskImagesDnp, Extensions: FileTypeGroupExtensions.DiskImagesDnp, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.DiskImagesDnp)),
        new FileTypeGroup(Name: FileTypeGroupNames.Program, Extensions: FileTypeGroupExtensions.Program, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.Program)),
        new FileTypeGroup(Name: FileTypeGroupNames.SidFile, Extensions: FileTypeGroupExtensions.SidFile, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.SidFile)),
        new FileTypeGroup(Name: FileTypeGroupNames.TapeImages, Extensions: FileTypeGroupExtensions.TapeImages, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.TapeImages)),
        new FileTypeGroup(Name: FileTypeGroupNames.TapeImageT64, Extensions: FileTypeGroupExtensions.TapeImageT64, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.TapeImageT64)),
        new FileTypeGroup(Name: FileTypeGroupNames.TapeImageTap, Extensions: FileTypeGroupExtensions.TapeImageTap, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.TapeImageTap)),
        new FileTypeGroup(Name: FileTypeGroupNames.TapeImageP00, Extensions: FileTypeGroupExtensions.TapeImageP00, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.TapeImageP00)),
        new FileTypeGroup(Name: FileTypeGroupNames.BinaryFile, Extensions: FileTypeGroupExtensions.BinaryFile, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.BinaryFile)),
        new FileTypeGroup(Name: FileTypeGroupNames.CartridgeImage, Extensions: FileTypeGroupExtensions.CartridgeImage, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.CartridgeImage)),
        new FileTypeGroup(Name: FileTypeGroupNames.RomFile, Extensions: FileTypeGroupExtensions.RomFile, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.RomFile)),
        new FileTypeGroup(Name: FileTypeGroupNames.ReuImage, Extensions: FileTypeGroupExtensions.ReuImage, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.ReuImage)),
        new FileTypeGroup(Name: FileTypeGroupNames.ModFile, Extensions: FileTypeGroupExtensions.ModFile, Enabled: true, BuiltIn: true, FilePickerOptions: FilePickerOptions.GetOptions(FileTypeGroupNames.ModFile))
    });

    public List<DeviceLocation> DeviceLocations = new List<DeviceLocation>(new[]
    {
        new DeviceLocation(Name: "Usb0", Path: "/Usb0", IconCss: "usb ph-duotone", BuiltIn: true) { Enabled = true, Default = true, Selected = true },
        new DeviceLocation(Name: "Usb1", Path: "/Usb1", IconCss: "usb ph-duotone", BuiltIn: true) { Enabled = true, Default = false },
        new DeviceLocation(Name: "Usb2", Path: "/Usb2", IconCss: "usb ph-duotone", BuiltIn: true) { Enabled = true, Default = false },
        new DeviceLocation(Name: "Temp", Path: "/Temp", IconCss: "trash ph-duotone", BuiltIn: true) { Enabled = true, Default = false },
        new DeviceLocation(Name: "Flash", Path: "/Flash", IconCss: "lightning ph-duotone", BuiltIn: true) { Enabled = true, Default = false },
    });

    public MachineCommand[] FloppyDriveCommands = [
        new MachineCommand(Name: "Load First File & Run", IconClass: "play", CommandFunc: MachineCommands.LoadFirstFileAndRun),
        new MachineCommand(Name: "Load Directory & List", IconClass: "currency-dollar-simple ph-list-bullets", CommandFunc: MachineCommands.ListDirectoryAndList),
        new MachineCommand(Name: "Load First File",  IconClass: "asterisk", CommandFunc: MachineCommands.LoadFirstFile),
        new MachineCommand(Name: "Load Directory", IconClass: "currency-dollar-simple", CommandFunc: MachineCommands.LoadDirectory),
        new MachineCommand(Name: "List", IconClass: "list-bullets", CommandFunc: (_) => MachineCommands.List),
        new MachineCommand(Name: "Run", IconClass: "play", CommandFunc: (_) => MachineCommands.Run),
    ];

}

public sealed record FileTypeGroup(string Name, string[] Extensions, bool Enabled, bool BuiltIn, [property: JsonIgnore] PickOptions FilePickerOptions);

public sealed record DeviceLocation(string Name, string Path, string? IconCss, bool BuiltIn)
{
    public bool Enabled { get; set; }
    public bool Default { get; set; }
    [JsonIgnore] public bool Selected { get; set; }
};

public sealed record MachineCommand(string Name, string IconClass, Func<int, string> CommandFunc);
