using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
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
            var periodo = BLLPrestamo.Instance.GetPeriodos(new PeriodoGetParams { Codigo = "MES", IdNegocio=1 }).FirstOrDefault();

            // necesito aqui un objeto que sea capaz de indicarme la tasa de interes para el periodo quincenal

            var infCuota = new InfoGeneradorDeCuotas(TiposAmortizacion.Amortizable_cuotas_fijas)
            {
                AcomodarFechaALasCuotas = false,
                CantidadDePeriodos = 7,
                TasaDeInteresDelPeriodo = 5,
                Periodo = periodo,
                MontoCapital = 10000,
                TipoAmortizacion = TiposAmortizacion.No_Amortizable_cuotas_fijas,
                MontoGastoDeCierre = 1000,
                CargarInteresAlGastoDeCierre = true,
                FinanciarGastoDeCierre = true,
                OtrosCargosSinInteres = 200
            };

            var generadorCuota = new GeneradorCuotasFijasNoAmortizable(infCuota);

            var cuotas = generadorCuota.GenerarCuotas();

            Assert.IsTrue(cuotas.Count>0,"no se generaron cuotas");
        }
    }
}