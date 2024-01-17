using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBlazorApp.Shared.Components.Forms;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Localidades.Components.LocalidadNegociosListForSelect
{
    public partial class SearchLocalidadesByProperty:BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        //[Parameter]
        //public string[] Columns { get; set; } = { };
        private string SearchText { get; set; }
        private string SelectedColumn { get; set; }
        private string OrderBy { get; set; }
        private int SelectedPropertySearch { get; set; } = 1;

        [Inject]
        LocalidadesService LocalidadesService { get; set; }
        IEnumerable<Localidad> clientes { get; set; } = new List<Localidad>();
        [Parameter]
        public Localidad Value
        {
            get => _value;
            set
            {
                if (_value == value) return;

                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<Localidad> ValueChanged { get; set; }

        private Localidad _value;
        public virtual void OnEditClick(string url)
        {
            NavManager.NavigateTo(url);
        }

        private async Task GetLocalidades()
        {
            clientes = await LocalidadesService.BuscarLocalidad(new BuscarLocalidadParams { Search = SearchText });
        }
        private async Task onSearchClick()
        {
            await GetLocalidades();
            //await GetClientes();
        }

        private async Task SelectClient(Localidad localidad)
        {
            Value = localidad;
            if (MudDialog != null)
            {
                MudDialog.Close(DialogResult.Ok(localidad));
            }
        }
    }
}
