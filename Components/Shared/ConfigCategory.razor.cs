using Microsoft.AspNetCore.Components;
using UltimateRemote.Interfaces;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Components.Shared;
public sealed partial class ConfigCategory : BaseComponent
{
    [Parameter, EditorRequired] public string CategoryName { get; set; } = default!;

    [Parameter, EditorRequired] public IConfigCategoryItemResponse[] ConfigCategoryItems { get; set; } = default!;

    private FileTypeGroup[] RomFileTypeGroups => new[] { PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.RomFile)!, PrefsMgr.GetFileTypeGroup(FileTypeGroupNames.BinaryFile)! };

    private readonly string[] _romForSettings =
        ["ROM for 1541 mode", "ROM for 1571 mode", "ROM for 1581 mode"];

    private async Task OnConfigUpdate()
    {

    }

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

        await CurrentDevice.UpdateConfigCategorySectionValue<ConfigItemResponse>(category: CategoryName, sectionName, value: romFile).ExecOnSuccess(async () =>
        {
            DisplaySuccessToast(
                message: Strings.ConfigCategoryItem.ToastMsgDriveConfigUpdated(sectionName, value: romFile,
                    CategoryName), Strings.ConfigCategoryItem.ToastTitleDriveConfigUpdated);
            await OnConfigUpdate();
        });

    }
}
