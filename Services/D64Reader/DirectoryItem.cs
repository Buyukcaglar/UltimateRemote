// https://github.com/Play-Zone/D64Reader

namespace UltimateRemote.Services.D64Reader
{
    public class DirectoryItem
    {
        public int Blocks { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int FileStartingTrack { get; set; }
        public int FileStartingSector { get; set; }
        public bool IsOpen { get; set; }
    }
}
