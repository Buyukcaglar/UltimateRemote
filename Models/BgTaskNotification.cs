namespace UltimateRemote.Models;

public enum TaskStatus { Started, Progress, Ended }

public sealed record BgTaskNotification(TaskStatus Status, string Message, bool Success)
{
    public string PopupTitle { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;

    public object? Data { private get; set; }

    public T? GetData<T>()
        => null != Data ? (T)Data : default(T);

}