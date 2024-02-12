using MonkeyCache.FileStore;
using UltimateRemote.Models;

namespace UltimateRemote.Services;
public sealed class LayoutManager
{
    public UserLayout[] Layouts { get; private set; }

    public LayoutManager()
    {
        Layouts = Barrel.Current.Get<UserLayout[]>(key: CacheKeys.UserLayouts) ?? DefaultLayouts;
    }

    public void AddLayout(UserLayout layout)
    {
        var layoutList = Layouts.ToList();
        layoutList.Insert(0, layout);
        Layouts = [.. layoutList];
    }

    public void RemoveLayout(int layoutIndex)
    {
        var layoutList = Layouts.ToList();
        layoutList.RemoveAt(layoutIndex);
        Layouts = [.. layoutList];
        PersistLayouts(Layouts);
    }

    public void UpdateLayoutName(int layoutIndex, string name)
    {
        Layouts[layoutIndex].Name = name;
        PersistLayouts(Layouts);
    }

    public void MoveLayoutUp(int layoutIndex)
    {
        var layoutList = Layouts.ToList();
        layoutList.MoveItem(layoutIndex, layoutIndex - 1);
        Layouts = [.. layoutList];
        PersistLayouts(Layouts);
    }

    public void MoveLayoutDown(int layoutIndex)
    {
        var layoutList = Layouts.ToList();
        layoutList.MoveItem(layoutIndex, layoutIndex + 1);
        Layouts = [.. layoutList];
        PersistLayouts(Layouts);
    }

    public void MoveItemUp(int layoutIndex, int layoutItemIndex)
    {
        Layouts[layoutIndex].Items.MoveItem(layoutItemIndex, layoutItemIndex - 1);
        PersistLayouts(Layouts);
    }

    public void MoveItemDown(int layoutIndex, int layoutItemIndex)
    {
        Layouts[layoutIndex].Items.MoveItem(layoutItemIndex, layoutItemIndex + 1);
        PersistLayouts(Layouts);
    }

    public void AddLayoutItem(int layoutIndex, LayoutItem item)
    {
        Layouts[layoutIndex].Items.Add(item);
        PersistLayouts(Layouts);
    }

    public void RemoveLayoutItem(int layoutIndex, int itemIndex)
    {
        Layouts[layoutIndex].Items.RemoveAt(itemIndex);
        PersistLayouts(Layouts);
    }

    public void RemoveLayoutItem(int layoutIndex, LayoutItem item)
    {
        Layouts[layoutIndex].Items.Remove(item);
        PersistLayouts(Layouts);
    }

    public void UpdateLayoutItem(int layoutIndex, int itemIndex, LayoutItem item)
    {
        Layouts[layoutIndex].Items[itemIndex] = item;
        PersistLayouts(Layouts);
    }

    public void SaveCurrentLayout()
        => PersistLayouts(Layouts);

    private static void PersistLayouts(UserLayout[] layoutList)
        => Barrel.Current.Add(CacheKeys.UserLayouts, layoutList, TimeSpan.Zero);

    private UserLayout[] DefaultLayouts
        => new[] {
            new UserLayout() { Name = "Run/Load", Items = [new LayoutItem(LayoutItemType.RunLoadProgram), new LayoutItem(LayoutItemType.RunCartridge)] },
            new UserLayout() { Name = "Play Music", Items = [new LayoutItem(LayoutItemType.PlaySidMusic), new LayoutItem(LayoutItemType.PlayModMusic)] },
            new UserLayout() { Name = "Machine", Items = [new LayoutItem(LayoutItemType.ResetRebootStack), new LayoutItem(LayoutItemType.MachineFunctions)] },
            new UserLayout() { Name = "Floppy Drives", Items = [new LayoutItem(LayoutItemType.FloppyDrives)] },
            new UserLayout() { Name = "Non-Floppy Drives", Items = [new LayoutItem(LayoutItemType.NonFloppyDrives)] },
            //new UserLayout() { Name = "Streams", Items = [new LayoutItem(LayoutItemType.Streams)] },
            new UserLayout() { Name = "Disk Image & File", Items = [new LayoutItem(LayoutItemType.CreateDiskImage), new LayoutItem(LayoutItemType.GetOnDeviceFileInfo)] }
        };

}
