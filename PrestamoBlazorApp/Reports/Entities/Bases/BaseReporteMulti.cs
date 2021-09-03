using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Reports.Entities.Bases
{
    public abstract class BaseReporteMulti
    {
        public string NombreEmpresa { get; set; }
        public string TituloDelReporte { get; set; }
        public string OrdenadoPor { get; set; }
        public string RangoFiltro { get; set; }
        public string OtrosDetalles { get; set; }
        public DateTime FechaImpresion { get; set; }
        public string ImpresoPor { get; set; }
    }
}
