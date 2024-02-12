namespace UltimateRemote.Models;
public sealed class FilePickResult : IDisposable, IAsyncDisposable
{
    public string? FileName { get; set; }
    public string? ContentType { get; set; }
    public string? FullPath { get; set; }
    public long FileSize => FileStream?.Length ?? 0;
    public Stream? FileStream { get; set; }
    public Exception? Exception { get; set; }
    public bool Success => Exception == null;
    public bool HasFile => null != FileStream;

    public void Dispose()
    {
        FileStream?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (FileStream != null) await FileStream.DisposeAsync();
    }
}
