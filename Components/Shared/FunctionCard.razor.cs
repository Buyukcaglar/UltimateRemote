using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UltimateRemote.Components.Shared;
public sealed partial class FunctionCard
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    [Inject] private NavigationManager NavMan { get; set; } = default!;

    [Parameter, EditorRequired] public string Title { get; set; } = default!;

    [Parameter, EditorRequired] public string IconClass { get; set; } = default!;

    [Parameter, EditorRequired] public string InfoText { get; set; } = default!;

    [Parameter] public string TitleContainerCss { get; set; } = "p-2";

    [Parameter] public bool DisplayUploadIcon { get; set; }

    [Parameter] public string? NavigationLink { get; set; }

    [Parameter] public bool RaiseEventOnClick { get; set; }

    [Parameter] public string? CustomIcon { get; set; }

    [Parameter] public CustomIconType CustomIconType { get; set; }

    [Parameter] public string? ContainerCss { get; set; }

    [Parameter] public string? ContainerAlignSelfStart { get; set; } = "align-self-start";

    [Parameter] public RenderFragment? CustomActions { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public EventCallback ClickedEvent { get; set; }

    private async Task Clicked()
    {
        if (RaiseEventOnClick)
            await ClickedEvent.InvokeAsync();

        if (!string.IsNullOrWhiteSpace(NavigationLink))
            NavMan.NavigateTo(NavigationLink);
    }

    private ValueTask DisplayInfoPopup()
        => JsRuntime.BlockPagePopup(message: InfoText, alignTextCenter: InfoText.Length < 66);

}
