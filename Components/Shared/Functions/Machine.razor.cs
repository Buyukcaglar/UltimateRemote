using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class Machine : BaseFileFunctionComponent
{
    protected override FileTypeGroup[] AllowedFileTypeGroups =>
        new[] { PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.BinaryFile)! };

    private string? _memContent;

    private string? _memAddress;

    private string? _readLength = "256";

    private string? _debugReg;

    private Task Reset()
        => CurrentDevice.ResetMachine();

    private Task Reboot()
        => CurrentDevice.RebootMachine();

    private Task Pause()
        => CurrentDevice.PauseMachine();

    private Task Resume()
        => CurrentDevice.ResumeMachine();
    private Task PowerOff()
        => CurrentDevice.PowerOffMachine();
    private async Task ReadDebugReg()
    {
        var debugRegisterResponse = await CurrentDevice.ReadDebugRegister();
        if (debugRegisterResponse is { Success: true })
        {
            _debugReg = debugRegisterResponse.Value;
            DisplaySuccessToast(message: Strings.Machine.ToastMsgDebugRegisterSuccess(debugRegisterResponse.Value),
                Strings.Machine.ToastTitleDebugRegisterReadSuccess);
        }
    }

    private async Task WriteDebugReg()
    {
        if (string.IsNullOrWhiteSpace(_debugReg))
            return;

        var debugRegisterResponse = await CurrentDevice.ReadDebugRegister();
        if (debugRegisterResponse is { Success: true })
        {
            _debugReg = debugRegisterResponse.Value;
            DisplaySuccessToast(message: Strings.Machine.ToastMsgDebugRegisterSuccess(debugRegisterResponse.Value),
                Strings.Machine.ToastTitleDebugRegisterWriteSuccess);
        }

    }


    private async Task ReadMemory()
    {

        if (!CheckMemoryAddressAndLength())
            return;

        var readLength = int.Parse(_readLength!);

        var readResult = await CurrentDevice.ReadMemory(_memAddress!, readLength);

        if (readResult is { Length: > 0 })
        {
            _memContent = BitConverter.ToString(readResult).Replace("-", "");
        }
    }

    private bool CheckMemoryAddress()
    {
        if (!string.IsNullOrWhiteSpace(_memAddress))
            return true;

        DisplayWarningToast(Strings.Machine.ToastMsgMemoryAddressCanNotBeEmpty,
            Strings.Machine.ToastTitleMemoryAddressCanNotBeEmpty);

        return false;

    }

    private bool CheckMemoryAddressAndLength()
    {
        if (!CheckMemoryAddress())
            return false;

        if (!int.TryParse(_readLength, out var readLength) || readLength == 0)
        {
            DisplayWarningToast(Strings.Machine.ToastMsgInvalidReadLength,
                Strings.Machine.ToastTitleInvalidReadLength);
            return false;
        }

        // FFFF boundary check
        var memAddress = Convert.ToInt32(_memAddress, 16);

        if ((readLength + memAddress) > 65536)
        {
            readLength = 65536 - memAddress;
            _readLength = readLength.ToString();
        }

        return true;

    }

    private async Task WriteMemory()
    {
        if (string.IsNullOrWhiteSpace(_memContent))
            return;

        if (!CheckMemoryAddress())
            return;

        if (_memContent is { Length: > 256 })
        {
            DisplayWarningToast(Strings.Machine.ToastMsgWriteContentTooBig,
                Strings.Machine.ToastTitleWriteContentTooBig);
            return;
        }

        var writeResponse = await CurrentDevice.WriteMemory(_memAddress!, hexData: _memContent!);

        if (writeResponse is { Success: true })
        {
            DisplaySuccessToast(Strings.Machine.ToastMsgWriteSuccess(writeResponse.Address),
                Strings.Machine.ToastTitleWriteContentSuccess);
        }

    }

    private async Task CopyToClipboard()
    {
        if (string.IsNullOrWhiteSpace(_memContent))
            return;
        await Clipboard.Default.SetTextAsync(_memContent);
        DisplayInfoToast(message: Strings.Machine.ToastMsgMemoryContentCopiedToClipboard,
            title: Strings.Generic.CopiedToClipboard);
    }

    private Task OnMemContentInputChange(ChangeEventArgs args)
    {
        _memContent = (string?)args.Value;
        return Task.CompletedTask;
    }

    private async Task UploadFile()
    {
        if (!CheckMemoryAddress())
            return;

        var fileContent = await ExecuteUiBlockingTask(task: FilePickerService.GetFileContentBytes(FilePickerOptions.BinaryFilesOptions, ToastService),
            blockingMessage: Strings.ContentListManager.BpMsgSelectFile(FileTypeGroupNames.BinaryFiles));

        if (fileContent is { ContentBytes.Length: > 65536 })
        {
            DisplayWarningToast(Strings.Machine.ToastMsgWriteContentFileTooBig,
                Strings.Machine.ToastTitleWriteContentFileTooBig);
            return;
        }

        if (fileContent is { ContentBytes.Length: > 0 })
        {
            // FFFF boundary check
            var contentSize = fileContent.ContentBytes.Length;
            var memAddress = Convert.ToInt32(_memAddress, 16);

            if ((contentSize + memAddress) > 65536)
            {
                DisplayWarningToast(message: Strings.Machine.ToastMsgContentOverflow,
                    Strings.Machine.ToastTitleContentOverflow);
                return;
            }

            var writeResponse = await CurrentDevice.WriteMemory(_memAddress!, fileContent.ContentBytes);

            if (writeResponse is { Success: true })
            {
                DisplaySuccessToast(message: Strings.Machine.ToastMsgWriteSuccess(writeResponse.Address),
                    Strings.Machine.ToastTitleWriteContentSuccess);
            }
        }
    }

    private Task HistoryItemSelected(HistoryItem item)
        => item.Type switch
        {
            //LibFileType.StorageContentFile => CurrentDevice.WriteMemory(GetPath(item.Path!))
            //    .ExecOnSuccess(() => HistoryManager.Add(item.Path!)),
            //LibFileType.UploadedFile => CurrentDevice.WriteMemory(item.ContentBytes!)
            //    .ExecOnSuccess(() => HistoryManager.Add(item.FileName, item.ContentBytes!)),
            _ => Task.CompletedTask
        };

}
