namespace UltimateRemote.Interfaces;
public interface IApiResponse
{
    string[] Errors { get; init; }
    bool Success { get; }
}
