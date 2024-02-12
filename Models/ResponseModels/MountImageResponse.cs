using System.Text.Json.Serialization;

namespace UltimateRemote.Models.ResponseModels;

public class MountImageResponse : ApiResponse
{
    [JsonPropertyName("Subsys")] public int SubSys { get; set; }

    [JsonPropertyName("Ftype")] public int FileType { get; set; } // 1541, 1571, 1581 etc

    [JsonPropertyName("command")] public int Command { get; set; }

    [JsonPropertyName("file")] public string? FilePath { get; set; }
}