namespace UltimateRemote.Constants;
internal static class FileTypeGroupNames
{
    public const string DiskImages = "Disk Images";
    public const string DiskImages1541 = "1541 Disk Images";
    public const string DiskImagesD64 = "D64 Disk Image";
    public const string DiskImagesG64 = "G64 Disk Image";
    public const string DiskImagesD71 = "D71 Disk Image";
    public const string DiskImagesG71 = "G71 Disk Image";
    public const string DiskImagesD81 = "D81 Disk Image";
    public const string DiskImagesDnp = "DNP CMD File System Image";
    public const string Program = nameof(Program);
    public const string SidFile = "SID File";
    public const string TapeImages = "Tape Images";
    public const string TapeImageT64 = "T64 Tape Image";
    public const string TapeImageTap = "TAP Tape Image";
    public const string TapeImageP00 = "P00 Tape Image";
    public const string BinaryFile = "Binary File";
    public const string BinaryFiles = "Binary Files";
    public const string CartridgeImage = "Cartridge Image";
    public const string RomFile = "ROM File";
    public const string ReuImage = "REU Image";
    public const string ModFile = "MOD File";
}

internal static class FileTypeGroupExtensions
{
    public static string[] DiskImages = new[] { "d64", "g64", "d71", "g71", "d81" };
    public static string[] DiskImages1541 = new[] { "d64", "g64" };
    public static string[] DiskImagesD64 = new[] { "d64" };
    public static string[] DiskImagesG64 = new[] { "g64" };
    public static string[] DiskImagesD71 = new[] { "d71" };
    public static string[] DiskImagesG71 = new[] { "g71" };
    public static string[] DiskImagesD81 = new[] { "d81" };
    public static string[] DiskImagesDnp = new[] { "dnp" };
    public static string[] Program = new[] { "prg" };
    public static string[] SidFile = new[] { "sid" };
    public static string[] TapeImages = new[] { "t64", "tap", "p00" };
    public static string[] TapeImageT64 = new[] { "t64" };
    public static string[] TapeImageTap = new[] { "tap" };
    public static string[] TapeImageP00 = new[] { "p00" };
    public static string[] BinaryFile = new[] { "bin" };
    public static string[] CartridgeImage = new[] { "crt" };
    public static string[] RomFile = new[] { "rom" };
    public static string[] ReuImage = new[] { "reu" };
    public static string[] ModFile = new[] { "mod" };
}

