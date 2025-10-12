using Blazored.Toast;
using MonkeyCache.FileStore;
using System.Text;
using Microsoft.Extensions.Logging;
using UltimateRemote.Interfaces;
using UltimateRemote.Models;

namespace UltimateRemote;
internal static class DependencyConfiguration
{
    public static MauiAppBuilder ConfigureDependencies(this MauiAppBuilder appBuilder)
    {
        AddIpAddressService(appBuilder.Services);
        AddHttpClients(appBuilder.Services);
        AddServices(appBuilder.Services);

        appBuilder.Services.AddMauiBlazorWebView();

#if DEBUG
        appBuilder.Services.AddBlazorWebViewDeveloperTools();
        appBuilder.Logging.AddDebug();
#endif

        return appBuilder;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<DeviceScanner>(client => { client.Timeout = TimeSpan.FromSeconds(4); });
        services.AddHttpClient<JukeboxService>(client => { client.Timeout = TimeSpan.FromSeconds(120); });

        services.AddHttpClient(ServiceKeys.HttpClientHeartBeat, client => { client.Timeout = TimeSpan.FromSeconds(3); });
        services.AddHttpClient(ServiceKeys.HttpClientApi, client => { client.Timeout = TimeSpan.FromSeconds(15); });

        return services;
    }

    private static IServiceCollection AddIpAddressService(this IServiceCollection services)
    {

#if WINDOWS || MACOS
        services.AddSingleton<IIpAddressService, Platforms.Windows.Services.IpAddressService>();
#endif

#if ANDROID
        services.AddScoped<IIpAddressService, Platforms.Android.Services.IpAddressService>();
#endif

#if IOS
        services.AddScoped<IIpAddressService, Platforms.iOS.Services.IpAddressService>();
#endif

#if MACCATALYST
        services.AddScoped<IIpAddressService, Platforms.MacCatalyst.Services.IpAddressService>();
#endif
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<UltimateDevice>();
        services.AddSingleton<DeviceProvider>();
        services.AddSingleton<PreferencesManager>();
        services.AddKeyedSingleton<IUltimateDevice, DummyDevice>(ServiceKeys.DummyDevice);
        services.AddSingleton<DeviceManager>();
        services.AddSingleton<HistoryManager>();
        services.AddSingleton<FilePickerService>();
        services.AddSingleton<StorageContentFileService>();
        services.AddSingleton<LayoutManager>();
        services.AddSingleton<JukeboxService>();
        services.AddSingleton<EventService>();
        services.AddSingleton<FtpClient>();

        services.AddBlazoredModal();
        services.AddBlazoredToast();

#if MACCATALYST
        // Add IFilePickerService service to your DI
        // LukeFilePickerService
        services.AddFilePicker();
#endif

        return services;
    }

    public static MauiApp ConfigureServices(this MauiApp app)
    {
        Barrel.ApplicationId = AppInfo.PackageName;
        Barrel.Current.AutoExpire = false;
        //Barrel.Current.EmptyAll();
        //Barrel.Current.Empty(CacheKeys.UserLayouts);
        //Barrel.Current.Empty(CacheKeys.StorageContentLists);
        //Barrel.Current.Empty(CacheKeys.Preferences);
        //Barrel.Current.Empty(CacheKeys.KeyMacros);
        Task.Run(async () =>
        {
            // For string search functions
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            await using var scope = app.Services.CreateAsyncScope();
            var storageContentFileService = scope.ServiceProvider.GetRequiredService<StorageContentFileService>();
            var prefsMgr = scope.ServiceProvider.GetRequiredService<PreferencesManager>();
            ToastServiceExtensions.DefaultTimeOut = (int)prefsMgr.PopUpTimeout;
            //var permissionsService = scope.ServiceProvider.GetRequiredService<PermissionsService>();
            //await permissionsService.CheckPermissionsAsync();
        }).Wait();

        return app;
    }
}
