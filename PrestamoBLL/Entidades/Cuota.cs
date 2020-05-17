using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class Cuota
    {
        public int idCuota { get; internal set; } = 0;
        public int IdPrestamo { get; internal set; } = 0;
        public decimal Numero { get; internal set; } = 0;
        public DateTime Fecha { get; internal set; } = DateTime.Now;
        public decimal Capital { get; internal set; } = 0;
        public decimal Interes { get; internal set; } = 0;
        //[ignorarEnParam]
        //public decimal BalanceTotal => Capital + Interes;
    }
}

