using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using PrestamoBlazorApp.Shared.Components;

using MudBlazor;
using PrestamoBlazorApp.Shared.Components.Localidades;
using PrestamoBlazorApp.Domain;

namespace PrestamoBlazorApp.Pages.Localidades
{
    public partial class ListadoPaises:BaseForCreateOrEdit
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        LocalidadesService localidadesService { get; set; }
        [Parameter]
        public Localidad Localidad { get; set; } = new Localidad();
        IEnumerable<Localidad> localidades { get; set; } = new List<Localidad>();


        protected override async Task OnInitializedAsync()
        {

            await GetLocalidades();
        }
      
        async Task SaveLocalidad()
        {

            //this.Localidad.IdTipoDivisionTerritorial = 3;
            this.Localidad.IdNegocio = 1;
            await Handle_SaveData(async () => await localidadesService.SaveLocalidad(this.Localidad), null, null,false, "/localidades/listadopaises");
            await CreateOrEdit(this.Localidad.IdLocalidad);
            //await UnBlockPage();

        }
        async Task GetLocalidades()
        {
            //var ter = await localidadesService.BuscarLocalidad(new BuscarLocalidadParams { Search = "", MinLength = 0 });
            //this.localidades = ter.Where(m => m.IdLocalidadPadre == 0);
            var ter = await localidadesService.BuscarLocalidad(new BuscarLocalidadParams { Search = "", MinLength = 0 });
            this.localidades = ter.Where(m => m.IdLocalidadPadre == 0);
        }
        async Task CreateOrEdit(int idLocalidad = -1)
        {
            await BlockPage();
            var parameters = new DialogParameters();
            parameters.Add("IdLocalidad", idLocalidad);
            //dialogOptions.MaxWidth = MaxWidth.Medium;
            var dialog = DialogService.Show<Shared.Components.Localidades.CreatePais>("Crear Pais",parameters,Showdialogs.DialogMedium);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetLocalidades();
            }
            //var localidad = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = idLocalidad });
            //Localidad = localidad.FirstOrDefault();
            await UnBlockPage();
        }
        private async Task ShowDialog(Localidad localidad)
        {
            var parameters = new DialogParameters();
            parameters.Add("IdLocalidad", localidad.IdLocalidad);
            var dialog = DialogService.Show<AddLocalidadesToPais>($"Agregar Localidades a {localidad.Nombre}", parameters, Showdialogs.DialogMedium);
            var result = await dialog.Result;
            if (result.Data != null)
            {
                if (result.Data.ToString() == "1")
                {
                    await GetLocalidades();
                    StateHasChanged();
                }
            }

        }
        
        void RaiseInvalidSubmit()
        {

        }
    }
}
