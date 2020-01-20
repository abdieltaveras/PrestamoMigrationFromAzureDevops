using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests

{
    [TestClass()]
    public class NegocioTests
    {

        [TestMethod()]
        public void insertNegocioTest()
        {
            var negocio = NewInstanceNegocioIntagsa();

            //act
            try
            {
                InsUpdNegocio(negocio);
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }

        [TestMethod()]
        public void insertUpdNegocioWithIdNegocioPadre()
        {

            mensajeError = string.Empty;
            //act
            try
            {
                createNegociosWithNegocioPadre();
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }

        [TestMethod()]
        public void GetNegociosTest()
        {
            
            mensajeError = string.Empty;
            var result = GetNegocios(new NegociosGetParams { Usuario="testNegocio" }).FirstOrDefault();
            Assert.IsTrue(result!=null, "no existen negocios en la tabla");
        }

        [TestMethod()]
        public void GetInfoAccionTest()
        {
            var infoAccionExpected = CreateInfoUAccion();
            var getParam = GetParamNegocioIntangsa();
            mensajeError = string.Empty;
            var result = GetNegocioIntagsa(getParam);
            var infoInsertadoAccion = result.InsertadoPor.ToType<InfoAccion>();
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }
        [TestMethod()]
        public void GetNegociosHijosTest()
        {
            var negocioPadre = GetNegocios(GetParamNegocioIntangsa()).FirstOrDefault();
            var getParam = new NegociosGetParams { IdNegocioPadre = negocioPadre.IdNegocio, IdNegocio = -1 };
            mensajeError = string.Empty;
            var result = GetNegocios(getParam).FirstOrDefault();
            var mashijos = GetNegocios(new NegociosGetParams {IdNegocioPadre=result.IdNegocio, IdNegocio=-1});
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }

        [TestMethod()]
        public void UpdateNegocio()
        {
            var getParam = GetParamNegocioIntangsa();
            mensajeError = string.Empty;
            var negocioAActualizar = GetNegocioIntagsa(getParam);
            negocioAActualizar.NombreComercial+= "Modificado";
            negocioAActualizar.Usuario = "NegocioTest";
            negocioAActualizar.InfoAccion = CreateInfoUAccion().ToJson();
            var nombreEsperado = negocioAActualizar.NombreComercial;
            InsUpdNegocio(negocioAActualizar);
            var negocioActualizado = GetNegocioIntagsa(new NegociosGetParams { IdNegocio = negocioAActualizar.IdNegocio });
            Assert.IsTrue(negocioActualizado.NombreComercial==nombreEsperado, mensajeError);
        }

        [TestMethod()]
        public void FailInsertDuplicateCodigo()
        {
            var negocio = GetNegocios(GetParamNegocioIntangsa()).FirstOrDefault();
            negocio.IdNegocio = 0;
            negocio.Usuario = "NegocioTest";
            negocio.TaxIdNo = negocio.TaxIdNo + "xxx";
            mensajeError = string.Empty;
            try
            {
                InsUpdNegocio(negocio);
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
                Assert.Fail(mensajeError);
            }
            Assert.IsTrue(string.IsNullOrEmpty(mensajeError));
        }
        [TestMethod()]
        public void ExistDataForTableTest()
        {
            var result = BLLPrestamo.Instance.ExistDataForTable("tblNegocios");
            var result2 = BLLPrestamo.Instance.ExistDataForTable("tblTiposMora");
        }

        [TestMethod()]
        public void NegocioCreateIfNotExistTest_fail()
        {
            var result = BLLPrestamo.Instance.NegocioCreateIfNotExist("");
            Assert.IsTrue(result <= 0, "Creo el negocio pero no debio ser asi po run fallo en una seguridad del parametro key que se le deben enviar un valor correcto");
        }
        [TestMethod()]
        public void NegocioCreateIfNotExistTest_fail_ifThereIsNegocios()
        {
            var negocios = BLLPrestamo.Instance.GetNegocios(new NegociosGetParams());
            var result = BLLPrestamo.Instance.NegocioCreateIfNotExist("pcp46232");
            Assert.IsTrue(result > 0, "Fallo la creacion de negocios porque existen ya negocios registrados en la tabla");
        }

        #region notTestMember
        string mensajeError = string.Empty;
        private static void createNegociosWithNegocioPadre()
        {
            var negocioPadre = GetNegocios(GetParamNegocioIntangsa()).FirstOrDefault();

            var negocio1 = NewInstanceNegocioIntagsa();
            negocio1.IdNegocioPadre = negocioPadre.IdNegocio;
            negocio1.NombreComercial = "Intagsa La Romana";
            negocio1.Codigo = "IntLR00";
            //InsUpdNegocio(negocio1);
            negocioPadre = GetNegocios(new NegociosGetParams { Codigo = negocio1.Codigo, IdNegocio = -1 }).FirstOrDefault();
            var negocio2 = NewInstanceNegocioIntagsa();
            negocio2.IdNegocioPadre = negocioPadre.IdNegocio;
            negocio2.NombreComercial = "Intagsa La Romana Prestamos";
            negocio2.Codigo = "IntLR00-01";
            InsUpdNegocio(negocio2);
            var negocio3 = NewInstanceNegocioIntagsa();
            negocio3.IdNegocioPadre = negocioPadre.IdNegocio;
            negocio3.NombreComercial = "Intagsa La Romana Ventas";
            negocio3.Codigo = "IntLR00-02";
            InsUpdNegocio(negocio3);
        }

        private static void InsUpdNegocio(Negocio negocio)
        {
            BLLPrestamo.Instance.insUpdNegocio(negocio);
        }

        private static Negocio NewInstanceNegocioIntagsa()
        {
            var negocioInfo = new NegocioInfo { CorreoElectronico = "intagsa@hotmail.com", Direccion1 = "Prol. Gregorio Luperon no 12, Villa España", Direccion2 = "La Romana Rep. Dom.", Telefono1 = "809-813-1719" };
            InfoAccion infoAccion = CreateInfoUAccion();
            var negocio = new Negocio { NombreComercial = "Intagsa", NombreJuridico = "Intagsa SRL", TaxIdNo = "112108236", Codigo = "int001", OtrosDetalles = negocioInfo.ToJson(), Usuario = "TestProject", InfoAccion = infoAccion.ToJson() };
            return negocio;
        }

        private static InfoAccion CreateInfoUAccion()
        {
            return new InfoAccion
            {
                IdAplicacion = 1,
                idNegocio = 1,
                IdUsuario = 0,
                Usuario = "NegocioTest"
            };
        }

        private Negocio GetNegocioIntagsa(NegociosGetParams getParam)
        {
            IEnumerable<Negocio> negocios = new List<Negocio>();
            
            int intento = 0;
            do
            {
                intento++;
                try
                {
                    negocios = GetNegocios(getParam);
                }
                catch (Exception e)
                {
                    mensajeError = e.Message;
                }
                if (intento == 1 && negocios.Count() == 0)
                {
                    InsUpdNegocio(NewInstanceNegocioIntagsa());
                }
            } while ((intento == 1) || (negocios.Count() == 0));

            return negocios.FirstOrDefault();
        }

        private static IEnumerable<Negocio> GetNegocios(NegociosGetParams buscar)
        {
            
            return BLLPrestamo.Instance.GetNegocios(buscar);
        }

        private static NegociosGetParams GetParamNegocioIntangsa()=> new NegociosGetParams { NombreComercial = "Intagsa", IdNegocio = -1 };
        #endregion notTestMember
    }
}