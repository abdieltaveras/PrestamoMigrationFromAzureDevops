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
    public partial class Forgot
    {
        [Inject] protected NotificationService notificationsService { get; set; }
        [Inject] NavigationManager navManager { get; set; }
        [Inject] UserManagerService userManagerService { get; set; }
        string Cedula { get; set; } = "";
        bool validForm;
        string[] errors = { };

        MudForm form;

        string validateCedula(string value)
        {
            var error = "";
            var isValid = Regex.IsMatch(value, "\\d{11}")
                          || Regex.IsMatch(value, "\\d{3}-\\d{7}-\\d");
            if (!isValid)
            {
                error = "La cédula solo debe contener números";
            }
            return error;
        }
        bool validateForm()
        {
            var cedError = validateCedula(Cedula);
            if (cedError != "")
            {
                errors = errors.Append(cedError).ToArray();
            }
            validForm = errors.Length == 0;
            return validForm;
        }
        async void Submmit()
        {
            validateForm();
            if (validForm)
            {
                await userManagerService.ResetUserPassword(Cedula);
                notificationsService.Notify("Correo Enviado", "*", "*", "*", true);
                navManager.NavigateTo("/");
            }
        }
    }
}
