using Blazored.Toast.Services;
using LukeMauiFilePicker;
using UltimateRemote.Models;

namespace UltimateRemote.Services;

#if MACCATALYST
public sealed class FilePickerService(IFilePickerService filePicker)
#endif
#if !MACCATALYST
public sealed class FilePickerService
#endif
{
    public async Task<FilePickResult> PickFile(PickOptions pickOptions)
    {
        var retVal = new FilePickResult();
        try
        {
#if !MACCATALYST
            var pickResult = await FilePicker.PickAsync(pickOptions);
            if (pickResult != null)
            {
                var fileStream = await pickResult.OpenReadAsync();
                retVal.FileName = pickResult.FileName;
                retVal.ContentType = pickResult.ContentType;
                retVal.FullPath = pickResult.FullPath;
                retVal.FileStream = fileStream;
            }
#endif
#if MACCATALYST
            var pickerOptions = FilePickerOptions.GetFileTypes(pickOptions);
            var pickResult = await filePicker.PickFileAsync(pickerOptions.Title, pickerOptions.FileTypes);
            if (pickResult?.FileResult != null)
            {
                var fileStream = await pickResult.OpenReadAsync();
                retVal.FileName = pickResult.FileName;
                retVal.ContentType = pickResult.FileResult.ContentType;
                retVal.FullPath = pickResult.FileResult.FullPath;
                retVal.FileStream = fileStream;
            }
#endif
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
