using Microsoft.AspNetCore.Components;
using UltimateRemote.Components.Shared;

namespace UltimateRemote.Components.Pages;

[Route(Blazor.RouteTemplates.Preferences)]
public sealed partial class Preferences : BaseComponent
{

    private const string PrefItemCss = "col-12 col-md-4 col-lg-3 p-1";
    private const string PrefItemDescCss = "form-text text-break mx-1";

    private string HistorySize
    {
        get => PrefsMgr.UserPrefs.HistorySize.ToString();
        set => PrefsMgr.UserPrefs.HistorySize = uint.Parse(value);
    }

    private string SnackDuration
    {
        get => PrefsMgr.UserPrefs.SnackDuration.ToString();
        set
        {
            PrefsMgr.UserPrefs.SnackDuration = uint.Parse(value);
            ToastServiceExtensions.DefaultTimeOut =  int.Parse(value);
        }
    }

    private bool ConnectivityCheckEnabled
    {
        get => PrefsMgr.UserPrefs.ConnectivityCheckEnabled;
        set
        {
            PrefsMgr.UserPrefs.ConnectivityCheckEnabled = value;
            if(!PrefsMgr.UserPrefs.ConnectivityCheckEnabled)
                DeviceManager.StopConnectivityCheck();
            else
                DeviceManager.StartConnectivityCheck();
        }
    }

    private string ConnectivityCheckInterval
    {
        get => PrefsMgr.UserPrefs.ConnectivityCheckInterval.ToString();
        set
        {
            PrefsMgr.UserPrefs.ConnectivityCheckInterval = uint.Parse(value);
            DeviceManager.ChangeConnectivityCheckInterval(PrefsMgr.UserPrefs.ConnectivityCheckInterval);
        }
    }

    private string ApiTimeOutDuration
    {
        get => PrefsMgr.UserPrefs.ApiClientTimeout.ToString();
        set
        {
            PrefsMgr.UserPrefs.ApiClientTimeout = uint.Parse(value);
            DeviceManager.ChangeRequestTimeOut(PrefsMgr.UserPrefs.ApiClientTimeout);
        }
    }

    private async Task ResetSettings()
    {
        PrefsMgr.Reset();
        await InvokeAsync(StateHasChanged);
    }

    private Task SaveChanges()
    {
        PrefsMgr.PersistPreferences();
        DisplaySuccessToast(message: Strings.Preferences.ToastMsgPreferencesSaved,
            title: Strings.Preferences.ToastTitlePreferencesSaved);
        return Task.CompletedTask;
    }
}

