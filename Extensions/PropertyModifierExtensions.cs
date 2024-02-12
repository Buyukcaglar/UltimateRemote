namespace UltimateRemote.Extensions;
internal static class PropertyModifierExtensions
{
    public static void SetValue<T>(this T[] objArray, Action<T> updateMethod)
        where T : class
    {
        foreach (var obj in objArray)
        {
            updateMethod(obj);
        }
    }

    public static void SetValue<T>(this List<T> objList, Action<T> updateMethod)
        where T : class
    {
        foreach (var obj in objList)
        {
            updateMethod(obj);
        }
    }

}
