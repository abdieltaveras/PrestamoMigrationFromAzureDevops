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
            PasswordExpired,
            BadEncryptedFormat,
            Blocked,
            Inactive,
            Sucess,
        }


        public IEnumerable<Usuario> GetUsuarios(UsuarioGetParams searchParam)
        {
            GetValidation(searchParam);
            return BllAcciones.GetData<Usuario, UsuarioGetParams>(searchParam, "spGetUsuarios");
        }
        public void InsUpdUsuario(Usuario insUpdParam)
        {
            var originalPassword = insUpdParam.Contraseña;
            insUpdParam.Contraseña = DecryptPassword(originalPassword);
            insUpdParam.LoginName = insUpdParam.LoginName.ToLower();
            BllAcciones.insUpdData<Usuario>(insUpdParam, "spInsUpdUsuario");
            insUpdParam.Contraseña = originalPassword;
        }

        
        private string  DecryptPassword(string password)
        {
            string result = password;
            try      { result = RijndaelSimple.Decrypt(password);  }
            catch (Exception)   { }
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
        public UserValidationResult UsuarioValidateCredential(int idNegocio, string loginName, PasswordInfo pwInfo)
        {
            var usuario = this.GetUsuarios(new UsuarioGetParams { IdNegocio = idNegocio, LoginName = loginName }).FirstOrDefault();
            if (usuario == null) return UserValidationResult.NoUserFound;
            if (usuario.Bloqueado) return UserValidationResult.Blocked;
            if (!usuario.Activo) return UserValidationResult.Inactive;
            if (pwInfo.Encrypted)
            {
                try
                {
                    RijndaelSimple.Decrypt(pwInfo.Text);
                }
                catch (Exception e)
                {
                    return UserValidationResult.BadEncryptedFormat;
                }
            }
            if (!pwInfo.Encrypted && usuario.Contraseña != RijndaelSimple.Encrypt(pwInfo.Text)) return UserValidationResult.InvalidPassword;
            if (usuario.DebeCambiarContraseñaAlIniciarSesion) return UserValidationResult.MustChangePassword;
            if (IsPasswordExpired(usuario.UsuarioValidoHasta)) return UserValidationResult.PasswordExpired;
            return UserValidationResult.Sucess;
        }

        public bool IsPasswordExpired(DateTime fecha)
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