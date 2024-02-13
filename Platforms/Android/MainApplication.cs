using Android.App;
using Android.Runtime;

//[assembly: UsesPermission(Android.Manifest.Permission.ReadExternalStorage)]
[assembly: UsesPermission(Name = "android.permission.READ_EXTERNAL_STORAGE", MaxSdkVersion = 32)]

namespace UltimateRemote;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
