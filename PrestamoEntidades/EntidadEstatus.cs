using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class EntidadEstatus //:BaseInsUpd //: BaseInsUpdCatalogo
    {
        public int IdEntidadEstatus { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsNotPrintOnReport { get; set; }
        public bool IsImpedirPagoEnCaja { get; set; }
        public bool IsRequiereAutorizacionEnCaja { get; set; }
        public bool IsActivo { get; set; } = true;
        public bool IsImpedirHacerPrestamo { get; set; }
    }
    public class EntidadEstatusGetParams
    {
        /// <summary>
        /// para permitir personalizar la columna y usarla para calculos y asuntos personalizados
        /// </summary>
        public int Option { get; set; }
        public int IdEntidadEstatus { get; set; }
        public string Name { get; set; }
    }
}
