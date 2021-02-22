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
        public int IdCuota { get; set; }
        public int Num_Cuota { get; set; }
        public decimal Monto_Original_Cuota { get; set; }
        public decimal Monto_Abonado { get; set; }
        public decimal Balance { get; set; }

    }

    public class IngresoPGetParams : BaseGetParams
    {
        public int IdPrestamo { get; set; }
    }
}
