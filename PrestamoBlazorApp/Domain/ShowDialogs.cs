using MudBlazor;

namespace PrestamoBlazorApp.Domain
{
    public class ShowDialogs
    {
        public static DialogOptions DlgOptionsMediumFullWidth => new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true, CloseOnEscapeKey = true, DisableBackdropClick=true };

        public static DialogOptions DlgOptionsExtraSmallFullWidth => new DialogOptions { MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, CloseButton = true, CloseOnEscapeKey = true };
    }
}