using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLLTests

{
    [TestClass()]
    public class NegocioTests
    {

        [TestMethod()]
        public void insertNegocioTest()
        {
            var negocio = NewInstanceNegocioIntagsa();
            var result = BLLPrestamo.Instance.GetNegocios(new NegociosGetParams { Codigo = negocio.Codigo });
            negocio.Codigo = (result.Count() > 0) ? Guid.NewGuid().ToString() : negocio.Codigo;
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
            var ocurrioError = false;
            //act
            try
            {
                createNegociosWithNegocioPadre();
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
                ocurrioError = true;
            }
            Assert.IsFalse(ocurrioError, mensajeError);
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
            var ocurrioError = false;
            var result = GetNegocioIntagsa(getParam);
            try
            {
                var infoInsertadoAccion = result.InsertadoPor.ToType<InfoAccion>();
            }
            catch (Exception e)
            {
                ocurrioError = true;
                mensajeError = e.Message;
            }
            Assert.IsFalse(ocurrioError, mensajeError);
        }
        [TestMethod()]
        public void GetNegociosHijosTest()
        {
            var negocioPadre = GetNegocios(GetParamNegocioIntangsa()).FirstOrDefault();
            var getParam = new NegociosGetParams { IdNegocioPadre = negocioPadre.IdNegocio, IdNegocio = -1,  };
            mensajeError = string.Empty;
            var negociosYSuHijos = BLLPrestamo.Instance.GetNegocioYSusHijos(negocioPadre.IdNegocio);
            Assert.IsTrue(negociosYSuHijos.Count()>0,$"No se encontraron hijos para el id negocio {negocioPadre.IdNegocio}");
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
            negocioAActualizar.CorreoElectronico = "intagsa@hotmail.com";
            var nombreEsperado = negocioAActualizar.NombreComercial;
            try
            {
                InsUpdNegocio(negocioAActualizar);
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }

            
            var negocioActualizado = GetNegocioIntagsa(new NegociosGetParams { IdNegocio = negocioAActualizar.IdNegocio });
            Assert.IsTrue(negocioActualizado.NombreComercial==nombreEsperado, mensajeError);
        }

        [TestMethod()]
        public void GetNegocios_PermitirOperaciones()
        {
            var getParam = new NegociosGetParams { IdNegocio = -1, PermitirOperaciones=1, Usuario=TestInfo.Usuario };
            mensajeError = "No se encontro informacion";
            var result = GetNegocios(getParam);
            Assert.IsTrue(result.Count() > 0 && result.FirstOrDefault().PermitirOperaciones, mensajeError);
        }

        [TestMethod()]
        public void GetNegociosPadres()
        {
            var result = BLLPrestamo.Instance.GetNegocioySusPadres(3);
            Assert.IsTrue(result.Count() > 0, "no se obtuvieron resultados");
        }
        [TestMethod()]
        public void GetNegociosSinNegociosPadres()
        {
            var result = BLLPrestamo.Instance.NegocioGetLosQueSonMatriz();
            Assert.IsTrue(result.Count() > 0, "no se obtuvieron resultados");
        }
        [TestMethod()]
        public void GetNegocioMatriz()
        {
            var result = BLLPrestamo.Instance.GetNegocioMatriz(12);
            Assert.IsTrue(result > 0, "no se obtuvieron resultados");
        }

        [TestMethod()]
        public void GetNegocios_NotPermitirOperaciones()
        {
            var getParam = new NegociosGetParams { IdNegocio = -1, PermitirOperaciones = 0, Usuario = TestInfo.Usuario };
            mensajeError = "No se econtro informacion";
            var result = GetNegocios(getParam);
            Assert.IsTrue(result.Count() > 0 && !result.FirstOrDefault().PermitirOperaciones, mensajeError);
        }



        [TestMethod()]
        public void InsertDuplicateCodigo_HayError_true()
        {
            var negocio = GetNegocios(GetParamNegocioIntangsa()).FirstOrDefault();
            negocio.IdNegocio = 0;
            negocio.Usuario = "NegocioTest";
            negocio.TaxIdNo = negocio.TaxIdNo + "xxx";
            mensajeError = string.Empty;
            bool ocurrioError = false;
            try
            {
                InsUpdNegocio(negocio);
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
                ocurrioError = true;
            }
            Assert.IsTrue(ocurrioError, mensajeError);
        }
        [TestMethod()]
        public void SimulateLogin_Success()
        {
            var getNegociosTruncales = BLLPrestamo.Instance.NegocioGetLosQueSonMatriz();
            var selecMatriz = getNegociosTruncales.FirstOrDefault();
            string loginName = "bryan";
            int idNegocioMatriz = selecMatriz.IdNegocio;
            string password = "1";
            var login = BLLPrestamo.Instance.LoginUser(idNegocioMatriz,loginName,password);
            Assert.IsTrue(login.ValidationMessage.UserValidationResult == UserValidationResult.Sucess, $"El loginName {loginName} para el negocio Matriz {idNegocioMatriz} con la contrasena {password} no fue exitoso revisar");
        }

        [TestMethod()]
        public void SimulateLoginAndGetNegociosHijosQuePermitanOperaciones()
        {
            var getNegociosTruncales = BLLPrestamo.Instance.NegocioGetLosQueSonMatriz();
            var selecMatriz = getNegociosTruncales.FirstOrDefault();
            string loginName = "bryan";
            int idNegocioMatriz = selecMatriz.IdNegocio;
            string password = "1";
            var login = BLLPrestamo.Instance.LoginUser(idNegocioMatriz, loginName, password);
            var negociosHijosQuePermitenOperaciones = new List<Negocio>();
            if (login.ValidationMessage.UserValidationResult == UserValidationResult.Sucess)
            {
                var idNegocio = login.Usuario.IdNegocio;
                var negociosHijos = BLLPrestamo.Instance.GetNegocioYSusHijos(idNegocio);
                negociosHijosQuePermitenOperaciones = negociosHijos.Where(neg => neg.PermitirOperaciones).ToList();
            }
        }

        [TestMethod()]
        public void SimulateLogin_NotFoundByProvidingANonValidIdNegocioMatriz()
        {
            var getNegociosTruncales = BLLPrestamo.Instance.NegocioGetLosQueSonMatriz();
            var selecMatriz = getNegociosTruncales.LastOrDefault();
            string loginName = "bryan";
            int idNegocioMatriz = selecMatriz.IdNegocio;
            string password = "1";
            var login = BLLPrestamo.Instance.LoginUser(idNegocioMatriz, loginName, password);
            Assert.IsTrue(login.ValidationMessage.UserValidationResult == UserValidationResult.NoUserFound, $"El loginName {loginName} para el negocio Matriz {idNegocioMatriz} con la contrasena {password} no fue exitoso revisar");

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
            var negocioPadre = BLLPrestamo.Instance.NegocioGetLosQueSonMatriz().FirstOrDefault();

            var negocio1 = NewInstanceNegocioIntagsa();
            var result = BLLPrestamo.Instance.GetNegocios(new NegociosGetParams { Codigo = negocio1.Codigo });
            negocio1.Codigo = "Guaricano "+Guid.NewGuid().ToString();
            negocio1.IdNegocioPadre = negocioPadre.IdNegocio;
            negocio1.NombreComercial = "Intagsa Santo Domingo";
            negocio1.PermitirOperaciones = false;

            var idNegocioSubMatrizPadre = InsUpdNegocio(negocio1);


            var negocio2 = NewInstanceNegocioIntagsa();
            negocio2.IdNegocioPadre = idNegocioSubMatrizPadre;
            negocio2.NombreComercial = "Guaricano Prestamo";
            negocio2.PermitirOperaciones = true;
            negocio2.Codigo = "GP "+Guid.NewGuid().ToString();

            InsUpdNegocio(negocio2);
            var negocio3 = NewInstanceNegocioIntagsa();
            negocio3.IdNegocioPadre = idNegocioSubMatrizPadre;
            negocio3.NombreComercial = "Guaricano Ventas";
            negocio3.Codigo = "GV "+Guid.NewGuid().ToString();
            negocio3.PermitirOperaciones = true;
            InsUpdNegocio(negocio3);
        }

        private static int InsUpdNegocio(Negocio negocio)
        {
            // attention: para retornar el id desde un stored procedure debemos usar siempre SCOPE_IDENTITY  porque devuelve el id que dentro de esa insersion fue realizado y no el id ultimo que en esa tabla se ha realizado
            return BLLPrestamo.Instance.NegocioinsUpd(negocio);
        }

        private static Negocio NewInstanceNegocioIntagsa()
        {
            var negocioInfo = new NegocioInfo { Direccion1 = "Prol. Gregorio Luperon no 12, Villa España", Direccion2 = "La Romana Rep. Dom.", Telefono1 = "809-813-1719" };
            InfoAccion infoAccion = CreateInfoUAccion();
            var negocio = new Negocio { NombreComercial = "Intagsa", NombreJuridico = "Intagsa SRL", TaxIdNo = "112108236", Codigo = "int001", OtrosDetalles = negocioInfo.ToJson(), Usuario = "TestProject", InfoAccion = infoAccion.ToJson(), Logo = "papito.png" };
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