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

    // https://stackoverflow.com/questions/7060983/c-sharp-class-to-parse-webrequestmethods-ftp-listdirectorydetails-ftp-response
    public Task<string> ListFolder(string path)
    {
        var requestUri = new Uri($"ftp://{IpAddress}{path}");
        return ftpClient.ListFolderDetails(requestUri);
    }
}
