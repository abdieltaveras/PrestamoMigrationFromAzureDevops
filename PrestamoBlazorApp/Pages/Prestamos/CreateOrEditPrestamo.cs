using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Models;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Prestamos
{
    public partial class CreateOrEditPrestamo : BaseForCreateOrEdit
    {
        private PrestamoConCalculos prestamo { get; set; } = new PrestamoConCalculos();

        private bool ShowSearchGarantia { get; set; } = false;
        [Inject]
        PrestamosService prestamoService { get; set; }
        //        en la clasificacion que indique
        //si lleva o no garantia
        //si lleva o no Codeudor

        //        igual tendremos tipos de vista de clientes
        //Full view
        //Basic View


        [Parameter]
        public int idPrestamo { get; set; } = -1;

        private string CodigoCliente { get; set; } = string.Empty;
        private string CodigoCodeudor { get; set; } = string.Empty;
        private string CodigoGarantia { get; set; } = string.Empty;
        private string CodigoPeriodo { get; set; } = string.Empty;
        private string CodigoInteres { get; set; } = string.Empty;
        private string CodigoMora { get; set; } = string.Empty;

        private bool SinVencimiento { get; set; } = true;
        //private decimal _montoPrestado;
        //decimal MontoPrestado { get { return _montoPrestado; } set { this._montoPrestado = value; OnChangeMontoText(value); } }
        //string FormattedMontoPrestadoText { get; set; }

        [Inject]
        ClasificacionesService clasificacionesService { get; set; }

        [Inject]
        TiposMoraService tiposMorasService { get; set; }

        [Inject]
        TasasInteresService tasasInteresService { get; set; }
        [Inject]
        PeriodosService periodosService { get; set; }


        [Inject]
        GarantiasService GarantiasService { get; set; }

        IEnumerable<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        IEnumerable<TipoMora> TiposMora { get; set; } = new List<TipoMora>();

        internal IEnumerable<TasaInteres> TasasDeInteres { get; set; } = new List<TasaInteres>();
        internal IEnumerable<Periodo> Periodos { get; set; } = new List<Periodo>();


        protected override async Task OnInitializedAsync()
        {
            await Handle_GetData(InitPrestamo);
        }

        private async Task InitPrestamo()
        {
            this.loading = true;
            Clasificaciones = await clasificacionesService.Get(new ClasificacionesGetParams());
            TiposMora = await tiposMorasService.Get(new TipoMoraGetParams());
            TasasDeInteres = await tasasInteresService.Get(new TasaInteresGetParams());
            TasasDeInteres = TasasDeInteres.ToList().OrderBy(ti => ti.InteresMensual);
            Periodos = await periodosService.Get(new PeriodoGetParams());

            if (idPrestamo > 0)
            {
                var getResult = await prestamoService.GetConDetallesForUiAsync(idPrestamo);
                //var getResult2 = await prestamoService.GetByIdAsync(idPrestamo);
                if (getResult == null)
                {
                    await SweetMessageBox("Lo siento no encontramos prestamo para su peticion", redirectTo: "/prestamos");
                };

                prestamo = getResult.infoPrestamo.ToJson().ToType<PrestamoConCalculos>();
                
                prestamo.SetServices(this.NotificadorDeMensaje, Clasificaciones, TiposMora, TasasDeInteres, Periodos);
                
                //var resultCliente = await clientesService.GetClientesAsync(new ClienteGetParams { IdCliente = prestamo.IdCliente });
                //var cliente = resultCliente.FirstOrDefault();
                updateInfoCliente(getResult.infoCliente);
                // todo: debe llamar el arreglo de garantia cuando tenga mas de una
                // debe llamar es las garantias indicandole el idDelprestamo
                //var resultGarantia = await GarantiasService.GetGarantiasByPrestamo(prestamo.IdPrestamo);
                //var garantia = resultGarantia.FirstOrDefault();
                //var garantiaConMarcaYModelo = new GarantiaConMarcaYModelo();
                updateInfoGarantia(getResult.infoGarantias);
                
            }
            else
            {
                prestamo.SetServices(this.NotificadorDeMensaje, Clasificaciones, TiposMora, TasasDeInteres, Periodos);

                this.prestamo.PrestamoNumero = "Nuevo";
                this.prestamo.IdClasificacion = Clasificaciones.FirstOrDefault().IdClasificacion;
                this.prestamo.IdTipoAmortizacion = (int)TiposAmortizacion.No_Amortizable_cuotas_fijas;
                this.prestamo.IdPeriodo = Periodos.FirstOrDefault().idPeriodo;
                this.prestamo.IdTipoMora = TiposMora.FirstOrDefault().IdTipoMora;
                this.prestamo.IdTasaInteres = TasasDeInteres.FirstOrDefault().idTasaInteres;
                await setParametros.ForPrestamo(this.prestamo);
                //await prestamoCalculo.UpdatePrestamoCalculo();
            }
            
            this.prestamo.ProyectarPrimeraYUltima = true;
            prestamo.ActivateCalculos();
            await prestamo.ExecCalcs();
            this.loading = false;
        }
        private void NotificadorDeMensaje(object sender, string e)
        {
            NotifyMessageBox(e);
        }

        private string InfoCuotas()
        {
            decimal montoCuota = 0;
            if (prestamo.TipoAmortizacion == TiposAmortizacion.No_Amortizable_cuotas_fijas)
            {
                montoCuota = prestamo.Cuotas != null ? prestamo.Cuotas[0].TotalOrig : 0;
            }
            var result = $"{prestamo.CantidadDePeriodos} - {prestamo.Periodo.Nombre} por valor de {montoCuota.ToString("C")}";
            return result;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await this.prestamo.ExecCalcs();
                //await JsInteropUtils.SetInputMask(jsRuntime);
            }
        }


        async Task SavePrestamo()
        {
            //todo: validationresult https://www.c-sharpcorner.com/UploadFile/20c06b/using-data-annotations-to-validate-models-in-net/

            var resultbool1 = prestamo.LlevaGarantia();
            var resultbool2 = prestamo.IdGarantias.Count() > 0;


            var prestamoValidator =
                Validator<PrestamoConCalculos>.Empty
                .IsNotValidWhen(p => p == null, "El prestamo no puede estar nulo", ValidationOptions.StopOnFailure)
                .IsValidWhen(p => p.IdClasificacion > 0, "Debe elegir una clasificacion valida")
                .IsNotValidWhen(p => p.LlevaGarantia() && p.IdGarantias.Count() <=0 , "Debe establecer una garantia")
                .IsValidWhen(p => p.MontoPrestado >= 0, "El monto a prestar no puede ser menor a 0 (cero)")
                .IsValidWhen(p => p.IdCliente > 0, "Debe establecer un cliente");

            var result = prestamoValidator.Validate(prestamo);
            var validacionesFallidas = result.Where(item => item.Success == false);
            var MensajesValidacionesFallida = string.Join(", ", validacionesFallidas.Select(item => item.Message));
            //var resultToJson  = result.Dump();
            if (validacionesFallidas.Count() > 0)
            {
                await SweetMessageBox("Se han encontrado errores " + MensajesValidacionesFallida, "error", "", 5000);
                return;
            }

            try
            {
                Prestamo pr = this.prestamo;
                await Handle_SaveData(async () => await prestamoService.SavePrestamo(pr));
            }
            catch (ValidationObjectException e)
            {
                await JsInteropUtils.NotifyMessageBox(jsRuntime, $"Lo siento error al guardar los datos mensaje recibido {e.Message}");
            }

        }

        private void CalcularGastoDeCierre()
        {
            prestamo.MontoGastoDeCierre = prestamo.LlevaGastoDeCierre ? prestamo.MontoPrestado * (prestamo.InteresGastoDeCierre / 100) : 0;
        }
        private async Task Test(MouseEventArgs mouseEventArgs)
        {
            await NotifyMessageBox("ejecutando Test");
            //JsInteropUtils.NotifyMessageBox
            await JsInteropUtils.ConsoleLog(jsRuntime, prestamo);
            var pr = this.prestamo;
            Console.WriteLine(prestamo);
        }



        private async Task UpdateLlevaGastoDeCierre(ChangeEventArgs arg)
        {

            prestamo.InteresGastoDeCierre = prestamo.LlevaGastoDeCierre ? 0 : 10;
            await prestamo.ExecCalcs();
        }




        int IdGarantiaSelected { get; set; }
        string InfoGarantia { get; set; }

        private async Task ActivateSearchGarantia()
        {
            InfoGarantia = string.Empty;
            ShowSearchGarantia = true;
        }

        private async Task UpdateGarantiaSelected(int idGarantia)
        {
            this.ShowSearchGarantia = false;
            this.IdGarantiaSelected = idGarantia;
            //await NotifyMessageBox("garantia seleccionada " + idGarantia);
            var Garantias = await GarantiasService.GetGarantias(new GarantiaGetParams { IdGarantia = idGarantia });
            var garantia = Garantias.FirstOrDefault();
            updateInfoGarantia(garantia);
        }

        private void updateInfoGarantia(GarantiaConMarcaYModelo garantia)
        {
            CodigoGarantia = garantia.NoIdentificacion;
            this.prestamo.IdGarantias.Add(garantia.IdGarantia);
            InfoGarantia = $"{garantia.NombreMarca} {garantia.NombreModelo} {garantia.DetallesJSON.Ano} {garantia.NombreColor}  placa {@garantia.DetallesJSON.Placa} matricula {@garantia.DetallesJSON.Matricula}";
        }

        private void updateInfoGarantia(IEnumerable<InfoGarantiaDrCr> garantias)
        {
            foreach (var garantia in garantias)
            {
                CodigoGarantia = garantia.NumeracionGarantia;
                this.prestamo.IdGarantias.Add(garantia.IdGarantia);
                InfoGarantia = $"{garantia.NombreMarca} {garantia.NombreModelo} {garantia.GetDetallesGarantia().Color}  placa {garantia.GetDetallesGarantia().Placa} matricula {@garantia.GetDetallesGarantia().Matricula}";
            }
        }

        int IdClienteSelected { get; set; }
        bool ShowSearchCliente { get; set; }
        string InfoCliente { get; set; }
        [Inject]
        ClientesService clientesService { get; set; }

        private async Task ActivateSearchCliente()
        {
            InfoCliente = string.Empty;
            ShowSearchCliente = true;
        }
        private async Task UpdateClienteSelected(int idCliente)
        {
            this.ShowSearchCliente = false;
            this.IdGarantiaSelected = idCliente;
            //await NotifyMessageBox("garantia seleccionada " + idGarantia);
            var clientes = await clientesService.GetClientesAsync(new ClienteGetParams { IdCliente = idCliente });
            var cliente = clientes.FirstOrDefault();
            updateInfoCliente(cliente);
        }

        private void updateInfoCliente(Cliente cliente)
        {
            CodigoCliente = cliente.Codigo;
            this.prestamo.IdCliente = cliente.IdCliente;
            InfoCliente = $"{cliente.NoIdentificacion} {cliente.NombreCompleto } ";
        }

        private void updateInfoCliente(InfoClienteDrCr cliente)
        {
            CodigoCliente = cliente.CodigoCliente;
            this.prestamo.IdCliente = cliente.IdCliente;
            InfoCliente = $"{cliente.NumeracionDocumentoIdentidad} {cliente.NombreCompleto} ";
        }

        private async Task OnCloseSearchCliente() => ShowSearchCliente = false;
        private async Task OnCloseSearchGarantia() => ShowSearchGarantia = false;
    }

    public class PrestamoValidator
    {
        public void Buil()
        {

        }
    }
}
