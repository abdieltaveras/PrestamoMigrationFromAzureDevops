using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<TasaInteres> TasasInteresGet(TasaInteresGetParams searchParam)
        {
            return BllAcciones.GetData<TasaInteres, TasaInteresGetParams>(searchParam, "spGetTasasInteres", GetValidation);
        }
        public int TasaInteresInsUpd(TasaInteres insUpdParam)
        {
           return BllAcciones.InsUpdData<TasaInteres>(insUpdParam, "spInsUpdTasaInteres");
        }
        
        public void TasaInteresDelete(TasaInteresDelParams delParam)
        {
            DBPrestamo.ExecSelSP("spDelTasaInteres", SearchRec.ToSqlParams(delParam));
        }

        public class TasaInteresPorPeriodos {
            public string PeriodoCodigo { get; internal set; }
            public string PeriodoNombre { get; internal set; }
            public decimal InteresDiario { get; internal set; }
            public decimal InteresSemanal { get; internal set; }
            public decimal InteresQuincenal { get; internal set; }
            public decimal InteresMensual { get; internal set; }
            public decimal InteresAnual { get; set; }
            public decimal InteresDelPeriodo { get;  internal set; }
        }
        public TasaInteresPorPeriodos CalcularTasaInterePorPeriodo(decimal tasaInteresMensual, Periodo periodo)
        {
            
            var tasaInteresPorPeriodos = new TasaInteresPorPeriodos { InteresMensual = tasaInteresMensual };
            tasaInteresPorPeriodos.PeriodoCodigo = periodo.Codigo;
            tasaInteresPorPeriodos.PeriodoNombre = periodo.Nombre;
            tasaInteresPorPeriodos.InteresDiario = (tasaInteresMensual / 30) * 1;
            tasaInteresPorPeriodos.InteresSemanal = (tasaInteresMensual / 30) * 7;
            tasaInteresPorPeriodos.InteresQuincenal = (tasaInteresMensual / 30) * 15;
            tasaInteresPorPeriodos.InteresAnual = (tasaInteresMensual / 30) * 360;
            tasaInteresPorPeriodos.InteresDelPeriodo = 0;
            switch (periodo.PeriodoBase)        
            {
                case PeriodoBase.Dia:
                    tasaInteresPorPeriodos.InteresDelPeriodo = tasaInteresPorPeriodos.InteresDiario * periodo.MultiploPeriodoBase; ;
                    break;
                case PeriodoBase.Semana:
                    tasaInteresPorPeriodos.InteresDelPeriodo = tasaInteresPorPeriodos.InteresSemanal * periodo.MultiploPeriodoBase; ;
                    break;
                case PeriodoBase.Quincena:
                    tasaInteresPorPeriodos.InteresDelPeriodo = tasaInteresPorPeriodos.InteresQuincenal * periodo.MultiploPeriodoBase; ;
                    break;
                case PeriodoBase.Mes:
                    tasaInteresPorPeriodos.InteresDelPeriodo =  tasaInteresPorPeriodos.InteresMensual * periodo.MultiploPeriodoBase; ;
                    break;
                case PeriodoBase.Ano:
                    tasaInteresPorPeriodos.InteresDelPeriodo = tasaInteresPorPeriodos.InteresAnual * periodo.MultiploPeriodoBase; ;
                    break;
                default:
                    break;
            }

            return tasaInteresPorPeriodos;
        }
    }
}
