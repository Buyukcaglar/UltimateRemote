using System.Text.Json.Serialization;

namespace UltimateRemote.Models.ResponseModels;

public class DrivesResponse : ApiResponse
{
    [JsonPropertyName("drives")] public Dictionary<string, DriveInfoResponse>[]? Drives { get; init; }
}

public sealed record DriveInfoResponse(
    [property: JsonPropertyName("enabled")] bool Enabled,
    [property: JsonPropertyName("bus_id")] int BusId,
    [property: JsonPropertyName("type")] string? Type,
    [property: JsonPropertyName("rom")] string? Rom,
    [property: JsonPropertyName("image_file")] string? ImageFile,
    [property: JsonPropertyName("image_path")] string? ImagePath,
    [property: JsonPropertyName("last_error")] string? LastError,
    [property: JsonPropertyName("partitions")] PartitionInfo[]? Partitions
);

public sealed record PartitionInfo([property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("path")] string Path
);