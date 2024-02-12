using System.Timers;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Modals;

public sealed partial class HvscSidFileSearchModal : ComponentBase, IDisposable
{
    [Inject] private JukeboxService JukeboxService { get; set; } = default!;

    [Parameter] public IModalReference? Self { get; set; }

    [Parameter] public string ModalTitle { get; set; } = "Search HVSC SID File";

    [Parameter] public bool MultiSelect { get; set; } = true;

    [Parameter] public int ResultLimit { get; set; } = 100;

    private System.Timers.Timer _timer = default!;
    
    private string InputLabel => Strings.HvscSidFileSearch.InputLabel(
        total: JukeboxService.Library.Length,
        count: _searchResult?.Length ?? 0,
        limit: ResultLimit,
        selectedCount: _selectedItems.Count);
    private string? _searchPhrase;
    private SidFileInfo[]? _searchResult;
    private readonly List<SidFileInfo> _selectedItems = [];

    protected override void OnInitialized()
    {
        SetupTypeTimer();
        base.OnInitialized();
    }

    private void SetupTypeTimer()
    {
        _timer = new System.Timers.Timer(250);
        _timer.Elapsed += OnTimeOut;
        _timer.AutoReset = false;
    }

    private Task ItemSelected(SidFileInfo selectedItem)
        => CloseModal([selectedItem]);

    private Task OnItemSelect((SidFileInfo Item, bool Selected) itemSelect)
    {
        if(itemSelect.Selected)
            _selectedItems.Add(itemSelect.Item);
        else
            _selectedItems.Remove(itemSelect.Item);

        return Task.CompletedTask;
    }

    private Task AddItems()
    {
        if (_selectedItems.Count > 0)
            return CloseModal(_selectedItems.ToArray());

        Self?.Close(ModalResult.Cancel());
        return Task.CompletedTask;
    }

    private async void OnTimeOut(object? source, ElapsedEventArgs? e)
    {
        if (JukeboxService.Library.Length > 0 && _searchPhrase is { Length: > 2 })
        {
            _searchResult = JukeboxService.Library.Search(_searchPhrase, ResultLimit);
            await InvokeAsync(StateHasChanged);
            return;
        }

        if (_searchResult == null)
            return;
        
        _searchResult = null;
        
        await InvokeAsync(StateHasChanged);
    }
    private Task ValueChanged(string? value)
    {
        _timer.Stop();
        _timer.Start();
        _searchPhrase = value;

        return Task.CompletedTask;
    }

    private Task CloseModal(SidFileInfo[] selectedTunes)
    {
        Self?.Close(ModalResult.Ok<SidFileInfo[]>(selectedTunes));
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}
