using UltimateRemote.Interfaces;
using UltimateRemote.Models.ResponseModels;

namespace UltimateRemote.Extensions;
internal static class ApiResponseExtensions
{
    public static bool Success(this IApiResponse? apiResponse)
        => apiResponse is { Success: true };

    public static async Task ExecOnSuccess(this Task<ApiResponse?> apiTask, Action successAction)
    {
        var taskResult = await apiTask;
        if (taskResult.Success())
        {
            successAction();
        }
    }

    public static async Task ExecOnSuccess(this Task<ApiResponse?> apiTask, Func<Task> successTaskFunc)
    {
        var taskResult = await apiTask;
        if (taskResult.Success())
        {
            await successTaskFunc();
        }
    }

    public static async Task ExecOnSuccess(this Task<ApiResponse?> apiTask, Task successTask)
    {
        var taskResult = await apiTask;
        if (taskResult.Success())
        {
            await successTask;
        }
    }

    public static async Task ExecOnSuccess<TApiResponse>(this Task<TApiResponse?> apiTask, Func<Task> successTaskFunc)
        where TApiResponse : IApiResponse
    {
        var taskResult = await apiTask;
        if (taskResult.Success())
        {
            await successTaskFunc();
        }
    }

    public static async Task ExecOnSuccess<TApiResponse>(this Task<TApiResponse?> apiTask, Func<TApiResponse, Task> successTaskFunc)
        where TApiResponse : IApiResponse
    {
        var taskResult = await apiTask;
        if (taskResult.Success())
        {
            await successTaskFunc(taskResult!);
        }
    }

    public static async Task<TApiResponse?> ExecOnSuccess2<TApiResponse>(
        this Task<TApiResponse?> apiTask, 
        Func<Task> successTaskFunc)
        where TApiResponse : IApiResponse
    {
        var taskResult = await apiTask;
        if (taskResult.Success())
        {
            await successTaskFunc();
        }
        return taskResult;
    }


    public static async Task<TApiResponse?> ExecOnSuccess2<TApiResponse>(
        this Task<TApiResponse?> apiTask,
        Func<TApiResponse, Task> successTaskFunc)
        where TApiResponse : IApiResponse
    {
        var taskResult = await apiTask;
        if (taskResult.Success())
        {
            await successTaskFunc(taskResult!);
        }
        return taskResult;
    }

}
