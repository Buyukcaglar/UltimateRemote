using System.Net;

namespace UltimateRemote.Services;
public class FtpClient
{
    public async Task<byte[]> GetFile(string filePath)
    {
        var retVal = Array.Empty<byte>();

        try
        {
            var request = (FtpWebRequest)WebRequest.Create(filePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("anonymous", "");

            var response = (FtpWebResponse)request.GetResponse();

            await using var responseStream = response.GetResponseStream();
            using var memoryStream = new MemoryStream();

            await responseStream.CopyToAsync(memoryStream);

            retVal = memoryStream.ToArray();

            response.Close();
        }
        catch { }

        return retVal;
    }
}
