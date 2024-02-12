using System.Text.Json.Serialization;
using UltimateRemote.Interfaces;

namespace UltimateRemote.Models.ResponseModels;

public class ApiResponse : IApiResponse
{
    [JsonPropertyName("errors")] public string[] Errors { get; init; } = Array.Empty<string>();

    public bool Success => Errors.Length == 0;
}