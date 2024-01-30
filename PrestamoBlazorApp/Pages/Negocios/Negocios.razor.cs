using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using MudBlazor;
using PrestamoBlazorApp.Pages.Localidades.Components.LocalidadNegociosListForSelect;

namespace PrestamoBlazorApp.Pages.Negocios
{
    public partial class Negocios : BaseForCreateOrEdit
    {
        [Inject]
        public NegociosService NegociosService { get; set; }
        [Inject]
        IDialogService DialogService { get; set; }
        private IEnumerable<Negocio> negocios { get; set; } = new List<Negocio>();
        public int IdLocalidadNegocio { get; set; } = -1;
        private Localidad LocalidadSelected { get; set; }
        protected override async Task OnInitializedAsync()
        {
            negocios = await NegociosService.Get(new NegociosGetParams());

        }

        public virtual void OnEditClick(string url)
        {
            NavManager.NavigateTo(url);
        }
        private async Task AsignarLocalidad(int id)
        {
            var parameters = new DialogParameters { };
            DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
            var dialog = DialogService.Show<SearchLocalidadesByProperty>("Seleccionar Localidad", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                LocalidadSelected = (Localidad)result.Data;
            }
        }
    }
}
