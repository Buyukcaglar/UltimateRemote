namespace UltimateRemote.Services;
public class PermissionsManager
{
    public async Task CheckPermissionsAsync()
    {
        var permissionStatus = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();
        if (permissionStatus != PermissionStatus.Granted)
        {
            permissionStatus = await Permissions.RequestAsync<Permissions.Bluetooth>();
            if (permissionStatus != PermissionStatus.Granted)
            {
                System.Diagnostics.Debug.WriteLine("DENIED!");
            }
        }
    }
}

public class LocalNetworkPermission : Permissions.BasePermission, ILocalNetworkPermission
{
    public override Task<PermissionStatus> CheckStatusAsync()
    {
        throw new NotImplementedException();
    }

    public override Task<PermissionStatus> RequestAsync()
    {
        throw new NotImplementedException();
    }

    public override void EnsureDeclared()
    {
        throw new NotImplementedException();
    }

    public override bool ShouldShowRationale()
    {
        throw new NotImplementedException();
    }
}

public interface ILocalNetworkPermission
{
    Task<PermissionStatus> CheckStatusAsync();
    Task<PermissionStatus> RequestAsync();
}