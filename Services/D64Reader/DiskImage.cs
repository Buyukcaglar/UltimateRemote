// https://github.com/Play-Zone/D64Reader

namespace UltimateRemote.Services.D64Reader
{
    public class DiskImage
    {
        public string DiskType { get; set; }
        public List<DirectoryItem> DirectoryItems { get; set; }
        public string DiskName { get; set; }
        public string DiskId { get; set; }
        public int FreeBlocks { get; set; }
    }
}
