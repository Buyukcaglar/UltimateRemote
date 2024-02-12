using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;
public sealed partial class InputGroupTextButton : BaseFormInput
{
    [Parameter] public string? Placeholder { get; set; }

    [Parameter] public int? MaxLen { get; set; }

    [Parameter] public bool DisplayMaxLengthCount { get; set; } = true;

    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback<string?> ButtonClickedEvent { get; set; }

    [Parameter] public string? InputCss { get; set; }

    [Parameter] public string? LabelCss { get; set; } = "border border-bottom-0 rounded-top p-2 mb-0";

    [Parameter] public string? ButtonText { get; set; }

    [Parameter] public string? ButtonCss { get; set; }

    [Parameter] public string? ButtonIconCss { get; set; }

    [Parameter] public Func<string?, bool>? FuncValidator { get; set; }

    [Parameter] public string? ValidatorMessage { get; set; }

    [Parameter] public bool HideValidationMessage { get; set; }

    [Parameter] public RenderFragment? BeforeTextInputContent { get; set; }
    
    //private Task OnInputChange(ChangeEventArgs args)
    //{
    //    Value = (string?)args.Value;
    //    return Task.CompletedTask;
    //}

    private async Task ButtonClicked()
    {
        if (Required && string.IsNullOrWhiteSpace(Value))
        {
            ValidationState = ValidationState.Invalid;
            ValidationMessage ??= "Required";
            return;
        }

        if (FuncValidator != null && !FuncValidator(Value))
        {
            ValidationState = ValidationState.Invalid;
            ValidationMessage ??= ValidatorMessage;
            return;
        }

        ValidationState = ValidationState.Valid;
        await ButtonClickedEvent.InvokeAsync(Value?.Trim());
    }
}
