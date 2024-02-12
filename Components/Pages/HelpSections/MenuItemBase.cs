using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Pages.HelpSections;
public abstract class MenuItemBase : ComponentBase
{
    [Parameter, EditorRequired] public HelpTopicIdentifier MenuItem { get; set; }

    [Parameter] public int? Index { get; set; }

    protected HelpTopicAttribute<HelpTopicIdentifier>? ItemAttribute;

    protected abstract string? GetItemCss(HelpTopicAttribute<HelpTopicIdentifier> attr);

    protected string ItemLink => Blazor.Routes.HelpSection(MenuItem);

}
