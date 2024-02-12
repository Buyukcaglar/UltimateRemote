namespace UltimateRemote.Helpers;
internal static class ThreadPoolHelper
{
    public static RegisteredWaitHandle RegisterBgTask(Func<Task> taskAction)
        => RegisterBgTask(taskAction, TimeSpan.Zero, true);

    public static RegisteredWaitHandle RegisterBgTask(Func<Task> taskAction, TimeSpan interval, bool executeOnlyOnce = false)
    {
        var waitHandle = new AutoResetEvent(false);
        var registeredWaitHandle =  ThreadPool.RegisterWaitForSingleObject(
            waitHandle,
            //// Method to execute
            //async (state, timeout) =>
            //{
            //    //System.Diagnostics.Debug.WriteLine("ThreadPool.RegisterWaitForSingleObject");
            //    await taskAction();
            //}
            (state, timeout) => taskAction(),
            // optional state object to pass to the method
            null,
            // Execute the method after interval
            interval,
            // Set this to false in order to execute it repeatedly every interval
            executeOnlyOnce
        );
        
        return registeredWaitHandle;
    }
}
