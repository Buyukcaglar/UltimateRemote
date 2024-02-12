using UltimateRemote.Components.Shared.FormInputs;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class PlayModMusic : BaseFileFunctionComponent
{
    protected override FileTypeGroup[] AllowedFileTypeGroups => new FileTypeGroup[]
        { PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.ModFile)! };
    

    private Task ButtonClicked(FileSelectorModel selectedFile)
        => CurrentDevice.PlayOnDeviceModFile(selectedFile.LocationPath)
            .ExecOnSuccess(() => HistoryManager.Add(selectedFile.Path));

    private async Task UploadFile()
    {
        var fileContent = await ExecuteUiBlockingTask(task: FilePickerService.GetFileContentBytes(FilePickerOptions.ModFileOptions, ToastService),
            blockingMessage: Strings.ContentListManager.BpMsgSelectFile(FileTypeGroupNames.ModFile));

        if (fileContent is { ContentBytes.Length: > 0 })
        {
            await CurrentDevice.PlayUploadedModFile(fileContent.ContentBytes, fileContent.FileName)
                .ExecOnSuccess(() => HistoryManager.Add(fileContent.FileName, fileContent.ContentBytes));
        }
    }

    private Task HistoryItemSelected(HistoryItem item)
        => item.Type switch
        {
            HistoryItemType.StorageContentFile => CurrentDevice.PlayOnDeviceModFile(GetPath(item.Path!))
                .ExecOnSuccess(() => HistoryManager.Add(item.Path!)),
            HistoryItemType.UploadedFile => CurrentDevice.PlayUploadedModFile(item.ContentBytes!, item.FileName)
                .ExecOnSuccess(() => HistoryManager.Add(item.FileName, item.ContentBytes!)),
            _ => Task.CompletedTask
        };

}
