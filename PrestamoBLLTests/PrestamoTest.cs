using Microsoft.AspNetCore.JsonPatch.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Tests;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLLTests
{
    [TestClass]
    public class PrestamoTest
    {
        public PrestamoTest()
        {

        }

        [TestMethod]
        public async Task CreatePrestamo()
        {
            Prestamo prestamo = new Prestamo(); ;

            var testInfo = new TestInfo();
            try
            {
                var idCliente = getCliente();
                var idClasificacion = getClasificacionForNombre("Personal");
                var idPeriodo = getPeriodoForCodigo("MES");
                var idTasainteres = getTasaDeInteres("E00");
                var idTipoMora = getTipoMora("P10I");
                prestamo = new Prestamo()
                {
                    // base properties
                    IdNegocio = TestInfo.GetIdNegocio(),
                    IdLocalidadNegocio= TestInfo.GetIdLocalidadNegocio(),
                    Usuario = TestInfo.Usuario,
                    //
                    IdClasificacion = idClasificacion,
                    IdCliente = idCliente,
                    IdPeriodo = idPeriodo,
                    FechaEmisionReal = new DateTime(2023,01, 01),
                    CantidadDeCuotas = 10,
                    MontoPrestado = 10000.00M,
                    IdTasaInteres = idTasainteres,
                    IdTipoMora = idTipoMora
                };
                await BLLPrestamo.Instance.InsUpdPrestamo(prestamo);
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
                testInfo.ExceptionOccured = e;
            }

            Assert.IsTrue(string.IsNullOrEmpty(testInfo.MensajeError), "fallo creando prestamo" + testInfo.MensajeError);

        }

        private int getTipoMora(string codigo)
        {
            var tiposMora = new TipoMoraBLL(1, TestInfo.Usuario).GetTiposMoras(new TipoMoraGetParams());
            var tipoMora = tiposMora.FirstOrDefault(item => item.Codigo == codigo);
            return GetResult<TipoMora>(tipoMora).IdTipoMora;
        }

        private int getCliente() 
        {
            var clientes = new ClienteBLL(TestInfo.GetIdLocalidadNegocio(), TestInfo.Usuario).GetClientes(new ClienteGetParams());
            var cliente = clientes.FirstOrDefault();
            return GetResult<Cliente>(cliente).IdCliente;
        }


        private int getPeriodoForCodigo(string codigo)
        {
            var periodos = new PeriodoBLL(1, TestInfo.Usuario).GetPeriodos(new PeriodoGetParams());
            var periodo = periodos.FirstOrDefault(item => item.Codigo == codigo);
            return GetResult<Periodo>(periodo).idPeriodo;
        }

        private int getTasaDeInteres(string codigo)
        {
            var tasasDeInteres = new TasaInteresBLL(1, TestInfo.Usuario).GetTasasDeInteres(new TasaInteresGetParams { Codigo = codigo });
            return GetResult<int>(tasasDeInteres.FirstOrDefault().idTasaInteres);
        }

        private int getClasificacionForNombre(string nombreClasificacion)
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
