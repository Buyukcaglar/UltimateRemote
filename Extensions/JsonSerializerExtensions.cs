using System.Text.Json;

namespace UltimateRemote.Extensions;
internal static class JsonSerializerExtensions
{
    public static bool TryDeserialize<T>(this string? json, out T? value)
    {
        var retVal = false;
        value = default(T?);
        if (string.IsNullOrWhiteSpace(json))
            return retVal;
        
        try
        {
            value = JsonSerializer.Deserialize<T>(json);
            retVal = true;
        }
        catch { }

        return retVal;
    }
}
