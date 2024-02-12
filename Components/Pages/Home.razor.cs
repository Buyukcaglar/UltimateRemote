using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UltimateRemote.Components.Pages;

[Route(Blazor.RouteTemplates.Home)]
public sealed partial class Home
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;


    [Inject] private IToastService ToastService { get; set; } = default!;

    [Inject] private DeviceManager DeviceManager { get; set; } = default!;

    [Inject] private LayoutManager LayoutManager { get; set; } = default!;
    
    /*
    protected override void OnInitialized()
    {
        DeviceManager.DeviceListUpdatedEvent -= OnDeviceListUpdated;
        DeviceManager.DeviceListUpdatedEvent += OnDeviceListUpdated;
        base.OnInitialized();
    }
    */

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            //await JsRuntime.SetContentHeight();
            await JsRuntime.SetCardActions();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    /*
    private async void OnDeviceListUpdated(object? sender, EventArgs eventArgs)
    {
        await InvokeAsync(StateHasChanged);
    }
    */


}
