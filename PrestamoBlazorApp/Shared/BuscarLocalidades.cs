using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public partial class BuscarLocalidades
    {
        [Inject]
        LocalidadesService localidadService { get; set; }
        
        [Parameter]
        public EventCallback<BuscarLocalidad> OnLocalidadSelected { get; set; }
        IEnumerable<BuscarLocalidad> LocalidadesFiltradas { get; set; }

        bool selectLocalidad { get; set; } = false;
        IEnumerable<BuscarLocalidad> Localidades { get; set; }

        BuscarLocalidadParams buscarLocalidad { get; set; }
        int SelectedLocalidad { get; set; } = 1;

        string LocalidadElegida { get; set; } = "Ninguna";
        int totalRegistros { get; set; }
        bool loading { get; set; }
        protected override async Task OnInitializedAsync()
        {
            this.buscarLocalidad = new BuscarLocalidadParams();
            await GetTodasLasLocalidadesQueAceptenCalles();
            await base.OnInitializedAsync();
        }

        private async Task GetTodasLasLocalidadesQueAceptenCalles()
        {
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
