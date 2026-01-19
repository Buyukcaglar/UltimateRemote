using System.Text.Json;
using System.Text.Json.Serialization;

namespace UltimateRemote.Models;

public sealed class UserLayout
{
    [JsonInclude] public string Id { get; private set; } = Guid.CreateVersion7(DateTimeOffset.Now).ToString();
    
    public required string Name { get; set; }

    public List<LayoutItem> Items { get; set; } = new List<LayoutItem>();

    public LayoutItem[] ValidItems => Items.Where(item => item.IsValid).ToArray();

}

public sealed record LayoutItem(LayoutItemType Type)
{
    [JsonInclude] public string Id { get; private set; } = Guid.CreateVersion7(DateTimeOffset.Now).ToString();
    
    public string? Name { get; set; }

    public string? FileName { get; set; }

    public string? Extension { get; set; }

    public string? Path { get; set; }

    public string? Location { get; set; }

    public byte[]? ContentBytes { get; set; }

    public string? CustomIcon { get; set; }

    public CustomIconType IconType { get; set; }
    
    public string? SerializedData { get; set; }

    [JsonIgnore] public string LocationPath => FilePathHelper.LocationPath(Location, Path);

    public void SetData<T>(T data)
        => SerializedData = JsonSerializer.Serialize(data);

    public T? GetData<T>()
        => !string.IsNullOrWhiteSpace(SerializedData) ? JsonSerializer.Deserialize<T>(SerializedData) : default(T?);

    public bool IsValid => Type switch
    {
        LayoutItemType.StorageContentFile => !string.IsNullOrWhiteSpace(Path),
        LayoutItemType.UploadedFile => ContentBytes is { Length: > 0 } && !string.IsNullOrWhiteSpace(FileName),
        LayoutItemType.HVSCSIDFile => null != GetData<SidFileInfo>(),
        LayoutItemType.JukeboxPlaylist => !string.IsNullOrWhiteSpace(GetData<string>()),
        LayoutItemType.PlaySidMusic => true,
        LayoutItemType.PlayModMusic => true,
        LayoutItemType.RunLoadProgram => true,
        LayoutItemType.RunCartridge => true,
        LayoutItemType.ResetMachine => true,
        LayoutItemType.RebootMachine => true,
        LayoutItemType.ResetRebootStack => true,
        LayoutItemType.MachineFunctions => true,
        LayoutItemType.FloppyDrives => true,
        LayoutItemType.NonFloppyDrives => true,
        LayoutItemType.DriveByBusId => GetData<KeyValuePair<string, int>>().Value is > 7 and < 12,
        LayoutItemType.Streams => true,
        LayoutItemType.CreateDiskImage => true,
        LayoutItemType.GetOnDeviceFileInfo => true,
        LayoutItemType.KeyMacros => true,
        _ => false
    };

}