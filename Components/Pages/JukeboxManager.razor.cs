using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UltimateRemote.Components.Shared;
using UltimateRemote.Components.Shared.Modals;
using UltimateRemote.Models;
using TaskStatus = UltimateRemote.Models.TaskStatus;

namespace UltimateRemote.Components.Pages;

[Route(Blazor.RouteTemplates.JukeboxManager)]
public sealed partial class JukeboxManager : BaseComponent, IDisposable
{
    [Inject] private JukeboxService JukeboxService { get; set; } = default!;

    private IJSObjectReference? _js;
    private DotNetObjectReference<JukeboxManager>? _dotNetRef;

    private string? _playlistId;

    protected override async Task OnInitializedAsync()
    {
        JukeboxService.ImportStatusChangedEvent -= OnImportStatusChange;
        JukeboxService.ImportStatusChangedEvent += OnImportStatusChange;
        
        _dotNetRef ??= DotNetObjectReference.Create(this);
        
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
            await RegisterPlaylistNameChangeEvents();

        if (!string.IsNullOrWhiteSpace(_playlistId))
        {
            var playlistId = new string(_playlistId);
            _playlistId = null;
            await RegisterPlaylistNameChangeEvent(playlistId);
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task ImportHvscPrompt()
        => await BlockPageWithConfirm(Strings.JukeboxManager.ImportWarningPrompt, nameof(ImportHvsc), _dotNetRef!);

    private Task AddPlaylist()
    {
        var newPlayList = new JukeboxPlaylist() { Name = "Click to rename" };
        _playlistId = newPlayList.Id;
        JukeboxService.AddPlaylist(newPlayList);
        return Task.CompletedTask;
    }

    private ValueTask RemovePlayListConfirm(int playlistIndex, string playlistName)
        => BlockPageWithConfirmWithParam(Strings.JukeboxManager.RemovePlaylistWarning(playlistName), nameof(RemovePlaylist),
            playlistIndex, _dotNetRef!);


    private void RemoveItem(int playlistIndex, int itemIndex)
        => JukeboxService.RemovePlaylistItem(playlistIndex, itemIndex);

    private async Task DisplayTuneSearchModal(int playlistIndex)
    {
        var modalParams = new ModalParameters();

        var itemConfigModal = ModalService.Show<HvscSidFileSearchModal>(title: "", modalParams);

        modalParams.Add(nameof(HvscSidFileSearchModal.Self), itemConfigModal);

        var result = await itemConfigModal.Result;

        if (result is { Confirmed: true, Data: not null })
        {
            var selectedItems = (SidFileInfo[]) result.Data;
            JukeboxService.AddPlaylistItems(playlistIndex, selectedItems);
            JukeboxService.SaveCurrentPlaylists();
        }
    }

    [JSInvokable]
    public async Task RemovePlaylist(int layoutIndex)
    {
        JukeboxService.RemovePlaylist(layoutIndex);
        await InvokeAsync(StateHasChanged);
    }

    [JSInvokable]
    public Task ImportHvsc()
    {
        JukeboxService.StartImport();
        return Task.CompletedTask;
    }

    //[JSInvokable]
    //public Task OnPlaylistNameChange(string layoutIndex, string? name)
    //{
    //    if(string.IsNullOrWhiteSpace(name))
    //        return Task.CompletedTask;

    //    JukeboxService.UpdatePlaylistName(int.Parse(layoutIndex), name);

    //    return Task.CompletedTask;
    //}

    [JSInvokable]
    public Task OnPlaylistNameChange(string playlistId, string? name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Task.CompletedTask;

        JukeboxService.UpdatePlaylistName(playlistId, name);

        return Task.CompletedTask;
    }

    private async void OnImportStatusChange(object? sender, BgTaskNotification notification)
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
                DisplayErrorToast(notification.Message, Strings.JukeboxManager.ToastTitleHVSCImportFail);
                break;
            case { Status: TaskStatus.Ended, Success: true }:
                DisplaySuccessToast(Strings.JukeboxManager.ToastMsgHVSCImportSuccess, Strings.JukeboxManager.ToastTitleHVSCImportSuccess);
                await InvokeAsync(StateHasChanged);
                break;
            default:
                await UnBlock();
                break;
        }
    }

    private async Task RegisterPlaylistNameChangeEvents()
    {
        _js ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/Pages/JukeboxManager.razor.js");
        _dotNetRef ??= DotNetObjectReference.Create(this);
        await _js.InvokeVoidAsync("registerPlaylistNameChangeEvents", _dotNetRef);
    }

    private async Task RegisterPlaylistNameChangeEvent(string playlistId)
    {
        _js ??= await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/Pages/JukeboxManager.razor.js");
        _dotNetRef ??= DotNetObjectReference.Create(this);
        await _js.InvokeVoidAsync("registerPlaylistNameChangeEvent", playlistId, _dotNetRef);
    }

    public override void Dispose()
    {
        if (_js is IDisposable jsDisposable)
            jsDisposable.Dispose();
        else if (_js != null)
            _ = _js.DisposeAsync().AsTask();
        _dotNetRef?.Dispose();

        base.Dispose();
    }
}
