namespace UltimateRemote.Services;
public class EventService
{
    public event EventHandler<string>? SidPlayInitiatedEvent;

    public event EventHandler? SignalDeviceListUpdateEvent;

    public void SignalSidPlay(object? sender, string componentId)
        => SidPlayInitiatedEvent?.Invoke(sender, componentId);

    public void SignalDeviceListUpdate(object? sender)
        => SignalDeviceListUpdateEvent?.Invoke(sender, EventArgs.Empty);
}
