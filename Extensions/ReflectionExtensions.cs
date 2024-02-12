using System.Reflection;
using System.Text.Json.Serialization;

namespace UltimateRemote.Extensions;
internal static class ReflectionExtensions
{
    public static string GetJsonPropertyName(this object obj, string propertyName)
        => obj.GetType().GetProperty(propertyName)?.GetCustomAttribute<JsonPropertyNameAttribute>(true)?.Name ??
           propertyName;
}
