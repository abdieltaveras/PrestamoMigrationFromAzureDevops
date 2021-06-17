using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrestamoEntidades;


namespace PrestamoBLL.Tests
{
    public class TestInfo
    {
        /// <summary>
        ///  Usuario del proyecto 
        /// </summary>
        public static string Usuario => "PrestamoBllTestProject";
        public string _Usuario => Usuario;
        public string MensajeError { get; set; } = string.Empty;
    }

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

        

        public void GetOperacionesWithUserId_2()
        {
            var response = BLLPrestamo.Instance.GetOperaciones(new UsuarioOperacionesGetParams() { IdUsuario = 2 });
            Assert.IsTrue(response.Count() > 0, "No retorno registros");

        }
    }
}