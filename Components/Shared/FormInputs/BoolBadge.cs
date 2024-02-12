using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;
public class BoolBadge : Badge
{
    [Parameter, EditorRequired] public Func<bool> BoolFunc { get; set; } = default!;

    [Parameter, EditorRequired] public string FalseText { get; set; } = default!;

    [Parameter, EditorRequired] public string TrueText { get; set; } = default!;

    protected override void OnParametersSet()
    {
        Text = BoolFunc.Invoke() ? TrueText : FalseText;
        Css = BoolFunc.Invoke() ? "bg-success" : "bg-danger";
        base.OnParametersSet();
    }
}
