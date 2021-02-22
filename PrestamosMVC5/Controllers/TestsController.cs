using Microsoft.Ajax.Utilities;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
namespace PrestamosMVC5.Controllers
{
    //Controller to make Test

    public class TestsController : ControllerBasePcp
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

        public ActionResult Create()
        {
            var periodo = new Periodo();
            periodo.Nombre = "Semana";
            return View(periodo);
        }

        public ActionResult TreeViewExample2()
        {
            var treeData = ElementData.CreateTree();
            return View(treeData);
        }

        public ActionResult Periodo()
        {
            var model = new Periodo();
            return View(model);
        }

        public ActionResult BindingDates()
        {
            UpdViewBag_LoadCssAndJsGrp2(true);
            UpdViewBag_ShowSummaryErrorsTime(10);

            var model = new FormatosFecha();
            model.Fecha1 = DateTime.Now;
            model.Fecha2 = DateTime.Now;
            model.Fecha3 = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public JsonResult BindingDates2(FormatosFecha fechas)
        {
            var data = fechas.ToJson();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BindingDates3(FormatosFecha fechas)
        {
            var data = fechas.ToJson();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Periodo(Periodo periodo)
        {
            return Content("no me importa lo que me enviaste");
            //periodo.ToJson());
        }

        public ActionResult TreeViewExample3()
        {
            var search = new DivisionSearchParams { IdDivisionTerritorial = 2, IdNegocio = 1 };
            var result = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(search);
            var divTerritorialTree = new DivisionTerritorialTree(result);
            return View("treeViewExample2", divTerritorialTree.ElementsForTree);
        }
        public ActionResult CheckBoxes()
        {
            var model = new TestCheckBox();
            model.Bloqueado = false;
            return View(model);
        }

        public ActionResult DataTables()
        {
            CatalogoVM data = new CatalogoVM();
            var tabla = "tblOcupaciones";
            data.Lista = BLLPrestamo.Instance.GetCatalogos(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio, NombreTabla = "tblOcupaciones", IdTabla = "IdOcupacion" });
            data.TipoCatalogo = "Ocupaciones";
            data.IdTabla = "IdOcupacion";
            data.NombreTabla = tabla;
            return View("", data);
        }


        public ActionResult DialogBox()
        {
            return View();
        }

        public ActionResult PNotifyConfirm()
        {
            return View();
        }

        public ActionResult JQueryConfirm()
        {
            return View();
        }
        public ActionResult Delete(DelCatalogo catalogo)
        {
            pcpSetUsuarioTo(catalogo);
            var borrado = false;
            try
            {
                //BLLPrestamo.Instance.CatalogoDel(catalogo);
                borrado = true;
            }
            catch (Exception)
            {

            }

            return Json(new { borrado = borrado });
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

        public ActionResult Error404()
        {
            return new HttpStatusCodeResult(404);
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

        public ActionResult masks()
        {
            var model = new TestMask();
            model.Text = "randy";
            model.Number = 12;
            model.Fecha = DateTime.Now;
            model.TextAsNumber = model.Number.ToString();
            return View(model);
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
            var files = Utils.SaveFiles(Server.MapPath(SiteDirectory.ImagesForClientes), infoConImagen.ImagesForCliente).ToList();
            var mensaje = string.Empty;
            files.ToList().ForEach(f =>
            {
                mensaje += f;
                //if (f.IndexOf("error") > 0) 
            });
            return Content(mensaje);
        }

        public ActionResult CheckSessionInfo(string token)
        {
            var data = new
            {
                NegocioMatrizIdNegocio = AuthInSession.GetStringValueForKey(AuthInSession.NegocioMatrizIdNegocio),
                NegocioMatrizNombre = AuthInSession.GetStringValueForKey(AuthInSession.NegocioMatrizNombre),
                NegocioPadreIdNegocio = AuthInSession.GetStringValueForKey(AuthInSession.NegocioPadreIdNegocio),
                NegocioPadreNombre = AuthInSession.GetStringValueForKey(AuthInSession.NegocioPadreNombre),
                NegocioSelectedNombre = AuthInSession.GetStringValueForKey(AuthInSession.NegocioSelectedNombre),
                NegocioSelectedIdNegocio = AuthInSession.GetStringValueForKey(AuthInSession.NegocioSelectedIdNegocio),
            };

            return Json(data.ToJson(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Usuario(int id = -1, bool showAdvancedView = true)
        {
            var userContr = new UserController();
            var model = userContr.GetUserAndSetItToModel(id);
            model.ShowAdvancedOptions = showAdvancedView;
            userContr.prepareUserModelForGet(model);
            defaultTestNewModel(id, model);
            //model.ForActivo = true;
            model.Usuario.VigenteDesde = DateTime.Now.AddDays(-10);
            return View("../User/CreateOrEdit", model);
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
        public ActionResult Usuario(UserModel userModel)
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
        public string returnJson()
        {
            var param = new NegociosGetParams { IdNegocio = -1, Usuario = "testController" };
            var data = BLLPrestamo.Instance.GetNegocios(param).FirstOrDefault();
            return data.ToJson();
        }
        public ActionResult returnUrl(string returnUrl = "")
        {
            return Content(returnUrl);
        }

        public ActionResult CustomizeInputFile1()
        {
            return View();
        }

        public ActionResult DropDownList1()
        {
            var model = new Item();
            
            return View(model);
        }

        public ActionResult PostPeriodoWithJson()
        {
            var model = new Periodo();
            return View(model);
        }
        [HttpPost]
        public ActionResult PostPeriodoWithJson(Periodo periodo)
        {
            return Json(periodo.ToJson(), JsonRequestBehavior.AllowGet);
        }

    }

    public class InfoConImagen
    {
    
        public IEnumerable<System.Web.HttpPostedFileBase> ImagesForCliente { get; set; }

        public ImagesFor imgsForCliente => new ImagesFor("ImagesForCliente", "Clientes") { Qty = 3 };

        public IEnumerable<System.Web.HttpPostedFileBase> ImagesForGarantia { get; set; }

        public ImagesFor imgsForGarantia => new ImagesFor("ImagesForGarantia", "Garantia") { Qty = 2 };
    }


    public class FormatosFecha
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha1 { get; set; } = InitValues._19000101;
        public DateTime Fecha2 { get; set; } = InitValues._19000101;
        public DateTime Fecha3 { get; set; } = InitValues._19000101;

        public string FechaSt1 { get; set; } = InitValues._19000101.ToShortDateString();
        public string FechaSt2 { get; set; } = InitValues._19000101.ToShortDateString();
    }
    
}
