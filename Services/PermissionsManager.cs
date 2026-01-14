using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;

#if IOS || MACCATALYST
using Foundation;
using Network; // Xamarin.iOS / .NET for iOS bindings
using CoreFoundation;
#endif

namespace UltimateRemote.Services;

public sealed class LocalNetworkPermission : Permissions.BasePermission
{
    // Put your actual Bonjour browse type here (must also be in NSBonjourServices)
    // Must match a value in NSBonjourServices
    private const string ServiceType = "_http._tcp";

    public override Task<PermissionStatus> CheckStatusAsync()
        => Task.FromResult(PermissionStatus.Unknown); // no direct “status” API

    public override async Task<PermissionStatus> RequestAsync()
    {
#if IOS || MACCATALYST
        EnsureDeclared();

        var ok = await ProbeBonjourBrowseAsync(ServiceType, timeoutMs: 3500);
        return ok ? PermissionStatus.Granted : PermissionStatus.Denied;
#else
        return PermissionStatus.Granted;
#endif
    }

    public override void EnsureDeclared()
    {
#if IOS || MACCATALYST
        var usage = NSBundle.MainBundle.ObjectForInfoDictionary("NSLocalNetworkUsageDescription");
        if (usage is null)
            throw new PermissionException("Missing NSLocalNetworkUsageDescription in Info.plist.");
#endif
    }

    public override bool ShouldShowRationale() => false;

#if IOS || MACCATALYST

    private static Task<bool> ProbeBonjourBrowseAsync(string serviceType, int timeoutMs)
    {
        var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        using var descriptor = NWBrowserDescriptor.CreateBonjourService(serviceType, domain: null);
        using var parameters = new NWParameters { IncludePeerToPeer = true };

        using var browser = new NWBrowser(descriptor, parameters);

        // State callback (binding uses method, not property)
        browser.SetStateChangesHandler((state, error) =>
        {
            // If iOS denies Local Network, you typically end up in Failed.
            if (state == NWBrowserState.Failed)
                tcs.TrySetResult(false);

            // “Ready” means the browser could start; that’s a good sign.
            // But we still keep listening for results until timeout.
        }); // :contentReference[oaicite:1]{index=1}

        // Results callback (binding uses IndividualChangesDelegate)
        browser.IndividualChangesDelegate = (oldResult, newResult) =>
        {
            // If we ever see a non-null result, browsing works => permission effectively granted.
            // (You may want to additionally check newResult.Endpoint etc.)
            if (newResult is not null)
                tcs.TrySetResult(true);
        }; // :contentReference[oaicite:2]{index=2}

        // Required: set queue, then start
        browser.SetDispatchQueue(DispatchQueue.DefaultGlobalQueue); // :contentReference[oaicite:3]{index=3}
        browser.Start(); // :contentReference[oaicite:4]{index=4}

        // Timeout: if no results and no failure, we can’t be sure.
        var cts = new CancellationTokenSource(timeoutMs);
        cts.Token.Register(() => tcs.TrySetResult(false));

        return tcs.Task.ContinueWith(t =>
        {
            try { browser.Cancel(); } catch { /* ignore */ }
            return t.Result;
        }, TaskScheduler.Default);
    }
#endif

}
