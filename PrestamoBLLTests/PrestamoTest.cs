using Microsoft.VisualStudio.TestTools.UnitTesting;

using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLLTests
{
    [TestClass()]
    public class PrestamoTest
    {
        TestInfo testInfo = new TestInfo();
        
        public Dictionary<int, string> Clasificacion = new Dictionary<int, string>();

        [TestMethod()]
        public void PrestamoInsUpdTest()
        {
            var idResult = 0;
            Func<bool> condicion = ()=>(idResult > 0);
            try
            {
                idResult = BLLPrestamo.Instance.InsUpdPrestamo(CreatePrestamo());
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

            PrestamoConCuotas result = null;
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
                TipoAmortizacion = TiposAmortizacion.Cuotas_fijas_No_amortizable,
                IdClasificacion = GetClasificacion(),
                IdNegocio = 6,
                IdDivisa = 1, // equivale a la moneda nacional (siempre el codigo 1 es la moneda nacional del pais
                MontoPrestado = 10000,
                IdTasaInteres = GetTasaDeInteres(),
                IdPeriodo = GetPeriodo(),
                CantidadDePeriodos = 5,
                IdTipoMora = GetTipoMora(),
            };
            pre.Clientes.Add(GetClientes().FirstOrDefault());
            pre.Garantias.Add(GetGarantias().FirstOrDefault());
            return pre;
        }

        [TestMethod()]
        public void GeneradoDeCuotasFijasNoAmortizable()
        {
            var periodo = new Periodo { MultiploPeriodoBase = 1, PeriodoBase = PeriodoBase.Mes };
            IPrestamoForGeneradorCuotas prestamo = new Prestamo(periodo)
            {
                FechaEmisionReal = new DateTime(2020, 01, 01),
                CantidadDePeriodos = 5,
                TasaDeInteresPorPeriodo = 5,
                MontoPrestado = 10000,
            };
            var genCuota = new GeneradorCuotasFijasNoAmortizables(prestamo);
            var cuotas = genCuota.GenerarCuotas();
            var todoBien = true;
        }


        private int GetClasificacion()
        {
            Clasificacion.Add(1, "Vehiculo");
            Clasificacion.Add(2, "Motores");
            Clasificacion.Add(3, "Personal");
            Clasificacion.Add(4, "Hipotecario");
            Clasificacion.Add(5, "Comercial");
            var itemDict = Clasificacion.Where(item => item.Value == "Vehiculo").FirstOrDefault();
            return itemDict.Key;
            
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
            var result = BLLPrestamo.Instance.ClientesGet(new ClientesGetParams { IdNegocio = 6 });
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