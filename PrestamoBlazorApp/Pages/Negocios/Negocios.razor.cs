using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Negocios
{
    public partial class Negocios : BaseForCreateOrEdit
    {
        [Inject]
        public NegociosService NegociosService { get; set; }


        private IEnumerable<Negocio> negocios { get; set; } = new List<Negocio>();
        public int IdLocalidadNegocio { get; set; } = -1;
        protected override async Task OnInitializedAsync()
        {
            negocios = await NegociosService.Get(new NegociosGetParams());
        }

        public virtual void OnEditClick(string url)
        {
            NavManager.NavigateTo(url);
        }
    }
}
