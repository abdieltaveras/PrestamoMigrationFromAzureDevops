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
using DevBox.Core.Classes.Utils;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class CatalogosTest
    {
        private int idNegocio;

        private string NombreTabla = "TblOcupaciones";
        private string IdNombreColumna = "IdTipoCargo";

        [TestMethod()]
        public void InsertCataloTiposCargoDebitoTest()
        {
            var data = new TipoCargoDebito
            {
                Codigo = "TEST",
                Nombre = "Codigo para probar que inserta ",
                IdNegocio = TestInfo.GetIdNegocio(),
                IdLocalidadNegocio = TestInfo.GetIdLocalidadNegocio(),
                Usuario = TestInfo.Usuario

            };

            var json = data.ToJSON();
            var resultTest = new TestInfo();
            
            try
            {
                BLLPrestamo.Instance.InsUpdCatalogo(CatalogoName.TipoCargoDebito, data);
                //BLLPrestamo.Instance.InsUpdCatalogo
            }
            catch (Exception e)
            {
                resultTest.MensajeError = e.Message;
            }
            

        }

        [TestMethod()]
        public void CatalogosGetTest()
        {
            var testinfo = new TestInfo();
            IEnumerable<BaseInsUpdCatalogo> result = null;
            try
            {
                //result = BLLPrestamo.Instance.GetCatalogos (new CatalogoGetParams { NombreTabla = NombreTabla, IdNegocio = -1, IdTabla = IdNombreColumna });
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

            //var catalogos = BLLPrestamo.Instance.GetCatalogos(new CatalogoGetParams { NombreTabla = NombreTabla, IdTabla = IdNombreColumna }); ;

            //var ocupacionMecanico = catalogos.Where(item => item.Nombre == "Mecanico").FirstOrDefault();

            //var ocupacionABorrar = new BaseCatalogoDeleteParams { IdNombreColumna = "IdOcupacion", NombreTabla = "TblOcupaciones", IdRegistro = ocupacionMecanico.GetId(), Users = TestInfo.Users };

            //BLLPrestamo.Instance.DeleteCatalogo(ocupacionABorrar);
        }

        [TestMethod()]
        public void GetCatalogosV2Test()
        {
            var result = BLLPrestamo.Instance.GetCatalogos<TipoSexo>(CatalogoName.TipoSexo, new BaseCatalogoGetParams());
        }

        [TestMethod()]
        public void CreateSqlParamsTest()
        {

            var cataName = CatalogoName.Ocupacion;
            var result = BLLPrestamo.Instance.CreateSqlParams(CatalogoName.Ocupacion, new BaseCatalogoGetParams());
        }

        [TestMethod()]
        public void GetDivisonTerritorialComponentsTest()
        {

        }
    }
}