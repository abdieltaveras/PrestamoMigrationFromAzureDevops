using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Ingreso : BaseInsUpd
    {
        public int IdIngreso { get; set; }
        public int IdPrestamo { get; set; } 
        public string NumeroTransaccion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }

        // old property to be removed only to avoid errors on blltest was again placed here
        public int IdCuota { get; set; }
        public int Num_Cuota { get; set; }
        public decimal Monto_Original_Cuota
        {
            get; set;
        }
        public decimal Monto_Abonado { get; set; }
        public decimal Balance { get; set; }

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
