using System.Text.Json;
using Microsoft.AspNetCore.Components;
using UltimateRemote.Interfaces;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Components.Shared.Modals;

public sealed partial class FloppyDriveSettingsModal : BaseComponent
{
    [Parameter, EditorRequired] public string DriveId { get; set; } = default!;
    
    [Parameter, EditorRequired] public ConfigCategoryItem[] SettingValues { get; set; } = default!;

    [Parameter] public IModalReference? Self { get; set; }

    [Parameter] public string ModalTitle { get; set; } = default!;

    [Parameter] public EventCallback DriveChangedEvent { get; set; }

    private FileTypeGroup[] RomFileTypeGroups => new[] { PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.RomFile)!, PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.BinaryFile)!  };

    private IConfigCategoryItemResponse[]? _settingValueItems;

    private readonly List<string> _driveRomForSettings =
        ["ROM for 1541 mode", "ROM for 1571 mode", "ROM for 1581 mode"];

    protected override async Task OnInitializedAsync()
    {
        await GetConfigSectionItems();
        await base.OnInitializedAsync();
    }


    private async Task GetConfigSectionItems()
    {
        if (SettingValues.Length > 0)
        {
            var itemResponseList = await CurrentDevice.GetConfigSectionItems(ModalTitle, SettingValues);
            // WTF!!!!
            var moveToBottom = itemResponseList.Where(item => item.Section.Contains("ROM for")).ToList();
            itemResponseList.RemoveAll(item => item.Section.Contains("ROM for"));
            var diskSwapDelay = itemResponseList.FirstOrDefault(item => item.Section == "Disk swap delay");
            
            if (null != diskSwapDelay)
            {
                moveToBottom.Insert(0, diskSwapDelay);
                itemResponseList.Remove(diskSwapDelay);
            }
            
            itemResponseList.AddRange(moveToBottom);
            
            _settingValueItems = [.. itemResponseList];
        }
    }

    private Task OnConfigUpdate()
        => DriveChangedEvent.InvokeAsync();

    private async Task ResetDriveRom(string sectionName)
    {
        var romFile = sectionName switch
        {
            "ROM for 1541 mode" => "1541.rom",
            "ROM for 1571 mode" => "1571.rom",
            "ROM for 1581 mode" => "1581.rom",
            _ => ""
        };

        if (string.IsNullOrWhiteSpace(romFile))
            return;
        
        await CurrentDevice.UpdateConfigCategorySectionValue<ConfigItemResponse>(category: ModalTitle, sectionName, value: romFile).ExecOnSuccess(async () =>
        {
            DisplaySuccessToast(
                message: Strings.ConfigCategoryItem.ToastMsgDriveConfigUpdated(sectionName, value: romFile,
                    ModalTitle), Strings.ConfigCategoryItem.ToastTitleDriveConfigUpdated);
            
            await DriveChangedEvent.InvokeAsync();
        });

    }
}
