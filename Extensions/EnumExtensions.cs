using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.Serialization;
using UltimateRemote.Models;

namespace UltimateRemote.Extensions;

public static class ValidationStateExtensions
{
    public static string? InputStyle(this ValidationState validationState)
        => validationState.GetStringValue()?.Split('|', StringSplitOptions.RemoveEmptyEntries)[0] ?? null;

    public static string? FeedbackStyle(this ValidationState validationState)
        => validationState.GetStringValue()?.Split('|', StringSplitOptions.RemoveEmptyEntries)[0] ?? null;

}

public static class EnumMemberExtensions
{
    public static string GetEnumMemberValue<TEnum>(this TEnum enm) where TEnum : struct, Enum
        => EnumAttrCache<TEnum>.CachedNames[enm];

    public static List<SelectOption> ToSelectOptions<TEnum>() where TEnum : struct, Enum
        => EnumAttrCache<TEnum>.CachedNames.Select(enm => new SelectOption(enm.Value, enm.Key.ToString())).ToList();

    public static TEnum[] GetEnumValues<TEnum>(this TEnum enm) where TEnum : struct, Enum
        => Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();

    private static class EnumAttrCache<TEnum>
        where TEnum : struct, Enum
    {
        public static readonly ImmutableDictionary<TEnum, string> CachedNames = LoadNames();

        private static ImmutableDictionary<TEnum, string> LoadNames()
        {
            return typeof(TEnum)
                .GetTypeInfo()
                .DeclaredFields
                .Where(f => f.IsStatic && f.IsPublic && f.FieldType == typeof(TEnum))
                .Select(f => (field: f, attrib: f.GetCustomAttribute<EnumMemberAttribute>()))
                .Where(t => (t.attrib?.IsValueSetExplicitly ?? false) && !string.IsNullOrEmpty(t.attrib.Value))
                .ToDictionary(
                    keySelector: t => (TEnum)t.field.GetValue(obj: null)!,
                    elementSelector: t => t.attrib!.Value!
                )
                .ToImmutableDictionary();
        }
    }

    public static string? GetStringValue(this Enum value)
    {
        Type type = value.GetType();
        FieldInfo? fieldInfo = type.GetField(value.ToString());
        StringValueAttribute[]? attributes = (StringValueAttribute[]?) fieldInfo?.GetCustomAttributes(typeof(StringValueAttribute), false);
        return attributes is { Length: > 0 } ? attributes[0].StringValue : null;
    }

    public static HelpTopicAttribute<TEnum>? GetHelpTopicAttr<TEnum>(this Enum value) where TEnum : Enum
    {
        Type type = value.GetType();
        FieldInfo? fieldInfo = type.GetField(value.ToString());
        HelpTopicAttribute<TEnum>[]? attributes = (HelpTopicAttribute<TEnum>[]?) fieldInfo?.GetCustomAttributes(typeof(HelpTopicAttribute<TEnum>), false);
        return attributes is { Length: > 0 } ? attributes[0] : null;
    }

}

