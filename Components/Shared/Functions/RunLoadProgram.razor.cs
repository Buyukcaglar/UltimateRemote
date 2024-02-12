using UltimateRemote.Components.Shared.FormInputs;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class RunLoadProgram : BaseFileFunctionComponent
{
    protected override FileTypeGroup[] AllowedFileTypeGroups =>
        new[] { PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.Program)! };

    private ActionType _actionType;
    private readonly ActionType[] _actionTypes = ActionType.Run.GetEnumValues();

    private Task ButtonClicked(FileSelectorModel selectedFile)
        => _actionType switch
        {
            ActionType.Run => CurrentDevice.RunPrgFileOnDevice(selectedFile.LocationPath)
                .ExecOnSuccess(() => HistoryManager.Add(selectedFile.Path)),
            ActionType.Load => CurrentDevice.LoadPrgFileOnDevice(selectedFile.LocationPath)
                .ExecOnSuccess(() => HistoryManager.Add(selectedFile.Path)),
            _ => Task.CompletedTask
        };

    private async Task UploadFile()
    {
        var fileContent = await ExecuteUiBlockingTask(task: FilePickerService.GetFileContentBytes(FilePickerOptions.ProgramFileOptions, ToastService),
            blockingMessage: Strings.ContentListManager.BpMsgSelectFile(FileTypeGroupNames.Program));

        if (fileContent is { ContentBytes.Length: > 0 })
        {
            switch (_actionType)
            {
                case ActionType.Run:
                    await CurrentDevice.RunUploadedPrgFile(fileContent.ContentBytes, fileContent.FileName)
                        .ExecOnSuccess(() => HistoryManager.Add(fileContent.FileName, fileContent.ContentBytes));
                    break;
                case ActionType.Load:
                    await CurrentDevice.LoadUploadedPrgFile(fileContent.ContentBytes, fileContent.FileName)
                        .ExecOnSuccess(() => HistoryManager.Add(fileContent.FileName, fileContent.ContentBytes));
                    break;
                default:
                    return;
            }
        }
    }

    private Task HistoryItemSelected(HistoryItem item)
        => (ItemType: item.Type, Action: _actionType) switch
        {
            (HistoryItemType.StorageContentFile, ActionType.Run) => CurrentDevice.RunPrgFileOnDevice(GetPath(item.Path!))
                .ExecOnSuccess(() => HistoryManager.Add(item.Path!)),
            (HistoryItemType.UploadedFile, ActionType.Run) => CurrentDevice.RunUploadedPrgFile(item.ContentBytes!, item.FileName)
                .ExecOnSuccess(() => HistoryManager.Add(item.FileName, item.ContentBytes!)),
            (HistoryItemType.StorageContentFile, ActionType.Load) => CurrentDevice
                .LoadPrgFileOnDevice(GetPath(item.Path!))
                .ExecOnSuccess(() => HistoryManager.Add(item.Path!)),
            (HistoryItemType.UploadedFile, ActionType.Load) => CurrentDevice.LoadUploadedPrgFile(item.ContentBytes!, item.FileName)
                .ExecOnSuccess(() => HistoryManager.Add(item.FileName, item.ContentBytes!)),
            _ => Task.CompletedTask
        };

}
