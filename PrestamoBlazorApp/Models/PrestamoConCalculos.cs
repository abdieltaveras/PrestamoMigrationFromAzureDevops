using PrestamoBlazorApp.Services;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBlazorApp.Models
{
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
                RejectNegativeValue(value);
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

        public override bool GastoDeCierreEsDeducible
        {
            get => base.GastoDeCierreEsDeducible;
            set
            {
                base.GastoDeCierreEsDeducible = value;
                Update();
            }
        }

        public override bool FinanciarGastoDeCierre
        {
            get => base.FinanciarGastoDeCierre;
            set
            {
                base.FinanciarGastoDeCierre = value;
                Update();
            }
        }

        public override bool CargarInteresAlGastoDeCierre
        {
            get => base.CargarInteresAlGastoDeCierre;
            set
            {
                base.CargarInteresAlGastoDeCierre = value;
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

        private void RejectNegativeValue(decimal value)
        {
            if (value < 0)
            {
                this.OnNotificarMensaje.Invoke(this, "el valor del monto prestamo no puede ser menor o igual a 0");
                return;
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

        public IEnumerable<Cuota> GenerarCuotas(IInfoGeneradorCuotas info)
        {
            var generadorCuotas = PrestamoBuilder.GetGeneradorDeCuotas(info);
            var cuotas = generadorCuotas.GenerarCuotas();
            return cuotas;
        }

        public  TasaInteresPorPeriodos CalculateTasaInteresPorPeriodo(decimal tasaInteresMensual, Periodo periodo)
        {
            var result =  BLLPrestamo.Instance.CalcularTasaInterePorPeriodo(tasaInteresMensual, periodo);
            return result;
        }

        private async Task CalcularCuotas()
        {
            if (IdPeriodo < 0 || IdTasaInteres <= 0) return;
            var periodo = Periodos.Where(per => per.idPeriodo == IdPeriodo).FirstOrDefault();
            var tasaDeInteres = TasasDeInteres.Where(ti => ti.idTasaInteres == IdTasaInteres).FirstOrDefault();
            var tasaDeInteresPorPeriodo = CalculateTasaInteresPorPeriodo(tasaDeInteres.InteresMensual, periodo);
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
                GastoDeCierreEsDeducible = GastoDeCierreEsDeducible,
                TipoAmortizacion = TipoAmortizacion,
                TasaDeInteresPorPeriodo = tasaDeInteresPorPeriodo.InteresDelPeriodo,
                Periodo = periodo
            };
            // todo poner el calculo de tasa de interes por periodo donde hace el calculo de generar
            // cuotas y no que se le envie esa informacion
            var cuotas = GenerarCuotas(infoCuotas);
            this.Cuotas.Clear();
            this.Cuotas.AddRange(cuotas);
            //await JsInteropUtils.NotifyMessageBox(jsRuntime,"calculando cuotas"+cuotas.Count().ToString());
        }
    }
}
