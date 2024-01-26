using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.LocalidadesNegocios
{
    public partial class LocalidadesNegocios : BaseForCreateOrEdit
    {
        [Inject]
        public LocalidadesNegociosService LocalidadesNegociosService { get; set; }
        [Inject]
        public NegociosService NegociosService { get; set; }
        private IEnumerable<Negocio> negocios { get; set; } = new List<Negocio>();

        private LocalidadNegociosGetParams LocalidadNegociosGetParams { get; set; } = new LocalidadNegociosGetParams { Opcion = 1 };
        private IEnumerable<LocalidadNegocio> localidadesnegocios { get; set; } = new List<LocalidadNegocio>();
        private LocalidadNegocio LocalidadNegocio { get; set; } = new LocalidadNegocio();
        public int IdLocalidadNegocio { get; set; } = -1;
        public string Nombrecomercial { get; set; }
        protected override async Task OnInitializedAsync()
        {
            negocios = await NegociosService.Get(new NegociosGetParams());
            localidadesnegocios = await LocalidadesNegociosService.Get(new LocalidadNegociosGetParams { Opcion = 1 });

            var value = negocios.FirstOrDefault(item => item.NombreComercial != null);
            Nombrecomercial = value.NombreComercial;

        }
        //protected async Task SelectedNombreNegocio()
        //{
            

            
        //}

        public virtual void OnEditClick(string url)
        {
            NavManager.NavigateTo(url);
        }
    }
}
