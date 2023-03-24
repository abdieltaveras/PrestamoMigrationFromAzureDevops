using MudBlazor;

namespace PrestamoBlazorApp.Domain
{
    public class Showdialogs
    {
        public static DialogOptions DialogMedium => new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true, CloseOnEscapeKey = true, DisableBackdropClick=true };

        public static DialogOptions BasicOptions => new DialogOptions { DisableBackdropClick = true,MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, CloseButton = true, CloseOnEscapeKey = true };
    }
}