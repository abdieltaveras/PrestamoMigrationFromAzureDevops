using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public partial class BuscarLocalidades : BaseForSearch
    {
        [Inject]
        LocalidadesService localidadService { get; set; }
        
        [Parameter]
        public EventCallback<BuscarLocalidad> OnLocalidadSelected { get; set; }
        IEnumerable<BuscarLocalidad> LocalidadesFiltradas { get; set; } = new List<BuscarLocalidad>();

        bool selectLocalidad { get; set; } = false;
        IEnumerable<BuscarLocalidad> Localidades { get; set; } = new List<BuscarLocalidad>();

        BuscarLocalidadParams buscarLocalidad { get; set; } = new BuscarLocalidadParams();
        private int SelectedLocalidad { get; set; } = 1;

        private string LocalidadElegida { get; set; } = "Ninguna";
        private int totalRegistros { get; set; }
        private bool loading { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Handle_GetDataForSearch(GetTodasLasLocalidadesQueAceptenCalles);
            }
        }
        private async Task GetTodasLasLocalidadesQueAceptenCalles()
        {
            await CountAndShowExecutionTime("Localidades AferRender");
            this.buscarLocalidad = new BuscarLocalidadParams();
            this.buscarLocalidad.SoloLosQuePermitenCalle = true;
            this.buscarLocalidad.MinLength = 0;
            var result =  await localidadService.BuscarLocalidad(buscarLocalidad);
            this.Localidades = result;
        }

        private void filtrar() 
        {

            loading = true;
            this.LocalidadesFiltradas = Localidades.Where(loc => loc.Nombre.ToLower().Contains(this.buscarLocalidad.Search.ToLower()));
            totalRegistros = this.LocalidadesFiltradas == null ? 0 : this.LocalidadesFiltradas.Count();
            if (totalRegistros > 0)
            {
                this.SelectedLocalidad = this.LocalidadesFiltradas.FirstOrDefault().IdLocalidad;
            }
            selectLocalidad = totalRegistros > 0;
            loading = false;
        }

        void seleccionar()
        {
            var locSelected = Localidades.Where(loc => loc.IdLocalidad == SelectedLocalidad).FirstOrDefault();
            OnLocalidadSelected.InvokeAsync(locSelected);
            selectLocalidad = false;
        }
    }
}
