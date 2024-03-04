using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using UltimateRemote.Services.D64Reader;

namespace UltimateRemote.Components.Shared.Modals;

public sealed partial class DiskDirectoryModal : ComponentBase
{
    [Parameter] public IModalReference? Self { get; set; }

    [Parameter] public string ModalTitle { get; set; } = default!;

    [Parameter] public D64Reader D64Reader { get; set; } = default!;

    private Task ItemSelected(DirectoryItem directoryItem, bool  run)
    {
        Self?.Close(ModalResult.Ok<(DirectoryItem DirItem, bool Run)>((directoryItem, run)));
        return Task.CompletedTask;
    }


}
