namespace UltimateRemote.Services;
public class EventService
{
    public event EventHandler<string>? SidPlayInitiatedEvent;

    public void SignalSidPlay(object? sender, string componentId)
        => SidPlayInitiatedEvent?.Invoke(sender, componentId);
}
