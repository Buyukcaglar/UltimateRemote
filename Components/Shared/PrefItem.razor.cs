using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared;
public sealed partial class PrefItem
{
    [Parameter, EditorRequired] public string Label { get; set; } = default!;

    [Parameter] public string? Css { get; set; }

    [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; } = default!;
}
