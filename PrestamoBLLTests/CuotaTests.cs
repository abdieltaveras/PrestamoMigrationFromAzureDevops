using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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

        [TestMethod]
        public async Task CuotasGeneratorTest()
        {
            TestInfo testInfo;
            InfoGeneradorDeCuotas cuotaInfo;
            GetInfoCuota(out testInfo, out cuotaInfo);
            //var result = new TasaInteresBLL(prestamo.IdLocalidadNegocio, prestamo.Users).CalcularTasaInteresPorPeriodos (tasaDeInteres.InteresMensual,prestamo.Periodo);
            //var tasaDeInteresDelPeriodo = result.InteresDelPeriodo;

            IEnumerable<CxCCuota> cuotas = null;
            try
            {
                cuotas = CuotasGenerator.CreateCuotas(cuotaInfo);
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
                testInfo.ExceptionOccured = e;
            }
            Assert.IsTrue(string.IsNullOrEmpty(testInfo.MensajeError), "fallo creando prestamo" + testInfo.MensajeError);

        }

        private static void GetInfoCuota(out TestInfo testInfo, out InfoGeneradorDeCuotas cuotaInfo)
        {
            testInfo = new TestInfo();
            var prestamoTest = new PrestamoTest();

            var idPeriodo = prestamoTest.GetIdPeriodoForCodigo("MES");
            //var periodo = GetPeriodoInstance("MES");
            var idTasainteres = prestamoTest.GetIdTasaDeInteres("E00");
            var montoPrestado = 10000;

            cuotaInfo = new InfoGeneradorDeCuotas()
            {
                Usuario = testInfo._Usuario,
                IdLocalidadNegocio = TestInfo.GetIdLocalidadNegocio(),
                FechaEmisionReal = new DateTime(2023, 01, 01),
                TipoAmortizacion = TiposAmortizacion.No_Amortizable_cuotas_fijas,
                MontoCapital = montoPrestado,
                IdPeriodo = idPeriodo,
                IdTasaInteres = idTasainteres,
                CantidadDeCuotas = 10,
                MontoGastoDeCierre = Convert.ToDecimal(montoPrestado * 0.10),
                FinanciarGastoDeCierre = true,
                CargarInteresAlGastoDeCierre = true,
            };
        }

        [TestMethod]
        public async Task InsUpdCuotasMaestroTest()
        {
            TestInfo testInfo;
            InfoGeneradorDeCuotas cuotaInfo;
            GetInfoCuota(out testInfo, out cuotaInfo);
            IEnumerable<IMaestroDebitoConDetallesCxC> cuotas = new List<IMaestroDebitoConDetallesCxC>();
            try
            {
                var prestamoResult = ConfigurationManager.AppSettings["IdPrestamoTestGenerarCuotasMaestroDetalle"];
                var idPrestamo = 12;
                cuotas = CuotasGenerator.CreateCuotasMaestroDetalle(idPrestamo, cuotaInfo);
                
                BLLPrestamo.Instance.InsUpdDebitoMaestro(cuotas);

                // guardar este objeto en una tabla de la base de datos
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
                testInfo.ExceptionOccured = e;
            }
            Assert.IsTrue(string.IsNullOrEmpty(testInfo.MensajeError), "fallo creando prestamo" + testInfo.MensajeError);

        }

        [TestMethod]
        public async Task GetCuotasMaestroDetallesTest()
        {
            TestInfo testInfo = new TestInfo();
            try
            {

                MaestroDetallerDr.GetCuotasMaestroDetalles(1, 1, 12);

                // guardar este objeto en una tabla de la base de datos
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
                testInfo.ExceptionOccured = e;
            }
            Assert.IsTrue(string.IsNullOrEmpty(testInfo.MensajeError), "fallo creando prestamo" + testInfo.MensajeError);

        }
        /// <summary>
        /// Para probar insertar los cargos en vez de un json a una tabla
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task InsUpdDatallesCargoToTableTest()
        {
            TestInfo testInfo;
            InfoGeneradorDeCuotas cuotaInfo;
            GetInfoCuota(out testInfo, out cuotaInfo);
            IEnumerable<IMaestroDebitoConDetallesCxC> cuotas = new List<IMaestroDebitoConDetallesCxC>();
            try
            {
                var prestamoResult = ConfigurationManager.AppSettings["IdPrestamoTestGenerarCuotasMaestroDetalle"];
                var idPrestamo = 12;
                cuotas = CuotasGenerator.CreateCuotasMaestroDetalle(idPrestamo, cuotaInfo);

                BLLPrestamo.Instance.InsUpdDetallesCargos(cuotas);

                // guardar este objeto en una tabla de la base de datos
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
                testInfo.ExceptionOccured = e;
            }
            Assert.IsTrue(string.IsNullOrEmpty(testInfo.MensajeError), "fallo creando prestamo" + testInfo.MensajeError);

        }
        [TestMethod]
        public async Task TableVaueTypeToDtaTableConversionTest()
        {
            TestInfo testInfo= new TestInfo();

            try
            {
                BLLPrestamo.Instance.TestTVToDataTable();
                // guardar este objeto en una tabla de la base de datos
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
                testInfo.ExceptionOccured = e;
            }
            Assert.IsTrue(string.IsNullOrEmpty(testInfo.MensajeError), "fallo creando prestamo" + testInfo.MensajeError);

        }
        [TestMethod]
        public async Task CuotasMaestroDetallesCargosTest()
        {
            TestInfo testInfo;
            InfoGeneradorDeCuotas cuotaInfo;
            GetInfoCuota(out testInfo, out cuotaInfo);
            IEnumerable<IMaestroDebitoConDetallesCxC> cuotas = new List<IMaestroDebitoConDetallesCxC>();
            try
            {
                var prestamoResult = ConfigurationManager.AppSettings["IdPrestamoTestGenerarCuotasMaestroDetalle"];
                var idPrestamo = 12;
                cuotas = CuotasGenerator.CreateCuotasMaestroDetalle(idPrestamo, cuotaInfo);
                BLLPrestamo.Instance.InsUpdDebitoMaestroDetalle(cuotas);
                //BLLPrestamo.Instance.TryJsonDeserialization(cuotas);

                // guardar este objeto en una tabla de la base de datos
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
                testInfo.ExceptionOccured = e;
            }
            Assert.IsTrue(string.IsNullOrEmpty(testInfo.MensajeError), "fallo creando prestamo" + testInfo.MensajeError);

        }
        

        [TestMethod()]
        public void GenerarCuotasForDifferentValuesTest()
        {

            // este procedimiento debera ser revisado por completo y toda la responsabilidad debe estar en el objeto
            // que genera las cuotas que es quien sabra daterminar todo lo que aqui se hacer o desea conocer
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



            //necesito aqui un objeto que sea capaz de indicarme la tasa de interes para el Periodo quincenal

            //var infCuota = new InfoGeneradorDeCuotas()
            //{
            //    AcomodarFechaALasCuotas = false,
            //    CantidadDeCuotas = 7,
            //    TasaDeInteresDelPeriodo = 5,
            //    Periodo = Periodo,
            //    MontoCapital = 10000,
            //    TipoAmortizacion = TiposAmortizacion.No_Amortizable_cuotas_fijas,
            //    MontoGastoDeCierre = 1000,
            //    CargarInteresAlGastoDeCierre = true,
            //    FinanciarGastoDeCierre = true,
            //    OtrosCargos = 200
            //};

            // ignorador para evitar error
            //IGeneradorCuotasV2 generadorCuota = new GeneradorCuotasFijasNoAmortizable2(prestamo, prestamo.IdPrestamo);

            
            //var cuotas = generadorCuota.GenerarCuotas();
            var cuotas = new List<CuotaPrestamo>(); // la linea que en verdad va es la anterior

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
            var diasDelPeriodoEnElMes = 30;
            var tasaInteresDelPeriodo = 5 / diasDelPeriodoEnElMes;
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
                CargarInteresOtrosCargos = true
            };

            //generadorCuota = new GeneradorCuotasFijasNoAmortizable2(prestamo,-1);
            //cuotas = generadorCuota.GenerarCuotas();
            
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
                // estas lineas ignoradas se deberan restablecer luego
                //var compInteres = TotalesCalculados.TInteres == (Math.Round(DatosAComparar.MontoCapital * DatosAComparar.TasaDeInteresDelPeriodo / 100, 2) * DatosAComparar.CantidadDeCuotas);
                //Resultados.Add(new ResultadosComparacion("interes", compInteres));
                //var compInteresGastoDeCierre = TotalesCalculados.TInteresGastoDeCierre == (Math.Round(DatosAComparar.MontoGastoDeCierre * DatosAComparar.TasaDeInteresDelPeriodo / 100, 2) * DatosAComparar.CantidadDeCuotas);
                //Resultados.Add(new ResultadosComparacion("interes Gasto de Cierre", compInteresGastoDeCierre));
                //var compInteresOtrosCargos = TotalesCalculados.TInteresOtrosCargos == (Math.Round(DatosAComparar.OtrosCargos * (DatosAComparar.CargarInteresOtrosCargos ? DatosAComparar.TasaDeInteresDelPeriodo / 100 : 0), 2) * DatosAComparar.CantidadDeCuotas);
                //Resultados.Add(new ResultadosComparacion("interes Otros Cargos", compInteresOtrosCargos));
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