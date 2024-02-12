using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UltimateRemote.Interfaces;
using UltimateRemote.Models;
using TaskStatus = UltimateRemote.Models.TaskStatus;

namespace UltimateRemote.Components.Shared;
public abstract class BaseComponent : ComponentBase, IDisposable, IAsyncDisposable
{
    [CascadingParameter] public IModalService ModalService { get; set; } = default!;

    [Inject] protected IJSRuntime JsRuntime { get; set; } = default!;

    [Inject] protected IToastService ToastService { get; set; } = default!;

    [Inject] protected DeviceManager DeviceManager { get; set; } = default!;

    [Inject] protected PreferencesManager PrefsMgr { get; set; } = default!;

    [Inject] protected StorageContentFileService FileService { get; set; } = default!;

    [Inject] protected HistoryManager HistoryManager { get; set; } = default!;

    [Inject] protected FilePickerService FilePickerService { get; set; } = default!;

    protected IUltimateDevice CurrentDevice => DeviceManager.GetCurrentDevice(ToastService);

    protected ValueTask BlockPage(string message)
        => JsRuntime.BlockPage(message);

    protected ValueTask BlockPageWithConfirm<T>(string message, string invokeFunctionName,
        DotNetObjectReference<T> dotNetObjRef, bool textAlignCenter = false) where T : BaseComponent
        => JsRuntime.InvokeVoidAsync(identifier: "blockPageWithConfirm", message, invokeFunctionName, dotNetObjRef,
            textAlignCenter ? "center" : "justify");

    protected ValueTask BlockPageWithConfirmWithParam<T>(string message, string invokeFunctionName, object param,
        DotNetObjectReference<T> dotNetObjRef, bool textAlignCenter = false) where T : BaseComponent
        => JsRuntime.InvokeVoidAsync(identifier: "blockPageWithConfirmWithParam", message, invokeFunctionName, param,
            dotNetObjRef, textAlignCenter ? "center" : "justify");

    protected ValueTask UpdateBlockPageMessage(string message)
        => JsRuntime.UpdateBlockPageMessage(message);

    protected ValueTask UnBlock()
        => JsRuntime.UnBlock();

    protected ValueTask SetContentHeight()
        => JsRuntime.SetContentHeight();

    protected ValueTask SetCardActions()
        => JsRuntime.SetCardActions();

    protected ValueTask ScrollToElementWithId(string id)
        => JsRuntime.ScrollToElementWithId(id);

    protected void DisplayInfoToast(string message, string title)
        => ToastService.DisplayInfoToast(message, title);

    protected void DisplaySuccessToast(string message, string title)
        => ToastService.DisplaySuccessToast(message, title);

    protected void DisplayWarningToast(string message, string title)
        => ToastService.DisplayWarningToast(message, title);

    protected void DisplayErrorToast(string message, string title)
        => ToastService.DisplayErrorToast(message, title);

    protected async Task<T?> ExecuteUiBlockingTask<T>(Task<T> task, string blockingMessage)
    {
        var taskResult = default(T?);
        await BlockPage(message: blockingMessage);
        try
        {
            taskResult = await task;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
        
        await UnBlock();
        return taskResult;
    }

    protected async Task ExecuteUiBlockingTask(Task task, string blockingMessage)
    {
        await BlockPage(message: blockingMessage);
        await task;
        await UnBlock();
    }

    protected virtual async void BgTaskStatusChange(object? sender, BgTaskNotification notification)
    {
        if (notification.Status == TaskStatus.Ended)
            await UnBlock();

        switch (notification)
        {
            case { Status: TaskStatus.Started }:
                await BlockPage(notification.Message);
                break;
            case { Status: TaskStatus.Progress }:
                await UpdateBlockPageMessage(notification.Message);
                break;
            case { Status: TaskStatus.Ended, Success: false }:
                DisplayErrorToast(notification.Message, notification.PopupTitle);
                break;
            case { Status: TaskStatus.Ended, Success: true }:
                DisplaySuccessToast(notification.Message, notification.PopupTitle);
                break;
            default:
                await UnBlock();
                break;
        }
    }


    public virtual void Dispose()
    {
        FileService.Dispose();
    }

    public virtual async ValueTask DisposeAsync()
    {
        await FileService.DisposeAsync();
    }
}
