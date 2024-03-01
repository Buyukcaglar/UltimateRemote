using System.Text;

// https://github.com/Play-Zone/D64Reader

namespace UltimateRemote.Services.D64Reader
{
    public class D64Reader
    {
        private readonly byte[] _imageData;
        private const int Loc180 = 91396;
        private const int Loc181 = 91648;

        /// <summary>
        /// Class to read D64-Images (35 Tracks, 35 Tracks extended and 40 Tracks)
        /// </summary>
        /// <param name="imageData">D64-image as a byte-array</param>
        public D64Reader(byte[] imageData)
        {
            this._imageData = imageData;

            if (DiskType == "unknown")
            {
                throw new ArgumentException($"ImageData has an invalid size of {imageData.Length}");
            }

            Image = new DiskImage()
            {
                DiskType = DiskType,
                DirectoryItems = DirectoryItems,
                DiskId = DiskId,
                DiskName = DiskName,
                FreeBlocks = FreeBlocks,
            };
        }

        /// <summary>
        /// all the data of the Image (incl. Type, Directory, Name, Id, Blocks)
        /// </summary>
        public DiskImage Image { get; }

        /// <summary>
        /// Returns the diskname incl. spaces
        /// </summary>
        public string DiskName
        {
            get
            {
                var sb = new StringBuilder();

                for (var i = Loc181 - 0x70; i <= (Loc181 - 0x61); i++)
                {
                    sb.Append((char)_imageData[i]);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Returns the Id of the images, incl. spaces
        /// </summary>
        public string DiskId
        {
            get
            {
                var sb = new StringBuilder();

                for (var i = Loc181 - 0x5E; i <= Loc181 - 0x5A; i++)
                {
                    sb.Append((char)_imageData[i]);
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Returns the Free Blocks
        /// </summary>
        public int FreeBlocks
        {
            get
            {
                var loc = Loc180;
                var freeBlocks = 0;

                for (var i = 1; i <= 35; i++)
                {
                    if (i != 18) freeBlocks += _imageData[loc];
                    loc += 4;
                }
                return freeBlocks;
            }
        }

        /// <summary>
        /// Returns all the directory-items of the image
        /// </summary>
        public List<DirectoryItem> DirectoryItems
        {
            get
            {
                var dirItems = new List<DirectoryItem>();
                var startOffset = Loc181;
                var curDirSector = 1;

                // Iterate thru all sectors
                for (var t = 1; t <= 18; t++)
                {
                    var sectorOffset = 0;

                    // Get next track and sector of the directory
                    var nextDirTrack = _imageData[startOffset];
                    var nextDirSector = _imageData[startOffset + 1];

                    // loop through each directory sector 8 times
                    for (var s = 0; s < 8; s++)
                    {
                        var dirItem = new DirectoryItem { Name = string.Empty };

                        // parse the filename
                        for (var i = (startOffset + sectorOffset + 0x05); i <= (startOffset + sectorOffset + 0x14); i++)
                        {
                            dirItem.Name += (char)_imageData[i];
                        }

                        // get the starting track and sector
                        dirItem.FileStartingTrack = _imageData[startOffset + sectorOffset + 0x03];
                        dirItem.FileStartingSector = _imageData[startOffset + sectorOffset + 0x04];

                        // get the file size (blocks)
                        var sfilesize1 = (int)_imageData[startOffset + sectorOffset + 0x1E];
                        var sfilesize2 = (int)_imageData[startOffset + sectorOffset + 0x1F];
                        dirItem.Blocks = sfilesize1 + sfilesize2 * 256;

                        // get the filetype
                        var sfiletype = _imageData[startOffset + sectorOffset + 0x02];
                        switch (ParseTypeBits(sfiletype))
                        {
                            case "100":
                                dirItem.Type = "REL";
                                break;
                            case "011":
                                dirItem.Type = "USR";
                                break;
                            case "010":
                                dirItem.Type = "PRG";
                                break;
                            case "001":
                                dirItem.Type = "SEQ";
                                break;
                            case "000":
                                dirItem.Type = "DEL";
                                break;
                            default:
                                dirItem.Type = "???";
                                break;
                        }

                        // Is the file open?
                        if (!IsBitSet(sfiletype, 7))
                        {
                            dirItem.Type += "*";
                            dirItem.IsOpen = true;
                        }

                        // Helps against DirProtects
                        if (dirItem.FileStartingTrack == 0 && dirItem.FileStartingSector == 0 && sfilesize1 == 0 && dirItem.Name == "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0")
                        {
                            return dirItems;
                        }
                        dirItems.Add(dirItem);
                        sectorOffset += 0x20;

                    }

                    // last sector?
                    if (nextDirSector == 0 || nextDirTrack == 0)
                    {
                        break;
                    }
                    startOffset = startOffset + (256 * (nextDirSector - curDirSector));
                    curDirSector = nextDirSector;
                }
                return dirItems;
            }
        }

        /// <summary>
        /// Returns the disktype
        /// </summary>
        public string DiskType
        {
            get
            {
                switch (_imageData.Length)
                {
                    case 174848:
                        return "dt35";
                    case 196608:
                        return "dt40";
                    case 175531:
                        return "dt35e";
                    default:
                        return "unknown";
                }
            }
        }

        private bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private string ParseTypeBits(byte b)
        {
            var b2 = IsBitSet(b, 2) ? "1" : "0";
            var b1 = IsBitSet(b, 1) ? "1" : "0";
            var b0 = IsBitSet(b, 0) ? "1" : "0";

            return b2 + b1 + b0;
        }
    }
}
