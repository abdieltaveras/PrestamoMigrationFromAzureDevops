using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public static class UtilsBLL
    {



        //public static IEnumerable<TResult> SearchTableByColunm<TResult>(string SearchText, string Tabla, string Columna, string OrderBy = "") where TResult : class
        //{
        //    return DBPrestamo.ExecReaderSelSP<TResult>("spSearchTableByColunm", SearchRec.ToSqlParams(new
        //    {
        //        SearchText = SearchText,
        //        Tabla = Tabla,
        //        Columna = Columna,
        //        OrderBy = OrderBy
        //    }));
        //}
        //public static DateTime GetDateFromSqlServer()
        //{
        //    // todo investigar con ernesto como hacerlo ahora con la nueva version
        //    var result = BLLPrestamo.DBPrestamo.ExecReaderSelSP("select GetDate() as DT", "SysDate");
        //    var r = Convert.ToDateTime(result.Rows[0][0]);
        //    return r;
        //}
    }
}
