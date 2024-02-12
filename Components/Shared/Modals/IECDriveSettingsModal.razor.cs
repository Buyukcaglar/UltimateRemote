using Microsoft.AspNetCore.Components;
using UltimateRemote.Interfaces;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Modals;
// ReSharper disable once InconsistentNaming
public sealed partial class IECDriveSettingsModal : BaseComponent
{
    [Parameter, EditorRequired] public string DriveId { get; set; } = default!;

    [Parameter, EditorRequired] public ConfigCategoryItem[] SettingValues { get; set; } = default!;

    [Parameter] public IModalReference? Self { get; set; }

    [Parameter] public string ModalTitle { get; set; } = default!;

    [Parameter] public EventCallback DriveChangedEvent { get; set; }

    private IConfigCategoryItemResponse[]? _settingValueItems;

    protected override async Task OnInitializedAsync()
    {
        await GetSetConfigSectionItems();
        await base.OnInitializedAsync();
    }

    private async Task GetSetConfigSectionItems()
    {
        if (SettingValues.Length > 0)
        {
            var itemResponseList = await CurrentDevice.GetConfigSectionItems(ModalTitle, SettingValues);
            _settingValueItems = itemResponseList.ToArray();
        }
    }

    private Task OnConfigUpdate()
        => DriveChangedEvent.InvokeAsync();

}
