using UltimateRemote.Components.Shared.FormInputs;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class RunCartridge : BaseFileFunctionComponent
{
    protected override FileTypeGroup[] AllowedFileTypeGroups =>
        new[] { PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.CartridgeImage)! };

    private Task ButtonClicked(FileSelectorModel selectedFile)
        => CurrentDevice.RunCrtFileOnDevice(selectedFile.LocationPath)
            .ExecOnSuccess(() => HistoryManager.Add(selectedFile.Path));

    private async Task UploadFile()
    {
        var fileContent = await ExecuteUiBlockingTask(task: FilePickerService.GetFileContentBytes(FilePickerOptions.CartridgeImageFileOptions, ToastService),
            blockingMessage: Strings.ContentListManager.BpMsgSelectFile(FileTypeGroupNames.CartridgeImage));

        if (fileContent is { ContentBytes.Length: > 0 })
        {
            await CurrentDevice.RunUploadedCrtFile(fileContent.ContentBytes, fileContent.FileName)
                .ExecOnSuccess(() => HistoryManager.Add(fileContent.FileName, fileContent.ContentBytes));
        }
    }

    private Task HistoryItemSelected(HistoryItem item)
        => item.Type switch
        {
            HistoryItemType.StorageContentFile => CurrentDevice.RunCrtFileOnDevice(GetPath(item.Path!))
                .ExecOnSuccess(() => HistoryManager.Add(item.Path!)),
            HistoryItemType.UploadedFile => CurrentDevice.RunUploadedCrtFile(item.ContentBytes!, item.FileName)
                .ExecOnSuccess(() => HistoryManager.Add(item.FileName, item.ContentBytes!)),
            _ => Task.CompletedTask
        };

}
