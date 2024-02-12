// ReSharper disable InconsistentNaming
namespace UltimateRemote.Enums;
public enum HelpTopicIdentifier
{
    [StringValue("Basics, Icons and Functions")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Basics, Icons and Functions", iconCss: "list")]
    Basics,
    [StringValue("User Menu")] 
    [HelpTopic<HelpTopicIdentifier>(name: "User Menu", iconCss: "gear ph-duotone")]
    UserMenu,
    [StringValue("Device Manager")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Device Manager", iconCss: "monitor ph-duotone")]
    DeviceManager,
    [StringValue("Storage Content Manager")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Storage Content Manager", iconCss: "usb ph-duotone")]
    StorageContentManager,
    [StringValue("Layout Manager")] 
    //[HelpTopic<HelpTopicIdentifier>(name: "Layout Manager", iconCss: "layout ph-duotone", subTopics: [LayoutItemTypes, UserTypes, StorageContentFile, UploadedFile, HVSCSIDFile, JukeboxPlaylist, FunctionTypes, PlaySIDMusic, PlayMODMusic, RunLoadProgram, RunCartridge, ResetMachine, RebootMachine, ResetRebootStack, MachineFunctions, FloppyDrives, NonFloppyDrives, DriveByBusId, Streams, CreateDiskImage, GetFileInfo])]
    [HelpTopic<HelpTopicIdentifier>(name: "Layout Manager", iconCss: "layout ph-duotone")]
    LayoutManager,
    [StringValue("Configuration Manager")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Configuration Manager", iconCss: "sliders ph-duotone")]
    ConfigurationManager,
    [StringValue("Basic Editor")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Basic Editor", iconCss: "code ph-duotone")]
    BasicEditor,
    [StringValue("Jukebox Manager")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Jukebox Manager", iconCss: "playlist ph-duotone")]
    JukeboxManager,
    [StringValue("Preferences")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Preferences", iconCss: "toggle-left ph-duotone")]
    Preferences,
    [StringValue("Layout Item Types")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Layout Item Types", iconCss: "list", nonSelectable: true, topLevel: false)]
    LayoutItemTypes,
    [StringValue("User Types")] 
    [HelpTopic<HelpTopicIdentifier>(name: "User Types", iconCss: null, nonSelectable: true, topLevel: false)]
    UserTypes,
    [StringValue("Storage Content File")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Storage Content File", iconCss: "usb ph-duotone", topLevel: false)]
    StorageContentFile,
    [StringValue("Uploaded File")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Uploaded File", iconCss: "upload ph-duotone", topLevel: false)]
    UploadedFile,
    [StringValue("HVSC SID File")] 
    [HelpTopic<HelpTopicIdentifier>(name: "HVSC SID File", iconCss: "wave-triangle ph-duotone", topLevel: false)]
    HVSCSIDFile,
    [StringValue("Jukebox Playlist")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Jukebox Playlist", iconCss: "playlist ph-duotone", topLevel: false)]
    JukeboxPlaylist,
    [StringValue("Function Types")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Function Types", iconCss: null, nonSelectable: true, topLevel: false)]
    FunctionTypes,
    [StringValue("Play SID Music")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Play SID Music", iconCss: "wave-triangle ph-duotone", topLevel: false)]
    PlaySIDMusic,
    [StringValue("Play MOD Music")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Play MOD Music", iconCss: "music-note ph-duotone", topLevel: false)]
    PlayMODMusic,
    [StringValue("Run/Load Program")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Run/Load Program", iconCss: "app-window ph-duotone", topLevel: false)]
    RunLoadProgram,
    [StringValue("Run Cartridge")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Run Cartridge", iconCss: "circuitry ph-duotone", topLevel: false)]
    RunCartridge,
    [StringValue("Reset Machine")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Reset Machine", iconCss: "arrow-clockwise ph-duotone", topLevel: false)]
    ResetMachine,
    [StringValue("Reboot Machine")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Reboot Machine", iconCss: "arrow-counter-clockwise ph-duotone", topLevel: false)]
    RebootMachine,
    [StringValue("Reset/Reboot Stack")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Reset/Reboot Stack", iconCss: "arrows-clockwise ph-duotone", topLevel: false)]
    ResetRebootStack,
    [StringValue("Machine Functions")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Machine Functions", iconCss: "cpu ph-duotone", topLevel: false)]
    MachineFunctions,
    [StringValue("Floppy Drives")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Floppy Drives", iconCss: "floppy-disk ph-duotone", topLevel: false)]
    FloppyDrives,
    [StringValue("Non-FloppyDrives")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Non-Floppy Drives", iconCss: "hard-drives ph-duotone", topLevel: false)]
    NonFloppyDrives,
    [StringValue("Drive By BusId")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Drive By BusId", iconCss: "floppy-disk ph-duotone", topLevel: false)]
    DriveByBusId,
    [StringValue("Streams")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Streams", iconCss: "flow-arrow ph-duotone", topLevel: false)]
    Streams,
    [StringValue("Create Disk Image")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Create Disk Image", iconCss: "disc ph-duotone", topLevel: false)]
    CreateDiskImage,
    [StringValue("Get File Info")] 
    [HelpTopic<HelpTopicIdentifier>(name: "Get File Info", iconCss: "file-search ph-duotone", topLevel: false)]
    GetFileInfo
}
