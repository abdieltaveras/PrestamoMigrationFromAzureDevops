using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class TasaInteresPorPeriodos
    {
        public string PeriodoCodigo { get;  set; }
        public string PeriodoNombre { get; set; }
        public decimal InteresDiario { get;  set; }
        public decimal InteresSemanal { get; set; }
        public decimal InteresQuincenal { get; set; }
        public decimal InteresMensual { get;  set; }
        public decimal InteresAnual { get; set; }
        public decimal InteresDelPeriodo { get;  set; }
    }

}
