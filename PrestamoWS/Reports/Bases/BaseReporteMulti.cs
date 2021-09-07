using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoWS.Reports.Bases
{
    public class BaseReporteMulti
    {
        public string NombreEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public string RNCEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string TituloReporte { get; set; }
        public string OrdenadoPor { get; set; }
        public string RangoFiltro { get; set; }
        public string OtrosDetalles { get; set; }
        public DateTime FechaImpresion { get; set; }
        public string ImpresoPor { get; set; }
    }
}
