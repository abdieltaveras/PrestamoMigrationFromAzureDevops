using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    [AuthorizeUser]
    public class UserController : ControllerBasePcp
    {
        #region Request
        // GET: User
        public ActionResult Index()
        {
            IEnumerable<Usuario> usuarios = GetUsers();
            ActionResult actResult = View(usuarios);
            return actResult;
        }
        //[AuthorizeUser]
        // GET: User/Create
        public ActionResult CreateOrEdit(int id = -1, bool showAdvancedView = true)
        {
            var model = GetUserAndSetItToModel(id);
            prepareUserModelForGet(model);
            return View(model);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult CreateOrEdit(UserModel userModel)
        {
            ActionResult actionResult;
            Usuario usuario;
            prepareUsuarioFromModelForSave(userModel, out actionResult, out usuario);
            actionResult = SaveData(actionResult, usuario);
            return actionResult;
        }
        
        public ActionResult ChangePassword(int id = -1)
        {
            var getUsuarioParam = new UsuarioGetParams
            {
                IdUsuario = id,
            };
            this.pcpSetUsuarioTo(getUsuarioParam);
            var model = new ChangePasswordModel();
            var usr = BLLPrestamo.Instance.GetUsuarios(getUsuarioParam).FirstOrDefault();
            if (usr != null)
            {
                model.IdUsuario = usr.IdUsuario;
                model.LoginName = usr.LoginName;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "El usuario indicado no existe");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            ActionResult actResult = View(model); 
            if (!ModelState.IsValid)

            {
                ModelState.AddModelError(string.Empty, "Favor revisar hay errores en el formulario");
            }
            else
            {
                var changeP = new changePassword { Contraseña = model.Contraseña, IdUsuario = model.IdUsuario };
                pcpSetUsuarioTo(changeP);
                try
                {
                    BLLPrestamo.Instance.UsuarioChangePassword(changeP);
                    actResult = RedirectToAction("index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrio un error no se pudo cambiar la contraseña");
                }
            }
            return actResult;
        }
        #endregion Request
        #region Operations
        internal IEnumerable<Usuario> GetUsers()
        {
            var usuarioGetParams = new UsuarioGetParams();
            this.pcpSetUsuarioAndIdNegocioTo(usuarioGetParams);
            var usuarios = BLLPrestamo.Instance.GetUsuarios(usuarioGetParams);
            return usuarios;
        }
        internal UserModel GetUserAndSetItToModel(int id)
        {
            var model = new UserModel();
            model.Usuario = new Usuario();
            if (id > 0)
            {
                var getUsuarioParam = new UsuarioGetParams
                {
                    Usuario = AuthInSession.GetLoginName(),
                    IdUsuario = id,
                };
                model.Usuario = BLLPrestamo.Instance.GetUsuarios(getUsuarioParam).FirstOrDefault();
                if (model.Usuario == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontro usuario para su peticion");
                    model.Usuario = new Usuario();
                }
            }
            return model;
        }
        internal ActionResult SaveData(ActionResult actionResult, Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.Usuario = AuthInSession.GetLoginName();
                    BLLPrestamo.Instance.InsUpdUsuario(usuario);
                    actionResult = RedirectToAction("index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return actionResult;
        }

        protected void prepareUsuarioFromModelForSave(UserModel userModel, out ActionResult actionResult, out Usuario usuario)
        {
            actionResult = View(userModel);
            usuario = SetUsuarioFromUserModel(userModel);
            this.pcpSetUsuarioAndIdNegocioTo(usuario);
            if (usuario.DebeCambiarContraseñaAlIniciarSesion || usuario.IdUsuario > 0)
            {
                ModelState.Remove("Contraseña");
                ModelState.Remove("ConfirmarContraseña");
                usuario.Contraseña = string.Empty;
            }
        }

        protected  Usuario SetUsuarioFromUserModel(UserModel userModel)
        {
            Usuario usuario = userModel.Usuario;
            usuario.Contraseña = userModel.Contraseña;
            //usuario.Activo = userModel.ForActivo;
            //usuario.Bloqueado = userModel.ForBloqueado;
            //usuario.DebeCambiarContraseñaAlIniciarSesion = userModel.ForCambiarContraseñaAlIniciarSesion;
            usuario.ContraseñaExpiraCadaXMes = userModel.LaContraseñaExpira ?
                                   userModel.ContraseñaExpiraCadaXMes : -1;
            usuario.VigenteHasta = userModel.LimitarVigenciaDeCuenta ?
                                             usuario.VigenteHasta : InitValues._19000101;
            return usuario;
        }

        internal void prepareUserModelForGet(UserModel model)
        {
            var usuario = model.Usuario;
            model.LimitarVigenciaDeCuenta = (usuario.VigenteHasta != InitValues._19000101);
            var dateAreEquals = DateTime.Compare(usuario.VigenteHasta, InitValues._19000101) == 0;
            usuario.VigenteDesde = dateAreEquals ? DateTime.Now : usuario.VigenteDesde;
            usuario.VigenteHasta = dateAreEquals ? DateTime.Now : usuario.VigenteHasta;
            usuario.Usuario = AuthInSession.GetLoginName();
            model.LaContraseñaExpira = usuario.LaContrasenaExpira();
            model.ContraseñaExpiraCadaXMes = model.LaContraseñaExpira ? usuario.ContraseñaExpiraCadaXMes : 1;
            model.Usuario = usuario;
            this.pcpSetUsuarioAndIdNegocioTo(model.Usuario);
            model.ShowAdvancedOptions = true;

        }
        #endregion Operations
    }
}
