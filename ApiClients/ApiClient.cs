using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using UltimateRemote.Interfaces;

namespace UltimateRemote.ApiClients;

public sealed partial class UltimateDevice
{
    private async Task<T?> PerformMultipartFormDataRequest<T>(
        string url,
        byte[] fileContentBytes,
        string fileName,
        HttpMethod httpMethod,
        string formDataFieldName = "file") where T : IApiResponse
    {
        var boundary = "----------------------------" + DateTime.Now.Ticks.ToString("X");
        var multipartContent = new MultipartContent("form-data", boundary);
        var byteArrayContent = new ByteArrayContent(fileContentBytes);
        
        byteArrayContent.Headers.ContentDisposition =
            new ContentDispositionHeaderValue("form-data")
            {
                Name = formDataFieldName,
                // Contrary to boundry= parameter below HttpClient sends these values without quotes,
                // guess what, Ultimate API expects filename parameter value in quotes otherwise ignores it.
                // This is not a problem on Sid, Mod Play and Prg, Crt Run/Load functions,
                // but when filename is ignored, automatic detection of disk image type fails
                // and API returns Invalid File Type error when trying to mount disk images and not supplying type parameter...
                FileName = $"\"{fileName}\""
            };

        byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
        
        multipartContent.Add(byteArrayContent);

        #region ContentType Header Manipulation

        // https://github.com/tmenier/Flurl/issues/545
        // HttpClient sends Content-Type: multipart/form-data; boundary="----------------------------638410478678758858"
        // boundary= value in quotes and that causes problem on Ultimate API side, and returns "Upload File Failed" error!
        
        var contentTypeHeader = multipartContent.Headers.ContentType!.ToString();

        multipartContent.Headers.Remove("Content-Type");

        var fixedContentType = new string(contentTypeHeader.Where(x => x != '\"').Select(x => x).ToArray());

        multipartContent.Headers.TryAddWithoutValidation("Content-Type", fixedContentType); 
        #endregion

        var request = new HttpRequestMessage(httpMethod, new Uri(url));
        
        request.Content = multipartContent;

        return await PerformHttpRequest<T>(request);
    }

    //private async Task<T?> PerformMultipartRequest<T>(
    //    string url,
    //    byte[] fileContentBytes,
    //    string fileName,
    //    HttpMethod httpMethod) where T : IApiResponse
    //{

    //    var content = new MultipartContent();
    //    content.Add(new StringContent($"filename={fileName}"));
    //    content.Add(new ByteArrayContent(fileContentBytes));

    //    var request = new HttpRequestMessage(httpMethod, new Uri(url));
    //    request.Content = content;

    //    return await PerformHttpRequest<T>(request);
    //}

    private Task<T?> PerformFileUploadRequest<T>(
        string url,
        byte[] fileContentBytes,
        HttpMethod httpMethod) where T : IApiResponse
    {
        var request = new HttpRequestMessage(httpMethod, new Uri(url));
        request.Content = new ByteArrayContent(fileContentBytes);
        return PerformHttpRequest<T>(request);
    }

    private Task<T?> PerformHttpRequest<T>(string url, HttpMethod httpMethod) where T : IApiResponse
    {
        var request = new HttpRequestMessage(httpMethod, new Uri(url));
        return PerformHttpRequest<T>(request);
    }

    private Task<T?> PerformHttpRequest<T>(string url, HttpMethod httpMethod, JsonSerializerOptions jsonSerializerOptions) where T : IApiResponse
    {
        var request = new HttpRequestMessage(httpMethod, new Uri(url));
        return PerformHttpRequest<T>(request, jsonSerializerOptions);
    }

    private async Task<T?> PerformHttpRequest<T>(HttpRequestMessage request, JsonSerializerOptions? jsonSerializerOptions = null) where T : IApiResponse
    {
        var retVal = default(T?);

        HttpResponseMessage? response = default(HttpResponseMessage?);
        try
        {
            response = await _httpClient.SendAsync(request);
            if (response is { IsSuccessStatusCode: true })
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();
                retVal = await JsonSerializer.DeserializeAsync<T>(responseStream, jsonSerializerOptions);
                return retVal;
            }

            await DisplayRequestFailedPopup<T>(response, request.RequestUri?.AbsolutePath ?? "RequestUri IS NULL");

        }
        catch (HttpRequestException httpEx)
        {
            await DisplayRequestFailedPopup<T>(response, request.RequestUri?.AbsolutePath ?? "RequestUri IS NULL");
        }
        catch (Exception ex)
        {
            _toastService?.DisplayErrorToast(message: $"{ex.Message} Request Uri: {request.RequestUri?.AbsolutePath}", title: "PerformHttpRequest exception");
        }
        finally
        {
            request.Dispose();
            response?.Dispose();
        }

        return retVal;
    }

    private async Task<byte[]?> PerformGetByteArray(string url, HttpMethod httpMethod)
    {
        var retVal = default(byte[]?);

        try
        {
            retVal = await _httpClient.GetByteArrayAsync(url);

        }
        catch (Exception ex)
        {
            _toastService?.DisplayErrorToast(message: $"{ex.Message} Request Uri: {url}", title: "Request Failed");
        }

        return retVal;
    }

    private async Task<T?> PerformPostJsonRequest<TPayload, T>(string url, TPayload payload)
        where T : IApiResponse
        where TPayload : class
    {
        var retVal = default(T?);
        try
        {
            var response = await _httpClient.PostAsJsonAsync<TPayload>(new Uri(url), payload);

            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();
                retVal = await JsonSerializer.DeserializeAsync<T>(responseStream);
                return retVal;
            }

            await DisplayRequestFailedPopup<T>(response, url);

        }
        catch (Exception ex)
        {
            _toastService?.DisplayErrorToast(message: $"{ex.Message} Request Uri: {url}", title: "PerformPostJsonHttpRequest exception");
        }

        return retVal;
    }

    private async Task DisplayRequestFailedPopup<T>(HttpResponseMessage? httpResponse, string requestUrl)
        where T : IApiResponse
    {
        if (_toastService == null)
            return;

        if (httpResponse != null)
        {
            try
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();

                if (responseString.TryDeserialize<T>(out var responseObject) && null != responseObject)
                {
                    var errorMessage = string.Join("<br />", responseObject.Errors);
                    _toastService.DisplayErrorToast(errorMessage, "API Response");
                    return;
                }
                
                _toastService.DisplayErrorToast(message: $"Status Code: {httpResponse.StatusCode} ({(int)httpResponse.StatusCode}) Request Uri: {requestUrl}", title: "Request Failed");

            }
            catch (Exception ex)
            {
                _toastService.DisplayErrorToast(ex.ToString(), "Popup Exception");
            }
        }
    }

}
