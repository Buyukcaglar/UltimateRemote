using UltimateRemote.Components.Shared.FormInputs;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class GetOnDeviceFileInfo : BaseFileFunctionComponent
{
    protected override FileTypeGroup[] AllowedFileTypeGroups => PrefsMgr.EnabledFileTypeGroups;
    
    private Dictionary<string, object?>? _fileInfo;

    private async Task GetFileInfo(FileSelectorModel selectedFile)
    {
        var fileInfoResponse = await CurrentDevice.GetFileInfo(selectedFile.LocationPath);
        _fileInfo = fileInfoResponse?.FileInfo;
    }

}
