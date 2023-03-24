using MudBlazor;

namespace PrestamoBlazorApp.Domain
{
    public class Showdialogs
    {
        public static DialogOptions BasicOptions => new DialogOptions{ DisableBackdropClick=true, MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        public static DialogOptions DialogMedium => new DialogOptions { DisableBackdropClick = true, MaxWidth = MaxWidth.Medium, FullWidth = true, CloseOnEscapeKey = true };

    }
}
