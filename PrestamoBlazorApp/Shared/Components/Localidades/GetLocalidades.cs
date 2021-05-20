using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Localidades
{
    public partial class GetLocalidades : BaseForSearch
    {
        [Inject]
        LocalidadesService localidadService { get; set; }
        [Parameter]
        public EventCallback<BuscarLocalidad> OnLocalidadSelected { get; set; }
        [Parameter]
        public bool LocalidadSoloConCalle { get; set; } = false;
        //public IEnumerable<BuscarLocalidad> LocalidadesFiltradas { get; set; } = new List<BuscarLocalidad>();
        BuscarLocalidadParams buscarLocalidad { get; set; } = new BuscarLocalidadParams();
       
        IEnumerable<BuscarLocalidad> Localidades { get; set; } = new List<BuscarLocalidad>();
        private int? _SelectedLocalidad = null;
        [Parameter]
        public int? SelectedLocalidad { get { return _SelectedLocalidad; } set { _SelectedLocalidad = value; Seleccionar(); } } 
        private void Seleccionar()
        {
            //SelectedLocalidad = Convert.ToInt32(args.);
            var selected = Localidades.Where(m => m.IdLocalidad == SelectedLocalidad).FirstOrDefault();
            OnLocalidadSelected.InvokeAsync(selected);
            
        }
        private async Task GetLocalidadesAll()
        {
            //await CountAndShowExecutionTime("Localidades AferRender");
            //this.buscarLocalidad = new BuscarLocalidadParams();
            this.buscarLocalidad.SoloLosQuePermitenCalle = LocalidadSoloConCalle;
            this.buscarLocalidad.MinLength = 0;
            var result = await localidadService.BuscarLocalidad(buscarLocalidad);
            this.Localidades = result;
           
        }
    }
}
