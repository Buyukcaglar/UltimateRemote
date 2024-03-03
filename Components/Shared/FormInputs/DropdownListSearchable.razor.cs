using Microsoft.AspNetCore.Components;

namespace UltimateRemote.Components.Shared.FormInputs;

public sealed partial class DropdownListSearchable<T>
{
    [Parameter] public string? Label { get; set; }

    [Parameter] public string? LabelTemplate { get; set; }

    [Parameter] public string? HeaderLabel { get; set; } = default!;

    [Parameter] public string? ContainerCss { get; set; } = "border rounded p-1 mb-0";

    [Parameter] public string ButtonCss { get; set; } = "dropdown-toggle p-1";

    [Parameter] public string? ButtonLabelCss { get; set; }

    [Parameter] public string? HeaderIconCss { get; set; }

    [Parameter] public T[]? Options { get; set; }

    [Parameter] public T? SelectedOption { get; set; }

    [Parameter] public bool ExcludeDefault { get; set; }

    [Parameter] public bool NoLabel { get; set; }
    
    [Parameter] public bool FixedLabel { get; set; }
    
    [Parameter] public bool Scrollable { get; set; }

    [Parameter] public bool AlignMenuStart { get; set; }

    [Parameter] public bool SearchEnabled { get; set; }

    [Parameter, EditorRequired] public Func<T, string> LabelFunc { get; set; } = default!;

    [Parameter] public Func<T, string>? IconFunc { get; set; }

    [Parameter] public EventCallback<T> ItemSelectedEvent { get; set; }

    [Parameter] public EventCallback<T> SelectedOptionChanged { get; set; }

    private T[]? Items { get; set; }

    private string? _scrollable => Scrollable ? "dropdown-menu-scrollable" : null;

    private string? _alignMenuStart => !AlignMenuStart ?  "dropdown-menu-end" : null;

    private string? _label;
    private T? _selectedItem;

    protected override void OnInitialized()
    {
        Items = Options;
        Init();
        base.OnInitialized();
    }

    //protected override void OnParametersSet()
    //{
    //    Init();
    //    base.OnParametersSet();
    //}

    private void Init()
    {
        _selectedItem = Items != null && Items.Contains(SelectedOption)
            ? SelectedOption
            : default(T?);
        
        if (!NoLabel)
        {
            if(FixedLabel)
                _label = Label;
            else if (_selectedItem == null || (ExcludeDefault && _selectedItem.Equals(default(T))))
                _label = Label;
            else
                _label = !string.IsNullOrWhiteSpace(LabelTemplate)
                    ? string.Format(LabelTemplate, LabelFunc(_selectedItem)) : LabelFunc(_selectedItem);
        }
    }

    private Task OnSearchPhraseChange(ChangeEventArgs args)
    {
        var searchPhrase = (string?) args.Value;
        Items = StringSearchExtensions.GetSearchResults(Options, LabelFunc, searchPhrase);
        return Task.CompletedTask;
    }

    private async Task ItemSelected(T item)
    {
        if (!string.IsNullOrWhiteSpace(Label) && !NoLabel && !FixedLabel)
        {
            _label = !string.IsNullOrWhiteSpace(LabelTemplate)
                ? string.Format(LabelTemplate, LabelFunc(item))
                : $"{Label}: {LabelFunc(item)}";
        }

        await ItemSelectedEvent.InvokeAsync(item);
    }

}
