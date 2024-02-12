using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Timers;
using Blazored.Toast.Services;
using Microsoft.JSInterop;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Modals;

public sealed partial class StorageContentFileSearch : ComponentBase, IDisposable
{
    [Inject] private DeviceManager DeviceManager { get; set; } = default!;

    [Inject] private PreferencesManager PrefsMgr { get; set; } = default!;

    [Inject] private IToastService ToastService { get; set; } = default!;

    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    [Parameter] public IModalReference? Self { get; set; }

    [Parameter] public bool IgnoreApiV0174CharWarning { get; set; }

    [Parameter] public bool DisableDeviceRegistryCheck { get; set; }

    [Parameter] public FileTypeGroup[] FileTypeGroups { get; set; } = default!;

    [Parameter] public DeviceStorageFileList FileList { get; set; } = default!;

    [Parameter] public string ModalTitle { get; set; } = default!;

    [Parameter] public int ResultLimit { get; set; } = 100;

    private System.Timers.Timer _timer = default!;

    private string? _inputLabel;
    private string? _searchPhrase;
    private bool _searchOnlyFileNames;
    private bool _selectionEnabled = true;
    private bool _enforceApiV01CharLimit;
    private string? _currentDeviceName;
    private List<string>? _searchResult;
    private string[]? _searchInExtensions;
    private SelectOption[]? _fileTypeGroupOptions;

    private StorageFile[]? _searchSet;

    private string? ExtensionsIndicator => 
        Strings.ContentListSearch.ExtensionsIndicator(_searchInExtensions);

    protected override void OnInitialized()
    {
        ConfigureAllowedExtensions();
        SetupTypeTimer();
        CheckCurrentDevice();

        _inputLabel = Strings.ContentFileService.InputLabel(FileList.Name, FileList.FileCount);

        base.OnInitialized();
    }

    private void CheckCurrentDevice()
    {
        var currentDevice = DeviceManager.GetCurrentDevice(ToastService);
        
        if (!DisableDeviceRegistryCheck && currentDevice.Type == UltimateDeviceType.None)
        {
            _selectionEnabled = false;
            return;
        }

        if (!IgnoreApiV0174CharWarning && currentDevice.Version == Strings.ApiVersions.V01)
        {
            _currentDeviceName = currentDevice.Name;
            if(PrefsMgr.ApiV01CharLimitEnforcement)
                _enforceApiV01CharLimit = true;
        }
    }

    private void SetupTypeTimer()
    {
        _timer = new System.Timers.Timer(250);
        _timer.Elapsed += OnTimeOut;
        _timer.AutoReset = false;
    }

    private void ConfigureAllowedExtensions()
    {
        switch (FileTypeGroups.Length)
        {
            case 1:
                _searchInExtensions = FileTypeGroups[0].Extensions;
                ThreadPoolHelper.RegisterBgTask(async () =>
                    {
                        await JsRuntime.BlockPage(Strings.FileSelector.BpMsgRetrievingFileCacheFor(FileList.Name));
                        _searchSet = FileList.GetCachedFiles(_searchInExtensions);
                        await JsRuntime.UnBlock();
                    }, TimeSpan.Zero, true);
                break;
            case > 1:
                _fileTypeGroupOptions = FileTypeGroups.ToList().ToSelectOptions();
                ThreadPoolHelper.RegisterBgTask(async () =>
                {
                    await JsRuntime.BlockPage(Strings.FileSelector.BpMsgRetrievingFileCacheFor(FileList.Name));
                    _searchSet = FileList.GetCachedFiles(FileTypeGroups.SelectMany(ftg => ftg.Extensions).Distinct().ToArray());
                    await JsRuntime.UnBlock();
                }, TimeSpan.Zero, true);
                break;
        }
    }

    private Task ItemSelected(string file)
    {
        Self?.Close(ModalResult.Ok<string>(file));
        return Task.CompletedTask;
    }

    private async Task CopyToClipboard(string file)
    {
        await Clipboard.Default.SetTextAsync(file);
        ToastService.DisplayInfoToast(file, Strings.Generic.CopiedToClipboard);
        Self?.Close(ModalResult.Cancel());
    }

    private Task SearchInExtensions(string selectedItem)
    {
        if (selectedItem == Strings.InternalTokens.AllExtensionsToken)
        {
            _searchInExtensions = null;
            _searchSet = FileList.GetCachedFiles(FileTypeGroups.SelectMany(ftg => ftg.Extensions).Distinct().ToArray());
            OnTimeOut(null, null);
            return Task.CompletedTask;
        }

        var fileTypeGroup = FileTypeGroups.FirstOrDefault(fileTypeGroup => fileTypeGroup.Name == selectedItem);
        if (fileTypeGroup != null)
        {
            _searchInExtensions = fileTypeGroup.Extensions;
            _searchSet = FileList.GetCachedFiles(_searchInExtensions);
        }

        OnTimeOut(null, null);
        return Task.CompletedTask;
    }

    private async void OnTimeOut(object? source, ElapsedEventArgs? e)
    {
        //if (FileList.Files is { Length: > 0 } && _searchPhrase is { Length: > 2 })
        //{
        //    var searchResult = !_searchOnlyFileNames
        //        ? FileList.SearchInEveryWhere(_searchPhrase, ResultLimit, _searchInExtensions)
        //        : FileList.SearchInFileNames(_searchPhrase, ResultLimit, _searchInExtensions);

        //    _searchResult = searchResult.Results;

        //    _inputLabel = Strings.ContentFileService.InputLabel(FileList.Name, searchResult.SearchSetCount,
        //        searchResult.ResultSetCount, searchResult.Limit);

        //    await InvokeAsync(StateHasChanged);
        //    return;
        //}

        if (_searchSet is { Length: > 0 } && _searchPhrase is { Length: > 2 })
        {
            var searchResult = _searchSet.Search(_searchPhrase, _searchOnlyFileNames, ResultLimit);

            _searchResult = searchResult.Results;

            _inputLabel = Strings.ContentFileService.InputLabel(FileList.Name, searchResult.SearchSetCount,
                searchResult.ResultSetCount, searchResult.Limit);

            await InvokeAsync(StateHasChanged);
            return;
        }

        if (_searchResult == null)
            return;

        _inputLabel = Strings.ContentListSearch.InputLabel(FileList.Name, FileList.FileCount);

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

    public void Dispose()
    {
        _timer.Dispose();
    }
}
