using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UltimateRemote.Components.Shared;
public sealed partial class Card
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    
    [Parameter, EditorRequired] public string Title { get; set; } = default!;

    [Parameter] public bool DisplayActions { get; set; } = true;

    [Parameter] public string BodyCss { get; set; } = "d-flex flex-wrap justify-content-center p-0";
    
    [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; } = default!;

    [Parameter] public RenderFragment? ExternalActions { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
            await JsRuntime.SetCardActions();

        await base.OnAfterRenderAsync(firstRender);
    }
}
