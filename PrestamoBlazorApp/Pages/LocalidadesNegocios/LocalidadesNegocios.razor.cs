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

        private LocalidadNegociosGetParams LocalidadNegociosGetParams { get; set; } = new LocalidadNegociosGetParams { Opcion = 1 };
        private IEnumerable<LocalidadNegocio> localidadesnegocios { get; set; } = new List<LocalidadNegocio>();
        private LocalidadNegocio LocalidadNegocio { get; set; } = new LocalidadNegocio();
        public int IdLocalidadNegocio { get; set; } = -1;
        protected override async Task OnInitializedAsync()
        {
            await Handle_GetData(GetNegocios);
        }

        private async Task GetNegocios()
        {
            this.localidadesnegocios = await LocalidadesNegociosService.Get(new LocalidadNegociosGetParams { Opcion = 1 });
        }

        public virtual void OnEditClick(string url)
        {
            NavManager.NavigateTo(url);
        }
    }
}
