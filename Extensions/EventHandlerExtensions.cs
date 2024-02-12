using UltimateRemote.Models;

namespace UltimateRemote.Extensions;
internal static class EventHandlerExtensions
{
    public static void SignalStart(this EventHandler<BgTaskNotification>? eventHandler, string blockPageMessage, object? sender = null)
        => eventHandler?.Invoke(sender, new BgTaskNotification(Models.TaskStatus.Started, blockPageMessage, Success: true /*Does not matter for Started Status*/));

    public static void SignalProgress(this EventHandler<BgTaskNotification>? eventHandler, string blockPageMessage, object? sender = null)
        => eventHandler?.Invoke(sender, new BgTaskNotification(Models.TaskStatus.Progress, blockPageMessage, Success: true /*Does not matter for Progress Status*/));

    // BgTask ended prematurely, due to an error. Display warn popup.
    public static void SignalFail(this EventHandler<BgTaskNotification>? eventHandler, string popUpMessage, string popUpTitle, object? sender = null)
        => eventHandler?.Invoke(sender, new BgTaskNotification(Models.TaskStatus.Ended, popUpMessage, Success: false /*DOES matter*/) { PopupTitle = popUpTitle });

    // BgTask ended greacefully. Display success popup
    public static void SignalSuccess(this EventHandler<BgTaskNotification>? eventHandler, string popUpMessage, string popUpTitle, object? sender = null)
        => eventHandler?.Invoke(sender, new BgTaskNotification(Models.TaskStatus.Ended, popUpMessage, Success: true /*DOES matter*/) { PopupTitle = popUpTitle });

    public static void SignalSuccess<T>(this EventHandler<BgTaskNotification>? eventHandler, string popUpMessage, string popUpTitle, T data, object? sender = null) 
        => eventHandler?.Invoke(sender, new BgTaskNotification(Models.TaskStatus.Ended, popUpMessage, Success: true /*DOES matter*/) { PopupTitle = popUpTitle, Data = data });

    public static void SignalSuccess<T>(this EventHandler<BgTaskNotification>? eventHandler, string popUpMessage, string popUpTitle, string tag, T data, object? sender = null) 
        => eventHandler?.Invoke(sender, new BgTaskNotification(Models.TaskStatus.Ended, popUpMessage, Success: true /*DOES matter*/) { PopupTitle = popUpTitle, Tag = tag, Data = data });
}
