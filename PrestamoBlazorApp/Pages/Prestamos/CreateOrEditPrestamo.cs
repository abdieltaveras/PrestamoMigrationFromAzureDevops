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
            this.prestamo = new PrestamoConCalculos(this.NotificadorDeMensaje, Clasificaciones, TiposMora, TasasDeInteres, Periodos);
            this.prestamo.PrestamoNumero = "Nuevo";
            this.prestamo.IdClasificacion = Clasificaciones.FirstOrDefault().IdClasificacion;
            this.prestamo.IdTipoAmortizacion = (int)TiposAmortizacion.No_Amortizable_cuotas_fijas;
            this.prestamo.IdPeriodo = Periodos.FirstOrDefault().idPeriodo;
            this.prestamo.IdTipoMora = TiposMora.FirstOrDefault().IdTipoMora;
            this.prestamo.IdTasaInteres = TasasDeInteres.FirstOrDefault().idTasaInteres;
            await setParametros.ForPrestamo(this.prestamo);

            //await prestamoCalculo.UpdatePrestamoCalculo();
            this.loading = false;
        }

        private void NotificadorDeMensaje(object sender, string e)
        {
            NotifyMessageBox(e);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await this.prestamo.Update();
                //await JsInteropUtils.SetInputMask(jsRuntime);
            }
        }


        async Task SavePrestamo()
        {

            //todo: validationresult https://www.c-sharpcorner.com/UploadFile/20c06b/using-data-annotations-to-validate-models-in-net/

            try
            {
                //await prestamoService.SavePrestamo(this.prestamo);
                //await OnGuardarNotification(redirectTo: "/Prestamos");
            }
            catch (Exception e)
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
            await prestamo.Update();
        }

        private async Task ActivateSearchGarantia()
        {
            InfoGarantia = string.Empty;
            ShowSearchGarantia = true;
        }

        int IdGarantiaSelected { get; set; }
        string InfoGarantia { get; set; }
        private async Task UpdateGarantiaSelected(int idGarantia)
        {
            this.ShowSearchGarantia = false;
            this.IdGarantiaSelected = idGarantia;
            //await NotifyMessageBox("garantia seleccionada " + idGarantia);
            var Garantias = await GarantiasService.GetGarantias(new GarantiaGetParams { IdGarantia = idGarantia });
            var garantia = Garantias.FirstOrDefault();
            CodigoGarantia = garantia.NoIdentificacion;
            InfoGarantia = $"{garantia.NombreMarca} {garantia.NombreModelo} {garantia.DetallesJSON.Ano} {garantia.NombreColor}  placa {@garantia.DetallesJSON.Placa} matricula {@garantia.DetallesJSON.Matricula}";
        }
    }


}
