using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    // solo para detallar la transaccion de debito o credito 
    public class DetalleDrCrImpresionDocumento
    {
        public string NCF { get; set; }
        public string TipoComprobante { get; set; }

        public string NombreDocumento { get; set; }
        public string NumeracionDocumento { get; set; }

        public string CodigoCliente { get; set; }

        public string NombreCompletoCliente { get; set; }

        public string NumeracionGarantia { get; set; }

        public DateTime Fecha { get; set; }
        public string NombreDocumentoCxC { get; set; }

        public float BalanceDocumentoCXC { get; set; }

        public float MontoPagado { get; set; }

        public float TotalCuotasPorPagar { get; set; }

        public float CuotasAtrasadas { get; set; }

        public float MontoAtrasado { get; set; }


        public float Capital { get; set; }

        public float Interes { get; set; }

        public float Moras { get; set; }

        public float InteresDespuesDeVencido { get; set; }

        public float OtrosDebitos { get; set; }
        public string NombreUsuario { get; set; }
        public string LoginName { get; set; }
        public string NombreNegocio { get; set; }
        public string ClienteTaxId { get; set; }
    }
    
}
