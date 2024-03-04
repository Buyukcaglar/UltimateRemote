namespace UltimateRemote.Extensions;
internal static class ArrayExtensions
{
    public static T[] Add<T>(this T[] array, T item)
    {
        var list = array.ToList();
        list.Add(item);
        array = [.. list];
        return array;
    }

    public static T[] RemoveAt<T>(this T[] array, int index)
    {
        var list = array.ToList();
        list.RemoveAt(index);
        array = [.. list];
        return array;
    }

}
