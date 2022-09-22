using MudBlazor;

namespace PrestamoBlazorApp.Services
{

    public static class OptionsForDialog
    {
        public static DialogOptions SmallFullWidthCloseButtonCenter => new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseButton = true, Position = DialogPosition.Center };

        public static DialogOptions closeButton => new DialogOptions() { CloseButton = true };
        public static DialogOptions noHeader => new DialogOptions() { NoHeader = true };
        public static DialogOptions disableBackdropClick => new DialogOptions() { DisableBackdropClick = true };
        public static DialogOptions fullScreen => new DialogOptions() { FullScreen = true, CloseButton = true };
        public static DialogOptions topCenter => new DialogOptions() { Position = DialogPosition.Center };
    }

}
