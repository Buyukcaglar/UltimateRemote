using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;
public sealed partial class LabelInput
{
    [Parameter] public string? LabelText { get; set; }

    [Parameter] public string? LabelHtml { get; set; }

    [Parameter] public string? Css { get; set; } = "align-items-center justify-content-center";

    [Parameter] public string? CssPaddingOverride { get; set; } = "px-2 py-1";

    [Parameter] public string? LabelCss { get; set; }

    [Parameter] public string? ContainerCss { get; set; }
}
