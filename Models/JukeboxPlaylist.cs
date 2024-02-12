using System.Text.Json.Serialization;

namespace UltimateRemote.Models;
public sealed class JukeboxPlaylist
{
    [JsonInclude] public string Id { get; private set; } = Guid.NewGuid().ToString();

    public required string Name { get; set; }

    public List<SidFileInfo> Items { get; set; } = new List<SidFileInfo>();

    public int ItemCount => Items.Count;

    public int TuneCount => Items.Sum(sidFileInfo => sidFileInfo.NumberOfSongs);
}
