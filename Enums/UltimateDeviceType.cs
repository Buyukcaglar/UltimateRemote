namespace UltimateRemote.Enums;

// ULTIMATE-II+L, ULTIMATE-II+, ULTIMATE-II, ULTIMATE 64
public enum UltimateDeviceType {[StringValue("NA")] None, [StringValue("Ultimate-II")] Ultimate1541, [StringValue("Ultimate 64")] UltimateC64 }

public enum DiskImageType { [StringValue("Auto")] NotSpecified, [StringValue("D64")] D64, [StringValue("G64")] G64, [StringValue("D71")] D71, [StringValue("G71")] G71, [StringValue("D81")] D81 }

public enum DiskMode { [StringValue("Default")] NotSpecified, [StringValue("Read/Write")] ReadWrite, [StringValue("Read Only")] ReadOnly, [StringValue("Unlinked")] Unlinked }

public enum DriveMode { [StringValue("Unk")] Unknown = 0, [StringValue("1541")] Drive1541 = 1541, [StringValue("1571")] Drive1571 = 1571, [StringValue("1581")] Drive1581 = 1581 }

public enum StreamType { [StringValue("Video")] Video, [StringValue("Audio")] Audio, [StringValue("Debug")] Debug }

public enum D64Tracks { Tracks35 = 35, Tracks40 = 40 }

public enum ImageFileType { [StringValue("D64")] D64, [StringValue("D71")] D71, [StringValue("D81")] D81, [StringValue("DNP")] Dnp }

public enum ConfigOp { LoadFromFlash, SaveToFlash, ResetToDefault }

