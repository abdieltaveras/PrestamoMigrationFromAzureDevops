using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UIClient.Services;

namespace UIClient.Pages.Authentication
{
    public partial class Reset
    {
        [Inject] protected NotificationService notificationsService { get; set; }
        [Inject] NavigationManager navManager { get; set; }
        [Inject] UserManagerService userManagerService { get; set; }

        MudTextField<string> pwField1;
        [Parameter] public string guid { get; set; }
        string Password { get; set; }

        bool PasswordVisibility;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        void TogglePasswordVisibility()
        {
            if(PasswordVisibility)
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
        private IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "La contraseña es requerida!";
                yield break;
            }
            if (pw.Length < 8)
                yield return "La contraseña por lo menos debe tener 8 caracteres";
            if (!Regex.IsMatch(pw, @"[A-Z]"))
                yield return "La contraseña debe tener por lo menos una letra en Mayúscula";
            if (!Regex.IsMatch(pw, @"[a-z]"))
                yield return "La contraseña debe tener por lo menos una letra en minúscula";
            if (!Regex.IsMatch(pw, @"[0-9]"))
                yield return "La contraseña debe tener por lo menos un número";
        }

        private string PasswordMatch(string arg)
        {
            if (pwField1.Value != arg)
                return "Las contraseñas no son iguales";
            return null;
        }
        private async void Submmit()
        {
            await userManagerService.ChangeUserPassword(Guid.Parse(guid), Password);
            notificationsService.Notify("Contraseña cambiada con exito", "*", "*", "*", true);
            navManager.NavigateTo("/");
        }
    }
}
