using Microsoft.AspNetCore.JsonPatch.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Tests;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass]
    public class PrestamoTest
    {
        public PrestamoTest()
        {

        }

        [TestMethod]
        public async Task CreatePrestamoTest()
        {
            Prestamo prestamo = new Prestamo(); ;
            int id = 0;
            var testInfo = new TestInfo();
            try
            {
                prestamo = await CreatePrestamoInstance();
                id = await new PrestamoBLLC(TestInfo.GetIdLocalidadNegocio(), TestInfo.Usuario).InsUpdPrestamo(prestamo);
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
                testInfo.ExceptionOccured = e;
            }

            Assert.IsTrue(string.IsNullOrEmpty(testInfo.MensajeError), "fallo creando prestamo" + testInfo.MensajeError);

        }

        
        
        public async Task<Prestamo> CreatePrestamoInstance()
        {
            Prestamo prestamo = new Prestamo(); ;
            var testInfo = new TestInfo();

            var idCliente = getIdCliente();
            var idClasificacion = getIdClasificacionForNombre("Personal");
            var idPeriodo = GetIdPeriodoForCodigo("MES");
            //var periodo = GetPeriodoInstance("MES");
            var idTasainteres = GetIdTasaDeInteres("E00");
            //var tasadeInteres = GetTasaDeInteresInstance("E00");
            var idTipoMora = getTipoMora("P10I");
            prestamo = new Prestamo()
            {
                // base properties
                IdNegocio = TestInfo.GetIdNegocio(),
                IdLocalidadNegocio = TestInfo.GetIdLocalidadNegocio(),
                Usuario = TestInfo.Usuario,
                //
                IdClasificacion = idClasificacion,
                IdCliente = idCliente,
                IdPeriodo = idPeriodo,
                //Periodo = periodo,
                FechaEmisionReal = new DateTime(2023, 01, 01),
                CantidadDeCuotas = 10,
                MontoPrestado = new Random().Next(10000, 30000),
                IdTasaInteres = idTasainteres,
                IdTipoMora = idTipoMora,
            };
            return prestamo;
        }
        private int getTipoMora(string codigo)
        {
            var tiposMora = new TipoMoraBLL(1, TestInfo.Usuario).GetTiposMoras(new TipoMoraGetParams());
            var tipoMora = tiposMora.FirstOrDefault(item => item.Codigo == codigo);
            return GetResult<TipoMora>(tipoMora).IdTipoMora;
        }

        private int getIdCliente()
        {
            Cliente? cliente = GetClienteInstance();
            return GetResult<Cliente>(cliente).IdCliente;
        }

        private static Cliente? GetClienteInstance()
        {
            var clientes = new ClienteBLL(TestInfo.GetIdLocalidadNegocio(), TestInfo.Usuario).GetClientes(new ClienteGetParams());
            var cliente = clientes.FirstOrDefault();
            return cliente;
        }

        internal int GetIdPeriodoForCodigo(string codigo)
        {
            Periodo? periodo = GetPeriodoInstance(codigo);
            return GetResult<Periodo>(periodo).idPeriodo;
        }

        private static Periodo? GetPeriodoInstance(string codigo)
        {
            var periodos = new PeriodoBLL(1, TestInfo.Usuario).GetPeriodos(new PeriodoGetParams { Codigo = codigo });
            var periodo = periodos.FirstOrDefault();
            return periodo;
        }

        internal int GetIdTasaDeInteres(string codigo)
        {
            TasaInteres? tasaDeInteres = GetTasaDeInteresInstance(codigo);
            return GetResult<int>(tasaDeInteres.idTasaInteres);
        }

        private static TasaInteres? GetTasaDeInteresInstance(string codigo)
        {
            var tasasDeInteres = new TasaInteresBLL(1, TestInfo.Usuario).GetTasasDeInteres(new TasaInteresGetParams { Codigo = codigo });
            var tasaDeInteres = tasasDeInteres.FirstOrDefault();
            return tasaDeInteres;
        }

        private int getIdClasificacionForNombre(string nombreClasificacion)
        {
            var clasificaciones = BLLPrestamo.Instance.GetClasificaciones(new ClasificacionesGetParams { IdNegocio = 1 });
            var clasificacion = clasificaciones.FirstOrDefault(item => item.Nombre == nombreClasificacion);
            return GetResult<Clasificacion>(clasificacion).IdClasificacion;
        }

        private T GetResult<T>(T obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException("No encontramos ninguna clasificacion para prestamos personales");
            }
            return obj;
        }
    }
}
