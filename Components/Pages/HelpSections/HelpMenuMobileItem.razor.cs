using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Pages.HelpSections;
public sealed partial class HelpMenuMobileItem : MenuItemBase
{
    protected override void OnInitialized()
    {
        ItemAttribute = MenuItem.GetHelpTopicAttr<HelpTopicIdentifier>()!;
        base.OnInitialized();
    }

    protected override string? GetItemCss(HelpTopicAttribute<HelpTopicIdentifier> attr)
        => null;
}
