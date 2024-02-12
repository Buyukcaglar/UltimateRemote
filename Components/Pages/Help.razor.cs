using Microsoft.AspNetCore.Components;
using UltimateRemote.Components.Shared;

namespace UltimateRemote.Components.Pages;

[Route(Blazor.RouteTemplates.Help)]
[Route(Blazor.RouteTemplates.HelpSection)]
public sealed partial class Help : BaseComponent
{
    [Parameter] public HelpTopicIdentifier Section { get; set; } = HelpTopicIdentifier.Basics;
}