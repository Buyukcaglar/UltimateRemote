using System.Text.Json.Serialization;

namespace UltimateRemote.Models.ResponseModels;

public class CreateDiskImageResponse : ApiResponse
{
    [JsonPropertyName("path")] public string? Path { get; set; }

    [JsonPropertyName("tracks")] public int Tracks { get; set; }

    [JsonPropertyName("diskname")] public string? DiskLabel { get; set; }

    [JsonPropertyName("bytes_written")] public int BytesWritten { get; set; }
}