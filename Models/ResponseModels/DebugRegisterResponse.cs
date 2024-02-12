using System.Text.Json.Serialization;

namespace UltimateRemote.Models.ResponseModels;

public sealed class DebugRegisterResponse : ApiResponse
{
    [JsonPropertyName("value")] public string? Value { get; init; }
}