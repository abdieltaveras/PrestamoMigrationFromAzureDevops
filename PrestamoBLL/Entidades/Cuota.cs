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
        public float Numero { get; set; } = 0;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public float Capital { get; set; } = 0;
        public float Interes { get; set; } = 0;
    }
}

