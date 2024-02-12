using Microsoft.AspNetCore.Components;
using ModalSize = UltimateRemote.Enums.ModalSize;

namespace UltimateRemote.Components.Shared;

public sealed partial class Modal
{
    [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

    [Parameter] public ModalSize Size { get; set; } = ModalSize.Large;

    [Parameter] public bool VCenter { get; set; } = true;

    [Parameter] public bool ShowFooter { get; set; } = false;

    [Parameter] public string? Title { get; set; }

    [Parameter] public string? TitleIcon { get; set; }

    [Parameter] public RenderFragment? Content { get; set; }

    [Parameter] public RenderFragment? FooterContent { get; set; }
        
    [Parameter] public bool Show { get; set; }

    [Parameter] public string? BodyCss { get; set; }

    private async Task Close()
    {
        Show = false;
        await BlazoredModal.CloseAsync(Blazored.Modal.Services.ModalResult.Ok(true));
    }

    private async Task Cancel()
    {
        Show = false;
        await BlazoredModal.CancelAsync();
    }
}