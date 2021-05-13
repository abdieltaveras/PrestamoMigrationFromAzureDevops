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
        internal PrestamoConCalculos prestamo { get; set; } = new PrestamoConCalculos();

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

        private async Task Update()
        {
            await NotifyMessageBox("updating");
        }

    }

    /// <summary>
    /// esta clase es especializada para hacer calculo en la medida que las propiedades
    /// son afectadas.
    /// </summary>
    public class PrestamoConCalculos : Prestamo
    {
        public PrestamoConCalculos()
        {

        }
        public PrestamoConCalculos(EventHandler<string> notificarMensaje,
            IEnumerable<Clasificacion> clasificaciones,
            IEnumerable<TipoMora> tiposMora,
            IEnumerable<TasaInteres> tasasDeInteres,
            IEnumerable<Periodo> periodos
            )
        {
            this.OnNotificarMensaje = notificarMensaje;
            this.Clasificaciones = clasificaciones;
            this.TiposMora = tiposMora;
            this.TasasDeInteres = tasasDeInteres;
            this.Periodos = periodos;

        }

        EventHandler<string> OnNotificarMensaje;
        IEnumerable<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        IEnumerable<TipoMora> TiposMora { get; set; } = new List<TipoMora>();

        public List<Cuota> Cuotas { get; set; } = new List<Cuota>();

        IEnumerable<TasaInteres> TasasDeInteres { get; set; } = new List<TasaInteres>();
        IEnumerable<Periodo> Periodos { get; set; } = new List<Periodo>();

        public override decimal MontoPrestado
        {
            get => base.MontoPrestado;
            set
            {
                if (value < 0)
                {
                    this.OnNotificarMensaje.Invoke(this, "el valor del monto prestamo no puede ser menor o igual a 0");
                    return;
                }
                base.MontoPrestado = value;
                Update();
            }
        }



        public override decimal InteresGastoDeCierre
        {
            get => base.InteresGastoDeCierre;
            set
            {
                RejectNegativeValue(value);
                base.InteresGastoDeCierre = value;
                Update();
            }
        }


        public override int IdTasaInteres
        {
            get => base.IdTasaInteres;
            set
            {
                base.IdTasaInteres = value;
                Update();
            }
        }

        public override int IdPeriodo
        {
            get => base.IdPeriodo;
            set
            {
                base.IdPeriodo = value;
                Update();
            }
        }
        private void RejectNegativeValue(decimal value)
        {
            if (value < 0)
            {
                this.OnNotificarMensaje.Invoke(this, "el valor del monto prestamo no puede ser menor o igual a 0");
                return;
            }
        }

        public override int CantidadDePeriodos
        {
            get => base.CantidadDePeriodos;
            set
            {
                RejectNegativeValue(value);
                base.CantidadDePeriodos = value;
                Update();
            }
        }

        public override DateTime FechaEmisionReal
        {
            get => base.FechaEmisionReal;
            set
            {
                base.FechaEmisionReal = value;
                Update();
            }
        }
        private async Task CalcularGastoDeCierre()
        {
            MontoGastoDeCierre = LlevaGastoDeCierre ? MontoPrestado * (InteresGastoDeCierre / 100) : 0;
        }

        public async Task Update()
        {
            await CalcularGastoDeCierre();
            await CalcularCuotas();
        }

        private async Task CalcularCuotas()
        {
            if (IdPeriodo < 0 || IdTasaInteres <= 0) return;
            var calP = new CalculosParaPrestamosService();
            var periodo = Periodos.Where(per => per.idPeriodo == IdPeriodo).FirstOrDefault();
            var tasaDeInteres = TasasDeInteres.Where(ti => ti.idTasaInteres == IdTasaInteres).FirstOrDefault();
            var infoCuotas = new infoGeneradorDeCuotas
            {
                AcomodarFechaALasCuotas = false,
                CantidadDePeriodos = CantidadDePeriodos,

                MontoCapital = MontoCapital,
                FechaEmisionReal = FechaEmisionReal,
                FechaInicioPrimeraCuota = FechaInicioPrimeraCuota,
                CargarInteresAlGastoDeCierre = CargarInteresAlGastoDeCierre,
                FinanciarGastoDeCierre = FinanciarGastoDeCierre,
                MontoGastoDeCierre = MontoGastoDeCierre,
                OtrosCargosSinInteres = OtrosCargosSinInteres,
                TipoAmortizacion = TipoAmortizacion,
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
