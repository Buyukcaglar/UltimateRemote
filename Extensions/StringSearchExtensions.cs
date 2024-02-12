using System.Text;

namespace UltimateRemote.Extensions;

// https://stackoverflow.com/questions/9453731/how-to-calculate-distance-similarity-measure-of-given-2-strings
internal static class StringSearchExtensions
{
    public static string ConvertToSearchableString(string value)
    {
        var spacedString = value
            .Replace("/", " ")
            .Replace("-", " ")
            .Replace("_", " ")
            .Replace(".", " ");
        
        // https://synergy-usa-llc.com/how-to-replace-accented-characters-with-regular-latin-characters/
        // Thiss effectively replaces international chars with latin ones
        var encodingConvertedString =
            Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(spacedString));

        var loweredString = encodingConvertedString.ToLowerInvariant();
        var oneMoreString = string.Concat(loweredString.Where(c => char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)));
        var singleSpacedString = string.Join(" ", oneMoreString.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries ));
        
        return singleSpacedString;
    }

}
