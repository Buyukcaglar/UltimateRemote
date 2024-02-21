﻿namespace UltimateRemote.Constants;
internal static class FilePickerOptions
{
    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> SidFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.sid" } }, // UTType values
        { DevicePlatform.Android, new[] { "application/octet-stream" } }, // MIME type
        { DevicePlatform.WinUI, new[] { ".sid" } }, // file extension
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.sid" } }, // UTType values
        { DevicePlatform.MacCatalyst, new[] { "public.c64.sid" } },
    };

    private static readonly FilePickerFileType SidFile = new FilePickerFileType(SidFileTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> TextFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.text" } },
        { DevicePlatform.Android, new[] { "text/plain" } },
        { DevicePlatform.WinUI, new[] { ".txt" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.text" } },
        { DevicePlatform.MacCatalyst, new[] { "public.text" } }
    };

    private static readonly FilePickerFileType TextFile = new FilePickerFileType(TextFileTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> DiskImageTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.diskimage" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".d64", ".g64", ".d71", ".g71", ".d81" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.diskimage" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.diskimage" } },

    };

    private static readonly FilePickerFileType DiskImages = new FilePickerFileType(DiskImageTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> DiskImage1541Types = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.diskimage.1541" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".d64", ".g64"} },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.diskimage.1541" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.diskimage.1541" } },
    };

    private static readonly FilePickerFileType DiskImage1541 = new FilePickerFileType(DiskImage1541Types);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> DiskImageD64Types = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.diskimage.d64" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".d64" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.diskimage.d64" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.diskimage.d64" } },
    };

    private static readonly FilePickerFileType DiskImageD64 = new FilePickerFileType(DiskImageD64Types);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> DiskImageG64Types = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.diskimage.g64" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".g64"} },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.diskimage.g64" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.diskimage.g64" } },
    };

    private static readonly FilePickerFileType DiskImageG64 = new FilePickerFileType(DiskImageG64Types);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> DiskImageD71Types = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.diskimage.d71" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".d71" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.diskimaged.d71" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.diskimaged.d71" } },
    };

    private static readonly FilePickerFileType DiskImageD71 = new FilePickerFileType(DiskImageD71Types);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> DiskImageG71Types = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.diskimage.g71" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".g71" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.diskimaged.g71" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.diskimaged.g71" } },
    };

    private static readonly FilePickerFileType DiskImageG71 = new FilePickerFileType(DiskImageG71Types);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> DiskImageD81Types = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.diskimage.d81" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".d81" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.diskimage.d81" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.diskimage.d81" } },
    };

    private static readonly FilePickerFileType DiskImageD81 = new FilePickerFileType(DiskImageD81Types);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> CmdFileSystemImageDnpTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.cmd.filesystemimage.dnp" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".dnp" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.cmd.filesystemimage.dnp" } },
        { DevicePlatform.MacCatalyst, new[] { "public.cmd.filesystemimage.dnp" } },
    };

    private static readonly FilePickerFileType CmdFileSystemImageDnp = new FilePickerFileType(CmdFileSystemImageDnpTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> C64ProgramFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.program" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".prg" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.program" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.program" } },
    };

    private static readonly FilePickerFileType C64ProgramFile = new FilePickerFileType(C64ProgramFileTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> C64TapeImageFilesTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.tapeimage" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".t64", ".tap", ".p00" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.tapeimage" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.tapeimage" } },
    };

    private static readonly FilePickerFileType C64TapeImageFiles = new FilePickerFileType(C64TapeImageFilesTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> C64T64ImageFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.tapeimage.t64" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".t64" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.tapeimage.t64" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.tapeimage.t64" } },
    };

    private static readonly FilePickerFileType C64T64ImageFile = new FilePickerFileType(C64T64ImageFileTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> C64TapImageFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.tapeimage.tap" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".tap" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.tapeimage.tap" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.tapeimage.tap" } },
    };

    private static readonly FilePickerFileType C64TapImageFile = new FilePickerFileType(C64TapImageFileTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> C64P00ImageFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.tapeimage.p00" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".p00" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.tapeimage.p00" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.tapeimage.p00" } },
    };

    private static readonly FilePickerFileType C64P00ImageFile = new FilePickerFileType(C64P00ImageFileTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> C64BinaryFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.binary" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".bin" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.binary" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.binary" } },
    };

    private static readonly FilePickerFileType C64BinaryFile = new FilePickerFileType(C64BinaryFileTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> BinaryFilesTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.data" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { "" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.data" } },
        { DevicePlatform.MacCatalyst, new[] { "public.data" } },
    };

    private static readonly FilePickerFileType BinaryFiles = new FilePickerFileType(BinaryFilesTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> C64CartridgeImageTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.catridgeimage" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".crt" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.catridgeimage" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.catridgeimage" } },
    };

    private static readonly FilePickerFileType C64CartridgeImage = new FilePickerFileType(C64CartridgeImageTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> C64RomFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.rom" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".rom" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.rom" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.rom" } },
    };

    private static readonly FilePickerFileType C64RomFile = new FilePickerFileType(C64RomFileTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> ReuImageTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.reu" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".reu" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.reu" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.reu" } },
    };

    private static readonly FilePickerFileType ReuImage = new FilePickerFileType(ReuImageTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> ModFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.music.mod" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".mod" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.music.mod" } },
        { DevicePlatform.MacCatalyst, new[] { "public.music.mod" } },
    };

    private static readonly FilePickerFileType ModFile = new FilePickerFileType(ModFileTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> LayoutItemIconImageFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.png", "org.webmproject.webp", "public.svg-image" } },
        { DevicePlatform.Android, new[] { "image/png", "image/webp", "image/svg+xml" } },
        { DevicePlatform.WinUI, new[] { ".png", ".webp", ".svg" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.png", "org.webmproject.webp", "public.svg-image" } },
        { DevicePlatform.MacCatalyst, new[] { "public.png", "org.webmproject.webp", "public.svg-image" } },
    };

    private static readonly FilePickerFileType LayoutItemIconImageFile = new FilePickerFileType(LayoutItemIconImageFileTypes);

    public static readonly Dictionary<DevicePlatform, IEnumerable<string>> LayoutItemUploadFileTypes = new Dictionary<DevicePlatform, IEnumerable<string>>
    {
        { DevicePlatform.iOS, new[] { "public.c64.diskimage", "public.c64.program", "public.c64.catridgeimage", "public.c64.sid", "public.music.mod" } },
        { DevicePlatform.Android, new[] { "application/octet-stream" } },
        { DevicePlatform.WinUI, new[] { ".d64", ".g64", ".d71", ".g71", ".d81", ".prg", ".crt", ".sid", ".mod" } },
        { DevicePlatform.Tizen, new[] { "*/*" } },
        { DevicePlatform.macOS, new[] { "public.c64.diskimage", "public.c64.program", "public.c64.catridgeimage", "public.c64.sid", "public.music.mod" } },
        { DevicePlatform.MacCatalyst, new[] { "public.c64.diskimage", "public.c64.program", "public.c64.catridgeimage", "public.c64.sid", "public.music.mod" } },
    };

    private static readonly FilePickerFileType LayoutItemUploadFile = new FilePickerFileType(LayoutItemUploadFileTypes);

    public static PickOptions SidFileOptions = new()
    {
        PickerTitle = "Select a SID file",
        FileTypes = SidFile,
    };

    public static PickOptions TextFileOptions = new()
    {
        PickerTitle = "Select a TXT file",
        FileTypes = TextFile,
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
        PickerTitle = "Select a file",
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

    public static Dictionary<DevicePlatform, IEnumerable<string>> GetFileTypes(string fileTypeGroupName)
        => fileTypeGroupName switch
        {
            FileTypeGroupNames.DiskImages => DiskImageTypes,
            FileTypeGroupNames.DiskImages1541 => DiskImage1541Types,
            FileTypeGroupNames.DiskImagesD64 => DiskImageD64Types,
            FileTypeGroupNames.DiskImagesG64 => DiskImageG64Types,
            FileTypeGroupNames.DiskImagesD71 => DiskImageD71Types,
            FileTypeGroupNames.DiskImagesG71 => DiskImageG71Types,
            FileTypeGroupNames.DiskImagesD81 => DiskImageD81Types,
            FileTypeGroupNames.DiskImagesDnp => CmdFileSystemImageDnpTypes,
            FileTypeGroupNames.Program => C64ProgramFileTypes,
            FileTypeGroupNames.SidFile => SidFileTypes,
            FileTypeGroupNames.TapeImages => C64TapeImageFilesTypes,
            FileTypeGroupNames.TapeImageT64 => C64T64ImageFileTypes, 
            FileTypeGroupNames.TapeImageTap => C64TapImageFileTypes, 
            FileTypeGroupNames.TapeImageP00 => C64P00ImageFileTypes,
            FileTypeGroupNames.BinaryFile => C64BinaryFileTypes,
            FileTypeGroupNames.CartridgeImage => C64CartridgeImageTypes,
            FileTypeGroupNames.RomFile => C64RomFileTypes,
            FileTypeGroupNames.ReuImage => ReuImageTypes,
            FileTypeGroupNames.ModFile => ModFileTypes,
            _ =>  BinaryFilesTypes
        };

    public static (string Title, Dictionary<DevicePlatform, IEnumerable<string>> FileTypes) GetFileTypes(
        PickOptions pickOptions)
    {
        if (pickOptions.Equals(DiskImageFileOptions))
            return (DiskImageFileOptions.PickerTitle!, DiskImageTypes);
        
        if (pickOptions.Equals(DiskImage1541FileOptions))
            return (DiskImage1541FileOptions.PickerTitle!, DiskImage1541Types);
        
        if (pickOptions.Equals(DiskImageD64FileOptions))
            return (DiskImageD64FileOptions.PickerTitle!, DiskImageD64Types);
        
        if (pickOptions.Equals(DiskImageG64FileOptions))
            return (DiskImageG64FileOptions.PickerTitle!, DiskImageG64Types);
        
        if (pickOptions.Equals(DiskImageD71FileOptions))
            return (DiskImageD71FileOptions.PickerTitle!, DiskImageD71Types);
        
        if (pickOptions.Equals(DiskImageG71FileOptions))
            return (DiskImageG71FileOptions.PickerTitle!, DiskImageG71Types);
        
        if (pickOptions.Equals(DiskImageD81FileOptions))
            return (DiskImageD81FileOptions.PickerTitle!, DiskImageD81Types);
        
        if (pickOptions.Equals(CmdFileSystemImageDnpFileOptions))
            return (CmdFileSystemImageDnpFileOptions.PickerTitle!, CmdFileSystemImageDnpTypes);
        
        if (pickOptions.Equals(ProgramFileOptions))
            return (ProgramFileOptions.PickerTitle!, C64ProgramFileTypes);
        
        if (pickOptions.Equals(SidFileOptions))
            return (SidFileOptions.PickerTitle!, SidFileTypes);
        
        if (pickOptions.Equals(TapeImageOptions))
            return (TapeImageOptions.PickerTitle!, C64TapeImageFilesTypes);
        
        if (pickOptions.Equals(TapeImageT64Options))
            return (TapeImageT64Options.PickerTitle!, C64T64ImageFileTypes);
        
        if (pickOptions.Equals(TapeImageTapOptions))
            return (TapeImageTapOptions.PickerTitle!, C64TapImageFileTypes);
        
        if (pickOptions.Equals(TapeImageP00Options))
            return (TapeImageP00Options.PickerTitle!, C64P00ImageFileTypes);
        
        if (pickOptions.Equals(BinaryFileOptions))
            return (BinaryFileOptions.PickerTitle!, C64BinaryFileTypes);
        
        if (pickOptions.Equals(CartridgeImageFileOptions))
            return (CartridgeImageFileOptions.PickerTitle!, C64CartridgeImageTypes);
        
        if (pickOptions.Equals(RomFileOptions))
            return (RomFileOptions.PickerTitle!, C64RomFileTypes);
        
        if (pickOptions.Equals(ReuImageFileOptions))
            return (ReuImageFileOptions.PickerTitle!, ReuImageTypes);
        
        if (pickOptions.Equals(ModFileOptions))
            return (ModFileOptions.PickerTitle!, ModFileTypes);

        return (BinaryFilesOptions.PickerTitle!, BinaryFilesTypes);

    }

}