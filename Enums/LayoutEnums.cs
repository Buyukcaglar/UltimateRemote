namespace UltimateRemote.Enums;

// ReSharper disable once InconsistentNaming

public enum LayoutItemType
{
    [StringValue("Storage Content File")] StorageContentFile,
    [StringValue("Uploaded File")] UploadedFile,
    [StringValue("HVSC SID File")] HVSCSIDFile,
    [StringValue("Jukebox Playlist")] JukeboxPlaylist,
    [StringValue("Play SID Music")] PlaySidMusic,
    [StringValue("Play MOD Music")] PlayModMusic,
    [StringValue("Run/Load Program")] RunLoadProgram,
    [StringValue("Run Cartridge")] RunCartridge,
    [StringValue("Reset Machine")] ResetMachine,
    [StringValue("Reboot Machine")] RebootMachine,
    [StringValue("Reset/Reboot Stack")] ResetRebootStack,
    [StringValue("Machine Functions")] MachineFunctions,
    [StringValue("Floppy Drives")] FloppyDrives,
    [StringValue("Non-Floppy Drives")] NonFloppyDrives,
    [StringValue("Drive by BusId")] DriveByBusId,
    [StringValue("Streams")] Streams,
    [StringValue("Create Disk Image")] CreateDiskImage,
    [StringValue("Get File Info")] GetOnDeviceFileInfo,
    [StringValue("Keyboard Macros")] KeyMacros,
    [StringValue("Power Off Machine")] PowerOffMachine,
}

public enum CustomIconType
{
    None = 0,
    Png = 1,
    WebP = 2,
    Svg = 3
}
