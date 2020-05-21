using emtSoft.DAL;
using PrestamoBLL.Entidades;
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
            return BllAcciones.GetData<Periodo, PeriodoGetParams>(searchParam, "spGetPeriodos", GetValidation);
            
        }
        public void InsUpdPeriodo(Periodo insupdparam)
        {
            BllAcciones.InsUpdData<Periodo>(insupdparam, "spinsupdPeriodo");
        }

        public void anularPeriodo(Periodo delParam)
        {
            PrestamosDB.ExecSelSP("spdelPeriodo", SearchRec.ToSqlParams(delParam));
        }
    }
}
