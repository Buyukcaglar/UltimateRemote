using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;
public sealed partial class ButtonInput
{
    [Parameter] public string? Label { get; set; }

    [Parameter] public string? Css { get; set; }

    [Parameter] public string? IconCss { get; set; }

    [Parameter] public EventCallback ClickedEvent { get; set; }

    [Parameter] public Func<Task>? OnClickTaskFunc { get; set; }

    private async Task ButtonClicked()
    {
        if(null != OnClickTaskFunc)
            await OnClickTaskFunc.Invoke();
        await ClickedEvent.InvokeAsync();
    }
}
