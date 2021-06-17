using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public static class UtilsBLL
    {
        public static DateTime GetDateFromSqlServer()
        {
            var result = BLLPrestamo.DBPrestamo.ExecQuery("select GetDate() as DT", "SysDate");
            var r = Convert.ToDateTime(result.Rows[0][0]);
            return r;
        }
    }
}
