using System.Text.Json.Serialization;

namespace UltimateRemote.Models.ResponseModels;

public class GetFileInfoResponse : ApiResponse
{
    [JsonPropertyName("files")] public Dictionary<string, object?>? FileInfo { get; set; }
}