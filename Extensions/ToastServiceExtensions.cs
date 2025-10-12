using Blazored.Toast;
using Blazored.Toast.Services;
using UltimateRemote.Components.Shared;

namespace UltimateRemote.Extensions
{
    public static class ToastServiceExtensions
    {
        public static int DefaultTimeOut { get; set; } = 5;

        public static void DisplayInfoToast(this IToastService toastService, string message, string title)
        {
            var toastParameters = new ToastParameters()
                .Add(nameof(Toast.HeaderColor), BgColorStyle.Info)
                .Add(nameof(Toast.Icon), PhosphorIcon.InfoCircle)
                .Add(nameof(Toast.Title), title)
                .Add(nameof(Toast.Message), message);

            toastService.ShowToast<Toast>(
                toastParameters,
                settings =>
                {
                    settings.Timeout = DefaultTimeOut;
                    settings.ShowProgressBar = true;
                });
        }

        public static void DisplayInfoToast(this IToastService toastService, string message, string title, int timeOut)
        {
            var toastParameters = new ToastParameters()
                .Add(nameof(Toast.HeaderColor), BgColorStyle.Info)
                .Add(nameof(Toast.Icon), PhosphorIcon.InfoCircle)
                .Add(nameof(Toast.Title), title)
                .Add(nameof(Toast.Message), message);

            toastService.ShowToast<Toast>(
                toastParameters,
                settings =>
                {
                    settings.Timeout = timeOut;
                    settings.ShowProgressBar = true;
                });
        }

        public static void DisplaySuccessToast(this IToastService toastService, string message, string title)
        {
            var toastParameters = new ToastParameters()
                .Add(nameof(Toast.HeaderColor), BgColorStyle.Success)
                .Add(nameof(Toast.Icon), PhosphorIcon.CheckCircle)
                .Add(nameof(Toast.Title), title)
                .Add(nameof(Toast.Message), message);

            toastService.ShowToast<Toast>(
                toastParameters,
                settings =>
                {
                    settings.Timeout = DefaultTimeOut;
                    settings.ShowProgressBar = true;
                });
        }

        public static void DisplayWarningToast(this IToastService toastService, string message, string title)
        {
            var toastParameters = new ToastParameters()
                .Add(nameof(Toast.HeaderColor), BgColorStyle.Warning)
                .Add(nameof(Toast.Icon), PhosphorIcon.WarningTriangle)
                .Add(nameof(Toast.Title), title)
                .Add(nameof(Toast.Message), message);

            toastService.ShowToast<Toast>(
                toastParameters,
                settings =>
                {
                    settings.Timeout = DefaultTimeOut;
                    settings.ShowProgressBar = true;
                });
        }

        public static void DisplayErrorToast(this IToastService toastService, string message, string title)
        {
            var toastParameters = new ToastParameters()
                .Add(nameof(Toast.HeaderColor), BgColorStyle.Danger)
                .Add(nameof(Toast.Icon), PhosphorIcon.ErrorOctagon)
                .Add(nameof(Toast.Title), title)
                .Add(nameof(Toast.Message), message);

            toastService.ShowToast<Toast>(
                toastParameters,
                settings =>
                {
                    settings.Timeout = DefaultTimeOut;
                    settings.ShowProgressBar = true;
                });
        }

    }
}
