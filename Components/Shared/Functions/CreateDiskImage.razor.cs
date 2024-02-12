using UltimateRemote.Models;

namespace UltimateRemote.Components.Shared.Functions;
public sealed partial class CreateDiskImage : BaseComponent
{
    public DeviceLocation[] DeviceLocations => PrefsMgr.EnabledDeviceLocations.ToArray();

    private DeviceLocation? _deviceLocation;

    private ImageFileType _imageFileType;

    private string ImageFilePathLabel => $"{Strings.DiskImage.ImageFilePathLabel} (Location: '{_deviceLocation?.Path}')";

    private string? _imageFilePath;
    private string? _diskLabel;
    private string? _tracks;

    protected override void OnInitialized()
    {
        _deviceLocation = DeviceLocations.GetSelectedOrDefault();
        _tracks = "35";
        base.OnInitialized();
    }

    private Task SelectedImageTypeChanged(ImageFileType imageFileType)
    {
        switch (imageFileType)
        {
            case ImageFileType.D64:
                if (_tracks != "40")
                    _tracks = "35";
                break;
            case ImageFileType.D71:
            case ImageFileType.D81:
                _tracks = null;
                break;
        }

        return Task.CompletedTask;
    }

    private Task CreateImage(string? imageFilePath)
        => _imageFileType switch
        {
            ImageFileType.D64 => CreateD64Image(imageFilePath),
            ImageFileType.D71 => CreateD71Image(imageFilePath),
            ImageFileType.D81 => CreateD81Image(imageFilePath),
            ImageFileType.Dnp => CreateDnpImage(imageFilePath),
            _ => Task.CompletedTask
        };

    private Task CreateD64Image(string? imageFilePath)
    {
        if (!CheckFilePath(imageFilePath)) return Task.CompletedTask;

        if (_tracks != "40" && _tracks != "35")
        {
            DisplayWarningToast(message: Strings.DiskImage.ToastMsgInvalidD64Tracks,
                title: Strings.DiskImage.ToastTitleInvalidD64Tracks);
            return Task.CompletedTask;
        }
        
        return CreateImage(_imageFileType, _imageFilePath!, int.Parse(_tracks!), _diskLabel);
    }

    private Task CreateD71Image(string? imageFilePath)
        => !CheckFilePath(imageFilePath)
            ? Task.CompletedTask
            : CreateImage(_imageFileType, _imageFilePath!, 0, _diskLabel);

    private Task CreateD81Image(string? imageFilePath)
        => !CheckFilePath(imageFilePath)
            ? Task.CompletedTask
            : CreateImage(_imageFileType, _imageFilePath!, 0, _diskLabel);

    private Task CreateDnpImage(string? imageFilePath)
    {
        if (!CheckFilePath(imageFilePath)) return Task.CompletedTask;

        if (!int.TryParse(_tracks, out var tracks) || !Enumerable.Range(1, 255).Contains(tracks))
        {
            DisplayWarningToast(message: Strings.DiskImage.ToastMsgInvalidDnpTracks,
                title: Strings.DiskImage.ToastTitleInvalidDnpTracks);
            return Task.CompletedTask;
        }

        return CreateImage(_imageFileType, _imageFilePath!, int.Parse(_tracks!), _diskLabel);
    }

    private Task CreateImage(ImageFileType imageFileType, string path, int tracks, string? diskLabel)
        => ExecuteUiBlockingTask(CurrentDevice
            .CreateDiskImage(imageFileType, path, tracks, diskLabel: string.IsNullOrWhiteSpace(diskLabel) ? null : diskLabel)
            .ExecOnSuccess(
                (createImageResponse) =>
                {
                    DisplaySuccessToast(message: Strings.DiskImage.ToastMsgDiskImageCreateSuccess(createImageResponse),
                        title: Strings.DiskImage.ToastTitleDiskImageCreateSuccess);
                    return Task.CompletedTask;
                }), blockingMessage: Strings.DiskImage.BpMsgCreatingDiskImage);

    private bool CheckFilePath(string? imageFilePath)
    {
        if (string.IsNullOrWhiteSpace(imageFilePath))
        {
            DisplayWarningToast(message: Strings.DiskImage.ToastMsgImageFilePathCanNotBeEmpty,
                title: Strings.DiskImage.ToastTitleImageFilePathCanNotBeEmpty);
            return false;
        }

        if (!imageFilePath.StartsWith('/'))
            imageFilePath = $"/{imageFilePath}";

        var fileInfo = new FileInfo(imageFilePath);
        var extension = fileInfo.Extension;
        var imageFileExtension = $".{_imageFileType.ToString().ToLowerInvariant()}";

        if (string.IsNullOrWhiteSpace(extension) || !extension.Equals(imageFileExtension, StringComparison.InvariantCultureIgnoreCase))
        {
            imageFilePath = $"{(string.IsNullOrWhiteSpace(extension) ? imageFilePath : imageFilePath[..(imageFilePath.LastIndexOf(extension, StringComparison.Ordinal))])}{imageFileExtension}";
        }

        _imageFilePath = $"{_deviceLocation!.Path}{imageFilePath}";

        return true;
    }

}