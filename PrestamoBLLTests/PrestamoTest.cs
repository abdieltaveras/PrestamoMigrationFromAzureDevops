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
        public async Task InsUpdPrestamo()
        {
            var prestamo = new Prestamo()
            {
                IdCliente = getCliente(),
                IdClasificacion = getClasificacionForNombre("Personal"),
                FechaEmisionReal = new DateTime(01, 01, 2023),
                IdPeriodo = getPeriodoForCodigo("MES"),
                CantidadDePeriodos = 10,
                




            };
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
            var periodo = periodos.FirstOrDefault(item => item.Codigo == "MES");
            return GetResult<Periodo>(periodo).idPeriodo;
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
