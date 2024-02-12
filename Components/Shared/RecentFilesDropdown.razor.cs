using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared;
public sealed partial class RecentFilesDropdown : BaseComponent
{
    [Parameter, EditorRequired] public FileTypeGroup[] FileTypeGroups { get; set; } = default!;

    [Parameter] public EventCallback<HistoryItem> ItemSelectedEvent { get; set; }

    private HistoryItem[]? _recentFiles;

    protected override void OnInitialized()
    {
        HistoryManager.HistoryItemsUpdatedEvent -= OnHistoryUpdate;
        HistoryManager.HistoryItemsUpdatedEvent += OnHistoryUpdate;
        
        SetUpRecentFiles(false);
        
        base.OnInitialized();
    }

    private void SetUpRecentFiles(bool invokeStateChange)
    {
        if (PrefsMgr.HistoryEnabled)
        {
            _recentFiles = HistoryManager.
                GetRecentFilesOfType(FileTypeGroups)?
                .ToArray();
            
            if(invokeStateChange)
                InvokeAsync(StateHasChanged);
        }
    }

    private void OnHistoryUpdate(object? sender, EventArgs eventArgs)
    {
        SetUpRecentFiles(true);
    }

    private static string GetHistoryItemIcon(HistoryItem historyItem)
        => historyItem.Type switch
        {
            HistoryItemType.StorageContentFile => "usb ph-duotone",
            HistoryItemType.UploadedFile => "upload ph-duotone",
            _ => ""
        };

    private Task OnItemSelected(HistoryItem item)
        => ItemSelectedEvent.InvokeAsync(item);

}
