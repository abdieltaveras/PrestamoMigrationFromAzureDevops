using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class CuotaTests
    {
        string mensajeError = string.Empty;
        enum testEnum {Nombre,Apellido }
        [TestMethod()]
        public void test()
        {
            var x = typeof(testEnum);
            var esEnum = x.IsEnum;
        }

        
        [TestMethod()]
        public void insUpdCuotasToDBTest()
        {
            
            mensajeError = string.Empty;
            try
            {
                //BLLPrestamo.Instance.CuotasinsUpd(CreateCuotas()); ;
            }
            catch (Exception e)
            {

                mensajeError = e.Message;
            }
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }


        [TestMethod()]
        public void GenerarCuotasTest()
        {
            var periodo = new Periodo { Codigo = "Mes", PeriodoBase = PeriodoBase.Mes, Nombre = "Cuotas Mensuales" };
            var prestamo = new Prestamo
            {
                IdPrestamo = 1,
                FechaEmisionReal = new DateTime(2021, 01, 01),
                CantidadDeCuotas = 7,
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
            //    CantidadDeCuotas = 7,
            //    TasaDeInteresDelPeriodo = 5,
            //    Periodo = periodo,
            //    MontoCapital = 10000,
            //    TipoAmortizacion = TiposAmortizacion.No_Amortizable_cuotas_fijas,
            //    MontoGastoDeCierre = 1000,
            //    CargarInteresAlGastoDeCierre = true,
            //    FinanciarGastoDeCierre = true,
            //    OtrosCargos = 200
            //};

            IGeneradorCuotasV2 generadorCuota = new GeneradorCuotasFijasNoAmortizable2(prestamo, prestamo.IdPrestamo);

            
            var cuotas = generadorCuota.GenerarCuotas();

            var totales = new ValoresTotalesDelPrestamo();
            IEnumerable<CxCPrestamoDrMaestroBase> testData = new  List<CxCPrestamoDrMaestroBase>();
            
            totales.TCapital = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.Capital);
            totales.TCapital = cuotas.TotalCapitalMonto();
            totales.TInteres = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.InteresCapital);
            totales.TGastoDeCierre = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.GastoDeCierre);
            totales.TInteresGastoDeCierre = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.InteresGastoDeCierre);
            totales.TOtrosCargos = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.OtrosCargos);
            totales.TInteresOtrosCargos = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.InteresOtrosCargos);

            var comparaciones = new ComparacionTotales(prestamo, totales);
            var resultados = comparaciones.RealizarComparacion();
            var operacionesFallidas = resultados.Where(item => item.Resultado == false);
            var mensajeOperacionesFallidas = operacionesFallidas.Select(item => item.NombreComparacion);
            var mensajeFinal = string.Join(",", mensajeOperacionesFallidas);


            periodo = new Periodo { Codigo = "Dia", PeriodoBase = PeriodoBase.Dia, Nombre = "Cuotas Diarias" };
            var diasDelPeriodoEnElMes = 30m;
            var tasaInteresDelPeriodo = 5m / diasDelPeriodoEnElMes;
            prestamo = new Prestamo
            {
                FechaEmisionReal = new DateTime(2021, 01, 01),
                CantidadDeCuotas = 60,
                TasaDeInteresDelPeriodo = tasaInteresDelPeriodo,
                Periodo = periodo,
                MontoPrestado = 12000,
                TipoAmortizacion = TiposAmortizacion.No_Amortizable_cuotas_fijas,
                MontoGastoDeCierre = 1200,
                CargarInteresAlGastoDeCierre = true,
                FinanciarGastoDeCierre = true,
                OtrosCargos = 300,
                CargarInteresAOtrosGastos = true
            };

            generadorCuota = new GeneradorCuotasFijasNoAmortizable2(prestamo, 1);
            cuotas = generadorCuota.GenerarCuotas();
            totales = new ValoresTotalesDelPrestamo();

            totales.TCapital = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.Capital);
            totales.TInteres = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.InteresCapital);
            totales.TGastoDeCierre = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.GastoDeCierre);
            totales.TInteresGastoDeCierre = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.InteresGastoDeCierre);
            totales.TOtrosCargos = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.OtrosCargos);
            totales.TInteresOtrosCargos = cuotas.TotalMontoOriginalPorTipoCargo(TiposCargosPrestamo.InteresOtrosCargos);
            comparaciones = new ComparacionTotales(prestamo, totales);
            resultados = comparaciones.RealizarComparacion();
            operacionesFallidas = comparaciones.GetOperacionesFallidas();
            mensajeFinal = comparaciones.ListadoDeOperacionesFallidas();

            Assert.IsTrue(operacionesFallidas.Count() > 0, mensajeFinal);
        }

        class ValoresTotalesDelPrestamo
        {
            public decimal TCapital { get; set; }
            public decimal TGastoDeCierre { get; set; }

            public decimal TOtrosCargos { get; set; }

            public decimal TInteres { get; set; }

            public decimal TInteresGastoDeCierre { get; set; }
            public decimal TInteresOtrosCargos { get; set; }
        }


        public class ResultadosComparacion
        {
            public string NombreComparacion { get; private set; }

            public bool Resultado { get; private set; }

            public ResultadosComparacion(string nombre, bool result)
            {
                this.NombreComparacion = nombre;
                this.Resultado = result;
            }
        }
        class ComparacionTotales
        {
            IInfoGeneradorCuotas DatosAComparar { get; set; }

            ValoresTotalesDelPrestamo TotalesCalculados { get; set; }

            public List<ResultadosComparacion> Resultados { get; set; } = new List<ResultadosComparacion>();
            public ComparacionTotales(IInfoGeneradorCuotas infg, ValoresTotalesDelPrestamo vTotales)
            {
                this.DatosAComparar = infg;
                TotalesCalculados = vTotales;
            }

            public List<ResultadosComparacion> RealizarComparacion()
            {
                var compCapital = TotalesCalculados.TCapital == DatosAComparar.MontoCapital;
                Resultados.Add(new ResultadosComparacion("capital", compCapital));
                var compGastoDeCierre = TotalesCalculados.TGastoDeCierre == DatosAComparar.MontoGastoDeCierre;
                Resultados.Add(new ResultadosComparacion("gastoDeCierre", compGastoDeCierre));
                var compOtrosCargos = TotalesCalculados.TOtrosCargos == DatosAComparar.OtrosCargos;
                Resultados.Add(new ResultadosComparacion("otrosCargos", compOtrosCargos));
                var compInteres = TotalesCalculados.TInteres == (Math.Round(DatosAComparar.MontoCapital * DatosAComparar.TasaDeInteresDelPeriodo / 100, 2) * DatosAComparar.CantidadDeCuotas);
                Resultados.Add(new ResultadosComparacion("interes", compInteres));
                var compInteresGastoDeCierre = TotalesCalculados.TInteresGastoDeCierre == (Math.Round(DatosAComparar.MontoGastoDeCierre * DatosAComparar.TasaDeInteresDelPeriodo / 100, 2) * DatosAComparar.CantidadDeCuotas);
                Resultados.Add(new ResultadosComparacion("interes Gasto de Cierre", compInteresGastoDeCierre));
                var compInteresOtrosCargos = TotalesCalculados.TInteresOtrosCargos == (Math.Round(DatosAComparar.OtrosCargos * (DatosAComparar.CargarInteresAOtrosGastos ? DatosAComparar.TasaDeInteresDelPeriodo / 100 : 0), 2) * DatosAComparar.CantidadDeCuotas);
                Resultados.Add(new ResultadosComparacion("interes Otros Cargos", compInteresOtrosCargos));
                return Resultados;
            }

            public IEnumerable<ResultadosComparacion> GetOperacionesFallidas()
            {
                var result = Resultados.Where(item => item.Resultado == false);
                return result;
            }

            public String ListadoDeOperacionesFallidas() => string.Join(",", GetOperacionesFallidas());
        }
        public class CompararTotales
        {
            public string NombreOperacion { get; set; }

            public Func<bool> OperacionDeComparacion { get; set; }
        }
        
    }
}