using UltimateRemote.Components.Shared.FormInputs;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class PlaySidMusic : BaseFileFunctionComponent
{
    protected override FileTypeGroup[] AllowedFileTypeGroups => new FileTypeGroup[]
        { PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.SidFile)! };

    private string? _songNr;

    private int SongNumber => int.TryParse(_songNr, out int songNr) ? songNr : 1;

    private Task ButtonClicked(FileSelectorModel selectedFile)
        => CurrentDevice.PlayOnDeviceSidFile(selectedFile.LocationPath, SongNumber)
            .ExecOnSuccess(() => HistoryManager.Add(selectedFile.Path));

    private async Task UploadFile()
    {
        var fileContent = await ExecuteUiBlockingTask(task: FilePickerService.GetFileContentBytes(FilePickerOptions.SidFileOptions, ToastService),
            blockingMessage: Strings.ContentListManager.BpMsgSelectFile(FileTypeGroupNames.SidFile));

        if (fileContent is { ContentBytes.Length: > 0 })
        {
            await CurrentDevice.PlayUploadedSidFile(fileContent.ContentBytes, fileContent.FileName, SongNumber)
                .ExecOnSuccess(() => HistoryManager.Add(fileContent.FileName, fileContent.ContentBytes));
        }
    }

    private Task HistoryItemSelected(HistoryItem item)
        => item.Type switch
        {
            HistoryItemType.StorageContentFile => CurrentDevice.PlayOnDeviceSidFile(GetPath(item.Path!), SongNumber)
                .ExecOnSuccess(() => HistoryManager.Add(item.Path!)),
            HistoryItemType.UploadedFile => CurrentDevice.PlayUploadedSidFile(item.ContentBytes!, item.FileName, SongNumber)
                .ExecOnSuccess(() => HistoryManager.Add(item.FileName, item.ContentBytes!)),
            _ => Task.CompletedTask
        };

    
}
