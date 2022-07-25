using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class TasaInteresBLL: BaseBLL
    {
        public TasaInteresBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName) { }

        public IEnumerable<TasaInteres> GetTasasDeInteres(TasaInteresGetParams searchParam)
        {
            return this.Get<TasaInteres>("spGetTasasInteres", searchParam);
        }
        public int InsUpdTasaInteres(TasaInteres insUpdParam)
        {
           return this.InsUpd( "spInsUpdTasaInteres",insUpdParam);
        }
        
        public void DeleteTasaInteres(TasaInteresDelParams delParam)
        {
            DBPrestamo.ExecSelSP("spDelTasaInteres", SearchRec.ToSqlParams(delParam));
        }

        
        public TasaInteresPorPeriodos CalcularTasaInteresPorPeriodos(decimal tasaInteresMensual, Periodo periodo)
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
                    tasaInteresPorPeriodos.InteresDelPeriodo = tasaInteresPorPeriodos.InteresDiario * (int)periodo.MultiploPeriodoBase; 
                    break;
                case PeriodoBase.Semana:
                    tasaInteresPorPeriodos.InteresDelPeriodo = tasaInteresPorPeriodos.InteresSemanal * (int)periodo.MultiploPeriodoBase; 
                    break;
                case PeriodoBase.Quincena:
                    tasaInteresPorPeriodos.InteresDelPeriodo = tasaInteresPorPeriodos.InteresQuincenal * (int)periodo.MultiploPeriodoBase; 
                    break;
                case PeriodoBase.Mes:
                    tasaInteresPorPeriodos.InteresDelPeriodo =  tasaInteresPorPeriodos.InteresMensual * (int)periodo.MultiploPeriodoBase; 
                    break;
                case PeriodoBase.Ano:
                    tasaInteresPorPeriodos.InteresDelPeriodo = tasaInteresPorPeriodos.InteresAnual * (int)periodo.MultiploPeriodoBase; 
                    break;
                default:
                    break;
            }

            return tasaInteresPorPeriodos;
        }
    }
}
