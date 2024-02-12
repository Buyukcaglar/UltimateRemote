namespace UltimateRemote.Helpers;
internal static class ImageHelper
{
    public static string GetDataImageSource(string imageBase64Str, string extension)
        => extension switch
        {
            ".webp" => $"data:image/webp;base64, {imageBase64Str}",
            ".jpg" or ".jpeg" => $"data:image/jpeg;base64, {imageBase64Str}",
            ".png" => $"data:image/png;base64, {imageBase64Str}",
            _ => throw new ArgumentOutOfRangeException(nameof(extension), extension, null)
        };
}
