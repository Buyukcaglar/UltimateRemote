using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;
public partial class Badge : ComponentBase
{
    [Parameter] public string? Text { get; set; }

    [Parameter] public string? Css { get; set; }
}
