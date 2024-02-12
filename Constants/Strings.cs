// ReSharper disable InconsistentNaming

using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Constants;
internal static class Strings
{
    public static class ApiVersions
    {
        public const string Dummy = "0.0";
        public const string V01 = "0.1";
    }

    public static class ErrorMessages
    {
        public static string CouldNotObtainLocalIpAddress =
            $"Unable to obtain local IP of your {PlatformDependent.DeviceName} you are currently using Ultimate Remote application on. This may be a result of platform specific permission not being granted or your device being offline and not connected to any network. Please try to register your Ultimate device(s) manually.";
        public static string NoDeviceFound(string partialIp) => 
            $"No Ultimate device found on {partialIp}.[0-255] IP address range please make sure that your Ultimate device(s) is/are turned on and connected to the same network and subnet as per your device. If your Ultimate device(s) is/are on another subnet, please try to register them manually.";

        public const string CouldNotAccessLocalNetwork =
            "Could not access Local Network, make sure that your device currently using is connected to network and Local Nework access permission is granted, then try again.";
    }

    public static class WarningMessages
    {
        public const string NoRegisteredDeviceFound =
            "No registered Ultimate device found. Either perform automatic network scan for devices on your network or register them manually via Device Manager.";
        public const string NoRegisteredDeviceFoundTitle =
            "No Device Registered";
    }

    public static class ContentListManager
    {
        public const string Info = "Import USB drive / SD Card contents that used in Ultimate device from here.<br />For more information on how to prepare proper import files of your storage media, check help section.";
        public const string ImportStoreContentListFile = "Import SD Card/USB Drive Content List";
        public static string FileSizeWarning(string fileName, long fileSize) => $"Selected import file '{fileName}' is {((decimal)fileSize/(1024*1204)):N} mb in size.<br/>On mobile platforms large content files may require high-end devices due to memory requirements of searching large volumes of text.<br/>It is recommended that you to create seperate import files for each content type such as SID Files, Program Files, Disk Images etc. and import them seperately in smaller file sizes.<br/>Proceeding this operation may crash Ultimate Remote application on memory constrained devices.";

        public static string ListName(int? itemCount) => $"List Name ({itemCount} files)";

        public static string BpMsgSelectFile(string fileTypeName) => $"Select {fileTypeName}...";
        public const string BpMsgImporting = "Importing...";
        public static string BpMsgSavingList(string listName) => $"Saving list '{listName}'...";
        public static string BpMsgDeletingList(string listName) => $"Deleting list '{listName}'...";

        public const string ToastMsgFileImportFailed = "Import list came up empty, probably an error occured while trying import contents.";
        public const string ToastTitleFileImportFailed = "Import list empty";

        public const string ToastMsgListNameCanNotBeEmpty = "Import list came up empty, probably an error occured while trying import contents.";
        public const string ToastTitleListNameCanNotBeEmpty = "Import list empty";

        public static string ToastMsgDuplicateListName(string listName) => $"Name '{listName}' already exists. List names must be unique.";
        public const string ToastTitleDuplicateListName = "Duplicate list name";

        public const string ToastMsgSaveListFailed = "An error occured while saving list.";
        public const string ToastTitleSaveListFailed = "Save list failed";

        public const string ToastMsgStorageReadPermissionNotGranted = "Storage read permission NOT granted, import file will likely to fail. Manually enable Storage (read) permission from O/S' permissions section.";
        public const string ToastTitleStorageReadPermissionNotGranted = "Storage Read Permission Missing";

    }

    public static class ContentListSearch
    {
        public const string NoDeviceRegistered = "No registered Ultimate device found,<br/>file selection disabled.";

        public static string ApiV01CharLimit(string? deviceName) =>
            $"Current selected Ultimate device '{deviceName}' reports its API version as 0.1 API version v0.1 has 74 characters limitation while performing on device API operation Play SID file. Automatic selection of file paths longer than 74 characters are disabled. This enforcement can be disabled from <strong>Preferences</strong> in case of Ultimate API behaviour changes without changing its version (v0.1) in future.";

        public static string? ExtensionsIndicator(string[]? extensions)
            => extensions is { Length: > 0 }
                ? $"Searching in '{string.Join(", ", (extensions.Select(ext => $"*.{ext}")))}'"
                : null;

        public static string InputLabel(string listName, int fileCount) => $"{listName} ({fileCount} files)";
    }

    public static class Function
    {
        public static class Names
        {
            // Runners
            public const string PlaySidMusic = "Play SID Music";
            public const string PlayModMusic = "Play MOD Music";
            public const string RunLoadProgram = "Run/Load Program";
            public const string RunCartridge = "Run Cartridge";

            // Machine
            public const string Machine = nameof(Machine);
            public const string ResetMachine = "Reset Machine";
            public const string RebootMachine = "Reboot Machine";
            public const string PauseMachine = "Pause Machine";
            public const string ResumeMachine = "Resume Machine";
            public const string WriteMemory = "Write Memory";
            public const string UploadToMemory = "Upload to Memory";
            public const string ReadMemory = "Read Memory";
            public const string ReadDebugRegister = "Read Debug Register";
            public const string WriteDebugRegister = "Write Debug Register";

            // Floppy Drives
            public const string ListDrives = "List Floppy Drives";
            public const string MountImageOnDevice = "Mount Image on Device";
            public const string MountUploadedImage = "Upload & Mount Image";
            public const string ResetDrive = "Reset Drive";
            public const string RemoveDrive = "Remove Drive";
            public const string UnlinkDrive = "Unlink Drive";
            public const string TurnOnDrive = "Turn On Drive";
            public const string TurnOffDrive = "Turn Off Drive";
            public const string LoadDriveRomOnDevice = "Load Drive ROM on Device";
            public const string UploadAndLoadDriveRom = "Upload Drive ROM & Load";
            public const string SetDriveMode = "Set Drive Mode";

            // Data Streams
            public const string Streams = nameof(Streams);
            public const string StartStream = "Start Stream";
            public const string StopStream = "Stop Stream";

            // File Manipulations
            public const string GetFileInfo = "Get File Info on Device";
            public const string CreateDiskImage = "Create Disk Image";

            // Configuration
            public const string ConfigurationManager = "Configuration Manager";

            public const string LayoutManager = "Layout Manager";

            public const string JukeboxManager = "Jukebox Manager";

            public const string BasicEditor = "BASIC Tokenizer / Editor";

            public const string Preferences = nameof(Preferences);

            // Storage Media Content File
            public const string ContentListManager = "Storage Content List Manager";

            public const string DeviceManager = "Device Manager";

            public const string ScanDevice = "Scan Devices";
            public const string ScanIp = "Scan Specific IP Range";
            public const string ManualRegister = "Manual Register";

            public const string Help = nameof(Help);

        }

        public static class Infos
        {
            // Runners
            public const string PlaySidMusic = "Plays either specified existing SID file on Ultimate device file system or uploaded SID file. You can select song number to be played in the file via +/-.";
            public const string PlayModMusic = "Plays either specified existing MOD file on Ultimate device file system or uploaded MOD file.";
            public const string RunLoadProgram = "Runs or Loads either specified Program file on Ultimate device file system or uploaded Program file to system memory.";
            public const string RunCartridge = "Runs either specified Cartridge image file on Ultimate device file system or uploaded CRT file.";

            // Machine
            public const string Machine = $"""
                                          <h6 class="text-center">Machine related functions</h6>
                                          <div class="form-text m-0 p-1 mb-2 overflow-auto" style="max-height:30rem;max-width:57rem;">
                                              {MachineHelp}
                                          </div>
                                          """;

            public const string MachineHelp = $"""
                                              <span class="fw-semibold"><i class="ph-arrow-clockwise ph-duotone me-1"></i> Reset Machine: </span> {ResetMachine}<br/>
                                              <span class="fw-semibold"><i class="ph-arrow-counter-clockwise ph-duotone me-1"></i> Reboot Machine: </span> {RebootMachine}<br/>
                                              <span class="fw-semibold"><i class="ph-pause ph-duotone me-1 "></i> Pause Machine: </span> {PauseMachine}<br/>
                                              <span class="fw-semibold"><i class="ph-play ph-duotone me-1 "></i> Resume Machine: </span> {ResumeMachine}<br/>
                                              <span class="fw-semibold"><i class="ph-power ph-duotone me-1 "></i> Power Off Machine: </span> {PowerOffMachine}<br/>
                                              <span class="fw-semibold"><i class="ph-upload-simple ph-duotone me-1 "></i> Upload to Memory: </span> {UploadToMemory}<br/>
                                              <span class="fw-semibold"><i class="ph-book-open-text ph-duotone me-1 "></i> Read Memory: </span> {ReadMemory}<br/>
                                              <span class="fw-semibold"><i class="ph-pencil-simple ph-duotone me-1 "></i> Write Memory: </span> {WriteMemory}<br/>
                                              <span class="fw-semibold"><i class="ph-book-open-text ph-duotone me-1 "></i> Read Debug Register: </span> {ReadDebugRegister}<br/>
                                              <span class="fw-semibold"><i class="ph-pencil-simple ph-duotone me-1 "></i> Write Debug Register: </span> {WriteDebugRegister}<br/>
                                          """;
            
            public const string ResetMachine = "Resets machine while preserving current configuration.";
            public const string RebootMachine = "Restarts machine. Re-initializes the cartridge configuration and resets machine.";
            public const string PauseMachine = "Pauses machine. Machine is paused by pulling the DMA line low at a safe moment. This stops the CPU. Note that this does not stop any timers.";
            public const string ResumeMachine = "Resumes machine from paused state. The DMA line is released and the CPU will continue where it left off.";
            public const string PowerOffMachine = "Turns off Ultimate 64 device.";
            public const string WriteMemory = "Writes provided data through DMA to specified address of Commodore 64 memory. During this operaiton memory map that is currently selected is used. Writing to the I/O registers of the 6510 is not possible. Data bytes are written in consequetive memory locations. The address argument specifies the memory location in hexadecimal format. The data argument contains a string of bytes in hexadecimal format. The maxmimum number of bytes written with this function is 128.";
            public const string UploadToMemory = "Uploads provided external binary data file Ultimate device for it to be written Commodore 64 memory through DMA. Data uploaded to Ultimate device will be written to memory starting from the location indicated by the address argument, which should be in hexadecimal format. Note that data should not wrap around $FFFF.";
            public const string ReadMemory = "This function performs a DMA read action on the cartridge bus and returns the retrieves result as a binary file. The address argument specifies the memory location in hexadecimal format. The optional argument length specifies the number of bytes being read. When not specified, first 256 bytes are returned.";
            public const string ReadDebugRegister = "This function reads the debug register ($D7FF) of Ultimate 64 device and returns it in the “value” field of the JSON response. The value is in hexadecimal format.";
            public const string WriteDebugRegister = "This function writes hexadecimal formatted value into Ultimate 64 device's debug register ($D7FF), then reads the debug register ($D7FF) and returns its value.";

            // Floppy Drives
            
            public const string IECDrive = "Displays Emulated Software IEC Drive Information along with the partitions.";
            public const string PrinterEmu = "Displays Printer Emulation Information.";
            
            public const string ListDrives = "Retrieves information about all (internal) drives on the IEC bus. In addition to the presence of devices, it also shows the image files and paths of the mounted disks or referenced paths.";

            public const string FloppyDrive = $"""
                                                <h6 class="text-center">Floppy drive functions</h6>
                                                <div class="form-text m-0 p-1 mb-2 overflow-auto" style="max-height:30rem;max-width:57rem;">
                                                    {FloppyDriveHelp}
                                                </div>
                                                """;

            public const string FloppyDriveHelp = $"""
                                                     <span class="fw-semibold"><i class="ph-arrow-clockwise ph-duotone me-1"></i> Reset Drive: </span> {ResetDrive}<br/>
                                                     <span class="fw-semibold"><i class="ph-eject ph-duotone me-1"></i> Remove Image: </span> {RemoveImage}<br/>
                                                     <span class="fw-semibold"><i class="ph-link-break ph-duotone me-1 "></i> Unlink Image: </span> {UnlinkDrive}<br/>
                                                     <span class="fw-semibold"><i class="ph-power ph-duotone me-1 "></i> Drive Power: </span> {ToggleDrive}<br/>
                                                     <span class="fw-semibold"><i class="ph-caret-down me-1 "></i> Drive Mode: </span> {SetDriveMode}<br/>
                                                     <span class="fw-semibold"><i class="ph-upload-simple ph-duotone me-1 "></i> Upload and Mount: </span> {MountUploadedImage}<br/>
                                                     <span class="fw-semibold"><i class="ph-paper-plane-tilt ph-duotone me-1 "></i> Mount on device Image: </span> {MountImageOnDevice}<br/>
                                                     <span class="fw-semibold"><i class="ph-upload-simple ph-duotone me-1 "></i> Upload and Set ROM: </span> {UploadAndLoadDriveRom}<br/>
                                                     <span class="fw-semibold"><i class="ph-paper-plane-tilt ph-duotone me-1 "></i> Mount on device ROM: </span> {LoadDriveRomOnDevice}<br/>
                                                     <span class="fw-semibold"><i class="ph-gear ph-duotone me-1 "></i> Configuration Settings: </span> {ConfigSettings}<br/>
                                                     """;

            public const string MountImageOnDevice = "Mounts specified disk image file on the Ultimate device file system. The optional <span class=\"fw-semibold\">Type</span> argument specifies the type of the image, and could be one of the following: <span class=\"fw-semibold\">d64, g64, d71, g71 or d81</span>. If this argument is omitted, Ultimate device will use the file extension of the file specified to determine the correct image type. The optional <span class=\"fw-semibold\">Mode</span> argument can be one of the following: <span class=\"fw-semibold\">readwrite, readonly or unlinked</span>. In <span class=\"fw-semibold\">readwrite mode</span>, the drive can write to the image file; in <span class=\"fw-semibold\">readonly mode</span> the disk is write protected and in <span class=\"fw-semibold\">unlinked mode</span> the disk is not write protected, but the changes are not written back to the disk image.";
            public const string MountUploadedImage = "Uploads external disk image file to Ultimate device and mounts. The optional <span class=\"fw-semibold\">Type</span> argument specifies the type of the image, and could be one of the following: <span class=\"fw-semibold\">d64, g64, d71, g71 or d81</span>. If this argument is omitted, Ultimate device will use the file extension of the file specified to determine the correct image type. The optional <span class=\"fw-semibold\">Mode</span> argument can be one of the following: <span class=\"fw-semibold\">readwrite, readonly or unlinked</span>. In <span class=\"fw-semibold\">readwrite mode</span>, the drive can write to the image file; in <span class=\"fw-semibold\">readonly mode</span> the disk is write protected and in <span class=\"fw-semibold\">unlinked mode</span> the disk is not write protected, but the changes are not written back to the disk image.";
            public const string ResetDrive = "Sends reset command to selected drive.";
            
            public const string RemoveImage = "Unmounts disk image from the selected drive.";
            public const string UnlinkDrive = "Unlinks disk image on the selected drive. In this mode the disk is not write protected, but the changes are not written back to the disk image.";
            
            public const string ToggleDrive = "Turns On/Off drive.";
            
            public const string LoadDriveRomOnDevice = "Loads specified disk drive ROM file on Ultimate device file system to selected drive. The size of the ROM file needs to be <span class=\"fw-semibold\">16K or 32K</span>, depending on the drive type. Loading the ROM is a temporary action, setting the drive type or rebooting the machine will load the default ROM.";
            public const string UploadAndLoadDriveRom = "Uploads external disk drive ROM file to Ultimate device and loads to selected drive. The size of the ROM file needs to be <span class=\"fw-semibold\">16K or 32K</span>, depending on the drive type. Loading the ROM is a temporary action, setting the drive type or rebooting the machine will load the default ROM.";
            
            public const string SetDriveMode = "Changes selected disk drive mode between available modes which are <span class=\"fw-semibold\">1541, 1571 and 1581</span>. This function will also cause Ultimate device to load the appropriate default drive ROM. Any previous ROM that was loaded with one of <span class=\"fw-semibold\">Load Rom functions</span> will be overwritten.";

            public const string ConfigSettings = "Displays configuration settings popup for drive. Note that changes reflected automatically but not persisted until permanently saved to flash from Configuration Manager.";

            // Data Streams
            public const string StartStream = "Starts the selected type of stream on Ultimate 64 device. IP number parameter is required for the U64 to know where to send the stream to. The default port number that the data stream is sent to is 11000 for the video stream, 11001 for the audio stream and 11002 for the debug stream. A custom port number can be added to the IP address, after a colon separator; e.g. 192.168.178.224:6789 . Note that turning on the video stream will automatically turn off the debug stream.";
            public const string StopStream = "Stops the selected type of stream on Ultimate 64 device.";
            public const string Streams = "Starts/Stops the selected type of stream on Ultimate 64 device. To start selected type of stream IP Address parameter is required for the U64 to know where to send the stream to. The default port number that the data stream is sent to is 11000 for the video stream, 11001 for the audio stream and 11002 for the debug stream. A custom port number can be added to the IP address, after a colon separator; e.g. 192.168.178.224:6789 . Note that turning on the video stream will automatically turn off the debug stream.";

            // File Manipulations
            public const string GetFileInfo = "Retrieves basic information about specified file found on Ultimate device's file system, like size and extension.";
            public const string CreateDiskImage = "Creates specified type of disk image file with the provided parameters. Full path of the file must be specified with respect to Ultimate device file system.<p>For <strong>D64</strong> images default number of tracks is 35, but it can also be set to 40.<br/ >For <strong>D71</strong> images number of tracks is fixed at 70 and tracks parameter is ignored.<br/ >For <strong>D81</strong> images number of tracks is fixed at 160 (80 on each side) and tracks parameter is ignored.<br/ >For <strong>DNP</strong> images number of tracks is a required parameter and must be between 1-255. Each track will have 256 sectors. The maximum number of tracks is 255, which makes the maximum DNP size almost 16 Megabytes. (Note that creation of maximum size (16mb) DNP image file takes up to 2.5 minutes on Ultimate-II devices, so be patient!)<br/ ><br/ >The optional diskname argument overrides the name to be used in the header of the disk. When not given, it is taken from the name of the file that is being created.</p>";

            // Storage Content List File Manager
            public const string ContentFileManager = "Imports provided storage content list file in order to be used in 'on device operations such as Play SID on Device, Run Program on Device etc.'. In order to successfully import storage contents (USB Flashdrive or SD Card) of your Ultimate device, a specific formatted file is required. To learn how to prepare such file please refer to Help section of the Ultimate Remote application. If you have more than one storage device that is used with your Ultimate device, you can upload as many as you want.";

            public static string ScanDevice = $"Scans network for Ultimate devices. This function tries to obtain current local IP address of your {PlatformDependent.DeviceName} then scans the network subnet for any Ultimate devices. For example if your {PlatformDependent.DeviceName}'s IP Address is 192.168.1.10, app scans 192.168.1.0 - 192.168.1.255 address range for any online Ultimate device to be found. For automatic scan to work your Ultimate devices must be online and on the same network and subnet as your {PlatformDependent.DeviceName}.";
            public const string ScanIp = "Scans network for Ultimate devices using the provided IP address by sweeping its subnet. This function tries to scans the network subnet for the given IP address for any Ultimate devices. For example if the provided IP address is 192.168.10.1, app scans 192.168.10.0 - 192.168.10.255 address range for any online Ultimate device to be found. For automatic scan to work your Ultimate devices must be online and on the same network and subnet as with provided IP address.";
            public const string ManualRegister = "Ultimate devices can be manually registered from here. In order to complete registration, Ultimate device should be up and running and have the IP address provided for API version querying.";
        }

        public static class Icons
        {
            public const string StorageContentFile = "file";
            public const string UploadedFile = "upload";
            public const string PlaySidMusic = "wave-triangle";
            public const string PlayModMusic = "music-note";
            public const string RunLoadProgram = "app-window";
            public const string RunCartridge = "circuitry";
            public const string Machine = "cpu";
            public const string ResetMachine = "arrow-clockwise";
            public const string RebootMachine = "arrow-counter-clockwise";
            public const string FloppyDrives = "floppy-disk";
            public const string NonFloppyDrives = "hard-drives";
            public const string DriveByBusId = "floppy-disk";
            public const string Streams = "flow-arrow";
            public const string CreateDiskImage = "disc";
            public const string GetFileInfo = "file-search";
        }

    }

    public static class ContentFileService
    {
        public static string InputLabel(string fileName, int total) =>
            $"{fileName} ({total} files)";

        public static string InputLabel(string fileName, int total, int count, int limit) =>
            $"{fileName} ({total} files / {count} found, displaying {(count > limit ? limit : count)} items)";

        public const string ImportStatusReadingContents = "Reading contents of import file...";
        public const string ImportStatusConvertToString = "Content read, converting to text...";
        public const string ImportStatusConvertToArray = "Converted to text, splitting lines...";
        public static string ImportStatusSplitCompleted(int itemCount) => $"Total {itemCount} files found. Converting to internal searchable format. This may take several minutes.";

        public static string ToastMsgFileImportFailed(string errorMessage) => $"Import failed with error: {errorMessage}";
        public const string ToastTitleFileImportFailed = "File Import Failed";

        public static string ToastMsgFileImportSuccess(int itemCount) => $"File import completed successfully. {itemCount} file paths imported.";
        public const string ToastTitleFileImportSuccess = "File Import Completed";
    }

    public static class Generic
    {
        public const string SearchOnlyFileNames = "Search only in filenames";

        public const string SearchInLists = "Search in lists";

        public const string SidFileSelectorPlaceHolder = "ex: /SID Files/last ninja2.sid";

        public const string RomFileSelectorPlaceHolder = "ex: /Drive Roms/jiffy_dos_1541.rom";

        public const string DiskImageSelectorPlaceHolder = "ex: /Games/d64/wizard of wor.d64";

        public const string ModFileSelectorPlaceHolder = "ex: /MOD Music/Lettrix.mod";

        public const string PrgFileSelectorPlaceHolder = "ex: /Games/Prg/Ikari Warriors.prg";

        public const string CrtFileSelectorPlaceHolder = "ex: /Games/Crt/eob v1.crt";

        public const string BinFileSelectorPlaceHolder = "ex: /Binary/MemContent.bin";

        public const string DiskImageFileSelectorPlaceHolder = "ex: /DiskImages/Lotus_Challenge.d64";

        public const string DiskRomFilePlaceholder = "ex: /DriveRoms/jiffy-dos-1541.bin";

        public const string AnyShortcutFile = "any PRG,CRT,D64,G64,D71,G71,D81,SID,MOD file";

        public const string CopiedToClipboard = "Copied to clipboard";

        public const string RecentFiles = "Recent Files";

        public const string DefaultDeviceName = "My Ultimate Device";

        public const string IpAddress = "IP Address";

        public const string DeviceIp = "Device IP";

        public const string DeviceName = "Device Name";

        public const string SelectDeviceType = "Select Device Type";

    }

    public static class InternalTokens
    {
        public const string AllExtensionsToken = "All";
    }

    public static class FileSelector
    {
        public const string ToastMsgSelectLocation = "Select file location on device.";
        public const string ToastTitleSelectLocation = "Select location";
        public static string BpMsgRetrievingFileCacheFor(string fileListName) => $"Retrieving file cache for '{fileListName}'...";
    }

    public static class DeviceManager
    {
        public const string IpAddressPlaceHolder = "ex: 192.168.1.100";

        public const string DeviceNamePlaceHolder = "ex: My Ultimate Device";

        public const string ToastMsgSelectDeviceType = "Select Ultimate device type.";
        public const string ToastTitleSelectDeviceType = "Select Device Type";

        public const string ToastTitleCouldNotObtainLocalIp = "Local device IP";
        public const string ToastTitleDeviceScanFail = "Device Scan Failed";
        
        public const string EnterValidIpAddress = "Enter a valid IP Address";
        public const string InvalidIpAddress = "Invalid IP Address";

        public static string ToastMsgUnableToQueryDevice(string ipAddress) => $"Could not communicate with Ultimate device on {ipAddress}. API version query failed. Make sure that device is on and connected to network and has the provided IP Address.";
        public const string ToastTitleUnableToQueryDevice = "Unable to Query Device";

        public static string NewDeviceLabel(int idx, string ip, string apiVersion)
            => $"#{idx} IP: {ip}, API: v{apiVersion}";

        public static string ScanStatus(int scannedSoFar, int total, int found)
            => $"Scanning {scannedSoFar} / {total}, Found: {found}";

        public static string ScanningDevices(string ipAddress) => $"Searching for Ultimate devices on IP address range {string.Join(".", ipAddress.Split('.', StringSplitOptions.RemoveEmptyEntries)[..^1])}.0-255 ...";

        public static string AccessingDevice(string ipAddress) => $"Accessing device on {ipAddress}.";
    }

    public static class FilePickerService
    {
        public static string ToastMsgFileSelectFailed(string? exMessage) => $"An error occurred during file selection. Error Message: {exMessage}";
        public const string ToastTitleFileSelectFailed = "File select failed";
    }

    public static class LoadPrg
    {
        public static string ToastMsgPrgLoaded(string fileName) => $"Program '{fileName}' loaded.";
        public const string ToastTitlePrgLoaded = "Program Loaded";
    }

    public static class Machine
    {
        public const string ToastMsgMemoryAddressCanNotBeEmpty = "Enter a valid memory address location for read.";
        public const string ToastTitleMemoryAddressCanNotBeEmpty = "Memory Address Empty";

        public const string ToastMsgInvalidReadLength = "Read length value must be greater than 0 and lower than 65536";
        public const string ToastTitleInvalidReadLength = "Invalid Value";

        public const string ToastMsgMemoryContentCopiedToClipboard = "Memory content copied to clipboard.";

        public const string ToastMsgWriteContentTooBig = "Maximum 128 bytes (256 hex chars) can be written with this function. Use external binary file upload function for contents that are larger than 128 bytes";
        public const string ToastTitleWriteContentTooBig = "Write Content Too Large";
        
        public static string ToastMsgWriteSuccess(string? address) => $"Address: {address}";
        public const string ToastTitleWriteContentSuccess = "Write Memory Success";

        public static string ToastMsgDebugRegisterSuccess(string? value) => $"Value: {value}";
        public const string ToastTitleDebugRegisterReadSuccess = "Debug Register Read Success";
        public const string ToastTitleDebugRegisterWriteSuccess = "Debug Register Write Success";

        public const string ToastMsgWriteContentFileTooBig = "File too big to fit in memory, maximum size is 64kb";
        public const string ToastTitleWriteContentFileTooBig = "File Too Big";

        public const string ToastMsgContentOverflow = "Content is too big to fit in memory space when starting from the provided address.";
        public const string ToastTitleContentOverflow = "Write Content Overflow";
    }

    public static class FloppyDrive
    {
        public const string DiskImageFilePath = "Disk Image File Path";

        public const string RomFileFilePath = "Rom File Path";

        public const string LoadRom = "Load Rom";

        public const string MountDiskImage = "Mount Disk Image";

        public static string ToastMsgSuccessfulMountResult(MountImageResponse response) =>
            $"<b>SubSys:</b>{response.SubSys}<br/><b>FileType:</b>{response.FileType}<br/><b>Command:</b>{response.Command}<br><b>File:</b>{response.FilePath}";
        public const string ToastTitleSuccessfulMountResult = "Mount Image Success";

    }

    public static class Streams
    {
        public const string StreamType = "Stream Type";
        public const string StreamTypeTemplate = "Stream Type: {0}";
        public const string StreamTo = "Stream Destination";
        public const string IpPortPlaceHolder = "ex: 192.168.10.5:1100";
        public const string InvalidIpPort = "Invalid IP and Port format. Port info is optional but when provided format should be like 196.168.10.5:1100";

        public static string ToastMsgStreamStarted(StreamType streamType, string destinationAddress) => $"{streamType.GetStringValue()} started to {destinationAddress}.";
        public const string ToastTitleStreamStarted = "Stream Started";

        public static string ToastMsgStreamStopped(StreamType streamType) => $"{streamType.GetStringValue()} stopped.";
        public const string ToastTitleStreamStopped = "Stream Stopped";
    }

    public static class DiskImage
    {
        public const string ImageFileTypeLabel = "Image Type";
        public const string ImageFileTypeLabelTemplate = "Image Type : {0}";

        public const string Tracks = nameof(Tracks);
        public const string DiskNameLabel = "Disk Label (optional)";
        public const string DiskNamePlaceholder = "ex: My Disk";

        public const string ImageFilePathLabel = "Image File Path";
        public static string ImageFilePathPlaceholder(ImageFileType imageFileType) => $"ex: /Disk Images/My Disk.{imageFileType.ToString().ToLowerInvariant()}";

        public const string ToastMsgInvalidD64Tracks = "D64 Images may only have 35 or 40 tracks.";
        public const string ToastTitleInvalidD64Tracks = "Invalid Number of Tracks";

        public const string ToastMsgInvalidDnpTracks = "DNP Images must specify number of tracks. Value may range between 1 and 255.";
        public const string ToastTitleInvalidDnpTracks = "Invalid Number of Tracks";

        public const string ToastMsgImageFilePathCanNotBeEmpty = "Image file path can not be empty. Provide full path including filename and extension.";
        public const string ToastTitleImageFilePathCanNotBeEmpty = "Image File Path Empty";

        public static string ToastMsgDiskImageCreateSuccess(CreateDiskImageResponse response) => $"Disk Image created successfully.<br/><span class=\"fw-semibold\">Path: </span>{response.Path}<br/><span class=\"fw-semibold\">Tracks: </span>{response.Tracks}<br/><span class=\"fw-semibold\">DiskLabel: </span>{response.DiskLabel}<br/><span class=\"fw-semibold\">BytesWritten: </span>{response.BytesWritten}";
        public const string ToastTitleDiskImageCreateSuccess = "Disk Image Created";
        public const string BpMsgCreatingDiskImage = "Creating disk image ...";

    }

    public static class ConfigCategoryItem
    {
        public static string ToastMsgDriveConfigUpdated(string section, string value, string category) => $"<span class=\"fw-semibold\">{section}</span> setting updated with value <span class=\"fw-semibold\">'{value}'</span> for <span class=\"fw-semibold\">{category}</span>.";
        public const string ToastTitleDriveConfigUpdated = "Configuration Updated";
    }

    public static class LabelInput
    {
        public static string LabelValueHtml(string label, string? value)
            => $"<span class=\"fw-semibold\">{label}:&nbsp;</span><span class=\"mxw-19rem text-break\"> {value}</span>";
    }

    public static class ConfigurationManager
    {
        public const string QueryingDeviceConfiguration = "Querying device configuration, this may take a while ...";

        public static string QueryingConfiguration(string configName) => $"Querying '{configName}' configuration.";

        public const string ToastMsgWarningConfigEmpty = "Configuration query failed. Verify selected device is online.";
        public const string ToastTitleWarningConfigEmpty = "Could not Query Configuration";

        public static string BpWarningMsgConfigOp(ConfigOp op) => op switch
        {
            ConfigOp.LoadFromFlash => "This will load complete configuration back from flash. Do you want to continue?",
            ConfigOp.SaveToFlash => "This will save complete configuration to flash and will be persisted. Do you want to continue?",
            ConfigOp.ResetToDefault => "This will reset complete configuration to factory default. Do you want to continue?",
            _ => ""
        };

        public static string ToastMsgConfigOpSuccess(ConfigOp op) => op switch
        {
            ConfigOp.LoadFromFlash => "Configuration loaded from flash.",
            ConfigOp.SaveToFlash => "Current configuration saved to flash.",
            ConfigOp.ResetToDefault => "Configuration reset to default.",
            _ => ""
        };
        
        public static string ToastTitleConfigOpSuccess(ConfigOp op) => op switch
        {
            ConfigOp.LoadFromFlash => "Configuration Saved",
            ConfigOp.SaveToFlash => "Configuration Loaded",
            ConfigOp.ResetToDefault => "Configuration Reset",
            _ => ""
        };
    }

    public static class LayoutManager
    {
        public static string RemoveLayoutWarning(string layoutName) =>
            $"This will remove '{layoutName}' and all its contents. Do you want to continue?";
    }

    public static class UserFile
    {
        public static string FunctionInfo(LayoutItemType type, string? fileName, string? extension, string? path, string? location)
            => type switch
            {
                LayoutItemType.StorageContentFile => extension switch
                {
                    "d64" or "g64" or "d71" or "g71" or "d81" => $"Mounts '{path}' on location '{location}' with default mount mode to first available floppy drive. Location, Mount Mode and Drive can be overriden.",
                    "prg" => $"Runs or loads '{path}' on location '{location}'. Default Run action and Location can be overriden.",
                    "crt" => $"Runs '{path}' on location '{location}'. Location can be overriden.",
                    "sid" or "mod" => $"Plays '{path}' on location '{location}'. Location can be overriden.",
                    _ => ""
                },
                LayoutItemType.UploadedFile => extension switch
                {
                    "d64" or "g64" or "d71" or "g71" or "d81" => $"Uploads and mounts '{fileName}' with default mount mode to first available floppy drive. Mount Mode and Drive can be overriden.",
                    "prg" => $"Uploads and run/loads '{fileName}'. Default Run action can be overriden.",
                    "crt" => $"Uploads and runs '{fileName}'.",
                    "sid" or "mod" => $"Uploads and plays '{fileName}'.",
                    _ => ""
                },
                _ => ""
            };
    }

    public static class JukeboxManager
    {
        public const string StartImport = "Importing HVSC...";
        public static string DownloadingFile(string fileName) => $"Downloading '{fileName}'...";
        public static string DownloadFailed(string fileName, string exMessage) => $"Download file '{fileName}' failed. Error: {exMessage}";
        public const string ParsingSongLengthDb = "Parsing song length db file...";
        public static string ParsingSongLengthDbFailed(string exMessage) => $"Parsing song length db failed. This may be result of malformed db file. Error: {exMessage}";
        public static string ExtractingFiles(string fileName) => $"Extracting SID files from '{fileName}'...";
        public static string ExtractingFiles(string fileName, int total, int soFar) => $"Extracting SID files from '{fileName}'<br />{soFar} / {total}";
        public static string ExtractFailed(string fileName, string exMessage) => $"Archive failed to extract '{fileName}'. Error: {exMessage}";
        public const string ToastMsgHVSCImportSuccess = "HVSC Import complete successfully.";
        public const string ToastTitleHVSCImportSuccess = "HVSC Import Success";
        public const string ToastTitleHVSCImportFail = "HVSC Import Failed";

        public const string ImportWarningPrompt = "This will automatically download <b>High Voltage SID Collection from hvsc.c64.org website</b> along with <b>Songlengths.md5</b> file. Then extracts archive and build one time cache to use with Ultimate Remote. Entire operation takes between <b>20 minutes to several hours</b> depending on your device capabilities and requires about <b>500 mb</b> storage space.";
        public static string NotYetImportedMessage => $"High Voltage SID Collection not yet imported. In order to create playlists or to play standalone HVSC SID files, it is required to perform one time import operation. This action requires internet connection, make sure your {@PlatformDependent.DeviceName} is online.";
        public const string NoPlaylistsText = "There are currently no playlists created. Create a new playlist by clicking button below.";

        public static string RemovePlaylistWarning(string playlistName) =>
            $"This will remove '{playlistName}' playlist. Do you want to continue?";

    }

    public static class HvscSidFileSearch
    {
        public static string InputLabel(int totalCount) => $"HVSC SID Files ({totalCount} files)";

        public static string InputLabel(int total, int count, int limit, int selectedCount) =>
            count > 0 && selectedCount > 0
                ? $"HVSC SID Files ({total} files / {count} found, displaying {(count > limit ? limit : count)} items, selected {selectedCount} items)"
                : count > 0
                    ? $"HVSC SID Files ({total} files / {count} found, displaying {(count > limit ? limit : count)} items)"
                    : selectedCount > 0
                        ? $"HVSC SID Files ({total} files, selected {selectedCount} items)"
                        : $"HVSC SID Files ({total} files)";
    }

    public static class Jukebox
    {
        public static string InfoPlaylist(string? playlistName, int? fileCount, int tuneCount) => $"Jukebox for <b>'{playlistName}'</b> playlist. This playlist has <b>{fileCount} files</b> and <b>{tuneCount} tunes</b>.";
        public static string InfoSidFile(string? fileName, int tuneCount, string totalLength) => $"Jukebox for <b>'{fileName}'</b> SID file. This file contains <b>{tuneCount} tunes</b> and <b>total length is {totalLength}</b>.";
        public static string TracksLabelPlaylist(string playlistName, int fileCount, int tuneCount) => $"{playlistName} ({fileCount} files / {tuneCount} tunes)";
        public static string TracksLabelSidFile(string fileName, int tuneCount) => $"{fileName} ({tuneCount} tunes)";

        public const string ToastMsgSidRetrieveFail = "Could not get cached HVSC SID file content. Try re-importing HVSC via Jukebox Manager.";
        public const string ToastTitleSidRetrieveFail = "Cached File Fetch Failed";
    }

    public static class BasicEditor
    {
        public const string ToastMsgProgramUploaded = "Basic program uploaded. You may run the program on your C64.";
        public const string ToastTitleProgramUploaded = "Program Uploaded";
    }

    public static class Preferences
    {
        public const string ToastMsgPreferencesSaved = "Current preferences saved.";
        public const string ToastTitlePreferencesSaved = "Preferences Saved";
        public const string SaveChangesDescription = "Changes reflected automatically for current session, but not preserved permanently until specifically saved using <i class=\"ph-floppy-disk ph-duotone\"></i>.";

    }

}