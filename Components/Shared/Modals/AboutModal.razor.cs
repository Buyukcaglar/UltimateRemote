using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.Modals;

public sealed partial class AboutModal : ComponentBase
{
    [Parameter] public IModalReference? Self { get; set; }

    [Parameter] public string ModalTitle { get; set; } = default!;


    private Task CloseModal(string file)
    {
        //Self?.Close(ModalResult.Ok<string>(file));
        return Task.CompletedTask;
    }
}
