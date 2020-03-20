﻿using PrestamoBLL;
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

        private IEnumerable<Usuario> GetUsers()
        {
            var usuarioGetParams = new UsuarioGetParams();
            this.pcpSetUsuarioAndIdNegocioTo(usuarioGetParams);
            var usuarios = BLLPrestamo.Instance.GetUsuarios(usuarioGetParams);
            return usuarios;
        }

        public ActionResult Test(int id = -1, bool showAdvancedView = true)
        {
            var model = GetUserAndSetItToModel(id);
            model.ShowAdvancedOptions = showAdvancedView;
            prepareUserModelForGet(model);
            defaultTestNewModel(id, model);
            model.ForActivo = true;
            model.Usuario.VigenteDesde = DateTime.Now.AddDays(-10);
            return View("CreateOrEdit", model);
        }

        private void defaultTestNewModel(int id, UserModel model)
        {
            if (id <= 0)
            {
                model.Usuario.NombreRealCompleto = "nombre real";
                model.Usuario.LoginName = "loginname";
                model.Usuario.Telefono1 = "8095508455";
                model.Usuario.Activo = false;
                model.Usuario.Bloqueado = true;

            }
        }

        [HttpPost]
        public ActionResult Test(UserModel userModel)
        {
            var usuario = userModel.Usuario;
            var dyna = new
            {
                cambiContrInicioSesion = usuario.DebeCambiarContraseñaAlIniciarSesion,
                _cambsesion = userModel.ForCambiarContraseñaAlIniciarSesion,
                usuarioactivo = usuario.Activo,
                _activo = userModel.ForActivo,
                bloqueado = usuario.Bloqueado,
                _bloqueado = userModel.ForBloqueado,
                contraseñaExpira = userModel.LaContraseñaExpira,
                limitarVigenciaDeCuenta = userModel.LimitarVigenciaDeCuenta
            };
            var dyna2 = new
            {
                expiraCadaXMes = userModel.ContraseñaExpiraCadaXMes
            };
            //return Content(dyna.ToJson());
            ModelState.AddModelError("", "probando quitar mensaje error");
            return View(userModel);
        }

        //[AuthorizeUser]
        // GET: User/Create
        public ActionResult CreateOrEdit(int id = -1, bool showAdvancedView = true)
        {
            var model = GetUserAndSetItToModel(id);
            prepareUserModelForGet(model);
            model.RoleList = GetRoles();
            return View(model);
        }
        
        // POST: User/Create
        [HttpPost]
        public ActionResult CreateOrEdit(UserModel userModel)
        {
            int userid = 0;
            Usuario usuario;
            ActionResult actionResult;
            prepareUsuarioFromModelForSave(userModel, out actionResult, out usuario);
            userid = SaveData( usuario);
            SetRolesToUser(userid, userModel.SelectedRoles);
            return RedirectToAction("index");
        }

        private void SetRolesToUser(int iduser, string[] selectedRoles)
        {
            int cont = 0;
            string roles = string.Empty;
            foreach (var role in selectedRoles)
            {
                cont++;
                roles += "(" + iduser + "," + role + ")" + ((selectedRoles.Length != cont) ? "," : "");
            }

            UserRoleInsUpdParams parametros = new UserRoleInsUpdParams()
            {
                IdUser = iduser,
                Values = roles
            };

            BLLPrestamo.Instance.InsUpdRoleUsuario(parametros);
        }

        [HttpPost]
        public ActionResult UpdateRolesToUser(int iduser, string[] SelectedRoles)
        {
            if (SelectedRoles == null) return RedirectToAction("EditUserRoles");
            int cont = 0;
            string roles = string.Empty;
            foreach (var role in SelectedRoles)
            {
                cont++;
                roles += "(" + iduser + "," + role + ")" + ((SelectedRoles.Length != cont) ? "," : "");
            }

            UserRoleInsUpdParams parametros = new UserRoleInsUpdParams()
            {
                IdUser = iduser,
                Values = roles
            };

            BLLPrestamo.Instance.InsUpdRoleUsuario(parametros);

            return RedirectToAction("EditUserRoles");
        }

        public ActionResult EditUserRoles()
        {
            RoleVM model = new RoleVM()
            {
                Usuarios = GetUsers(),
                ListaRoles = GetRoles(),
            };

            return View(model);
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
        private UserModel GetUserAndSetItToModel(int id)
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
        private IEnumerable<Role> GetRoles()
        {
            return BLLPrestamo.Instance.RolesGet(new RoleGetParams { IdNegocio = this.pcpUserIdNegocio });
        }
        private int SaveData(Usuario usuario)
        {
            int userid = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    usuario.Usuario = AuthInSession.GetLoginName();
                    userid = BLLPrestamo.Instance.InsUpdUsuario(usuario);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return userid;
        }

        public void prepareUsuarioFromModelForSave(UserModel userModel, out ActionResult actionResult, out Usuario usuario)
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

        private  Usuario SetUsuarioFromUserModel(UserModel userModel)
        {
            Usuario usuario = userModel.Usuario;
            usuario.Contraseña = userModel.Contraseña;
            usuario.Activo = userModel.ForActivo;
            usuario.Bloqueado = userModel.ForBloqueado;
            usuario.DebeCambiarContraseñaAlIniciarSesion = userModel.ForCambiarContraseñaAlIniciarSesion;
            usuario.ContraseñaExpiraCadaXMes = userModel.LaContraseñaExpira ?
                                   userModel.ContraseñaExpiraCadaXMes : -1;
            usuario.VigenteHasta = userModel.LimitarVigenciaDeCuenta ?
                                             usuario.VigenteHasta : InitValues._19000101;
            return usuario;
        }

        private void prepareUserModelForGet(UserModel model)
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
