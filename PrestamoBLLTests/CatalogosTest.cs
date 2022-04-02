using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using PrestamoBLL.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class CatalogosTest
    {
        private int idNegocio;

        private string NombreTabla = "TblOcupaciones";
        private string IdNombreColumna= "IdOcupacion";

        [TestMethod()]
        public void CatalogosGetTest()
        {
            var testinfo = new TestInfo();
            IEnumerable<BaseCatalogo> result = null;
            try
            {
                result = BLLPrestamo.Instance.GetCatalogos(new BaseCatalogoGetParams { NombreTabla = NombreTabla, IdNegocio = -1, IdTabla = IdNombreColumna });
            }
            catch (Exception e)
            {
                testinfo.MensajeError = e.Message;

            }


            Assert.IsTrue(result != null, "al hacer el get no se pudo realizar la consulta", testinfo.MensajeError);
        }

        [TestMethod()]
        public void DeleteCatalogoTest()
        {

            var catalogos = BLLPrestamo.Instance.GetCatalogos(new BaseCatalogoGetParams { NombreTabla = NombreTabla, IdTabla = IdNombreColumna }); ;

            var ocupacionMecanico = catalogos.Where(item => item.Nombre == "Mecanico").FirstOrDefault();
            
            var ocupacionABorrar = new BaseCatalogoDeleteParams { IdNombreColumna = "IdOcupacion", NombreTabla = "TblOcupaciones", IdRegistro = ocupacionMecanico.GetId(), Usuario = TestInfo.Usuario };

            BLLPrestamo.Instance.DeleteCatalogo(ocupacionABorrar);
        }
    }
}