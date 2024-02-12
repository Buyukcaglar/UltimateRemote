using System.Text.Json;
using UltimateRemote.Interfaces;

namespace UltimateRemote.Models.ResponseModels;

public sealed class ConfigCategoryResponse : Dictionary<string, object?>, IApiResponse
{
    private string[] _errors => this.Keys.Contains("errors") && null != this["errors"] ? ((JsonElement)this["errors"]!).Deserialize<string[]>()! : Array.Empty<string>();

    public string[] Errors
    {
        get => _errors;
        init { }
    }

    public bool Success => Errors.Length == 0;

    public T? GetValue<T>(string key) => this.Keys.Contains(key) && null != this[key] ? ((JsonElement)this[key]!).Deserialize<T>() : default(T?);

}