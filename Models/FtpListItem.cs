namespace UltimateRemote.Models;

public sealed record FtpListItem(string Name, string RelativePath, long Size, DateTime Modified, bool IsDirectory)
{
    private static readonly string[] DiskImageExtensions = ["d64", "g64", "d71", "g71", "d81"];

    public string Path => $"{RelativePath}{Name}";

    public string Extension => System.IO.Path.GetExtension(Name).TrimStart('.').ToLowerInvariant();

    public string Icon => (Path: Path, IsDirectory: IsDirectory, Extension: Extension) switch
    {
        { } when Path.Equals("/SD") => "sim-card ph-duotone",
        { } when Path.Equals("/Temp") => "trash ph-duotone",
        { } when Path.Equals("/Flash") => "lightning ph-duotone",
        { } when System.Text.RegularExpressions.Regex.IsMatch(input: Path, pattern: "/Usb\\d{1,2}") => "usb ph-duotone",
        
        (_, IsDirectory: true, _) => "folder ph-duotone",
        
        { } when !IsDirectory && DiskImageExtensions.Contains(Extension) => "floppy-disk ph-duotone",
        
        (_, IsDirectory: false, Extension: "dnp") => "hard-drives ph-duotone",
        (_, IsDirectory: false, Extension: "prg") => "app-window ph-duotone",
        (_, IsDirectory: false, Extension: "crt") => "circuitry ph-duotone",
        (_, IsDirectory: false, Extension: "sid") => "wave-triangle ph-duotone",
        (_, IsDirectory: false, Extension: "mod") => "music-note ph-duotone",
        
        _ => "file ph-duotone"
    };
}
