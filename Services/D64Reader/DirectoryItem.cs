// https://github.com/Play-Zone/D64Reader

using UltimateRemote.Models;

namespace UltimateRemote.Services.D64Reader
{
    public class DirectoryItem
    {
        public int Blocks { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int FileStartingTrack { get; set; }
        public int FileStartingSector { get; set; }
        public bool IsOpen { get; set; }
        
        public int[] NameAsciiCodeArray => !string.IsNullOrWhiteSpace(Name) ? Name.Select(c => (int)c).ToArray() : Array.Empty<int>();
        
        public string NameHtmlCodeString => PETSCIICodes.GetHtmlCodesString(NameAsciiCodeArray);

        public string NameHtmlHexCodeString => PETSCIICodes.GetHexValue(NameAsciiCodeArray);
    }
}
