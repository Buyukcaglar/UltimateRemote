using Blazored.Toast.Services;
using UltimateRemote.Models;

namespace UltimateRemote.Services;
public sealed class FilePickerService
{
    public async Task<FilePickResult> PickFile(PickOptions pickOptions)
    {
        var retVal = new FilePickResult();
        try
        {
            var pickResult = await FilePicker.PickAsync(pickOptions);
            if (pickResult != null)
            {
                var fileStream = await pickResult.OpenReadAsync();
                retVal.FileName = pickResult.FileName;
                retVal.ContentType = pickResult.ContentType;
                retVal.FullPath = pickResult.FullPath;
                retVal.FileStream = fileStream;
            }
        }
        catch (Exception ex)
        {
            retVal.Exception = ex;
        }
        return retVal;
    }

    public async Task<(string FileName, byte[]? ContentBytes)> GetFileContentBytes(PickOptions pickOptions, IToastService? toastService = null)
    {
        var retVal = default((string FileName, byte[]? ContentBytes));
        var pickResult = await PickFile(pickOptions);
        
        if (!pickResult.Success)
        {
            toastService?.DisplayErrorToast(message: Strings.FilePickerService.ToastMsgFileSelectFailed(pickResult.Exception?.Message), 
                title: Strings.FilePickerService.ToastTitleFileSelectFailed);
            return retVal;
        }

        if (pickResult.HasFile)
        {
            using var memoryStream = new MemoryStream();
            await pickResult.FileStream!.CopyToAsync(memoryStream);
            retVal = new ValueTuple<string, byte[]?>(pickResult.FileName!, memoryStream.ToArray());
        }

        await pickResult.DisposeAsync();
        return retVal;
    }

}
