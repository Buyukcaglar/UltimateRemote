using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.ApiClients;

public sealed partial class UltimateDevice
{
    public Task<ApiResponse?> ResetMachine()
        => PerformHttpRequest<ApiResponse>(ApiUrls.ResetMachine(IpAddress), HttpMethod.Put);

    public Task<ApiResponse?> RebootMachine()
        => PerformHttpRequest<ApiResponse>(ApiUrls.RebootMachine(IpAddress), HttpMethod.Put);

    public Task<ApiResponse?> PauseMachine()
        => PerformHttpRequest<ApiResponse>(ApiUrls.PauseMachine(IpAddress), HttpMethod.Put);

    public Task<ApiResponse?> ResumeMachine()
        => PerformHttpRequest<ApiResponse>(ApiUrls.ResumeMachine(IpAddress), HttpMethod.Put);

    public Task<ApiResponse?> PowerOffMachine()
        => PerformHttpRequest<ApiResponse>(ApiUrls.PowerOffMachine(IpAddress), HttpMethod.Put);

    public Task<WriteMemoryResponse?> WriteMemory(string address, string hexData)
        => PerformHttpRequest<WriteMemoryResponse>(ApiUrls.WriteMemory(IpAddress, address, hexData), HttpMethod.Put);

    public Task<WriteMemoryResponse?> WriteMemory(string address, byte[] dataContentBytes)
        => PerformFileUploadRequest<WriteMemoryResponse>(ApiUrls.WriteMemory(IpAddress, address), dataContentBytes, HttpMethod.Post);

    public Task<byte[]?> ReadMemory(string address, int length = 256)
        => PerformGetByteArray(ApiUrls.ReadMemory(IpAddress, address, length), HttpMethod.Get);

    public Task<DebugRegisterResponse?> ReadDebugRegister()
        => PerformHttpRequest<DebugRegisterResponse>(ApiUrls.ReadDebugRegister(IpAddress), HttpMethod.Get);

    public Task<DebugRegisterResponse?> WriteDebugRegister(string value)
        => PerformHttpRequest<DebugRegisterResponse>(ApiUrls.WriteDebugRegister(IpAddress, value), HttpMethod.Put);
}
