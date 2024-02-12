namespace UltimateRemote.Components.Shared.Functions;

public sealed partial class Streams : BaseComponent
{
    private StreamType _streamType;

    private Task StartStream(string? destinationAddress)
        => CurrentDevice.StartStream(_streamType, destinationAddress!)
            .ExecOnSuccess(() => DisplaySuccessToast(
                message: Strings.Streams.ToastMsgStreamStarted(_streamType, destinationAddress!), 
                title: Strings.Streams.ToastTitleStreamStarted));

    private Task StopStream()
        => CurrentDevice.StopStream(_streamType)
            .ExecOnSuccess(() => DisplaySuccessToast(
                message: Strings.Streams.ToastMsgStreamStopped(_streamType), 
                title: Strings.Streams.ToastTitleStreamStopped));

    private bool ValidateIpPort(string? ipPort)
        => !string.IsNullOrWhiteSpace(ipPort) &&
           System.Text.RegularExpressions.Regex.IsMatch(input: ipPort, pattern: RegexPatterns.ValidateIpPort);
}