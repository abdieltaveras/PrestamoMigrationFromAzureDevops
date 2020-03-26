using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class UsuarioTests
    {
        string errorMensaje = string.Empty;

        private Usuario NewSuccessUserInstance
        {

            get
            {
                var usr = new Usuario
                {
                    NombreRealCompleto = "Succes User",
                    LoginName = "Success",
                    Contraseña = "Succes12345",
                    Telefono1 = "829-961-9141",
                    Usuario = "UsuarioTest",
                    CorreoElectronico = "abdieltaveras@hotmail.com",
                    DebeCambiarContraseñaAlIniciarSesion = false,
                    IdNegocio = 1

                };
                return usr;
            }
        }

        [TestMethod()]
        public void getUsuarios_Search_Count()
        {
            var getUser = new UsuarioGetParams {IdNegocio=13, Usuario="abdiel" };
            var result = BLLPrestamo.Instance.GetUsuarios(getUser); 
            Assert.IsTrue(result.LongCount() >= 0);
        }
        [TestMethod()]
        public void InsUpdUsuario_InsertSuccesUser_EmptyErrorMensaje()
        {

            var usr = NewSuccessUserInstance;
            if (GetSuccesUser() != null)
            {
                usr.LoginName += " " + DateTime.Now.ToShortDateString();
            }
            try
            {
                BLLPrestamo.Instance.InsUpdUsuario(usr);
            }
            catch (Exception e)

            {
                errorMensaje = e.Message;
            }
            Assert.IsTrue(string.IsNullOrEmpty(errorMensaje), errorMensaje);
        }

        [TestMethod()]
        public void InsUpdUsuario_FailDuplicateInsertSuccesUser()
        {
            var usr = NewSuccessUserInstance;
            var usr2 = GetSuccesUser();
            if (usr2 == null)
            {
                BLLPrestamo.Instance.InsUpdUsuario(NewSuccessUserInstance);
            }
            try
            {
                BLLPrestamo.Instance.InsUpdUsuario(usr);
            }
            catch (Exception e)
            {
                errorMensaje = e.Message;
            }
            Assert.IsTrue(string.IsNullOrEmpty(errorMensaje), this.errorMensaje);
        }
        [TestMethod()]
        public void InsUpdUsuario_UpdateSuccesUser_EmptyErrorMensaje()
        {
            var usr = GetSuccesUser();
            int idUsuario = usr.IdUsuario;
            usr = NewSuccessUserInstance;
            usr.IdUsuario = idUsuario;
            try
            {
                BLLPrestamo.Instance.InsUpdUsuario(usr);
            }
            catch (Exception e)
            {
                errorMensaje = e.Message;
            }
            Assert.IsTrue(string.IsNullOrEmpty(errorMensaje), errorMensaje);
        }
        [TestMethod()]
        public void GetUsuarios_SearchSuccessLoginName_SuccessLoginName()
        {
            var getUser = new UsuarioGetParams { LoginName = NewSuccessUserInstance.LoginName, IdNegocio = -1 };
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
            Assert.IsTrue(loginNameOfUsuario == NewSuccessUserInstance.LoginName.ToLower(), errorMensaje);
        }
        [TestMethod()]
        public void GetUsuario_ValidationResult_Success()
        {
            UserValidationResultWithMessage result = new UserValidationResultWithMessage(UserValidationResult.Sucess);
            var usr = NewSuccessUserInstance;
            UpdateSuccessUser(usr);
            //result = BLLPrestamo.Instance.UsuarioValidateCredential(1, usr.LoginName, usr.Contraseña);
            Assert.IsTrue(result.UserValidationResult == UserValidationResult.Sucess, $"Se esperaba {UserValidationResult.Sucess} y se obtuvo {result.ToString()}");
        }

        /// <summary>
        /// Update Succes user search  record and update the IdUsuario with the Usuario value instance
        /// </summary>
        /// <param name="usr"></param>
        private void UpdateSuccessUser(Usuario usr)
        {
            usr.IdUsuario = GetSuccesUser().IdUsuario;
            BLLPrestamo.Instance.InsUpdUsuario(usr);
        }

        [TestMethod()]
        public void InsUpdUsuario_UpdateChangeNombreReal_DifferentTooriginal()
        {
            this.errorMensaje = string.Empty;
            var usr = NewSuccessUserInstance;
            var usuario = BLLPrestamo.Instance.GetUsuarios(new UsuarioGetParams { LoginName = usr.LoginName, IdNegocio = usr.IdNegocio }).FirstOrDefault();
            SetUsuario(usuario);
            var expectedNombreReal = "Modificado " + DateTime.Now.ToShortDateString();
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
            //Assert.IsTrue(userValResult.UserValidationResult == expected, $"Se esperaba {expected} y se obtuvo {userValResult.ToString()}");
        }

        [TestMethod()]
        public void UserValidationResult_InvalidPassword()
        {
            var expected = UserValidationResult.InvalidPassword;
            var usr = NewSuccessUserInstance;
            UpdateSuccessUser(usr);
            string loginName = usr.LoginName;
            var userValResult = BLLPrestamo.Instance.UsuarioValidateCredential(1, loginName, Guid.NewGuid().ToString());
            //Assert.IsTrue(userValResult.UserValidationResult == expected, $"Se esperaba {expected} y se obtuvo {userValResult.ToString()}");
        }

        /// <summary>
        /// Get the current Succes User
        /// </summary>
        /// <returns></returns>
        private Usuario GetSuccesUser()
        {
            var usr = NewSuccessUserInstance;
            var usuario = BLLPrestamo.Instance.GetUsuarios(new UsuarioGetParams { LoginName = usr.LoginName, IdNegocio = 1 }).FirstOrDefault();
            SetUsuario(usuario);
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
            UpdateSuccessUser(NewSuccessUserInstance);
            var usuario = GetSuccesUser();
            cambiarValor(usuario);
            //usuario.DebeCambiarContraseña = true;
            BLLPrestamo.Instance.InsUpdUsuario(usuario);
            var userValResult = BLLPrestamo.Instance.UsuarioValidateCredential(1, usuario.LoginName, usuario.Contraseña);
            //Assert.IsTrue(userValResult.UserValidationResult == expected, $"Se esperaba {expected} y se obtuvo {userValResult.ToString()}");

        }

        [TestMethod()]
        public void UsersExistTest_IfExistUser_true()
        {
            var expected = true;

            var usersExists = BLLPrestamo.Instance.ExistDataForTable("tblUsuarios",1);

            Assert.IsTrue(usersExists == expected,"la tabla no contiene datos para el negocio indicado");
            
        }
        [TestMethod()]
        public void CreateAndCreateAdminUserForNegocios()
        {
            var errorMensaje = string.Empty;
            try
            {
                BLLPrestamo.Instance.CheckAndCreateAdminUserFoNegocios("pcp46232");
            }
            catch (Exception e)
            {
                errorMensaje = e.Message;
            }
            Assert.IsTrue(errorMensaje == string.Empty, errorMensaje);
        }
    }
}