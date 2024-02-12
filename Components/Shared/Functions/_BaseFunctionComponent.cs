using Microsoft.AspNetCore.Components;
using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;
public abstract class BaseFileFunctionComponent : BaseComponent
{
    protected abstract FileTypeGroup[] AllowedFileTypeGroups { get; }

    protected DeviceLocation[] EnabledLocations = default!;

    protected override void OnInitialized()
    {
        EnabledLocations = PrefsMgr.EnabledDeviceLocations.ToArray();
        base.OnInitialized();
    }

    protected string GetPath(string path) => $"{EnabledLocations.GetSelectedOrDefault()?.Path}{path}";

}
