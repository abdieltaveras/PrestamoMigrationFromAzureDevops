using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Localidades
{
    public partial class ListadoPaises
    {
        [Inject]
        LocalidadesService localidadesService { get; set; }
        [Parameter]
        public Localidad Localidad { get; set; } = new Localidad();
        IEnumerable<Localidad> localidades { get; set; } = new List<Localidad>();


        protected override async Task OnInitializedAsync()
        {

            var ter = await localidadesService.BuscarLocalidad(new BuscarLocalidadParams { Search = "", MinLength = 0 });
            this.localidades = ter.Where(m => m.IdLocalidadPadre == 0);
        }
      
        async Task SaveLocalidad()
        {
            //await BlockPage();
            this.Localidad.IdLocalidadPadre = 0;
            this.Localidad.IdTipoLocalidad = 3;
            this.Localidad.IdNegocio = 1;
            await Handle_SaveData(async () => await localidadesService.SaveLocalidad(this.Localidad), null, null,false, "/localidades/listadopaises");
            await CreateOrEdit(this.Localidad.IdLocalidad);
            //await UnBlockPage();

        }
        async Task CreateOrEdit(int idLocalidad = -1)
        {
            if (idLocalidad > 0)
            {
                
                await BlockPage();
                var localidad = await localidadesService.Get(new LocalidadGetParams { IdLocalidad = idLocalidad });
                Localidad = localidad.FirstOrDefault();
                await UnBlockPage();
            }
            else
            {
                this.Localidad = new Localidad();
            }
            await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }

        void RaiseInvalidSubmit()
        {

        }
    }
}
