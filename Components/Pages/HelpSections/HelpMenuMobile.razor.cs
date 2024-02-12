using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UltimateRemote.Components.Pages.HelpSections;
public sealed partial class HelpMenuMobile
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync("initDropdownSubmenu");
        }
        System.Diagnostics.Debug.WriteLine($"HelpMenuMobile.OnAfterRenderAsync");
        await base.OnAfterRenderAsync(firstRender);
    }

    private string? GetItemCss(HelpTopicAttribute<HelpTopicIdentifier> attr, int? itemIndex = null)
        => null;
}
