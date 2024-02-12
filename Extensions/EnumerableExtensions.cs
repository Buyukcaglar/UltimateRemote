namespace UltimateRemote.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<List<T>> Partition<T>(this List<T> sourceList, int nSize)
    {
        for (int i = 0; i < sourceList.Count; i += nSize)
        {
            yield return sourceList.GetRange(i, Math.Min(nSize, sourceList.Count - i));
        }
    }
}