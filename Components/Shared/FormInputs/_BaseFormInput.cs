using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;
public abstract class BaseFormInput : ComponentBase
{
    [Parameter] public string? Id { get; set; }

    [Parameter] public virtual string? Label { get; set; }

    [Parameter] public string? ContainerCss { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Required { get; set; }

    [Parameter] public bool NumericInputOnly { get; set; }

    [Parameter] public string? CustomInputFunction { get; set; }

    protected Dictionary<string, object> Attributes => BuildAttributes();

    public ValidationState ValidationState { get; set; } = ValidationState.Neutral;

    public string? ValidationMessage { get; set; }

    protected virtual Dictionary<string, object> BuildAttributes()
    {
        var retVal = new Dictionary<string, object>();

        if (Disabled)
            retVal.Add("disabled", true);

        if (Required)
            retVal.Add("required", true);

        if (NumericInputOnly)
        {
            retVal.Add("onkeypress", JsFunctions.NumericOnly);

            //retVal.Add("pattern", InputPatterns.Numeric);
            
            if (!PlatformDependent.IsApple)
            {
                retVal.Add("inputmode", "numeric");
            }
        }

        if (!NumericInputOnly && !string.IsNullOrWhiteSpace(CustomInputFunction))
        {
            retVal.Add("onkeypress", CustomInputFunction);
        }

        return retVal;
    }

}
