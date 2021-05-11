using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
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
        Prestamo prestamo { get; set; } = new Prestamo();

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
        PrestamoCalculo prestamoCalculo { get; set; } = new PrestamoCalculo();
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

        IEnumerable<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        IEnumerable<TipoMora> TiposMora { get; set; } = new List<TipoMora>();

        List<Cuota> Cuotas { get; set; } = new List<Cuota>();

        IEnumerable<TasaInteres> TasasDeInteres { get; set; } = new List<TasaInteres>();
        IEnumerable<Periodo> Periodos { get; set; } = new List<Periodo>();


        protected override async Task OnInitializedAsync()
        {
            await Handle_GetData(InitPrestamo);
        }

        private async Task InitPrestamo()
        {
            prestamo = new Prestamo();
            await setParametros.ForPrestamo(prestamo);
            Clasificaciones = await clasificacionesService.Get(new ClasificacionesGetParams());
            TiposMora = await tiposMorasService.Get(new TipoMoraGetParams());
            TasasDeInteres = await tasasInteresService.Get(new TasaInteresGetParams());
            TasasDeInteres = TasasDeInteres.ToList().OrderBy(ti => ti.InteresMensual);
            Periodos = await periodosService.Get(new PeriodoGetParams());
            prestamo.PrestamoNumero = "Nuevo";
            prestamoCalculo = new PrestamoCalculo(prestamo, jsRuntime,Periodos, TasasDeInteres, Cuotas);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInteropUtils.SetInputMask(jsRuntime);
                
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

        //private void OnGastoDeCierreChange()
        //{
        //    NotifyMessageBox("GC changed");
        //    CalcularGastoDeCierre();
        //}

        //private void OnChangeMontoText(decimal value)
        //{
        //    FormattedMontoPrestadoText = string.Format("{0:c}", value);
        //    this.prestamo.MontoPrestado = value;
        //    CalcularGastoDeCierre();
        //}

        
    }

    public class PrestamoCalculo
    {
        Prestamo Prestamo { get; set; } = new Prestamo();

        
        [Inject]
        JsInteropUtils JsInteropUtils { get; set; }
        
        private IJSRuntime jsRuntime { get; set; }
        
        private IEnumerable<Periodo> Periodos { get; set; }

        private List<Cuota> Cuotas { get; set; }
        private IEnumerable<TasaInteres> TasasDeInteres { get; set; }

        public PrestamoCalculo() { }

        public PrestamoCalculo(Prestamo prestamo, IJSRuntime jsRuntime, IEnumerable<Periodo> periodos, IEnumerable<TasaInteres> tasasDeInteres, List<Cuota> cuotas)
        {
            this.Prestamo = prestamo;
            MontoPrestado = prestamo.MontoPrestado;
            LlevaGastoDeCierre = prestamo.LlevaGastoDeCierre;
            InteresGastoDeCierre = prestamo.InteresGastoDeCierre;
            TasasDeInteres = tasasDeInteres;
            CantidadDeCuotas = prestamo.CantidadDePeriodos;
            Periodos = periodos;
            this.Cuotas = cuotas;
            this.jsRuntime = jsRuntime;
        }

        
        private Decimal _montoPrestado;
        public decimal MontoPrestado { get { return _montoPrestado; } 
            set {
                if (value < 0) { JsInteropUtils.SweetMessageBox(jsRuntime, "No se aceptan valores negativos","warning");
                    value = 0;
                }
                this._montoPrestado = value; this.Prestamo.MontoPrestado = value;  UpdatePrestamoCalculo(); 
            } }

        private decimal _interesGastoDeCierre { get; set; }
        public decimal InteresGastoDeCierre { get { return _interesGastoDeCierre; } set { _interesGastoDeCierre = value; this.Prestamo.InteresGastoDeCierre = value; CalcularGastoDeCierre(); } } 
        
        bool _llevaGastoDeCierre { get; set; }
        public bool LlevaGastoDeCierre { get { return _llevaGastoDeCierre; } set { _llevaGastoDeCierre = value; CalcularGastoDeCierre(); } } 
        private async Task CalcularGastoDeCierre()
        {
            Prestamo.MontoGastoDeCierre = Prestamo.LlevaGastoDeCierre ? Prestamo.MontoPrestado * (Prestamo.InteresGastoDeCierre / 100) : 0;
        }

        
        private async Task UpdatePrestamoCalculo()
        {
            
            await CalcularGastoDeCierre();
            await CalcularCuotas();
            
        }

        int _cantidadDeCuota { get; set; }
        public int CantidadDeCuotas { get { return _cantidadDeCuota; } 
            set { _cantidadDeCuota = value; Prestamo.CantidadDePeriodos = value; CalcularCuotas(); } }

        private async Task CalcularCuotas()
        {
            var calP = new CalculosParaPrestamosService();
            var periodo = Periodos.Where(per => per.idPeriodo == Prestamo.IdPeriodo).FirstOrDefault();
            var tasaDeInteres = TasasDeInteres.Where(ti => ti.idTasaInteres == Prestamo.IdTasaInteres).FirstOrDefault();
            var infoCuotas = new infoGeneradorDeCuotas {
                AcomodarFechaALasCuotas = false,
                CantidadDePeriodos = Prestamo.CantidadDePeriodos,

                MontoCapital = Prestamo.MontoCapital,
                FechaEmisionReal = Prestamo.FechaEmisionReal,
                FechaInicioPrimeraCuota = Prestamo.FechaInicioPrimeraCuota,
                CargarInteresAlGastoDeCierre = Prestamo.CargarInteresAlGastoDeCierre,
                FinanciarGastoDeCierre = Prestamo.FinanciarGastoDeCierre,
                MontoGastoDeCierre = Prestamo.MontoGastoDeCierre,
                OtrosCargosSinInteres = Prestamo.OtrosCargosSinInteres,
                TipoAmortizacion = Prestamo.TipoAmortizacion,
                TasaDeInteresPorPeriodo = tasaDeInteres.InteresMensual,
                Periodo = periodo
            };


            // todo poner el calculo de tasa de interes por periodo donde hace el calculo de generar
            // cuotas y no que se le envie esa informacion
            var cuotas = calP.GenerarCuotas(infoCuotas);
            this.Cuotas.Clear();
            this.Cuotas.AddRange(cuotas);
            //await JsInteropUtils.NotifyMessageBox(jsRuntime,"calculando cuotas"+cuotas.Count().ToString());
        }

    }
}
