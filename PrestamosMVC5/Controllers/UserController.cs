using PcpUtilidades;
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
        public UserController() : base()
        {
            UpdViewBag_LoadCssAndJsGrp2(true);
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }
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
            if (model != null)
            {
                TempData["Usuario"] = model.Usuario;
            }
            prepareUserModelForGet(model);
            model.RoleList = GetRoles();
            return View(model);
        }
        
        // POST: User/Create
        [HttpPost]
        public ActionResult CreateOrEdit(UserModel userModel)
        {
            try
            {
                var usuarioTempData = GetValueFromTempData<Usuario>("Usuario");
                var imagen1ClienteFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForUsuarios), userModel.image1PreviewValue);
                userModel.Usuario.ImgFilePath = GeneralUtils.GetNameForFile(imagen1ClienteFileName, userModel.image1PreviewValue, usuarioTempData.ImgFilePath);
                int userid = 0;
                Usuario usuario;
                ActionResult actionResult;
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                var result = ModelState.IsValid;
                prepareUsuarioFromModelForSave(userModel, out actionResult, out usuario);

                userid = SaveData(usuario);

                if (userModel.Usuario.IdUsuario == 0)
                {
                    SetRolesToUser(userid, userModel.SelectedRoles);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(userModel);
            }
            return RedirectToAction("index");
        }

        private void SetRolesToUser(int iduser, string[] SelectedRoles)
        {
            LoadAndChangeUserRole(SelectedRoles, iduser);
        }

        [HttpPost]
        public ActionResult UpdateRolesToUser(int iduser, string[] SelectedRoles)
        {
            if (SelectedRoles == null) return RedirectToAction("EditUserRoles");
            
            LoadAndChangeUserRole(SelectedRoles, iduser);

            return RedirectToAction("EditUserRoles");
        }

        private void LoadAndChangeUserRole(string[] SelectedRoles, int iduser)
        {
            List<UsuarioRoleIns> lista = new List<UsuarioRoleIns>();

            foreach (var role in SelectedRoles)
            {
                lista.Add(new UsuarioRoleIns() { IdUser = iduser, IdRole = int.Parse(role) });
            }

            List<UsuarioRole> ListadoDB = (List<UsuarioRole>)BLLPrestamo.Instance.UserRolesSearchAll(new BuscarUserRolesParams { IdUsuario = iduser });

            List<UsuarioRoleIns> ListaAInsertar = GetInsertRoleForUser(lista, ListadoDB, iduser);
            List<UsuarioRoleIns> ListaAAnular = GetCancelRoleForUser(lista, ListadoDB, iduser);
            List<UsuarioRoleIns> ListaAModificar = GetModifyRoleForUser(lista, ListadoDB, iduser);

            BLLPrestamo.Instance.InsUpdRoleUsuario(ListaAInsertar, ListaAAnular, ListaAModificar, pcpUserLoginName);
        }

        private List<UsuarioRoleIns> GetInsertRoleForUser(List<UsuarioRoleIns> lista, List<UsuarioRole> ListadoDB, int user)
        {
            List<UsuarioRoleIns> ListaAInsertar = new List<UsuarioRoleIns>();

            // Determinar cuales se insertan
            foreach (var item in lista)
            {
                if (!ListadoDB.Exists(element => element.IdRole == item.IdRole))
                {
                    ListaAInsertar.Add(new UsuarioRoleIns() { IdUser = user, IdRole = item.IdRole });
                }
            }
            return ListaAInsertar;
        }
        private List<UsuarioRoleIns> GetCancelRoleForUser(List<UsuarioRoleIns> lista, List<UsuarioRole> ListadoDB, int user)
        {
            List<UsuarioRoleIns> ListaAAnular = new List<UsuarioRoleIns>();

            foreach (var item in ListadoDB)
            {
                if (!lista.Exists(element => element.IdRole == item.IdRole))
                {
                    ListaAAnular.Add(new UsuarioRoleIns() { IdUser = user, IdRole = item.IdRole });
                }
            }
            return ListaAAnular;
        }
        private List<UsuarioRoleIns> GetModifyRoleForUser(List<UsuarioRoleIns> lista, List<UsuarioRole> ListadoDB, int user)
        {
            List<UsuarioRoleIns> ListaAModificar = new List<UsuarioRoleIns>();

            foreach (var item in ListadoDB)
            {
                if (lista.Exists(element => element.IdRole == item.IdRole && !item.Anulado()))
                {
                    ListaAModificar.Add(new UsuarioRoleIns() { IdUser = user, IdRole = item.IdRole });
                }
            }

            return ListaAModificar;
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
                var changeP = new ChangePassword { Contraseña = model.Contraseña, IdUsuario = model.IdUsuario };
                pcpSetUsuarioTo(changeP);
                try
                {
                    BLLPrestamo.Instance.ChangePassword(changeP);
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
            var usuarioGetParams = new UsuarioGetParams { IdNegocio = pcpUserIdNegocio };
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
                    IdUsuario = id,
                };
                this.pcpSetUsuarioAndIdNegocioTo(getUsuarioParam);
                model.Usuario = BLLPrestamo.Instance.GetUsuarios(getUsuarioParam).FirstOrDefault();
                if (model.Usuario == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontro usuario para su peticion");
                    model.Usuario = new Usuario();
                }
            }
            return model;
        }
        // internal ActionResult SaveData(ActionResult actionResult, Usuario usuario);
        private IEnumerable<Role> GetRoles()
        {
            return BLLPrestamo.Instance.GetRoles(new RoleGetParams { IdNegocio = this.pcpUserIdNegocio });
        }
        private int SaveData(Usuario usuario)
        {
            int userid = 0;

            if (ModelState.IsValid)
            {
            
                    usuario.Usuario = AuthInSession.GetLoginName();
                    userid = BLLPrestamo.Instance.InsUpdUsuario(usuario);
            }
            return userid;
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
            //TODO revisar porque el binding de la fecha en UsuarioVigente hasta y desde a bryan no le funciona bien
            //TODO: Esto se debe de borrar, Bryan lo puso asi para evitar un error en el binding aun estamos investigando si da error de nuevo quitarlo de ignorado  y resolverlo definitivamente
            //ModelState.Remove("Usuario.VigenteDesde");
            //ModelState.Remove("Usuario.VigenteHasta");
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
