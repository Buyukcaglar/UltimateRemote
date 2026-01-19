using System.Globalization;
using System.Text.RegularExpressions;
using UltimateRemote.Models;

namespace UltimateRemote.ApiClients;

public sealed partial class UltimateDevice
{
    public Task<byte[]> GetFile(string filePath)
    {
        var requestUri = new Uri($"ftp://{IpAddress}{filePath}");
        return ftpClient.GetFile(requestUri);
    }

    public Task<string> GetStorageTargets()
    {
        var requestUri = new Uri($"ftp://{IpAddress}");
        return ftpClient.ListFolder(requestUri);
    }
    
    // https://stackoverflow.com/questions/1013486/parsing-ftpwebrequest-listdirectorydetails-line#40045894
    // "drw-rw-rw-   1 user     ftp            0 Jan 01  1980 Flash"
    public async Task<FtpListItem[]> ListFolder(string path)
    {
        string[] hourMinFormats = ["MMM dd HH:mm", "MMM dd H:mm", "MMM d HH:mm", "MMM d H:mm"];
        string[] yearFormats = ["MMM dd yyyy", "MMM d yyyy"];
        IFormatProvider culture = CultureInfo.GetCultureInfo("en-us");

        var requestUri = new Uri($"ftp://{IpAddress}{path}");
        var response = await ftpClient.ListFolderDetails(requestUri);
        var lines = response.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);

        var items = lines.Select(line =>
        {
            var match = FtpListingPattern().Match(line);
            string permissions = match.Groups[1].Value;
            int inode = int.Parse(match.Groups[2].Value, culture);
            string owner = match.Groups[3].Value;
            string group = match.Groups[4].Value;
            long size = long.Parse(match.Groups[5].Value, culture);
            string s = Regex.Replace(match.Groups[6].Value, @"\s+", " ");

            string[] formats = (s.IndexOf(':') >= 0) ? hourMinFormats : yearFormats;
            var modified = DateTime.ParseExact(s, formats, culture, DateTimeStyles.None);
            string name = match.Groups[7].Value;

            return new FtpListItem(name, path, size, modified, permissions.StartsWith('d'));
        }).ToArray();


        return items;
    }
    
    [GeneratedRegex(@"^([\w-]+)\s+(\d+)\s+(\w+)\s+(\w+)\s+(\d+)\s+(\w+\s+\d+\s+\d+|\w+\s+\d+\s+\d+:\d+)\s+(.+)$", RegexOptions.Compiled)]
    public partial Regex FtpListingPattern();

}
