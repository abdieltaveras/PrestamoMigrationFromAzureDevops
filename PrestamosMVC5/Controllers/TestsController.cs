using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace PrestamosMVC5.Controllers
{
    //Controller to make Test

    public class TestsController : Controller
    {
        // todo: para agregar seguridad he pensado al hacer el primer request este enviara un _requesVerificationToken
        // enviara en tempdata una informacion adicional FirsRequestToken, luego al autenticarse el usuario
        // guardara en la session el equipo y tambien el FirstRequestToken el cual tendra una duracion de 24 horas.
        // si se observa que del mismo equipo se ha creado otra session, con el mismo o diferente usuario, pero con un first
        // requesttoken, ahora bien si el mismo usuario no puede loguearse en 2 equipos a la vez, para el area de caja (cobros en efectivo), entonces el sistema debe detectar esto y sacar la otra session e impedir que ese otro token pueda operar.


        // todo check when the same user id is trying to log in  on multiple devices https://stackoverflow.com/questions/15903574/when-the-same-user-id-is-trying-to-log-in-on-multiple-devices-how-do-i-kill-the

        // GET: Tests
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckBoxes()
        {
            var model = new TestCheckBox();
            model.Bloqueado = false;
            return View(model);
        }

        public ActionResult RegistrarEquipo()
        {
            RegistroEquipo.Registrar(this.Response, RegistroEquipo.getValue, 1);
            return Content("el equipo fue registrado");
        }

        public ActionResult EstaElEquipoRegistrado()
        {
            if (RegistroEquipo.EstaRegistrado(this.Request))
            {
                return Content("Este equipo ya ha sido registrado");
            }
            else
            {
                return Content("Este equipo aun no ha sido registrado");
            }
        }

        public ActionResult Desvincular()
        {
            RegistroEquipo.DesvincularEquipo(this.Request.RequestContext.HttpContext);
            return Content("Desvinculado");
        }
        [HttpPost]
        public ActionResult CheckBoxes(TestCheckBox model)
        {
            return Content(model.ToJson());
        }

        public ActionResult Imagen()
        {
            var infoImg = new InfoConImagen();
            infoImg.ImagesForCliente = new List<HttpPostedFileBase>();
            return View(new InfoConImagen());
        }
        [HttpPost]
        public ActionResult Imagen(InfoConImagen infoConImagen)
        {
            ActionResult result = null;
            var files = Utils.SaveFiles(Server.MapPath(ImagePath.ForCliente), infoConImagen.ImagesForCliente).ToList();
            var mensaje = string.Empty;
            files.ToList().ForEach(f =>
            {
                mensaje += f;
                //if (f.IndexOf("error") > 0) 
            });
            return Content(mensaje);
        }

        

        public ActionResult Test(int id = -1, bool showAdvancedView = true)
        {
            var userContr = new UserController();
            var model = userContr.GetUserAndSetItToModel(id);
            model.ShowAdvancedOptions = showAdvancedView;
            userContr.prepareUserModelForGet(model);
            defaultTestNewModel(id, model);
            //model.ForActivo = true;
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
                debeCambiarContraseñaAlIniciarSesion = usuario.DebeCambiarContraseñaAlIniciarSesion,
                activo = usuario.Activo,
                bloqueado = usuario.Bloqueado,
                contraseñaExpira = userModel.LaContraseñaExpira,
                limitarVigenciaDeCuenta = userModel.LimitarVigenciaDeCuenta
            };
            var dyna2 = new
            {
                expiraCadaXMes = userModel.ContraseñaExpiraCadaXMes
            };
            return Content(dyna.ToJson());
            //ModelState.AddModelError("", "probando quitar mensaje error");
            //return View(userModel);
        }

        /// <summary>
        /// to see the return param value
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult test(string returnUrl = "")
        {
            var param = new NegociosGetParams { IdNegocio = -1 };
            var data = BLLPrestamo.Instance.GetNegocios(param);
            return Content(data.ToJson());
        }

        public ActionResult CustomizeInputFile1()
        {
            return View();
        }

    }

    public class InfoConImagen
    {
        public System.Web.HttpPostedFileBase Imagen { get; set; }
        public IEnumerable<System.Web.HttpPostedFileBase> ImagesForCliente { get; set; }

        public ImagesFor imgsForCliente => new ImagesFor("ImagesForCliente", "Clientes") {Qty=3 };
        
        public IEnumerable<System.Web.HttpPostedFileBase> ImagesForGarantia { get; set; }

        public ImagesFor imgsForGarantia => new ImagesFor("ImagesForGarantia","Garantia") {Qty=2 };
    }

    public class ImagesFor
    {
        public string PropName { get; } = string.Empty;

        public string FriendlyName { get; } = string.Empty;

        public int Qty { get; set; } = 5;

        public ImagesFor(string propName, string friendlyName)
        {
            this.PropName = propName;
            this.FriendlyName = friendlyName;
        }
    }
}