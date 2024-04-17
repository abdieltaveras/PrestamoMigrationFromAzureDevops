using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class BLLPrestamoTests
    {
        /// <summary>
        /// Consulta mediante el Bll el objeto que almacena las tasas de interes
        /// </summary>
        [TestMethod()]

        public void GetTasaInteresTest()
        {
            var result = new TasaInteresBLL(1, TestUtils.Usuario).GetTasasDeInteres(new TasaInteresGetParams { IdNegocio = 1 });
            Assert.IsTrue(result.Count() > 0);
        }
        /// <summary>
        /// insertar data mediante el BLL para el objeto TasaInteres
        /// </summary>
        [TestMethod()]
        public void insUpdTasaInteresTest()
        {
            var error = new Exception();
            var OperacionExitosa = true;
            var tasaInteres = new TasaInteres { Codigo = "B05", InteresMensual = 2.5M, Usuario = "TestProject", IdNegocio = 1 };
            var searchData = new TasaInteresGetParams { Codigo = "B05", IdNegocio = -1 };
            var result = new TasaInteresBLL(1, TestUtils.Usuario).GetTasasDeInteres(searchData);
            if (result.Count() != 0)
            {
                tasaInteres.idTasaInteres = result.First().idTasaInteres;
            }
            try
            {
             
                new TasaInteresBLL(1, TestUtils.Usuario).InsUpdTasaInteres(tasaInteres);
            }
            catch (Exception e)
            {
                error = e;
                OperacionExitosa = false;

            }
            Assert.IsTrue(OperacionExitosa, error.Message);
        }
        [TestMethod()]
        public void CalcularTasaInterePorPeriodoTest()
        {
            var search = new PeriodoGetParams();
            var periodos = new PeriodoBLL(1,TestUtils.Usuario).GetPeriodos(search);

            foreach (var item in periodos)
            {
                var result = new TasaInteresBLL(1, TestUtils.Usuario).CalcularTasaInteresPorPeriodos(10, item);
                var NombrePeriodo = item.Nombre;
                var tasaInteresPeriodo = result.InteresDelPeriodo;
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void CalcularTasaInterePorPeriodoTest2()
        {
            var search = new PeriodoGetParams();
            var periodos = new PeriodoBLL(1, "test").GetPeriodos(search);

            var diario = periodos.Where(per => per.Codigo == "DIA").FirstOrDefault();
            var semanal = periodos.Where(per => per.Codigo == "SEM").FirstOrDefault();
            var quincenal = periodos.Where(per => per.Codigo == "QUI").FirstOrDefault();
            var mensual = periodos.Where(per => per.Codigo == "MES").FirstOrDefault();

            var tasaIntBll = new TasaInteresBLL(1, TestUtils.Usuario);
            var resultDiario = tasaIntBll.CalcularTasaInteresPorPeriodos(10, diario);
            var resultQuincenal = tasaIntBll.CalcularTasaInteresPorPeriodos(10, quincenal);
            var resultSemanal = tasaIntBll.CalcularTasaInteresPorPeriodos(10, semanal);
            var resultMensual = tasaIntBll.CalcularTasaInteresPorPeriodos(10, mensual);
            Assert.Fail();
        }
    }
}