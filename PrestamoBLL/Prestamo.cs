using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
namespace PrestamoBLL
{

    
    public partial class BLLPrestamo
    {
        public int InsUpdPrestamo(Prestamo prestamo)
        {
            PrestamoBuilder prToBuild = new PrestamoBuilder(prestamo);
            var result = prToBuild.Build();
            var prestamoParam = SearchRec.ToSqlParams(result.Prestamo);
            var resultId = PrestamosDB.ExecSelSP("spInsUpdPrestamo",prestamoParam);
            //var result = PrestamosDB.ExecSelSP("spInsUpdNegocio", _insUpdParam);
            //idResult = Utils.GetIdFromDataTable(result);
            var  id= Utils.GetIdFromDataTable(resultId);
            return id;
        }
    }
}
