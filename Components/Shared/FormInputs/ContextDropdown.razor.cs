using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.FormInputs;
public sealed partial class ContextDropdown
{
    [Parameter, EditorRequired] public string HeaderLabel { get; set; } = default!;

    [Parameter] public string ButtonCss { get; set; } = "dropdown-toggle p-1 border-0";

    [Parameter] public string? HeaderIconCss { get; set; }

    [Parameter] public SelectOption[]? Options { get; set; }

    [Parameter] public EventCallback<string> ItemSelectedEvent { get; set; }

    //private async Task ItemSelected(string selectedValue)
    //{
    //    if (Options is { Length: > 0 })
    //    {
    //        foreach (var option in Options)
    //        {
    //            option.Selected = option.Value == selectedValue;
    //        }
    //    }

    //    await ItemSelectedEvent.InvokeAsync(selectedValue);
    //}

}
