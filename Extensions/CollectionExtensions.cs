namespace UltimateRemote.Extensions;
internal static class CollectionExtensions
{
    public static void MoveItem<TItem>(this IList<TItem>? sourceList, Func<TItem, bool> findFunc, int newIndex)
    {
        if (sourceList == null) return;
            
        var item = sourceList.FirstOrDefault(findFunc.Invoke);

        if (item == null) return;
            
        var currentIndex = sourceList.IndexOf(item);

        if (newIndex == currentIndex) return;

        sourceList.RemoveAt(currentIndex);

        if (newIndex > sourceList.Count-1)
            sourceList.Add(item);
        else
        {
            sourceList.Insert(newIndex, item);
        }
    }

    public static void MoveItem<TItem>(this IList<TItem>? sourceList, int currentIndex, int newIndex)
    {
        if (sourceList == null) return;
            
        var item = sourceList[currentIndex];

        if (item == null) return;

        if (newIndex == currentIndex) return;

        sourceList.RemoveAt(currentIndex);

        if (newIndex > sourceList.Count-1)
            sourceList.Add(item);
        else
        {
            // the actual index could have shifted due to the removal
            // Warning: Lan bu sıkıntılı galiba
            //if (currentIndex > newIndex) newIndex--;
            sourceList.Insert(newIndex, item);
        }
    }

}
