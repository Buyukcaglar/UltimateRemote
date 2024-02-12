using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using UltimateRemote.Components.Shared.Modals;

namespace UltimateRemote.Components.Layout;
public sealed partial class UserMenu
{
    [CascadingParameter] public IModalService ModalService { get; set; } = default!;

    private Task DisplayAboutModal()
    {
        var aboutModal = ModalService.Show<AboutModal>(title: "", new ModalParameters());
        return Task.CompletedTask;
    }
}
