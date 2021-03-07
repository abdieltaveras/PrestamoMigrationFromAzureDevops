using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class Ingreso : BaseInsUpd
    {
        public int IdIngreso { get; set; }
        public int IdPrestamo { get; set; } 
        public string NumeroTransaccion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }

    }


    public class IngresoGetParams : BaseGetParams
    {
        public int IdPrestamo { get; set; } 

        public int IdIngreso { get; set; } 

        public string NumeroTransaccion { get; set; } = string.Empty;

        public DateTime FechaDesde { get; set; }

        public  DateTime FechaHasta { get; set; }
    }
}
