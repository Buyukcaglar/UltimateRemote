using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;
public sealed partial class TextInput : BaseFormInput
{
    [Parameter] public string? Placeholder { get; set; }

    [Parameter] public string? Css { get; set; }

    [Parameter] public int? MaxLen { get; set; }

    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    [Parameter] public EventCallback<string?> OnInputChanged { get; set; }

    private async Task OnInputChange(ChangeEventArgs args)
    {
        var value = (string?) args.Value;
        if(Value == value) return;
        Value = value;
        await ValueChanged.InvokeAsync(Value);
        await OnInputChanged.InvokeAsync(Value);
    }
}
