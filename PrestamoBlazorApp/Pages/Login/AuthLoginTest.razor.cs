using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Login
{
    public partial class AuthLoginTest
    {
        [Inject]
        AuthService _AuthService { get; set; }
        private Users users { get; set; } = new Users();
        private LoginCredentialsDto _LoginCredentialsDto { get; set; }
        private IEnumerable<Users> UserList { get; set; } = new List<Users>();
        protected override void OnInitialized()
        {
            _LoginCredentialsDto = new LoginCredentialsDto
            {
                CompanyCode = "C1",
                UserName= "PcProg",
                Password= "pcp46232"
            };
            base.OnInitialized();
        }

        private async Task HandleValidSubmit()
        {
            var response = await _AuthService.Login(_LoginCredentialsDto);
            //var ValidatedUser = ValidateUser(users);
            // Validar el usuario utilizando la clase UserValidator
            if (!string.IsNullOrEmpty( response.Token))
            {
                // Usuario autenticado con éxito
                // Puedes redirigir a otra página, establecer información de sesión, etc.
                //await NavigateTo("/loginEmpresa");
                await NotifyMessageBySnackBar("Acceso concedido ", Severity.Success);

            }
            else
            {
                await NotifyMessageBySnackBar("Credenciales incorrectas", Severity.Error);
            }

        }

    }
}






