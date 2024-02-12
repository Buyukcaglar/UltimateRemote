using Microsoft.AspNetCore.Components;
using UltimateRemote.Components.Shared.FormInputs;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Components.Shared;
public sealed partial class ConfigCategoryItem<T> : BaseComponent
{
    private enum EditMode { Unknown, FileSelector, Dropdown, TextInput, NumericInput }
    
    [Parameter, EditorRequired] public string SectionName { get; set; } = default!;

    [Parameter, EditorRequired] public string CategoryName { get; set; } = default!;

    [Parameter, EditorRequired] public ConfigCategoryItemResponse<T> ConfigSettings { get; set; } = default!;

    [Parameter] public FileTypeGroup[] FileTypeGroups { get; set; } = Array.Empty<FileTypeGroup>();

    [Parameter] public EventCallback ConfigUpdatedEvent { get; set; }

    [Parameter] public RenderFragment? DefaultValueContent { get; set; }

    private DeviceLocation[] _enabledLocations = default!;

    private bool _displayCurrentValue = true;

    private bool EnableFileSelector => FileTypeGroups.Length > 0 &&
                                       null == ConfigSettings.Values &&
                                       null == ConfigSettings.MaxVal &&
                                       null == ConfigSettings.MinVal;

    private bool EnableDropDown => null != ConfigSettings.Values ||
                                   (null != ConfigSettings.MinVal && null != ConfigSettings.MaxVal &&
                                    (int)(Convert.ChangeType(ConfigSettings.MaxVal, typeof(int))) -
                                    (int)(Convert.ChangeType(ConfigSettings.MinVal, typeof(int))) < 6);

    private bool EnableNumericInput => ((null != ConfigSettings.MinVal && null != ConfigSettings.MaxVal &&
                                         (int)(Convert.ChangeType(ConfigSettings.MaxVal, typeof(int))) -
                                         (int)(Convert.ChangeType(ConfigSettings.MinVal, typeof(int))) > 5)) || (null != ConfigSettings.MinVal || null != ConfigSettings.MaxVal);

    private bool EnableTextInput => FileTypeGroups.Length == 0 &&
                                       null == ConfigSettings.Values &&
                                       null == ConfigSettings.MaxVal &&
                                       null == ConfigSettings.MinVal;

    private EditMode Mode => EnableFileSelector ? EditMode.FileSelector :
        EnableDropDown ? EditMode.Dropdown :
        EnableNumericInput ? EditMode.NumericInput :
        EnableTextInput ? EditMode.TextInput : EditMode.Unknown;

    private T[] _dropdownItems = Array.Empty<T>();
    private T? _selectedDropdownItem;

    private int _numericMin;
    private int _numericMax;
    private string _numericInputVal = string.Empty;

    protected override void OnInitialized()
    {
        if (Mode == EditMode.Dropdown)
        {
            if (ConfigSettings.Values != null)
            {
                _dropdownItems = ConfigSettings.Values;
            }

            if (ConfigSettings.Values == null)
            {
                var min = (int)Convert.ChangeType(ConfigSettings.MinVal!, typeof(int));
                var max = (int)Convert.ChangeType(ConfigSettings.MaxVal!, typeof(int));
                var diff = (max - min) + 1;
                // Cast<T> .. nonsense ....
                _dropdownItems = Enumerable.Range(min, diff).Cast<T>().ToArray();
            }

            if (null != ConfigSettings.Current)
                _selectedDropdownItem = ConfigSettings.Current;
            _displayCurrentValue = false;
        }

        if (Mode == EditMode.NumericInput)
        {
            _numericMin = (int)Convert.ChangeType(ConfigSettings.MinVal!, typeof(int));
            _numericMax = (int)Convert.ChangeType(ConfigSettings.MaxVal!, typeof(int));
            _numericInputVal = (string)Convert.ChangeType(ConfigSettings.Current, typeof(string))!;
        }

        _enabledLocations = PrefsMgr.EnabledDeviceLocations.ToArray();

        base.OnInitialized();
    }

    private async Task DropdownItemSelected(T selectedItem)
    {
        if (null != _selectedDropdownItem && _selectedDropdownItem.Equals(selectedItem))
            return;
        await UpdateConfig(selectedItem!.ToString()!);
    }

    private Task OnFileSelection(FileSelectorModel selectedFile)
        => UpdateConfig(selectedFile.LocationPath);

    private Task OnNumericInputCommit()
        => UpdateConfig(_numericInputVal);

    private Task OnTextInputCommit(string? value)
        => string.IsNullOrWhiteSpace(value) ? Task.CompletedTask : UpdateConfig(value);

    private Task UpdateConfig(string value)
        => CurrentDevice.UpdateConfigCategorySectionValue<ConfigItemResponse>(CategoryName, SectionName, value)
            .ExecOnSuccess(async () =>
            {
                DisplaySuccessToast(message: Strings.ConfigCategoryItem.ToastMsgDriveConfigUpdated(SectionName, value, CategoryName), 
                    Strings.ConfigCategoryItem.ToastTitleDriveConfigUpdated);
                ConfigSettings.Current = (T?)Convert.ChangeType(value, typeof(T?));
                await ConfigUpdatedEvent.InvokeAsync();
            });

}
