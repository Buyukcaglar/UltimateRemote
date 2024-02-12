using System.Text.Json.Serialization;

namespace UltimateRemote.Models.ResponseModels;

public class WriteMemoryResponse : ApiResponse
{
    [JsonPropertyName("address")] public string? Address { get; set; }
}