using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;
public sealed partial class CheckBoxSwitch
{
    [Parameter] public bool? Value { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Required { get; set; }

    [Parameter] public string? Css { get; set; }

    [Parameter] public string? InputCss { get; set; }

    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    [Parameter] public EventCallback<bool> ValueChangedEvent { get; set; }

    private Dictionary<string, object> Attributes => BuildAttributes();

    private async Task CheckChanged(ChangeEventArgs e)
    {
        var newValue = (bool?)e.Value;
        if (newValue == Value) return;
        Value = newValue;
        await ValueChanged.InvokeAsync((bool?)e.Value ?? false);
        await ValueChangedEvent.InvokeAsync((bool?)e.Value ?? false);
    }

    private Dictionary<string, object> BuildAttributes()
    {
        var retVal = new Dictionary<string, object>();

        if (Disabled)
            retVal.Add("disabled", true);

        if (Required)
            retVal.Add("required", true);

        return retVal;
    }

}
