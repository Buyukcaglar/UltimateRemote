using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared
{
    public sealed partial class Toast
    {
        [Parameter] public BgColorStyle HeaderColor { get; set; }

        [Parameter] public PhosphorIcon Icon { get; set; }

        [Parameter] public string? Title { get; set; }

        [Parameter] public string? Message { get; set; }
    }
}
