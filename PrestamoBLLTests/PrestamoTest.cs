using emtSoft.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class PrestamoTest
    {
        TestInfo testInfo = new TestInfo();

        public Dictionary<int, string> Clasificacion = new Dictionary<int, string>();

        [TestMethod()]
        public void GetPrestamoConDetalleForUIPrestamoTest()
        {
            var idPrestamo = BLLPrestamo.Instance.GetPrestamos(new PrestamosGetParams()).ToList().FirstOrDefault().IdPrestamo;
            PrestamoConDetallesParaUIPrestamo prConDetalle = null;
            Func<bool> condicion = () => (prConDetalle != null);
            try
            {
                prConDetalle = BLLPrestamo.Instance.GetPrestamoConDetalleForUIPrestamo(idPrestamo);
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
            }

            var infCliente = prConDetalle.infoCliente;
            var infPrestamo = prConDetalle.infoPrestamo;
            var infGarantias = prConDetalle.infoGarantias;
            Assert.IsTrue(condicion(), testInfo.MensajeError);
        }

        [TestMethod()]
        public void PrestamoInsUpdWithoutCodeudoresTest()
        {
            var idResult = 0;
            Func<bool> condicion = () => (idResult > 0);
            try
            {
                var prestamo = CreatePrestamo();
                idResult = BLLPrestamo.Instance.InsUpdPrestamo(prestamo) ;
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
            }

            Assert.IsTrue(condicion(), testInfo.MensajeError);
        }

        [TestMethod()]
        public void GetPrestamosTest()
        {
            var prestamos = new List<Prestamo>();
            Func<bool> condicion = () => (prestamos!=null);
            try
            {
                var search = new PrestamosGetParams();
                prestamos  = BLLPrestamo.Instance.GetPrestamos(search).ToList();
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
            }
            var prestamo = prestamos != null ? prestamos.FirstOrDefault() : new Prestamo();
            Assert.IsTrue(condicion(), testInfo.MensajeError);

        }

        [TestMethod()]
        public void GetPrestamosConDetalleDrCrTest()
        {
            var idPrestamo = BLLPrestamo.Instance.GetPrestamos(new PrestamosGetParams()).ToList().FirstOrDefault().IdPrestamo;
            PrestamoConDetallesParaCreditosYDebitos prConDetalle=null;
            Func<bool> condicion = () => (prConDetalle != null);
            try
            {
                prConDetalle = BLLPrestamo.Instance.GetPrestamoConDetalle(idPrestamo, DateTime.Now.AddDays(40));
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
            }

            var infCliente = prConDetalle.infoCliente;
            var infPrestamo = prConDetalle.infoPrestamo;
            var cuotas = prConDetalle.Cuotas;
            var infGarantias = prConDetalle.infoGarantias;
            var infDeuda = prConDetalle.InfoDeuda;

            Assert.IsTrue(condicion(), testInfo.MensajeError);
            
        }

        [TestMethod()]
        public void PrestamoInsUpdWithABadGarantiaTest()
        {
            var idResult = 0;
            Func<bool> condicion = () => (idResult > 0);
            var prestamo = CreatePrestamo();
            prestamo._Garantias[0].IdGarantia = 7;
            try
            {
                idResult = BLLPrestamo.Instance.InsUpdPrestamo(prestamo);

            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
            }

            Assert.IsTrue(condicion(), testInfo.MensajeError);

        }

        [TestMethod()]
        public void PrestamoBuilderTest()
        {
            // Debe tener informacion de si es inmobliario, mobiliario para que pueda determinar que tipo de garantia
            Prestamo pre = CreatePrestamo();
            var prestamoNuevo = new PrestamoBuilder(pre);

            Prestamo result = null;
            try
            {
                result = prestamoNuevo.Build();
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
            }
            Assert.IsNotNull(result, testInfo.MensajeError);
        }
        
        private Prestamo CreatePrestamo()
        {
            var cliente = new Cliente();
            var pre = new Prestamo
            {
                FechaEmisionReal = DateTime.Now,
                IdTipoAmortizacion =(int)TiposAmortizacion.No_Amortizable_cuotas_fijas,
                IdClasificacion = GetClasificacion(),
                IdNegocio = 6,
                IdDivisa = 1, // equivale a la moneda nacional (siempre el codigo 1 es la moneda nacional del pais
                MontoPrestado = 10000,
                IdTasaInteres = GetTasaDeInteres(),
                IdPeriodo = GetPeriodo(),
                CantidadDePeriodos = 5,
                IdTipoMora = GetTipoMora(),
            };
            pre.IdCliente = GetClientes().FirstOrDefault().IdCliente;
            pre.IdGarantias.Add(GetGarantias().FirstOrDefault().IdGarantia);
            return pre;
        }

        [TestMethod()]
        public void PrestamoConDependencias()
        {
            var pre = CreatePrestamo();
            var cuotas = CrearCuotasNoAmortizableCincoMeses();
            //PrestamoInsUpdParam pr = new PrestamoInsUpdParam(null,cuotas,null,null);
            //

        }
        [TestMethod()]
        public void GeneradoDeCuotasFijasNoAmortizable()
        {
            var result = CrearCuotasNoAmortizableCincoMeses();
            var todoBien = true;
        }

        private static IEnumerable<Cuota> CrearCuotasNoAmortizableCincoMeses()
        {
            var periodo = new Periodo { MultiploPeriodoBase = 1, PeriodoBase = PeriodoBase.Mes };
            var cuotas = CreateCuotasNoAmortizable(periodo, 5);
            return cuotas;
        }

        private static IEnumerable<Cuota> CreateCuotasNoAmortizable(Periodo periodo, int duracion)
        {
            IPrestamoForGeneradorCuotas prestamo = new Prestamo(periodo)
            {
                FechaEmisionReal = new DateTime(2020, 01, 01),
                CantidadDePeriodos = duracion,
                TasaDeInteresPorPeriodo = 5,
                MontoPrestado = 10000,
            };
            var genCuota = new GeneradorCuotasFijasNoAmortizables(prestamo);
            var cuotas = genCuota.GenerarCuotas();
            
            return cuotas;
        }

        private int GetClasificacion()
        {
            return 1;
            
        }

        private IEnumerable<Garantia> GetGarantias()
        {
            var result = BLLPrestamo.Instance.GarantiaSearch( new BuscarGarantiaParams { Search = "2626" });
            return result;
        }

        public Codeudor GetCodeudores()
        {
            return null;
        }
        private IEnumerable<Cliente> GetClientes()
        {
            var result = BLLPrestamo.Instance.ClientesGet(new ClienteGetParams { IdNegocio = 6 });
            return result;
        }

        private int GetTipoMora()
        {
            var mora = BLLPrestamo.Instance.TiposMorasGet(new TipoMoraGetParams { IdNegocio = 6, Codigo = "P10I" }).FirstOrDefault();
                return mora.IdTipoMora;
        }

        private int GetPeriodo()
        {
            var periodo = BLLPrestamo.Instance.GetPeriodos(new PeriodoGetParams { IdNegocio = 6, Codigo="MES" }).FirstOrDefault();
            return periodo.idPeriodo;
        }

        private int GetTasaDeInteres()
        {
            var tasainteres = BLLPrestamo.Instance.TasasInteresGet(new TasaInteresGetParams { IdNegocio = 6, Codigo="C00" }).FirstOrDefault();
            return tasainteres.idTasaInteres;
        }
    }
}