using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Modals;

public sealed partial class StorageFileExtInfoModal : ComponentBase
{
    [Parameter] public IModalReference? Self { get; set; }

    [Parameter] public string ModalTitle { get; set; } = default!;

    [Parameter] public ExtensionInfo[] Extensions { get; set; } = default!;

}
