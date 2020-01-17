using emtSoft.DAL;
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
        public enum UserValidationResult
        {
            NoUserFound = 1,
            InvalidPassword,
            MustChangePassword,
            ExpiredPassword,
            BadEncryptedFormat,
            Blocked,
            Inactive,
            ExpiredAccount,
            Sucess,
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
                        this.Mensaje = "Usuario no encontrado";
                        break;
                    case UserValidationResult.InvalidPassword:
                        this.Mensaje ="Contraseña Invalida";
                        break;
                    case UserValidationResult.MustChangePassword:
                        this.Mensaje = "El usuario debe cambiar la contraseña";
                        break;
                    case UserValidationResult.ExpiredPassword:
                        this.Mensaje = "la contraseña ya vencio su fecha de vigencia, debe cambiarla";
                        break;
                    case UserValidationResult.BadEncryptedFormat:
                        this.Mensaje = "error formato de contraseña";
                        break;
                    case UserValidationResult.Blocked:
                        this.Mensaje = "cuenta de usuario bloqueada";
                        break;
                    case UserValidationResult.Inactive:
                        this.Mensaje ="cuenta de usuario inactiva";
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
        public IEnumerable<Usuario> GetUsuarios(UsuarioGetParams searchParam)
        {
            GetValidation(searchParam);
            return BllAcciones.GetData<Usuario, UsuarioGetParams>(searchParam, "spGetUsuarios");
        }
        public void InsUpdUsuario(Usuario insUpdParam)
        {
            var vigenteHasta = insUpdParam.VigenteHasta;
            if (vigenteHasta != InitValues._19000101 ? vigenteHasta.CompareTo(DateTime.Now) <= 0 : false)
            {
                throw new Exception("La fecha de vigencia de la cuenta no es valida debe ser mayor a la de hoy");
            }
            //var originalPassword = insUpdParam.Contraseña;
            //insUpdParam.Contraseña = DecryptPassword(originalPassword);
            //insUpdParam.LoginName = insUpdParam.LoginName.ToLower();
            BllAcciones.insUpdData<Usuario>(insUpdParam, "spInsUpdUsuario");
            //insUpdParam.Contraseña = originalPassword;
        }

        public void UsuarioChangePassword(changePassword param)
        {
            var _updParam = SearchRec.ToSqlParams(param);
            Database.AdHoc(ConexionDB.Server).ExecSelSP("spChangePassword", _updParam);
        }


        public UserValidationResultWithMessage LoginUser(Usuario usr)
        {
            var result = UsuarioValidateCredential(usr.IdNegocio, usr.LoginName, new PasswordInfo(usr.Contraseña, false));
            return result;

        }
        private string DecryptPassword(string password)
        {
            string result = password;
            try { result = RijndaelSimple.Decrypt(password); }
            catch (Exception) { }
            return result;
        }

        /// <summary>
        /// to validate a user and retrieve his states it returns if is ok the password, 
        /// if user is bloked, active, or if must change password etc.
        /// </summary>
        /// <param name="idNegocio"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserValidationResultWithMessage  UsuarioValidateCredential(int idNegocio, string loginName, PasswordInfo pwInfo)
        {
            var usuario = this.GetUsuarios(new UsuarioGetParams { IdNegocio = idNegocio, LoginName = loginName }).FirstOrDefault();
            if (usuario == null) return new UserValidationResultWithMessage(UserValidationResult.NoUserFound);
            if (usuario.Bloqueado) return new UserValidationResultWithMessage(UserValidationResult.Blocked);
            if (!usuario.Activo) return new UserValidationResultWithMessage(UserValidationResult.Inactive);
            if (pwInfo.Encrypted)
            {
                try
                {
                    RijndaelSimple.Decrypt(pwInfo.Text);
                }
                catch (Exception e)
                {
                    return new UserValidationResultWithMessage(UserValidationResult.BadEncryptedFormat);
                }
            }
            if (!pwInfo.Encrypted && usuario.Contraseña != RijndaelSimple.Encrypt(pwInfo.Text)) return new UserValidationResultWithMessage (UserValidationResult.InvalidPassword);
            if (usuario.DebeCambiarContraseñaAlIniciarSesion) return new UserValidationResultWithMessage(UserValidationResult.MustChangePassword);
            if (IsExpiredAccount(usuario.VigenteHasta)) return new UserValidationResultWithMessage(UserValidationResult.ExpiredAccount);
            if (IsExpiredPassword(usuario.ContraseñaExpiraCadaXMes, usuario.InicioVigenciaContraseña)) return new UserValidationResultWithMessage(UserValidationResult.ExpiredAccount );
            return new UserValidationResultWithMessage(UserValidationResult.Sucess);
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

        public bool IsExpiredAccount(DateTime fecha)
        {
            var result = (fecha != null && fecha != new DateTime(1900, 1, 1) && fecha < DateTime.Now);
            return result;
        }
    }
    public class PasswordInfo
    {
        public readonly string Text;
        public readonly bool Encrypted;
        public PasswordInfo(string text, bool encrypted)
        {
            this.Text = text;
            this.Encrypted = encrypted;
        }
    }
}