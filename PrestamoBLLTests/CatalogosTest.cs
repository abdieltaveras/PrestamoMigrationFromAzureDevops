using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamoBLLTests;
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

        [TestMethod()]
        public void CatalogosGetTest()
        {
            var testinfo = new TestInfo();
            IEnumerable<BaseCatalogo> result = null;
            try
            {

                result = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { NombreTabla = "tblClasificaciones", IdNegocio = 6, IdTabla="idClasificacion"});
             
            }
            catch (Exception e)
            {
                testinfo.MensajeError = e.Message;
               
            }


            Assert.IsTrue(result != null, "al hacer el get no se pudo realizar la consulta", testinfo.MensajeError);
        }
    }
}