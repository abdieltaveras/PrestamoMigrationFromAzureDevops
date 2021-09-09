using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades.Tests
{
    [TestClass()]
    public class CuotasTest
    {
        [TestMethod()]
        public void CrearTest()
        {
            var periodoMensual = new Periodo { PeriodoBase = PeriodoBase.Mes, MultiploPeriodoBase = 1 };

            var prestamo = new Prestamo();
            prestamo.IdPrestamo = 1;
            prestamo.MontoPrestado = 1000;
            prestamo.Periodo = periodoMensual;
            prestamo.TipoAmortizacion = TiposAmortizacion.Amortizable_cuotas_fijas;


            
        }
    }
}