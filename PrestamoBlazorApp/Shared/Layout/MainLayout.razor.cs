using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Hosting;
using MudBlazor;
using PrestamoBlazorApp.Shared.MudUtils;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Shared.Layout
{
    public partial class MainLayout
    {
        [Inject] IWebHostEnvironment env { get; set; }
        [Inject] ProtectedSessionStorage _ProtectedSessionStorage { get; set; }
        public static bool SetOverLoad { get; set; } = false;
        private bool DrawerOpen = true;
        private string TextoForDrawer => DrawerOpen ? "Ocultar Menu" : "Mostrar Menu";

        private string TextoForAuth => AuthMenu ? "Desactivar Autorizacion" : "Activar Autorizacion";
        
        void DrawerToggle()
        {
            DrawerOpen = !DrawerOpen;
        }
        private MudTheme _currentTheme = new MudBlazorAdminDashboard();
        private bool AuthMenu { get; set; }
        private bool IsDevelopment => env.EnvironmentName == "Development";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var result = await _ProtectedSessionStorage.GetAsync<bool>("AuthMenu");

            if (result.Success)
            {
               AuthMenu = result.Value;
            }
            else
            {
                await Handle_AuthMenuChange(!AuthMenu);
            }
            StateHasChanged();
        }

        private async Task Handle_AuthMenuChange(bool value)
        {
            //await Task.Run(()=>AuthMenu = !AuthMenu);
            AuthMenu = !AuthMenu;
            await _ProtectedSessionStorage.SetAsync("AuthMenu", AuthMenu);
        }

        
    }
}
