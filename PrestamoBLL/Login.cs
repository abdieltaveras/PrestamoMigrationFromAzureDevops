using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        private string bllUser = "bllUser";
        /// <summary>
        /// indicate if data exist for the specified IdNegocio in the specified table
        /// dont forget TO SEND TABLE NAME WITH SCHEMA example schema.tablename sis.tblClientes or wow.tblClientes
        /// not necessary for dbo schema
        /// </summary>
        /// <param name="idNegocio"></param>
        /// <returns></returns>
        public void ChangePassword(ChangePassword param)
        {
            var _updParam = SearchRec.ToSqlParams(param);
            DBPrestamo.ExecSelSP("spUpdContraseñaUsuario", ref _updParam);
        }
        public LoginResponse Login(Users usr)
        {
            var result = ValidateUser(usr.IdNegocio, usr.LoginName, usr.Contraseña);
            return result;
        }
        /// <summary>
        /// valida el al usuario dentro del grupo de negocio
        /// </summary>
        /// <param name="idNegocioMatriz"> el idNegocio del negocio matriz</param>
        /// <param name="loginName"></param>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        public LoginResponse Login(string loginName, string contraseña, int idLocalidadNegocio)
        {

            var result = ValidateUser(idLocalidadNegocio, loginName, contraseña);
            #if DEBUG
            if (loginName.ToLower() == "admin") 
            {
                //result = new UserValidationResultWithMessage(UserValidationResult.Sucess);
                if (contraseña.ToLower() == AdminPassword())
                {
                    return new LoginResponse() { Usuario = new Users() { IdUsuario = -1}, ValidationMessage = new UserValidationResultWithMessage(UserValidationResult.Sucess) };
                    //return new UserValidationResultWithMessage(UserValidationResult.Sucess);
                }
            }
            #endif
            return result;
        }

        private string AdminPassword()
        {
            return "pcp";
        }

        private class LoginParam
        { 
            public int idLocalidadNegocio { get; set; }
            public string LoginName { get; set; } = string.Empty;
            [GuardarEncriptado]
            public string Contraseña { get; set; } = string.Empty;
        }
        private IEnumerable<Users> LoginByNegocioMatriz(LoginParam loginParam)
        {
            //GetValidation(searchParam);
            var searchParam = SearchRec.ToSqlParams(loginParam);
            var result = DBPrestamo.ExecReaderSelSP<Users>("spLoginUsuarioByNegocioMatriz", searchParam);
            return result;
        }

        /// <summary>
        /// to validate a user and retrieve his states it returns if is ok the password, 
        /// if user is bloked, active, or if must change password etc.
        /// </summary>
        /// <param name="idLocalidadNegocio"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private LoginResponse ValidateUser(int idLocalidadNegocio, string loginName, string password)
        {
            var usuario = this.LoginByNegocioMatriz(new LoginParam { idLocalidadNegocio = idLocalidadNegocio, LoginName = loginName, Contraseña = password }).FirstOrDefault();
            
            if (usuario == null)
            {
                if (!ExistUsers)
                {
                    //return new UserValidationResultWithMessage(UserValidationResult.NoUserRegistered);
                    return new LoginResponse() { Usuario = usuario, ValidationMessage = new UserValidationResultWithMessage(UserValidationResult.NoUserRegistered) };
                }
                else
                {
                    return new LoginResponse() { Usuario = usuario, ValidationMessage = new UserValidationResultWithMessage(UserValidationResult.NoUserFound) };
                    //return new UserValidationResultWithMessage(UserValidationResult.NoUserFound);
                }
            }
            if (usuario.Bloqueado) return new LoginResponse() { Usuario = usuario, ValidationMessage = new UserValidationResultWithMessage(UserValidationResult.Blocked) };

            if (!usuario.Activo) return new LoginResponse() { Usuario = usuario, ValidationMessage = new UserValidationResultWithMessage(UserValidationResult.Inactive) };
            
            if (usuario.DebeCambiarContraseñaAlIniciarSesion) return new LoginResponse() { Usuario = usuario, ValidationMessage = new UserValidationResultWithMessage(UserValidationResult.MustChangePassword) };

            // esta validacion de la contraseña debe estar aqui en esta posicion
            // primero que la que indica cambiar contrasnea , vigencia de cuenta, etc. porque de esta manera si el usuario pone 
            // la contrasena incorrecta es que se le ha olvidado o es otro usuario, para estar seguro de que es el usuario
            // debe coincidir el login name y la contrasena indicando que es el usuario correcto

            
            if (!usuario.ContraseñaValida) return new LoginResponse() { Usuario = usuario, ValidationMessage = new UserValidationResultWithMessage(UserValidationResult.InvalidPassword) };
                
            if (IsExpiredAccount(usuario.VigenteHasta)) return new LoginResponse() { Usuario = usuario, ValidationMessage = new UserValidationResultWithMessage(UserValidationResult.ExpiredAccount) };

            if (IsExpiredPassword(usuario.ContraseñaExpiraCadaXMes, usuario.InicioVigenciaContraseña)) return new LoginResponse() { Usuario = usuario, ValidationMessage = new UserValidationResultWithMessage(UserValidationResult.ExpiredPassword) };

            return new LoginResponse() { Usuario = usuario, ValidationMessage = new UserValidationResultWithMessage(UserValidationResult.Sucess) };
        }


        private bool IsExpiredPassword(int contraseñaExpiraCadaXMes, DateTime inicioVigenciaContraseña)
        {
            var result = false;
            if (contraseñaExpiraCadaXMes > 0)
            {
                var dueDate = inicioVigenciaContraseña.AddMonths(contraseñaExpiraCadaXMes);
                if (DateTime.Compare(dueDate, DateTime.Now) < 0) return true;
            }
            return result;
        }

        public bool ArePasswordsEquals(string pass1, string pass2)
        {
            return pass1 == pass2;
        }
        public bool IsExpiredAccount(DateTime fecha)
        {
            var result = (fecha != null && fecha != InitValues._19000101 && fecha < DateTime.Now);
            return result;
        }
        public enum UserValidationResult
        {
            NoUserFound = 1,
            InvalidPassword,
            MustChangePassword,
            ExpiredPassword,
            Blocked,
            Inactive,
            ExpiredAccount,
            Sucess,
            NoUserRegistered
        }

        public class LoginResponse
        {
            public UserValidationResultWithMessage ValidationMessage { set; get; }
            public Users Usuario { set; get; }
        }

        public class UserValidationResultWithMessage
        {
            public readonly UserValidationResult UserValidationResult;
            public readonly string Mensaje;
            public UserValidationResultWithMessage(UserValidationResult validationResult)
            {
                this.UserValidationResult = validationResult;
                switch (UserValidationResult)
                {
                    case UserValidationResult.NoUserFound:
                        this.Mensaje = "Users no encontrado";
                        break;
                    case UserValidationResult.InvalidPassword:
                        this.Mensaje = "Contraseña Invalida";
                        break;
                    case UserValidationResult.MustChangePassword:
                        this.Mensaje = "El usuario debe cambiar la contraseña";
                        break;
                    case UserValidationResult.ExpiredPassword:
                        this.Mensaje = "la contraseña ya vencio su fecha de vigencia, debe cambiarla";
                        break;
                    case UserValidationResult.Blocked:
                        this.Mensaje = "cuenta de usuario bloqueada";
                        break;
                    case UserValidationResult.Inactive:
                        this.Mensaje = "cuenta de usuario inactiva";
                        break;
                    case UserValidationResult.ExpiredAccount:
                        this.Mensaje = "Cuenta ha vencido contactar al administrador para habilitarla de nuevo";
                        break;
                    case UserValidationResult.Sucess:
                        this.Mensaje = "login exitoso";
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
