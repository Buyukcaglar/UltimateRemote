using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Pages.HelpSections;
public sealed partial class HelpMenu
{
    [Parameter] public EventCallback<HelpTopicIdentifier> HelpTopicItemSelectedEvent { get; set; }

    private string? GetItemCss(HelpTopicAttribute<HelpTopicIdentifier> attr, int? itemIndex = null)
    {
        var cssParams = new List<string>(["py-1"]);
        if(attr.HasSubTopics)
            cssParams.Add("d-flex justify-content-between");
        if(attr.NonSelectable)
            cssParams.Add("text-muted");

        if(attr.NonSelectable && itemIndex is > 0)
            cssParams.Add("border-top border-bottom");

        return cssParams.Count > 0 ? string.Join(' ', cssParams) : null;
    }
}
