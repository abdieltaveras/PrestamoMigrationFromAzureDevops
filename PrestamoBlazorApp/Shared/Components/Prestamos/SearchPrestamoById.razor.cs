using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Forms;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Prestamos
{
    public partial class SearchPrestamoById:BaseForCreateOrEdit
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
        private async Task Get()
        {
            PrestamoClienteUIGetParam param = new PrestamoClienteUIGetParam();
            param = await SearchFor(SelectedPropertySearch, SearchText);
            if (GetParams != null)
            {
                prestamos = await PrestamosService.GetPrestamoClienteUI(param);
                //prestamos.Add(prestamo);
            }
            else
            {
                await Alert("El Id del prestamo debe ser mayor a 0.");
                //await SweetMessageBox("El Id del prestamo debe ser mayor a 0.", "error");
            }
            
        }
        //private async Task onSelectCliente(Cliente cl)
        //{
        //    GetParams = new PrestamoClienteUIGetParam { IdCliente = cl.IdCliente };
        //    await Get();
        //}
        private async Task onSearchClick()
        {
            await Get();
        }

        private async Task<PrestamoClienteUIGetParam> SearchFor(int SelectedProperty, string searchText)
        {
            bool isDefined = Enum.IsDefined(typeof(eOpcionesSearchPrestamo), SelectedProperty);
            PrestamoClienteUIGetParam param = new PrestamoClienteUIGetParam();
            if (isDefined)
            {
                eOpcionesSearchPrestamo enumOp = (eOpcionesSearchPrestamo)SelectedProperty;
                switch (enumOp)
                {
                    case eOpcionesSearchPrestamo.NoIdentificacion:
                        param.NoIdentificacion = searchText;
                        break;
                    case eOpcionesSearchPrestamo.Nombres:
                        param.Nombres = searchText;
                        break;
                    case eOpcionesSearchPrestamo.Apellidos:
                        param.Apellidos = searchText;
                        break;
                    default:
                        break;
                }
            }
            return param;
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
