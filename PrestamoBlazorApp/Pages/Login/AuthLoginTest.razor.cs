using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using MudBlazor;
using Newtonsoft.Json.Linq;
using PrestamoBlazorApp.Providers;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIClient.Services;

namespace PrestamoBlazorApp.Pages.Login
{
    public partial class AuthLoginTest
    {
        
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }
        [Inject]
        AuthService _AuthService { get; set; }
        private Users users { get; set; } = new Users();
        private LoginCredentialsDto _LoginCredentialsDto { get; set; }
        private IEnumerable<Users> UserList { get; set; } = new List<Users>();
        [Inject] UserManagerService userManagerService { get; set; }
        [Inject] TokenAuthenticationStateProvider authStateProvider { get; set; }
        [Inject] AuthenticationStateProvider _authenticationStateProvider { get; set; }
        [Inject] private ILocalStorageService _localStorage { get; set; }

        [Inject] ISiteResourcesService _siteResources { get; set; }
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
        private async Task HandleCheckAuthState()
        {
            
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        }
        private async Task HandleValidSubmit()
        {
            var response = await _AuthService.Login(_LoginCredentialsDto);
            //var ValidatedUser = ValidateUser(users);
            // Validar el usuario utilizando la clase UserValidator
            if (!string.IsNullOrEmpty( response.Token))
            {
                if (response.MustChgPwd)
                {
                    NavManager.NavigateTo("pages/authentication/forgot-password");
                }
                else
                {
                    await authStateProvider.SetTokenAsync(response.Token);
                    var randonValue = new Random().Next(100000000).ToString();
                    await _localStorage.SetItemAsStringAsync(_siteResources.LoggedOutKey, randonValue);
                    //NavManager.NavigateTo("/clientes");
                }
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






