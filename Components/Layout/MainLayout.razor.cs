using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UltimateRemote.Components.Layout;

public sealed partial class MainLayout
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await JsRuntime.InvokeVoidAsync("initApp");

        await base.OnAfterRenderAsync(firstRender);
    }
}