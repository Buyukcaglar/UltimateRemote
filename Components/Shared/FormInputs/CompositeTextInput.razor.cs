using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;
public sealed partial class CompositeTextInput : BaseFormInput
{
    [Parameter] public string? Placeholder { get; set; }

    [Parameter] public int? MaxLen { get; set; }

    [Parameter] public bool DisplayMaxLengthCount { get; set; } = true;

    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    [Parameter] public string? InputCss { get; set; }

    [Parameter] public string? LabelCss { get; set; } = "bg-light border border-bottom-0 rounded-top px-2 py-1 mb-0";

    [Parameter] public RenderFragment? CustomLabel { get; set; }

    private async Task OnInputChange(ChangeEventArgs args)
    {
        var value = (string?) args.Value;
        if(Value == value) return;
        Value = value;
        await ValueChanged.InvokeAsync(Value);
    }
}
