using System.Text.Json.Serialization;

namespace UltimateRemote.Models.ResponseModels;

public sealed class VersionResponse : ApiResponse
{
    [JsonPropertyName("version")] public string? Version { get; init; }
}