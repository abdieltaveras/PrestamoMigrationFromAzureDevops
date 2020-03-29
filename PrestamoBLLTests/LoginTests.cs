using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoEntidades;
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
            result = BLLPrestamo.Instance.UsuarioValidateCredential(1, usr.LoginName, usr.Contraseña);
            Assert.IsTrue(result.UserValidationResult == UserValidationResult.Sucess, $"Se esperaba {UserValidationResult.Sucess} y se obtuvo {result.ToString()}");
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

        private static void SetUsuario(Usuario usuario)
        {
            usuario.Usuario = "testUser" + DateTime.Now.ToShortDateString();
        }

        [TestMethod()]
        public void UserValidationResult_NoUserFound()
        {

            var expected = UserValidationResult.NoUserFound;
            string loginName = DateTime.Now.ToString();
            var userValResult = BLLPrestamo.Instance.UsuarioValidateCredential(1, loginName, string.Empty);
            Assert.IsTrue(userValResult.UserValidationResult == expected, $"Se esperaba {expected} y se obtuvo {userValResult.ToString()}");
        }

        [TestMethod()]
        public void UserValidationResult_InvalidPassword()
        {
            var expected = UserValidationResult.InvalidPassword;
            var usr = UsuarioTests.NewSuccessUserInstance;
            UpdateSuccessUser(usr);
            string loginName = usr.LoginName;
            var userValResult = BLLPrestamo.Instance.UsuarioValidateCredential(1, loginName, Guid.NewGuid().ToString());
            Assert.IsTrue(userValResult.UserValidationResult == expected, $"Se esperaba {expected} y se obtuvo {userValResult.ToString()}");
        }

        /// <summary>
        /// Get the current Succes User
        /// </summary>
        /// <returns></returns>
        private Usuario GetSuccesUserCreateIfNotExist()
        {
            var usr = UsuarioTests.NewSuccessUserInstance;
            var usuario = BLLPrestamo.Instance.GetUsuarios(new UsuarioGetParams { LoginName = usr.LoginName, IdNegocio = 1 }).FirstOrDefault();
            if (usr != null)
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
            ExecuteValidation(UserValidationResult.ExpiredPassword, usr => usr.VigenteHasta = DateTime.Now.AddDays(-10));
        }
        private void ExecuteValidation(UserValidationResult expected, Action<Usuario> cambiarValor)
        {
            UpdateSuccessUser(UsuarioTests.NewSuccessUserInstance);
            var usuario = GetSuccesUserCreateIfNotExist();
            cambiarValor(usuario);
            //usuario.DebeCambiarContraseña = true;
            BLLPrestamo.Instance.InsUpdUsuario(usuario);
            var userValResult = BLLPrestamo.Instance.UsuarioValidateCredential(1, usuario.LoginName, usuario.Contraseña);
            Assert.IsTrue(userValResult.UserValidationResult == expected, $"Se esperaba {expected} y se obtuvo {userValResult.ToString()}");
        }
    }

}
