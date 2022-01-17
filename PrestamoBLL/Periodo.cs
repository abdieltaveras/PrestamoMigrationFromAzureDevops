using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo

    {
        public IEnumerable<Periodo> GetPeriodos(PeriodoGetParams searchParam)
        {
            //var mapping = new Dictionary<string, Func<Periodo, object>>();
            //mapping.Add("PeriodoBase", (per) => { 
            //    return (PeriodoBase)per.IdPeriodoBase; });

            //var result =  DBPrestamo.ExecReaderSelSP("spGetPeriodos", mapping, SearchRec.ToSqlParams(searchParam));

            var result = BllAcciones.GetData<Periodo, PeriodoGetParams>(searchParam, "spGetPeriodos", GetValidation);
            return result;
            
        }
        public void InsUpdPeriodo(Periodo insupdparam)
        {
            BllAcciones.InsUpdData<Periodo>(insupdparam, "spinsupdPeriodo");
        }

        public void anularPeriodo(Periodo delParam)
        {
            DBPrestamo.ExecSelSP("spdelPeriodo", SearchRec.ToSqlParams(delParam));
        }
    }
}
