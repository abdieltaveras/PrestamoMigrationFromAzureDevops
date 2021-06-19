using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Models
{
    public class PrestamoConCalculosVM : Prestamo
    {
        
        PrestamosService PrestamosService { get; set;}
        private Func<Task> execCalcs { get; set; }

        public new bool LlevaGarantia()
        {
                if (Clasificaciones != null)
                {
                    var result = Clasificaciones.Where(cl => cl.IdClasificacion == base.IdClasificacion).FirstOrDefault().RequiereGarantia;
                    return result;
                }
                else
                {
                    return false;
                }
        
        }

        private async Task NoCalcular() => await Task.Run(() => { });

        public async Task ExecCalcs() => await execCalcs();

        public void ActivateCalculos() => execCalcs = Calcular;
        

        EventHandler<string> OnNotificarMensaje;
        IEnumerable<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        IEnumerable<TipoMora> TiposMora { get; set; } = new List<TipoMora>();

        public List<Cuota> Cuotas { get; set; } = new List<Cuota>();

        IEnumerable<TasaInteres> TasasDeInteres { get; set; } = new List<TasaInteres>();
        IEnumerable<Periodo> Periodos { get; set; } = new List<Periodo>();

        public PrestamoConCalculosVM()
        {

            execCalcs = NoCalcular;
        }
        public PrestamoConCalculosVM(PrestamosService prestamoService)
        {
            this.PrestamosService = prestamoService;
            execCalcs = NoCalcular;
        }
        public override decimal MontoPrestado
        {
            get => base.MontoPrestado;
            set
            {
                RejectNegativeValue(value);
                base.MontoPrestado = value;
                execCalcs();
            }
        }


        public  DateTime CalcularFechaVencimiento(DateTime fechaPrestamo, Periodo periodo, int cantidadDePeriodos,
            DateTime FechaInicioPrimeraCuota)
        {
            
            var fechaVencimiento = CalcularFechaVencimiento();
            return fechaVencimiento;
        }
        private  DateTime CalcularFechaVencimiento()
        {
            // primero buscar el periodo
            // luego tomar la fecha inicial de partida y hacer los calculos
            var duracion = CantidadDePeriodos * Periodo.MultiploPeriodoBase;
            var fechaVencimiento = new DateTime();
            if (AcomodarFechaALasCuotas)
            {
                throw new Exception("aun no estamos trabajando con acomodar cuotas");
            }
            var fechaInicial = FechaEmisionReal;
            switch (Periodo.PeriodoBase)
            {
                case PeriodoBase.Dia:
                    fechaVencimiento = fechaInicial.AddDays(duracion);
                    break;
                case PeriodoBase.Semana:
                    fechaVencimiento = fechaInicial.AddDays(duracion * 7);
                    break;
                case PeriodoBase.Quincena:
                    var meses = (duracion / 2);
                    var quincenasImpares = (duracion % 2) == 1;
                    fechaVencimiento = fechaInicial;
                    if (meses >= 1)
                    {
                        fechaVencimiento = fechaInicial.AddMonths(meses);
                    }
                    if (quincenasImpares)
                    {
                        fechaVencimiento = fechaVencimiento.AddDays(15);
                    }
                    break;
                case PeriodoBase.Mes:
                    fechaVencimiento = fechaInicial.AddMonths(duracion);
                    break;
                case PeriodoBase.Ano:
                    fechaVencimiento = fechaInicial.AddYears(duracion);
                    break;
                default:
                    break;
            }
            FechaVencimiento = fechaVencimiento;

            return FechaVencimiento;
        }


        public override decimal InteresGastoDeCierre
        {
            get => base.InteresGastoDeCierre;
            set
            {
                RejectNegativeValue(value);
                base.InteresGastoDeCierre = value;
                execCalcs();
            }
        }


        public override int IdTasaInteres
        {
            get => base.IdTasaInteres;
            set
            {
                base.IdTasaInteres = value;
                execCalcs();
            }
        }

        public override int IdPeriodo
        {
            get => base.IdPeriodo;
            set
            {
                base.IdPeriodo = value;
                execCalcs();
            }
        }


        public override int CantidadDePeriodos
        {
            get => base.CantidadDePeriodos;
            set
            {
                RejectNegativeValue(value);
                base.CantidadDePeriodos = value;
                execCalcs();
            }
        }

        public override bool GastoDeCierreEsDeducible
        {
            get => base.GastoDeCierreEsDeducible;
            set
            {
                base.GastoDeCierreEsDeducible = value;
                execCalcs();
            }
        }

        public override bool FinanciarGastoDeCierre
        {
            get => base.FinanciarGastoDeCierre;
            set
            {
                base.FinanciarGastoDeCierre = value;
                execCalcs();
            }
        }

        public override bool CargarInteresAlGastoDeCierre
        {
            get => base.CargarInteresAlGastoDeCierre;
            set
            {
                base.CargarInteresAlGastoDeCierre = value;
                execCalcs();
            }
        }

        public override DateTime FechaEmisionReal
        {
            get => base.FechaEmisionReal;
            set
            {
                base.FechaEmisionReal = value;
                execCalcs();
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

        public async Task Calcular()
        {
            await CalcularGastoDeCierre();
            await CalcularCuotas();
            this.FechaVencimiento = this.Cuotas.Last().Fecha;
        }

        //todo: verificar si debe ser eliminado este metodo
        public async Task<PrestamoConCalculosVM > CalcularPrestamo()
        {
            await CalcularGastoDeCierre();
            await CalcularCuotas();
            this.FechaVencimiento = this.Cuotas.Last().Fecha;
            return this;
        }

        public async Task<IEnumerable<Cuota>>  GenerarCuotas()
        {
            var cuotas = await PrestamosService.GenerarCuotas(this);
            return cuotas;
        }

        public TasaInteresPorPeriodos CalculateTasaInteresPorPeriodo(decimal tasaInteresMensual, Periodo periodo)
        {

            //var result = PrestamosService.CalcularTasaInteresPorPeriodos(tasaInteresMensual, periodo);
            //return result;
            // notimplemented
            return null;
        }

        private async Task CalcularCuotas()
        {
            //if (IdPeriodo < 0 || IdTasaInteres <= 0) return;
            //base.Periodo = Periodos.Where(per => per.idPeriodo == IdPeriodo).FirstOrDefault();
            //var tasaDeInteres = TasasDeInteres.Where(ti => ti.idTasaInteres == IdTasaInteres).FirstOrDefault();
            //var tasaDeInteresPorPeriodos = CalculateTasaInteresPorPeriodo(tasaDeInteres.InteresMensual, Periodo);
            //base.TasaDeInteresPorPeriodo = tasaDeInteresPorPeriodos.InteresDelPeriodo;
            // not implemented

            // todo poner el calculo de tasa de interes por periodo donde hace el calculo de generar
            // cuotas y no que se le envie esa informacion
            var cuotas = await GenerarCuotas();
            this.Cuotas.Clear();
            this.Cuotas = cuotas.ToList();
            //await JsInteropUtils.NotifyMessageBox(jsRuntime,"calculando cuotas"+cuotas.Count().ToString());
            
        }

        public void SetServices(EventHandler<string> notificarMensaje,
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

    }
}