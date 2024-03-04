namespace UltimateRemote.ApiClients;

public sealed partial class UltimateDevice
{
    public Task<byte[]> GetFile(string filePath)
    {
        var requestUri = new Uri($"ftp://{IpAddress}{filePath}");
        return ftpClient.GetFile(requestUri);
    }
}
