using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class Cuota
    {
        public int idCuota { get; set; } = 0;
        public int IdPrestamo { get; set; } = 0;
        public decimal Numero { get; set; } = 0;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Capital { get; set; } = 0;
        public decimal Interes { get; set; } = 0;
        public decimal BalanceTotal => Capital + Interes;
    }
}

