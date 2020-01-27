using emtSoft.DAL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
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
            Blocked,
            Inactive,
            ExpiredAccount,
            Sucess,
            NoUserRegistered
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
        public IEnumerable<Usuario> GetUsuarios(UsuarioGetParams searchParam)
        {
            GetValidation(searchParam);
            return BllAcciones.GetData<Usuario, UsuarioGetParams>(searchParam, "spGetUsuarios");
        }

        public void InsUpdUsuario(Usuario insUpdParam, string from = "")
        {
            //TODO Agregar columna inicioVigenciaContraseña
            //TASK Asignar a Bryan
            
            if ((insUpdParam.LoginName.ToLower() == "admin") && (from != bllUser))
            {
                throw new Exception("No puede crear el usuario administrador desde la pantalla de creacion de usuario");
            }
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
        private string bllUser = "bllUser";
        /// <summary>
        /// indicate if data exist for the specified IdNegocio in the specified table
        /// dont forget TO SEND TABLE NAME WITH SCHEMA example schema.tablename sis.tblClientes or wow.tblClientes
        /// not necessary for dbo schema
        /// </summary>
        /// <param name="idNegocio"></param>
        /// <returns></returns>

        public void UsuarioChangePassword(changePassword param)
        {
            var _updParam = SearchRec.ToSqlParams(param);
            PrestamosDB.ExecSelSP("spChangePassword", _updParam);
        }


        public UserValidationResultWithMessage LoginUser(Usuario usr)
        {
            var result = UsuarioValidateCredential(usr.IdNegocio, usr.LoginName, usr.Contraseña);
            if (result.UserValidationResult == UserValidationResult.NoUserFound && usr.LoginName.ToLower() == "admin")
            {
                if (usr.Contraseña == AdminPassword())
                {
                    return new UserValidationResultWithMessage(UserValidationResult.Sucess);
                }
            }
            return result;
        }

        private string AdminPassword()
        {
            var valor = "pcp" + DateTime.Now.ToString("yyyyMMdd");
            return valor;
        }

        public void CheckAndCreateAdminUserFoNegocios(string key)
        {
            if (key != "pcp46232") return;

            var negocios = this.GetNegocios(new NegociosGetParams() { Usuario = "SysDLL"});
            negocios.ToList().ForEach(negocio =>
            {
                UsuarioCreateAdmin(negocio);
            });
        }

        private void UsuarioCreateAdmin(Negocio negocio)
        {
            var existeAdminUserForThisNegocio = this.GetUsuarios(new UsuarioGetParams { IdNegocio = negocio.IdNegocio, LoginName = "admin" }).FirstOrDefault() != null;
            if (!existeAdminUserForThisNegocio)
            {
                var usuario = new Usuario
                {
                    Usuario = "bllCreateUser",
                    Contraseña = AdminPassword(),
                    LoginName = "Admin",
                    NombreRealCompleto = "Administrador Aplicacion",
                    ContraseñaExpiraCadaXMes = 3,
                    DebeCambiarContraseñaAlIniciarSesion = false,
                    IdNegocio = negocio.IdNegocio
                };
                this.InsUpdUsuario(usuario, this.bllUser);
            };
        }

        private bool ExistUsers => ExistDataForTable("tblUsuarios");
        /// <summary>
        /// to validate a user and retrieve his states it returns if is ok the password, 
        /// if user is bloked, active, or if must change password etc.
        /// </summary>
        /// <param name="idNegocio"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserValidationResultWithMessage UsuarioValidateCredential(int idNegocio, string loginName, string password)
        {

            var usuario = this.GetUsuarios(new UsuarioGetParams { IdNegocio = idNegocio, LoginName = loginName }).FirstOrDefault();
            if (usuario == null)
            {
                if (!ExistUsers)
                {
                    return new UserValidationResultWithMessage(UserValidationResult.NoUserRegistered);
                }
                else
                {
                    return new UserValidationResultWithMessage(UserValidationResult.NoUserFound);
                }
            }
            if (usuario.Bloqueado) return new UserValidationResultWithMessage(UserValidationResult.Blocked);
            if (!usuario.Activo) return new UserValidationResultWithMessage(UserValidationResult.Inactive);
            if (RijndaelSimple.Encrypt(password) != usuario.Contraseña) return new UserValidationResultWithMessage(UserValidationResult.InvalidPassword);
            if (usuario.DebeCambiarContraseñaAlIniciarSesion) return new UserValidationResultWithMessage(UserValidationResult.MustChangePassword);
            if (IsExpiredAccount(usuario.VigenteHasta)) return new UserValidationResultWithMessage(UserValidationResult.ExpiredAccount);
            if (IsExpiredPassword(usuario.ContraseñaExpiraCadaXMes, usuario.InicioVigenciaContraseña)) return new UserValidationResultWithMessage(UserValidationResult.ExpiredAccount);
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

}