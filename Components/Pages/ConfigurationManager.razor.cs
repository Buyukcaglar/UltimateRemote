using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UltimateRemote.Components.Shared;
using UltimateRemote.Interfaces;
using UltimateRemote.Models;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Components.Pages;

[Route(Blazor.RouteTemplates.ConfigurationManager)]
public sealed partial class ConfigurationManager : BaseComponent, IDisposable
{
    private (string CategoryName, IConfigCategoryItemResponse[] CategoryItems)[]? _configCategories = Array.Empty<(string, IConfigCategoryItemResponse[])>();
    
    private DotNetObjectReference<ConfigurationManager>? _dotNetRef;

    protected override async Task OnInitializedAsync()
    {
        _dotNetRef ??= DotNetObjectReference.Create(this);
        await GetConfigurationCategories();
        await base.OnInitializedAsync();
    }

    private async Task GetConfigurationCategories()
    {
        if (CurrentDevice.Type == UltimateDeviceType.None)
        {
            DisplayWarningToast(message: Strings.WarningMessages.NoRegisteredDeviceFound,
                title: Strings.WarningMessages.NoRegisteredDeviceFoundTitle);
            return;
        }

        await BlockPage(Strings.ConfigurationManager.QueryingDeviceConfiguration);
        
        var configResponse = await CurrentDevice.GetConfigs();

        if (configResponse?.Categories is not {Length: > 0})
        {
            DisplayWarningToast(message: Strings.ConfigurationManager.ToastMsgWarningConfigEmpty,
                Strings.ConfigurationManager.ToastTitleWarningConfigEmpty);
            await UnBlock();
            return;
        }
        
        var configCategoryList = new List<(string CategoryName, IConfigCategoryItemResponse[] CategoryItems)>();
        
        foreach (var configCategory in configResponse.Categories)
        {
            var configCategoryResponse = await CurrentDevice.GetConfigCategory<ConfigCategoryResponse>(configCategory);
            var configCategorySections = configCategoryResponse?.GetValue<Dictionary<string, System.Text.Json.JsonElement?>>(configCategory);
            var configCategoryItems = configCategorySections?.Keys.Where(key => configCategorySections[key].HasValue)
                .Select(key => new ConfigCategoryItem(key, configCategorySections[key]!.Value.ValueKind)).ToArray();

            if (configCategoryItems is not { Length: > 0 }) 
                continue;

            await UpdateBlockPageMessage(Strings.ConfigurationManager.QueryingConfiguration(configCategory));

            var configSectionItemList = await CurrentDevice.GetConfigSectionItems(configCategory, configCategoryItems);
            
            if(configSectionItemList.Count > 0)
                configCategoryList.Add((CategoryName: configCategory, CategoryItems: configSectionItemList.ToArray()));
        }
        
        _configCategories = [.. configCategoryList];
        
        await UnBlock();
    }

    private ValueTask ConfirmConfigurationOperation(ConfigOp op)
        => BlockPageWithConfirmWithParam(message: Strings.ConfigurationManager.BpWarningMsgConfigOp(op),
            nameof(PerformConfigurationOperation), op, _dotNetRef!);

    [JSInvokable]
    public Task PerformConfigurationOperation(ConfigOp op)
        => CurrentDevice.ConfigurationOperation(op).ExecOnSuccess(async () =>
        {
            DisplaySuccessToast(message: Strings.ConfigurationManager.ToastMsgConfigOpSuccess(op),
                Strings.ConfigurationManager.ToastTitleConfigOpSuccess(op));

            if (op != ConfigOp.SaveToFlash)
            {
                await GetConfigurationCategories();
                await InvokeAsync(StateHasChanged);
            }

        });
    
    public override void Dispose()
    {
        _dotNetRef?.Dispose();
        base.Dispose();
    }
}
