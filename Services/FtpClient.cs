using System.Net;
using System.Text;

namespace UltimateRemote.Services;
public class FtpClient
{
    public async Task<byte[]> GetFile(Uri requestUri)
    {
        var retVal = Array.Empty<byte>();

        try
        {
            var request = await GetRequestTask(requestUri, WebRequestMethods.Ftp.DownloadFile);
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

    public async Task<string> ListFolder(Uri requestUri)
    {
        var retVal = string.Empty;

        try
        {
            var request = await GetRequestTask(requestUri, WebRequestMethods.Ftp.ListDirectory);
            var response = (FtpWebResponse)request.GetResponse();

            await using var responseStream = response.GetResponseStream();
            using var memoryStream = new MemoryStream();

            await responseStream.CopyToAsync(memoryStream);

            retVal = Encoding.ASCII.GetString(memoryStream.ToArray());

            responseStream.Close();
            response.Close();

        }
        catch { }

        return retVal;
    }

    public async Task<string> ListFolderDetails(Uri requestUri)
    {
        var retVal = string.Empty;

        try
        {
            var request = await GetRequestTask(requestUri, WebRequestMethods.Ftp.ListDirectoryDetails);
            var response = (FtpWebResponse)request.GetResponse();

            await using var responseStream = response.GetResponseStream();
            using var memoryStream = new MemoryStream();

            await responseStream.CopyToAsync(memoryStream);

            retVal = Encoding.ASCII.GetString(memoryStream.ToArray());

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

    private static Task<FtpWebRequest> GetRequestTask(Uri requestUri, string method)
    {
        var request = (FtpWebRequest)WebRequest.Create(requestUri);
        request.Method = method;
        request.Credentials = new NetworkCredential("anonymous", "");
        return Task.FromResult(request);
    }
}
