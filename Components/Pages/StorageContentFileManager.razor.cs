using Microsoft.AspNetCore.Components;
using UltimateRemote.Components.Shared;
using UltimateRemote.Components.Shared.Modals;
using UltimateRemote.Models;
using TaskStatus = UltimateRemote.Models.TaskStatus;

namespace UltimateRemote.Components.Pages;

[Route(Blazor.RouteTemplates.StorageManager)]
public sealed partial class StorageContentFileManager : BaseComponent
{
    private StorageFile[]? _importList;
    private FilePickResult? _filePickResult;

    private string ListName => Strings.ContentListManager.ListName(_importList?.Length);

    protected override async Task OnInitializedAsync()
    {
        FileService.ImportStatusChangedEvent -= BgTaskStatusChange;
        FileService.ImportStatusChangedEvent += BgTaskStatusChange;

#if ANDROID
        await CheckAndroidStoragePermissions();
#else 
        await CheckStoragePermissions();
#endif
        await base.OnInitializedAsync();
    }

    private async Task CheckStoragePermissions()
    {
        var storageReadPermission = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (storageReadPermission != PermissionStatus.Granted)
        {
            var reqStorageReadPermissionResult = await Permissions.RequestAsync<Permissions.StorageRead>();
            if (reqStorageReadPermissionResult != PermissionStatus.Granted)
            {
                DisplayWarningToast(message: Strings.ContentListManager.ToastMsgStorageReadPermissionNotGranted,
                    title: Strings.ContentListManager.ToastTitleStorageReadPermissionNotGranted);
            }
        }
    }

#if ANDROID
    private Task CheckAndroidStoragePermissions()
    {
        var status = AndroidX.Core.Content.ContextCompat.CheckSelfPermission(Android.App.Application.Context, Android.Manifest.Permission.ReadExternalStorage);
        if (status != Android.Content.PM.Permission.Granted)
        {
            if (AndroidX.Core.App.ActivityCompat.ShouldShowRequestPermissionRationale(Platform.CurrentActivity!,
                    Android.Manifest.Permission.ReadExternalStorage))
            {
                var toast = Android.Widget.Toast.MakeText(Android.App.Application.Context,
                    "Storage Read Permission is required!", Android.Widget.ToastLength.Long);
                if(toast != null)
                    toast.Show();
            }
            AndroidX.Core.App.ActivityCompat.RequestPermissions(Platform.CurrentActivity!, new[] { Android.Manifest.Permission.ReadExternalStorage }, 0);
        }

        return Task.CompletedTask;
    }    
#endif

    protected override async void BgTaskStatusChange(object? sender, BgTaskNotification notification)
    {
        if (notification.Status == TaskStatus.Ended)
        {
            _filePickResult = null;
            if (notification is { Success: true, Tag: nameof(StorageContentFileService.ImportContentFile) })
            {
                _importList = notification.GetData<StorageFile[]>();
                await InvokeAsync(StateHasChanged);
            }
        }

        base.BgTaskStatusChange(sender, notification);
    }

    private async Task ImportStorageContentListFile()
    {
        var filePickResult = await ExecuteUiBlockingTask(FileService.PickImportFile(),
            Strings.ContentListManager.BpMsgSelectFile("import file"));

        if (!filePickResult!.Success)
        {
            DisplayErrorToast(message: Strings.FilePickerService.ToastMsgFileSelectFailed(filePickResult.Exception?.Message),
                title: Strings.FilePickerService.ToastTitleFileSelectFailed);
            return;
        }

        if (!filePickResult.HasFile)
            return;

        _filePickResult = filePickResult;

        ThreadPoolHelper.RegisterBgTask(FileService.ImportContentFile);

    }

    private async Task CancelImport()
    {
        FileService.FinalizeImport();

        if (_filePickResult != null)
        {
            await _filePickResult.DisposeAsync();
            _filePickResult = null;
        }
    }

    private async Task SaveList(string? listName)
    {
        if (string.IsNullOrWhiteSpace(listName))
        {
            DisplayErrorToast(message: Strings.ContentListManager.ToastMsgListNameCanNotBeEmpty,
                title: Strings.ContentListManager.ToastTitleListNameCanNotBeEmpty);
            return;
        }

        listName = listName.Trim();

        if (FileService.StorageFileLists.Any(file => file.Name == listName))
        {
            DisplayErrorToast(Strings.ContentListManager.ToastMsgDuplicateListName(listName),
                Strings.ContentListManager.ToastTitleDuplicateListName);
            return;
        }

        await BlockPage(message: Strings.ContentListManager.BpMsgSavingList(listName));

        ThreadPoolHelper.RegisterBgTask(async () => {
            if (_importList is { Length: > 0 } && !string.IsNullOrWhiteSpace(listName))
            {
                try
                {
                    FileService.SaveFileList(listName, _importList);
                }
                catch (OutOfMemoryException)
                {
                    DisplayErrorToast(Strings.ContentListManager.ToastMsgSaveListFailedOutOfMemory,
                        Strings.ContentListManager.ToastTitleSaveListFailed);
                    await DeleteList(listName);
                }
                catch (Exception ex)
                {
                    DisplayErrorToast(Strings.ContentListManager.ToastMsgSaveListFailed(ex.Message),
                        Strings.ContentListManager.ToastTitleSaveListFailed);
                }
                finally
                {
                    _importList = null;
                    await UnBlock();
                    await InvokeAsync(StateHasChanged);
                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
                }
            }
        });
    }

    private async Task DeleteList(string listName)
    {
        await ExecuteUiBlockingTask(Task.Run(() => FileService.DeleteFileList(listName)),
            Strings.ContentListManager.BpMsgDeletingList(listName));
        await InvokeAsync(StateHasChanged);
    }

    private Task DisplayExtInfo(string listName)
    {
        var importFile = FileService.GetFileList(listName);
        if (null == importFile)
            return Task.CompletedTask;

        ModalService.DisplayModal<StorageFileExtInfoModal>($"File Types in '{importFile.Name}'",
            (nameof(StorageFileExtInfoModal.Extensions), importFile.Extensions));

        return Task.CompletedTask;
    }



}
