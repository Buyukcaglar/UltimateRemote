namespace UltimateRemote.Constants;
internal static class FilePickerOptions
{
    private static readonly FilePickerFileType SidFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.sid" } }, // UTType values
            { DevicePlatform.Android, new[] { "application/octet-stream" } }, // MIME type
            { DevicePlatform.WinUI, new[] { ".sid" } }, // file extension
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.sid" } }, // UTType values
        });

    private static readonly FilePickerFileType TxtFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.text" } },
            { DevicePlatform.Android, new[] { "text/plain" } },
            { DevicePlatform.WinUI, new[] { ".txt" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.text" } },
        });

    private static readonly FilePickerFileType DiskImages = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.diskimage" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".d64", ".g64", ".d71", ".d81" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.diskimage" } },
        });

    private static readonly FilePickerFileType DiskImage1541 = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.diskimage.1541" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".d64", ".g64"} },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.diskimage.1541" } },
        });

    private static readonly FilePickerFileType DiskImageD64 = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.diskimage.d64" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".d64" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.diskimage.d64" } },
        });

    private static readonly FilePickerFileType DiskImageG64 = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.diskimage.g64" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".g64"} },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.diskimage.g64" } },
        });

    private static readonly FilePickerFileType DiskImageD71 = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.diskimage.d71" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".d71" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.diskimaged.d71" } },
        });

    private static readonly FilePickerFileType DiskImageG71 = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.diskimage.g71" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".g71" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.diskimaged.g71" } },
        });

    private static readonly FilePickerFileType DiskImageD81 = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.diskimage.d81" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".d81" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.diskimage.d81" } },
        });

    private static readonly FilePickerFileType CmdFileSystemImageDnp = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.cmd.filesystemimage.dnp" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".dnp" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.cmd.filesystemimage.dnp" } },
        });

    private static readonly FilePickerFileType C64ProgramFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.program" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".prg" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.program" } },
        });

    private static readonly FilePickerFileType C64TapeImageFiles = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.tapeimage" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".t64", ".tap", ".p00" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.tapeimage" } },
        });

    private static readonly FilePickerFileType C64T64ImageFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.tapeimage.t64" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".t64" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.tapeimage.t64" } },
        });

    private static readonly FilePickerFileType C64TapImageFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.tapeimage.tap" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".tap" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.tapeimage.tap" } },
        });

    private static readonly FilePickerFileType C64P00ImageFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.tapeimage.p00" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".p00" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.tapeimage.p00" } },
        });

    private static readonly FilePickerFileType C64BinaryFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.binary" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".bin" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.binary" } },
        });

    private static readonly FilePickerFileType BinaryFiles = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.data" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { "" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.data" } },
        });

    private static readonly FilePickerFileType C64CartridgeImage = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.catridgeimage" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".crt" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.catridgeimage" } },
        });

    private static readonly FilePickerFileType C64RomFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.rom" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".rom" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.rom" } },
        });

    private static readonly FilePickerFileType ReuImage = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.reu" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".reu" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.reu" } },
        });

    private static readonly FilePickerFileType ModFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.music.mod" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".mod" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.music.mod" } },
        });

    private static readonly FilePickerFileType LayoutItemIconImageFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.png", "org.webmproject.webp", "public.svg-image" } },
            { DevicePlatform.Android, new[] { "image/png", "image/webp", "image/svg+xml" } },
            { DevicePlatform.WinUI, new[] { ".png", ".webp", ".svg" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.png", "org.webmproject.webp", "public.svg-image" } },
        });

    private static readonly FilePickerFileType LayoutItemUploadFile = new FilePickerFileType(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.c64.diskimage", "public.c64.program", "public.c64.catridgeimage", "public.c64.sid", "public.music.mod" } },
            { DevicePlatform.Android, new[] { "application/octet-stream" } },
            { DevicePlatform.WinUI, new[] { ".d64", ".g64", ".d71", ".g71", ".d81", ".prg", ".crt", ".sid", ".mod" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.c64.diskimage", "public.c64.program", "public.c64.catridgeimage", "public.c64.sid", "public.music.mod" } },
        });

    public static PickOptions SidFileOptions = new()
    {
        PickerTitle = "Select a SID file",
        FileTypes = SidFile,
    };

    public static PickOptions TextFileOptions = new()
    {
        PickerTitle = "Select a TXT file",
        FileTypes = TxtFile,
    };

    public static PickOptions DiskImageFileOptions = new()
    {
        PickerTitle = "Select a Disk Image file",
        FileTypes = DiskImages,
    };
    
    public static PickOptions DiskImage1541FileOptions = new()
    {
        PickerTitle = "Select a 1541 Disk Image file",
        FileTypes = DiskImage1541,
    };

    public static PickOptions DiskImageD64FileOptions = new()
    {
        PickerTitle = "Select a D64 Image file",
        FileTypes = DiskImageD64,
    };

    public static PickOptions DiskImageG64FileOptions = new()
    {
        PickerTitle = "Select a G64 Image file",
        FileTypes = DiskImageG64,
    };

    public static PickOptions DiskImageD71FileOptions = new()
    {
        PickerTitle = "Select a D71 Image file",
        FileTypes = DiskImageD71,
    };

    public static PickOptions DiskImageG71FileOptions = new()
    {
        PickerTitle = "Select a G71 Image file",
        FileTypes = DiskImageG71,
    };

    public static PickOptions DiskImageD81FileOptions = new()
    {
        PickerTitle = "Select a D81 Image file",
        FileTypes = DiskImageD81,
    };
    
    public static PickOptions CmdFileSystemImageDnpFileOptions = new()
    {
        PickerTitle = "Select a DNP Image file",
        FileTypes = CmdFileSystemImageDnp,
    };

    public static PickOptions ProgramFileOptions = new()
    {
        PickerTitle = "Select a PRG file",
        FileTypes = C64ProgramFile,
    };

    public static PickOptions TapeImageOptions = new()
    {
        PickerTitle = "Select a Tape Image file",
        FileTypes = C64TapeImageFiles,
    };

    public static PickOptions TapeImageT64Options = new()
    {
        PickerTitle = "Select a T64 file",
        FileTypes = C64T64ImageFile,
    };

    public static PickOptions TapeImageTapOptions = new()
    {
        PickerTitle = "Select a TAP file",
        FileTypes = C64TapImageFile,
    };

    public static PickOptions TapeImageP00Options = new()
    {
        PickerTitle = "Select a P00 file",
        FileTypes = C64P00ImageFile,
    };

    public static PickOptions BinaryFileOptions = new()
    {
        PickerTitle = "Select a BIN file",
        FileTypes = C64BinaryFile,
    };
    
    public static PickOptions BinaryFilesOptions = new()
    {
        PickerTitle = "Select a binary file",
        FileTypes = BinaryFiles,
    };

    public static PickOptions CartridgeImageFileOptions = new()
    {
        PickerTitle = "Select a CRT file",
        FileTypes = C64CartridgeImage,
    };

    public static PickOptions RomFileOptions = new()
    {
        PickerTitle = "Select a ROM file",
        FileTypes = C64RomFile,
    };

    public static PickOptions ReuImageFileOptions = new()
    {
        PickerTitle = "Select a REU file",
        FileTypes = ReuImage,
    };

    public static PickOptions ModFileOptions = new()
    {
        PickerTitle = "Select a MOD file",
        FileTypes = ModFile,
    };

    public static PickOptions ImageFilesOptions = new()
    {
        PickerTitle = "Select an image file",
        FileTypes = FilePickerFileType.Images,
    };

    public static PickOptions LayoutItemIconImageFileOptions = new()
    {
        PickerTitle = "Select a PNG, WEBP or SVG file",
        FileTypes = LayoutItemIconImageFile,
    };

    public static PickOptions LayoutItemUploadFileOptions = new()
    {
        PickerTitle = "Select any D64, G64, D71, G71, D81, PRG, CRT, SID, MOD file",
        FileTypes = LayoutItemUploadFile,
    };

    public static PickOptions GetOptions(string fileTypeGroupName)
        => fileTypeGroupName switch
        {
            FileTypeGroupNames.DiskImages => DiskImageFileOptions,
            FileTypeGroupNames.DiskImages1541 => DiskImage1541FileOptions,
            FileTypeGroupNames.DiskImagesD64 => DiskImageD64FileOptions,
            FileTypeGroupNames.DiskImagesG64 => DiskImageG64FileOptions,
            FileTypeGroupNames.DiskImagesD71 => DiskImageD71FileOptions,
            FileTypeGroupNames.DiskImagesG71 => DiskImageG71FileOptions,
            FileTypeGroupNames.DiskImagesD81 => DiskImageD81FileOptions,
            FileTypeGroupNames.DiskImagesDnp => CmdFileSystemImageDnpFileOptions,
            FileTypeGroupNames.Program => ProgramFileOptions,
            FileTypeGroupNames.SidFile => SidFileOptions,
            FileTypeGroupNames.TapeImages => TapeImageOptions,
            FileTypeGroupNames.TapeImageT64 => TapeImageT64Options, 
            FileTypeGroupNames.TapeImageTap => TapeImageTapOptions, 
            FileTypeGroupNames.TapeImageP00 => TapeImageP00Options,
            FileTypeGroupNames.BinaryFile => BinaryFileOptions,
            FileTypeGroupNames.CartridgeImage => CartridgeImageFileOptions,
            FileTypeGroupNames.RomFile => RomFileOptions,
            FileTypeGroupNames.ReuImage => ReuImageFileOptions,
            FileTypeGroupNames.ModFile => ModFileOptions,
            _ =>  BinaryFilesOptions
        };

}
