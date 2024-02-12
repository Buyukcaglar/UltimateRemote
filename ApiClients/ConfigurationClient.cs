using UltimateRemote.Interfaces;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.ApiClients;

public sealed partial class UltimateDevice
{
    public Task<ConfigsResponse?> GetConfigs()
        => PerformHttpRequest<ConfigsResponse>(ApiUrls.GetConfigurations(IpAddress), HttpMethod.Get);

    public Task<T?> GetConfigCategory<T>(string category) where T : IApiResponse
        => PerformHttpRequest<T>(ApiUrls.GetConfigCategory(IpAddress, category), HttpMethod.Get);

    public Task<T?> GetConfigCategorySection<T>(string category, string section) where T : IApiResponse
        => PerformHttpRequest<T>(ApiUrls.GetConfigCategorySection(IpAddress, category, section), HttpMethod.Get);

    public Task<T?> UpdateConfigCategorySectionValue<T>(string category, string section, string value) where T : IApiResponse
        => PerformHttpRequest<T>(ApiUrls.UpdateConfigCategorySectionValue(IpAddress, category, section, value), HttpMethod.Put);

    public Task<ApiResponse?> UpdateConfig<TConfig>(string category, string section, TConfig config) where TConfig : class
        => PerformPostJsonRequest<TConfig, ApiResponse>(ApiUrls.UpdateConfig(IpAddress), config);

    public Task<ApiResponse?> ConfigurationOperation(ConfigOp operation)
    => operation switch
    {
        ConfigOp.LoadFromFlash => PerformHttpRequest<ApiResponse>(ApiUrls.LoadConfigFromFlash(IpAddress), HttpMethod.Put),
        ConfigOp.SaveToFlash => PerformHttpRequest<ApiResponse>(ApiUrls.SaveConfigToFlash(IpAddress), HttpMethod.Put),
        ConfigOp.ResetToDefault => PerformHttpRequest<ApiResponse>(ApiUrls.ResetConfigToDefault(IpAddress), HttpMethod.Put),
        _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
    };


}
