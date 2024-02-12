namespace UltimateRemote.Models;

public sealed record SelectOption(string Label, string Value, string? IconCss = null)
{
    public bool Selected { get; set; }
}

//public sealed record SelectOption<T>(string Label, string Value, string? IconCss = null)
//    : SelectOption(Label, Value, IconCss)
//{
//    public T? Data { get; set; }
//}
