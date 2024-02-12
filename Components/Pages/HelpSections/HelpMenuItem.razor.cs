using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Pages.HelpSections;
public sealed partial class HelpMenuItem : MenuItemBase
{
    protected override void OnInitialized()
    {
        ItemAttribute = MenuItem.GetHelpTopicAttr<HelpTopicIdentifier>()!;
        base.OnInitialized();
    }

    protected override string? GetItemCss(HelpTopicAttribute<HelpTopicIdentifier> attr)
    {
        var cssParams = new List<string>(["py-1"]);
        if(attr.HasSubTopics)
            cssParams.Add("d-flex justify-content-between");
        if (attr.NonSelectable)
        {
            cssParams.Add("text-muted");
            if(!string.IsNullOrWhiteSpace(attr.IconCss))
                cssParams.Add("d-flex justify-content-between");
        }
            

        if(attr.NonSelectable && Index is > 0)
            cssParams.Add("border-top border-bottom");

        return cssParams.Count > 0 ? string.Join(' ', cssParams) : null;
    }

}
