using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Forms;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Garantias
{
    public partial class SearchGarantiaByProperty:BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter]
        public string[] Columns { get; set; } = { };
        private GarantiaGetParams GetParams { get; set; } = new GarantiaGetParams();
        private string SelectedColumn { get; set; }
        private string OrderBy { get; set; }
        private int SelectedPropertySearch { get; set; } = 1;
        private GarantiaConMarcaYModelo _Cliente { get; set; }
        private string SearchText { get; set; }
        //private Cliente Cliente { get { return _Cliente; } set { _Cliente = value; onSelectCliente(value); } }
        [Inject]
        GarantiasService GarantiasService { get; set; }
        IEnumerable<GarantiaConMarcaYModelo> garantias { get; set; } = new List<GarantiaConMarcaYModelo>();
        [Parameter]
        public GarantiaConMarcaYModelo Value
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
        public EventCallback<GarantiaConMarcaYModelo> ValueChanged { get; set; }
        MudMessageBox MudMessageBox { get; set; }

        private GarantiaConMarcaYModelo _value;
        //private async Task Get()
        //{
        //    PrestamoClienteUIGetParam param = new PrestamoClienteUIGetParam();
        //    param = await SearchFor(SelectedPropertySearch, SearchText);
        //    if (GetParams != null)
        //    {
        //        prestamos = await PrestamosService.GetPrestamoClienteUI(param);
        //        //prestamos.Add(prestamo);
        //    }
        //    else
        //    {
        //        await Alert("El Id del prestamo debe ser mayor a 0.");
        //        //await SweetMessageBox("El Id del prestamo debe ser mayor a 0.", "error");
        //    }
            
        //}
        //private async Task onSelectCliente(Cliente cl)
        //{
        //    GetParams = new PrestamoClienteUIGetParam { IdCliente = cl.IdCliente };
        //    await Get();
        //}
        private async Task onSearchClick()
        {
            await Get();
        }

        private async Task<GarantiaGetParams> Get()
        {
            bool isDefined = Enum.IsDefined(typeof(eOpcionesSearchGarantia), SelectedPropertySearch);
            GarantiaGetParams param = new GarantiaGetParams();
            if (isDefined)
            {
                if (SearchText.Length <= 2)
                {
                    await NotifyMessageBySnackBar("Debe digitar minimo 2 digitos (letras y/o numeros) para realizar la busqueda",Severity.Error);

                }
                else
                {
                    eOpcionesSearchGarantia opcion = (eOpcionesSearchGarantia)SelectedPropertySearch;
                   
                    switch (opcion)
                    {
                        case eOpcionesSearchGarantia.TextoLibre:
                            //param.s = searchText;
                            garantias = await GarantiasService.SearchGarantias(SearchText);
                            break;
                        case eOpcionesSearchGarantia.NoIdentificacion:
                            garantias = await GarantiasService.GetGarantias(new GarantiaGetParams { NoIdentificacion = SearchText });
                            break;
                        case eOpcionesSearchGarantia.Placa:
                            garantias = await GarantiasService.GetGarantias(new GarantiaGetParams { Placa = SearchText });
                            break;
                        case eOpcionesSearchGarantia.Matricula:
                            garantias = await GarantiasService.GetGarantias(new GarantiaGetParams { Matricula = SearchText });
                            break;
                        default:
                            break;
                    }
                }
            
                //eOpcionesSearchPrestamo enumOp = (eOpcionesSearchPrestamo)SelectedProperty;
                //switch (enumOp)
                //{
                //    case eOpcionesSearchPrestamo.NoIdentificacion:
                //        param.NoIdentificacion = searchText;
                //        break;
                //    case eOpcionesSearchPrestamo.Nombres:
                //        param.Nombres = searchText;
                //        break;
                //    case eOpcionesSearchPrestamo.Apellidos:
                //        param.Apellidos = searchText;
                //        break;
                //    case eOpcionesSearchPrestamo.NombreCompleto:
                //        param.NombreCompleto = searchText;
                //        break;
                //    case eOpcionesSearchPrestamo.Placa:
                //        param.Placa = searchText;
                //        break;
                //    case eOpcionesSearchPrestamo.Matricula:
                //        param.Matricula = searchText;
                //        break;
                //    default:
                //        break;
                //}
            }
            return param;
        }
        private async Task SelectedValue(GarantiaConMarcaYModelo select)
        {
            Value = select;
            if (MudDialog != null)
            {
                MudDialog.Close(DialogResult.Ok(select));
            }
        }
    }
}
