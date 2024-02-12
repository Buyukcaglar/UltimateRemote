using System.Text.Json;

namespace UltimateRemote.Interfaces;
public interface IConfigCategoryItemResponse<T> : IConfigCategoryItemResponse
{
    new T? Current { get; set; }

    new T[]? Values { get; set; }

    new T? MinVal { get; set; }

    new T? MaxVal { get; set; }

    new T? Default { get; set; }
}


public interface IConfigCategoryItemResponse
{
    string Section { get; set; }

    JsonValueKind ValueKind { get; set; }

    object? Current { get; set; }

    object[]? Values { get; set; }

    object? MinVal { get; set; }

    object? MaxVal { get; set; }

    string? Format { get; set; }

    object? Default { get; set; }

    int TextInputMaxLen { get; set; }

    Func<string?, bool>? FuncValidator { get; set; }

}