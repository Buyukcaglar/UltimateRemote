using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UltimateRemote.Components.Shared.FormInputs;
public sealed partial class NumberInput
{
    [Parameter] public string? MinButtonCss { get; set; }

    [Parameter] public string? MaxButtonCss { get; set; }

    [Parameter] public int Min { get; set; }

    [Parameter] public int Max { get; set; }

    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    private async Task OnInputChange(ChangeEventArgs args)
    {
        var strVal = (string?) args.Value;
        if(Value == strVal) return;
        if (int.TryParse(strVal, out var intVal))
        {
            Value = intVal.ToString();
            if (intVal < Min) Value = Min.ToString();
            if (intVal > Max) Value = Max.ToString();
            await ValueChanged.InvokeAsync(Value);
        }
    }
    
    public async Task StepChange(bool up)
    {
        if (int.TryParse(Value, out var intVal))
        {
            intVal = up ? ++intVal : --intVal;

            if (intVal < Min) intVal = Min;
            if (intVal > Max) intVal = Max;

            Value = intVal.ToString();
            await ValueChanged.InvokeAsync(Value);
            return;
        }

        Value = Min.ToString();
        await ValueChanged.InvokeAsync(Value);
    }
}
