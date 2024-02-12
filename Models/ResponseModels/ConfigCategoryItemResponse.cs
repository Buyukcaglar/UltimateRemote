using System.Text.Json;
using System.Text.Json.Serialization;
using UltimateRemote.Interfaces;

namespace UltimateRemote.Models.ResponseModels;

public sealed class ConfigCategoryItemResponse<T> : IConfigCategoryItemResponse<T>
{
    [JsonIgnore] public string Section { get; set; } = default!;

    [JsonIgnore] public JsonValueKind ValueKind { get; set; }
    
    [JsonIgnore] public int TextInputMaxLen { get; set; } = 128;
    
    [JsonIgnore] public Func<string?, bool>? FuncValidator { get; set; }

    object? IConfigCategoryItemResponse.Current
    {
        get => Current;
        set => Current = (T?)Convert.ChangeType(value, typeof(T?));
    }

    object[]? IConfigCategoryItemResponse.Values
    {
        get => Values?.Cast<object>().ToArray();
        set => Values = value?.Cast<T>().ToArray();
    }

    object? IConfigCategoryItemResponse.MinVal
    {
        get => MinVal;
        set => MinVal = (T?)Convert.ChangeType(value, typeof(T?));
    }

    object? IConfigCategoryItemResponse.MaxVal
    {
        get => MaxVal;
        set => MaxVal = (T?)Convert.ChangeType(value, typeof(T?));
    }

    [JsonPropertyName("current")] public T? Current { get; set; }

    [JsonPropertyName("values")] public T[]? Values { get; set; }

    [JsonPropertyName("min")] public T? MinVal { get; set; }
    
    [JsonPropertyName("max")] public T? MaxVal { get; set; }

    [JsonPropertyName("format")] public string? Format { get; set; }
    object? IConfigCategoryItemResponse.Default
    {
        get => Default;
        set => Default = (T?)Convert.ChangeType(value, typeof(T?));
    }

    [JsonPropertyName("default")] public T? Default { get; set; }
}