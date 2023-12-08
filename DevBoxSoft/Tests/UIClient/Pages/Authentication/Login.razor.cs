using DevBox.Core.Classes.Identity;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIClient.Providers;
using UIClient.Services;

namespace UIClient.Pages.Authentication
{
    public partial class Login
    {
        [Inject] NavigationManager NavManager { get; set; }
        [Inject] UserManagerService userManagerService { get; set; }
        [Inject] ApiAuthenticationStateProvider authStateProvider { get; set; }
        string Usuario { get; set; } = "Ernesto";
        string Password { get; set; } = "01234567890";

        // for abdiel 5892430

        bool PasswordVisibility;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        async Task OnSingInClick()
        {
            var token = await userManagerService.AuthenticateUserAsync(Usuario, Password);
            if (token.Token != null)
            {
                if (token.MustChgPwd)
                {
                    NavManager.NavigateTo("/pages/authentication/forgot-password");
                }
                else
                {
                    await authStateProvider.SetTokenAsync(token.Token);
                    NavManager.NavigateTo("/");
                }
            }
            else
            {
                NavManager.NavigateTo("Unauthorized");
            }
            
        }
        void TogglePasswordVisibility()
        {
            if (PasswordVisibility)
            {
                PasswordVisibility = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                PasswordVisibility = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}
