using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions.Browser;
public sealed partial class FtpFileItem : BaseComponent
{
    [Parameter, EditorRequired] public FtpListItem Item { get; set; } = default!;

    [Parameter] public EventCallback<FtpListItem> ClickedEvent { get; set; }
}