using UltimateRemote.Models;

namespace UltimateRemote.Extensions;
internal static class TargetSelectOptionExtensions
{
    public static SelectOption[] ToSelectOptions(this List<DeviceLocation> deviceLocations)
        => deviceLocations.ToSelectOptions(
            labelExpression: deviceLocation => deviceLocation.Name,
            valueExpression: deviceLocation => deviceLocation.Path,
            iconCssExpression: deviceLocation => deviceLocation.IconCss);
    
    public static SelectOption[] ToSelectOptions(this List<DeviceStorageFileList> fileList)
        => fileList.ToSelectOptions(
            labelExpression: file => file.Name, 
            valueExpression: file => file.Name);

    public static SelectOption[] ToSelectOptions(this List<FileTypeGroup> fileTypeGroups)
        => fileTypeGroups.ToSelectOptions(
            labelExpression: fileTypeGroup => fileTypeGroup.Name,
            valueExpression: fileTypeGroup => fileTypeGroup.Name);

    public static SelectOption[] ToSelectOptionsWithDefault(this List<FileTypeGroup> fileTypeGroups)
        => fileTypeGroups.ToSelectOptionsWithDefault(
            labelExpression: fileTypeGroup => fileTypeGroup.Name,
            valueExpression: fileTypeGroup => fileTypeGroup.Name,
            labelDefault: Strings.InternalTokens.AllExtensionsToken, valueDefault: Strings.InternalTokens.AllExtensionsToken, defaultItemFirst: true);

}
