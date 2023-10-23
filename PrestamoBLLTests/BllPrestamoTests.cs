using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoEntidades;
using System;
using System.Linq;


namespace PrestamoBLL.Tests
{

    [TestClass()]
    public class BllPrestamoTests
    {
        [TestMethod()]
        public void CatalogosGetTestForOcupacion()
        {

            //var lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = 1}, "ocupacion");
            //var datos = lista.Count() > 0;
            //Assert.IsTrue(datos, "no se econtraron datos");
        }
        [TestMethod()]
        public void CatalogosGetTestForVerificadirDireccion()
        {
            //var lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = 1 }, "verificadorDireccion");
            //var datos = lista.Count() > 0;
            //Assert.IsTrue(datos, "no se econtraron datos");
        }

        [TestMethod()]
        public void RoleInsUpdTest_result_resultgreaterThanZero()
        {
            var ins = new Role { Nombre = "Probando", Usuario = "test", IdNegocio = 1 };
            var result = BLLPrestamo.Instance.InsUpdRole(ins);
            Assert.IsTrue(result > 0, $"se esperaba valor mayor a 0 y se obtuvo {result}");
        }
        [TestMethod()]
        public void GetPrestamoCliente()
        {
            //var ins = new Role { Nombre = "Probando", Usuario = "test", IdNegocio = 1 };
            //var result = new PrestamoBLLC(1, "Test").GetPrestamoCliente(new PrestamoClienteUIGetParam { IdCliente = 1 });
            var result = new PrestamoBLLC(1, "Test").GetPrestamoConDetalle(1, DateTime.Now, false);

            //Assert.IsTrue(result > 0, $"se esperaba valor mayor a 0 y se obtuvo {result}");
        }



        public void GetOperacionesWithUserId_2()
        {
            var response = BLLPrestamo.Instance.GetOperaciones(new UsuarioOperacionesGetParams() { IdUsuario = 2 });
            Assert.IsTrue(response.Count() > 0, "No retorno registros");

        }
    }
}