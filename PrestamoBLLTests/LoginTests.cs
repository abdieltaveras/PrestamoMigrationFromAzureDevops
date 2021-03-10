using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class LoginTests
    {
        string errorMensaje = string.Empty;
        string NewSuccessUserInstanceloginName =>
            UsuarioTests.NewSuccessUserInstance.LoginName;
        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            GetSuccesUserCreateIfNotExist();
        }
        [TestMethod()]
        public void GetUsuarios_SearchSuccessLoginName_SuccessLoginName()
        {
            var getUser = new UsuarioGetParams { LoginName = NewSuccessUserInstanceloginName, IdNegocio = -1 };
            string loginNameOfUsuario = string.Empty;
            try
            {
                var usuario = BLLPrestamo.Instance.GetUsuarios(getUser).FirstOrDefault();
                loginNameOfUsuario = usuario.LoginName;
            }
            catch (Exception e)
            {
                errorMensaje = e.Message;
            }
            Assert.IsTrue(loginNameOfUsuario == NewSuccessUserInstanceloginName.ToLower(), errorMensaje);
        }
        [TestMethod()]
        public void GetUsuario_ValidationResult_Success()
        {
            UserValidationResultWithMessage result = new UserValidationResultWithMessage(UserValidationResult.Sucess);
            var usr = UsuarioTests.NewSuccessUserInstance;
            UpdateSuccessUser(usr);
            BLLPrestamo.Instance.ChangePassword(new ChangePassword { IdUsuario = usr.IdUsuario, Contraseña = usr.Contraseña, Usuario = TestInfo.Usuario });
            result = BLLPrestamo.Instance.Login(usr.LoginName, usr.Contraseña,1).ValidationMessage;
            Assert.IsTrue(result.UserValidationResult == UserValidationResult.Sucess, $"Se esperaba {UserValidationResult.Sucess} y se obtuvo {result.UserValidationResult.ToString()}");
        }

        /// <summary>
        /// Update Succes user search  record and update the IdUsuario with the Usuario value instance
        /// </summary>
        /// <param name="usr"></param>
        private void UpdateSuccessUser(Usuario usr)
        {
            usr.IdUsuario = GetSuccesUserCreateIfNotExist().IdUsuario;
            BLLPrestamo.Instance.InsUpdUsuario(usr);
        }

        [TestMethod()]
        public void ChangeNombreReal_DifferentTooriginal()
        {
            this.errorMensaje = string.Empty;
            var usr = UsuarioTests.NewSuccessUserInstance;
            var usuario = BLLPrestamo.Instance.GetUsuarios(new UsuarioGetParams { LoginName = usr.LoginName, IdNegocio = usr.IdNegocio }).FirstOrDefault();
            SetUsuario(usuario);
            var expectedNombreReal = "Modificado " + DateTime.Now.ToString();
            usuario.NombreRealCompleto = expectedNombreReal;
            string currentNombreRealCompleto = string.Empty;
            try
            {
                BLLPrestamo.Instance.InsUpdUsuario(usuario);
                var usuarios = BLLPrestamo.Instance.GetUsuarios(new UsuarioGetParams { LoginName = usr.LoginName, IdNegocio = usr.IdNegocio });
                currentNombreRealCompleto = usuarios.FirstOrDefault().NombreRealCompleto;

            }
            catch (Exception e)
            {
                this.errorMensaje = e.Message;
            }
            Assert.IsTrue(currentNombreRealCompleto == expectedNombreReal, this.errorMensaje);
        }

        [TestMethod()]
        public void ChangePasword()
        {
            this.errorMensaje = string.Empty;
            var usr = UsuarioTests.NewSuccessUserInstance;
            var usuario = BLLPrestamo.Instance.GetUsuarios(new UsuarioGetParams { LoginName = usr.LoginName, IdNegocio = usr.IdNegocio }).FirstOrDefault();
            SetUsuario(usuario);
            var contraseñaExpected = "5856256";
            var changeP = new ChangePassword { Contraseña = contraseñaExpected, IdUsuario = usuario.IdUsuario, Usuario = "bllTest" };
            try
            {
                BLLPrestamo.Instance.ChangePassword(changeP);
            }
            catch (Exception e)
            {
                this.errorMensaje = e.Message;
            }
            var response  = BLLPrestamo.Instance.Login(usr.LoginName, contraseñaExpected,1);
            Assert.IsFalse(response.ValidationMessage.UserValidationResult == UserValidationResult.InvalidPassword, this.errorMensaje);
        }
        private static void SetUsuario(Usuario usuario)
        {
            usuario.Usuario = "testUser" + DateTime.Now.ToShortDateString();
        }

        [TestMethod()]
        public void CheckIfUserBryanBelongsToNegocioMatrizId()
        {

            var expected = UserValidationResult.NoUserFound;
            string loginName = "bryan";
            var search = new UsuarioGetParams { LoginName = loginName, IdNegocio=-1 };
            var Result = BLLPrestamo.Instance.GetUsuarios(search);
            var idNegocio = Result.FirstOrDefault().IdNegocio;
            if (Result.Count() > 0)
            {
                var negocioMatriz =  BLLPrestamo.Instance.GetNegocioMatriz(idNegocio);
                var loginResultSuccess = BLLPrestamo.Instance.Login(new Usuario { LoginName="bryan", IdNegocio=negocioMatriz, Contraseña="1"});
                var loginResultNotFound = BLLPrestamo.Instance.Login(new Usuario { LoginName = "bryan", IdNegocio = 9, Contraseña = "1" });
            }
        }

        [TestMethod()]
        public void UserValidationResult_NoUserFound()
        {
            var expected = UserValidationResult.NoUserFound;
            string loginName = DateTime.Now.ToString();
            var userValResult = BLLPrestamo.Instance.Login(loginName, string.Empty,1);
            Assert.IsTrue(userValResult.ValidationMessage.UserValidationResult == expected, $"Se esperaba {expected} y se obtuvo {userValResult.ToString()}");
        }

        [TestMethod()]
        public void UserValidationResult_InvalidPassword()
        {
            var expected = UserValidationResult.InvalidPassword;
            var usr = UsuarioTests.NewSuccessUserInstance;
            UpdateSuccessUser(usr);
            string loginName = usr.LoginName;
            var userValResult = BLLPrestamo.Instance.Login(loginName, Guid.NewGuid().ToString(),1);
            Assert.IsTrue(userValResult.ValidationMessage.UserValidationResult == expected, $"Se esperaba {expected} y se obtuvo {userValResult.ToString()}");
        }

        /// <summary>
        /// Get the current Succes User
        /// </summary>
        /// <returns></returns>
        private static Usuario GetSuccesUserCreateIfNotExist()
        {
            var usr = UsuarioTests.NewSuccessUserInstance;
            var usuario = BLLPrestamo.Instance.GetUsuarios(
                new UsuarioGetParams { LoginName = usr.LoginName, IdNegocio = 1 })
                .FirstOrDefault();
            if (usuario != null)
            {
                SetUsuario(usuario);
            }
            else
            {
                BLLPrestamo.Instance.InsUpdUsuario(usr);
            }
            return usuario;
        }

        [TestMethod()]
        public void UserValidationResult_Blocked()
        {
            ExecuteValidation(UserValidationResult.Blocked, usr => usr.Bloqueado = true);
        }
        [TestMethod()]
        public void UserValidationResult_MustChangePassword()
        {
            ExecuteValidation(UserValidationResult.MustChangePassword, usr => usr.DebeCambiarContraseñaAlIniciarSesion = true);
        }

        [TestMethod()]
        public void UserValidationResultNotActive()
        {
            ExecuteValidation(UserValidationResult.Inactive, usr => usr.Activo = false);
        }
        [TestMethod()]
        public void UserValidationResPasswordExpired()
        {
            ExecuteValidation(UserValidationResult.ExpiredPassword, usr => { usr.InicioVigenciaContraseña = DateTime.Now.AddDays(-60); usr.ContraseñaExpiraCadaXMes = 1; });
        }

        [TestMethod()]
        
        private void ExecuteValidation(UserValidationResult expected, Action<Usuario> cambiarValor)
        {
            UpdateSuccessUser(UsuarioTests.NewSuccessUserInstance);
            var usuario = GetSuccesUserCreateIfNotExist();
            cambiarValor(usuario);
            //usuario.DebeCambiarContraseña = true;
            BLLPrestamo.Instance.InsUpdUsuario(usuario);
            var userValResult = BLLPrestamo.Instance.Login(usuario.LoginName, UsuarioTests.NewSuccessUserInstance.Contraseña,1);
            Assert.IsTrue(userValResult.ValidationMessage.UserValidationResult == expected, $"Se esperaba {expected} y se obtuvo {userValResult.ValidationMessage.UserValidationResult.ToString()}");
        }


    }

}
