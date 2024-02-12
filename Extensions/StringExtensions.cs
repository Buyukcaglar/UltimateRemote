namespace UltimateRemote.Extensions;
internal static class StringExtensions
{
    public static bool HasMoreThanOneChar(this string s, char c)
        => s.IndexOf(c) != s.LastIndexOf(c);

    public static string? ToUpperFirstChar(this string? input) 
        => input switch
        {
            not {Length: > 0} => null,
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

    public static string ToSlug(this string input)
        => StringSearchExtensions.ConvertToSearchableString(input).Replace(" ", "-");


}
