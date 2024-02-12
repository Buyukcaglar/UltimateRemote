using Microsoft.AspNetCore.Components;
using UltimateRemote.Interfaces;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Modals;
public sealed partial class PrinterEmuSettingsModal : BaseComponent
{
    [Parameter, EditorRequired] public string DriveId { get; set; } = default!;

    [Parameter, EditorRequired] public ConfigCategoryItem[] SettingValues { get; set; } = default!;

    [Parameter] public IModalReference? Self { get; set; }

    [Parameter] public string ModalTitle { get; set; } = default!;

    [Parameter] public Func<Task>? OnDriveChange { get; set; }

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
        => null == OnDriveChange ? Task.CompletedTask : OnDriveChange();

}
