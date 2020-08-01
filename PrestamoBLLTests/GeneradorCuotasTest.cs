using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class GeneradorCuotasTest
    {
        [TestMethod()]
        public void GenerarCuotasTest()
        {
            var periodo = BLLPrestamo.Instance.GetPeriodos(new Entidades.PeriodoGetParams { Codigo = "QUI", IdNegocio=1 }).FirstOrDefault();

            // necesito aqui un objeto que sea capaz de indicarme la tasa de interes para el periodo quincenal

            var infCuota = new infoGeneradorDeCuotas(Entidades.TiposAmortizacion.Amortizable_cuotas_fijas)
            {
                AcomodarFechaALasCuotas = false,
                CantidadDePeriodos = 10,
                TasaDeInteresPorPeriodo = 10,
                Periodo = periodo,
                MontoCapital = 10000,
                TipoAmortizacion = Entidades.TiposAmortizacion.No_Amortizable_cuotas_fijas,
            };

            var generadorCuota = new GeneradorCuotasFijasNoAmortizables(infCuota);

            var cuotas = generadorCuota.GenerarCuotas();

            Assert.IsTrue(cuotas.Count>0,"no se generaron cuotas");
        }
    }
}