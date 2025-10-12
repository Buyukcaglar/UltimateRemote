using Blazored.Toast.Services;
using LukeMauiFilePicker;
using UltimateRemote.Models;

namespace UltimateRemote.Services;

#if MACCATALYST
public sealed class FilePickerService(IFilePickerService lukeFilePickerService)
#else
public sealed class FilePickerService        
#endif
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

#if MACCATALYST
    public async Task<FilePickResult> PickFileLuke(PickOptions pickOptions)
    {
        var retVal = new FilePickResult();
        try
        {

            var pickResult = await lukeFilePickerService.PickFileAsync(pickOptions.PickerTitle!,
                new Dictionary<DevicePlatform, IEnumerable<string>>(
                    [new KeyValuePair<DevicePlatform, IEnumerable<string>>(DevicePlatform.MacCatalyst, pickOptions.FileTypes!.Value)]
                    
                    ));
            if (pickResult?.FileResult != null)
            {
                var fileStream = await pickResult.OpenReadAsync();
                retVal.FileName = pickResult.FileName;
                retVal.ContentType = pickResult.FileResult.ContentType;
                retVal.FullPath = pickResult.FileResult.FullPath;
                retVal.FileStream = fileStream;
            }
        }
        catch (Exception ex)
        {
            retVal.Exception = ex;
        }
        return retVal;
    }
#endif
    
    public async Task<(string FileName, byte[]? ContentBytes)> GetFileContentBytes(PickOptions pickOptions, IToastService? toastService = null)
    {
        var retVal = default((string FileName, byte[]? ContentBytes));
        Task<FilePickResult> pickResultTask;

#if MACCATALYST
        pickResultTask = PickFileLuke(pickOptions);
#else
        pickResultTask = PickFile(pickOptions);
#endif
        var pickResult = await pickResultTask;
        
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
