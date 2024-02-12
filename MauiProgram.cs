using Microsoft.Maui.LifecycleEvents;

namespace UltimateRemote;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
        => MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .ConfigureDependencies()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); })
// https://www.reddit.com/r/dotnetMAUI/comments/14vhme4/app_name_not_showing_while_alt_tabbing_or/?rdt=49252
#if WINDOWS
            .ConfigureLifecycleEvents(lifecycle =>
            {
                lifecycle.AddWindows((builder) =>
                {
                    builder.OnWindowCreated(del => { del.Title = "Ultimate Remote"; });
                });
            })
#endif
            .Build()
            .ConfigureServices();
    
}