using System.Net;

namespace UltimateRemote.Services;
public class FtpClient
{
    public async Task<byte[]> GetFile(Uri requestUri)
    {
        var retVal = Array.Empty<byte>();

        try
        {
            var request = GetRequest(requestUri, WebRequestMethods.Ftp.DownloadFile);
            var response = (FtpWebResponse)request.GetResponse();

            await using var responseStream = response.GetResponseStream();
            using var memoryStream = new MemoryStream();

            await responseStream.CopyToAsync(memoryStream);

            retVal = memoryStream.ToArray();
            
            responseStream.Close();
            response.Close();

        }
        catch { }

        return retVal;
    }

    private static FtpWebRequest GetRequest(Uri requestUri, string method)
    {
        var request = (FtpWebRequest)WebRequest.Create(requestUri);
        request.Method = method;
        request.Credentials = new NetworkCredential("anonymous", "");
        return request;
    }

}
