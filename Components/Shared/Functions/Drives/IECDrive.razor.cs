using Microsoft.AspNetCore.Components;
using UltimateRemote.Components.Shared.Modals;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Components.Shared.Functions.Drives;
// ReSharper disable once InconsistentNaming
public sealed partial class IECDrive : BaseComponent
{
    [Parameter, EditorRequired] public KeyValuePair<string, DriveInfoResponse> DriveInfo { get; set; } = default!;

    [Parameter] public EventCallback DriveChangedEvent { get; set; }

    private string CardTitle => $"{DriveInfo.Key} {DriveInfo.Value.BusId}";

    private string DriveIconClass =>
        $"hard-drives {(DriveInfo.Value.Enabled ? "indicate-online" : "indicate-offline")}";

    private string? Partitions =>
        DriveInfo.Value.Partitions is { Length: > 0 }
            ? string.Join(", ", DriveInfo.Value.Partitions.Select(partition => $"Id: {partition.Id} Path:{partition.Path}"))
            : null;

    private async Task TogglePower()
    {
        // In case of Drive Info is not up-to-date
        var currentPowerState = DriveInfo.Value.Enabled;
        await DriveChangedEvent.InvokeAsync();
        var updatedPowerState = DriveInfo.Value.Enabled;

        if (currentPowerState == updatedPowerState)
        {
            await CurrentDevice.UpdateConfigCategorySectionValue<ConfigItemResponse>(
                category: "SoftIEC Drive Settings", section: "IEC Drive", 
                value: !currentPowerState ? "Enabled" : "Disabled")
                .ExecOnSuccess(() => DriveChangedEvent.InvokeAsync());
        }
    }

    private async Task DisplayConfigurationPopUp()
    {
        var driveId = DriveInfo.Key.ToUpperInvariant();
        var slug = "SoftIEC Drive Settings";

        var driveConfig = await CurrentDevice.GetConfigCategory<ConfigCategoryResponse>(slug);
        var driveSettings = driveConfig?.GetValue<Dictionary<string, System.Text.Json.JsonElement?>>(slug);
        
        if(driveSettings == null)
            return;

        var driveSettingValues = driveSettings.Keys.Where(key => driveSettings[key].HasValue)
            .Select(key => new ConfigCategoryItem(key, driveSettings[key]!.Value.ValueKind)).ToArray();

        var modalParams = new ModalParameters()
        {
            { nameof(IECDriveSettingsModal.DriveId), driveId },
            { nameof(IECDriveSettingsModal.SettingValues), driveSettingValues },
            { nameof(IECDriveSettingsModal.ModalTitle), slug },
            { nameof(IECDriveSettingsModal.DriveChangedEvent), DriveChangedEvent }
        };

        var searchFileListModal = ModalService.Show<IECDriveSettingsModal>(title: "", modalParams);

        modalParams.Add(nameof(IECDriveSettingsModal.Self), searchFileListModal);
        
        await searchFileListModal.Result;

        await DriveChangedEvent.InvokeAsync();
    }

}
