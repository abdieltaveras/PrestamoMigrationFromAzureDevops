using System;
using System.Collections.Generic;
using PrestamoEntidades;

namespace PrestamoInMemoryDb
{
    public class DatosImpresionRecibosDeIngresos
    {


        public IEnumerable<DetalleDrCrImpresionDocumento> DocumentosIngresoData()
        {
            var documentosIngreso = new List<DetalleDrCrImpresionDocumento>();

            var doc1 = new DetalleDrCrImpresionDocumento
            {
                BalanceDocumentoCXC = 100000,
                MontoPagado = 10000,
                Capital = 5000,
                Interes = 2000,
                Moras = 1500,
                OtrosDebitos = 1500,
                CodigoCliente = "00000001",
                CuotasAtrasadas = 6,
                MontoAtrasado = 26000,
                Fecha = DateTime.Now,
                NombreDocumento = "Recibo Ingreso",
                NumeracionDocumento = "00007548",
                NumeracionGarantia = "lc6paga1526edfrt",
                TotalCuotasPorPagar = 15,
                NombreCompletoCliente = "Pedro Rodriguez Gutierrez",
                NombreDocumentoCxC = "Prestamo",
                InteresDespuesDeVencido = 0,
                NombreUsuario = "Ramon Rutin",
                LoginName = "RamonR",
                NombreNegocio = "Prestamos Rapido",
                ClienteTaxId = "112108236"
            };

            documentosIngreso.Add(doc1);
            return documentosIngreso;
        }
        
    }
}
