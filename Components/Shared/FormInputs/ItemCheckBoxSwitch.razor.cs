using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;
public sealed partial class ItemCheckBoxSwitch<T>
{
    [Parameter, EditorRequired] public T Item { get; set; } = default!;

    [Parameter, EditorRequired] public List<T> SelectedItems { get; set; } = default!;
    
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString("N").ToLowerInvariant();

    [Parameter] public EventCallback<(T Item , bool Selected)> ValueChangedEvent { get; set; }

    private bool _selected;

    protected override void OnParametersSet()
    {
        _selected = SelectedItems.Contains(Item);
        base.OnParametersSet();
    }

    private async Task CheckChanged(ChangeEventArgs e)
    {
        var newValue = (bool?)e.Value ?? false;
        if (newValue == _selected) return;
        _selected = newValue;
        await ValueChangedEvent.InvokeAsync((Item, _selected));
    }

}
