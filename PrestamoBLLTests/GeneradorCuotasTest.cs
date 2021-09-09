using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class GeneradorCuotasTest
    {
        [TestMethod()]
        public void GenerarCuotasTest()
        {
            var periodo = new Periodo { Codigo = "Mes", PeriodoBase = PeriodoBase.Mes, Nombre="Cuotas Mensuales" };
            var prestamo = new Prestamo
            {
                FechaEmisionReal = new DateTime(2021, 01, 01),
                CantidadDePeriodos = 7,
                TasaDeInteresDelPeriodo = 5,
                Periodo = periodo,
                MontoPrestado = 10000,
                TipoAmortizacion = TiposAmortizacion.No_Amortizable_cuotas_fijas,
                MontoGastoDeCierre = 1000,
                CargarInteresAlGastoDeCierre = true,
                FinanciarGastoDeCierre = true,
                OtrosCargos = 200,
            };
            
            //necesito aqui un objeto que sea capaz de indicarme la tasa de interes para el periodo quincenal

           //var infCuota = new InfoGeneradorDeCuotas()
           //{
           //    AcomodarFechaALasCuotas = false,
           //    CantidadDePeriodos = 7,
           //    TasaDeInteresDelPeriodo = 5,
           //    Periodo = periodo,
           //    MontoCapital = 10000,
           //    TipoAmortizacion = TiposAmortizacion.No_Amortizable_cuotas_fijas,
           //    MontoGastoDeCierre = 1000,
           //    CargarInteresAlGastoDeCierre = true,
           //    FinanciarGastoDeCierre = true,
           //    OtrosCargos = 200
           //};

            var generadorCuota = new GeneradorCuotasFijasNoAmortizable2(prestamo,1);
            var cuotas = generadorCuota.GenerarCuotas();
            decimal tCapital = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.Capital);
            decimal tInteres = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.Interes);
            decimal tGastoDeCierre = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.GastoDeCierre);
            decimal tInteresGastoDeCierre = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.InteresGastoDeCierre);
            decimal tOtrosCargos = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.OtrosCargos);
            decimal tInteresOtrosCargos = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.InteresOtrosCargos);

            var compCapital = tCapital == prestamo.MontoCapital;
            var compGastoDeCierre =tGastoDeCierre ==  prestamo.MontoGastoDeCierre;
            var compOtrosCargos = tOtrosCargos == prestamo.OtrosCargos;
            var compInteres = tInteres == (Math.Round(prestamo.MontoCapital * prestamo.TasaDeInteresDelPeriodo/100,2)* prestamo.CantidadDePeriodos);
            var compInteresGastoDeCierre = tInteresGastoDeCierre == (Math.Round(prestamo.MontoGastoDeCierre * prestamo.TasaDeInteresDelPeriodo / 100, 2) * prestamo.CantidadDePeriodos);

            
            //var total = cuotas.ForEach(cuota =>
            //    cuota.GetItems.Where(item => item.TipoCargo == tipoCargoCapital).Sum(data => data.Monto)
            //);

            Assert.IsTrue(cuotas.Count > 0 , "no se generaron cuotas");
        }
    }
}