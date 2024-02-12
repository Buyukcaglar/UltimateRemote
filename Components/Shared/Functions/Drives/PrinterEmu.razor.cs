using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using UltimateRemote.Components.Shared.Modals;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Components.Shared.Functions.Drives;
public sealed partial class PrinterEmu : BaseComponent
{
    [Parameter, EditorRequired] public KeyValuePair<string, DriveInfoResponse> DriveInfo { get; set; } = default!;

    [Parameter] public EventCallback DriveChangedEvent { get; set; }

    private KeyValuePair<string, System.Text.Json.JsonElement?>[]? _settings;

    private string CardTitle => $"{DriveInfo.Key} {DriveInfo.Value.BusId}";

    private string DriveIconClass =>
        $"printer {(DriveInfo.Value.Enabled ? "indicate-online" : "indicate-offline")}";

    private string GetLabelCss(int index, int length)
        => index == 0 ? "rounded-top" : index == length - 1 ? "border-top-0 rounded-bottom" : "border-top-0";

    protected override async Task OnInitializedAsync()
    {
        await GetSettings();
        await base.OnInitializedAsync();
    }

    private async Task GetSettings()
    {
        var driveConfig = await CurrentDevice.GetConfigCategory<ConfigCategoryResponse>("Printer Settings");
        var driveSettings = driveConfig?.GetValue<Dictionary<string, System.Text.Json.JsonElement?>>("Printer Settings");
        if (null != driveSettings)
        {
            _settings = driveSettings
                .Where(keyValuePair => null != keyValuePair.Value)
                .ToArray();
        }
    }

    private async Task TogglePower()
    {
        // In case of Drive Info is not up-to-date
        var currentPowerState = DriveInfo.Value.Enabled;
        await DriveChangedEvent.InvokeAsync();
        var updatedPowerState = DriveInfo.Value.Enabled;

        if (currentPowerState == updatedPowerState)
        {
            await CurrentDevice.UpdateConfigCategorySectionValue<ConfigItemResponse>(
                    category: "Printer Settings", section: "IEC printer", 
                    value: !currentPowerState ? "Enabled" : "Disabled")
                .ExecOnSuccess(async () =>
                {
                    await GetSettings();
                    await DriveChangedEvent.InvokeAsync();
                });
        }
    }

    private async Task DisplayConfigurationPopUp()
    {
        var driveId = DriveInfo.Key.ToUpperInvariant();
        var slug = "Printer Settings";

        var driveConfig = await CurrentDevice.GetConfigCategory<ConfigCategoryResponse>(slug);
        var driveSettings = driveConfig?.GetValue<Dictionary<string, System.Text.Json.JsonElement?>>(slug);
        
        if(driveSettings == null)
            return;

        var driveSettingValues = driveSettings.Keys.Where(key => driveSettings[key].HasValue)
            .Select(key => new ConfigCategoryItem(key, driveSettings[key]!.Value.ValueKind)).ToArray();

        var modalParams = new ModalParameters()
        {
            { nameof(PrinterEmuSettingsModal.DriveId), driveId },
            { nameof(PrinterEmuSettingsModal.SettingValues), driveSettingValues },
            { nameof(PrinterEmuSettingsModal.ModalTitle), slug },
            { nameof(PrinterEmuSettingsModal.OnDriveChange), async () =>
            {
                await GetSettings();
                await DriveChangedEvent.InvokeAsync();
            } }
        };

        var searchFileListModal = ModalService.Show<PrinterEmuSettingsModal>(title: "", modalParams);

        modalParams.Add(nameof(PrinterEmuSettingsModal.Self), searchFileListModal);
        
        await searchFileListModal.Result;

        await DriveChangedEvent.InvokeAsync();
    }

}
