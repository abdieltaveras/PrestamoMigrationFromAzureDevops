using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Domain;
using PrestamoBlazorApp.Pages.Prestamos.Components.PrestamoByColumnSelected;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Prestamos
{
    public partial class SearchPrestamoByProperty : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter]
        public string[] Columns { get; set; } = { };
        private PrestamoClienteUIGetParam GetParams { get; set; } = new PrestamoClienteUIGetParam();
        private string SelectedColumn { get; set; }
        private string OrderBy { get; set; }
        private int SelectedPropertySearch { get; set; } = 1;
        private Cliente _Cliente { get; set; }
        private string SearchText { get; set; }
        //private Cliente Cliente { get { return _Cliente; } set { _Cliente = value; onSelectCliente(value); } }
        [Inject]
        PrestamosService PrestamosService { get; set; }
        IEnumerable<PrestamoClienteUI> prestamos { get; set; } = new List<PrestamoClienteUI>();
        [Parameter]
        public PrestamoClienteUI Value
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
        public EventCallback<PrestamoClienteUI> ValueChanged { get; set; }
        MudMessageBox MudMessageBox { get; set; }

        private PrestamoClienteUI _value;

        private IGetDataByColumSelection<PrestamoClienteUIGetParamWtSearchText> PrestamosSearch { get; set; } = new PrestamoGetDataByColumnSelected();

        

        public async Task GetPrestamos(PrestamoClienteUIGetParamWtSearchText param)
        {
            prestamos = await PrestamosService.GetPrestamoClienteUI(param);
        
        }
        protected async Task GetData()
        {
            await PrestamosSearch.ExecGetDataAction(SelectedPropertySearch, SearchText, GetPrestamos);

        }
        
        private async Task SelectedValue(PrestamoClienteUI select)
        {
            Value = select;
            if (MudDialog != null)
            {
                MudDialog.Close(DialogResult.Ok(select));
            }
        }
    }
}
