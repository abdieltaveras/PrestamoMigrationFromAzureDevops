using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.Routing;
using System.Threading;

namespace PrestamosMVC5.Controllers
{
    [AuthorizeUser]
    public class ClientesController : ControllerBasePcp
    {
        // GET: Clientes

        public ClientesController()
        {
            UpdViewBag_LoadCssAndJsGrp2(true);
            UpdViewBag_ShowSummaryErrorsTime(10);
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }
        public ActionResult Index()
        {
            var clientes = GetClientes();
            ActionResult _actResult = View(clientes);
            return _actResult;
            //TASK: Cuando un cliente no tenga un estado civil de casado no es obligatorio
            // llenar datos del conyuge
            //TASK: cambiar mask del documento de identidad segun el tipo elegido si cedula o pasaporte
            // TASK agregar columna vivivienda propia (si o no) (analizar si poner alquilada o prestada)
        }



        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult CreateOrEdit(int id = -1, string mensaje = "")
        {
            var cl = new Cliente() { Activo = false };
            ClienteModel model = CreateClienteVm(true, null);
            model.MensajeError = mensaje;
            if (id != -1)
            {
                // Buscar el cliente
                var searchResult = getCliente(id);
                if (searchResult.DatosEncontrados)
                {
                    var data = searchResult.DataList.FirstOrDefault();
                    model = CreateClienteVm(false, data);
                    TempData["Cliente"] = data;
                }
                else
                {
                    model.MensajeError = "Lo siento no encontramos datos para su peticion";
                }
            }
            //model.Referencias = new List<Referencia>(new Referencia[4]);

            //model.Referencias[0] = new Referencia() { Tipo = 2 };
            //model.Referencias.Add(new Referencia() { NombreCompleto = "hola" });
            //model.Referencias.Add(new Referencia() { NombreCompleto = "adios" });

            // model.Referencias.Add(new Referencia() {});


            return View(model);
        }
        [HttpPost]
        public void UploadImage(HttpPostedFileBase imagen)
        {

            var imagen1Cliente = Utils.SaveFiles(Server.MapPath(SiteDirectory.ImagesForClientes), imagen, "probando " + Guid.NewGuid().ToString());
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(ClienteModel clienteVm)
        {

            if (!clienteVm.Cliente.TieneConyuge)
            {
                ModelState.Remove("Conyuge.Nombres");
                ModelState.Remove("Conyuge.Apellidos");
            }
            clienteVm.Cliente.Codigo = "nuevo";
            if (!ModelState.IsValid)
            {
                var modError = GetErrorsFromModelState(ModelState);
                ModelState.AddModelError("", "revise los tabs algo debe faltar o no haberse digitado bien en algun campo");
                return View(clienteVm);
            }
            ActionResult result;
            try
            {
                var clienteTempData = GetValueFromTempData<Cliente>("Cliente");
                var imagen1ClienteFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForClientes), clienteVm.image1PreviewValue);
                var imagen2ClienteFileName = Utils.SaveFile(Server.MapPath(SiteDirectory.ImagesForClientes), clienteVm.image2PreviewValue);
                clienteVm.Cliente.Imagen1FileName = GeneralUtils.GetNameForFile(imagen1ClienteFileName, clienteVm.image1PreviewValue, clienteTempData.Imagen1FileName);

                clienteVm.Cliente.Imagen2FileName = GeneralUtils.GetNameForFile(imagen2ClienteFileName, clienteVm.image2PreviewValue, clienteTempData.Imagen2FileName);
                pcpSetUsuarioAndIdNegocioTo(clienteVm.Cliente);
                BLLPrestamo.Instance.ClientesInsUpd(clienteVm.Cliente, clienteVm.Conyuge, clienteVm.InfoLaboral, clienteVm.Direccion, clienteVm.Referencias);
                var mensaje = "Sus datos fueron guardados correctamente, Gracias";
                result = RedirectToAction("CreateOrEdit", new { id = -1, mensaje = mensaje });
            }
            catch (Exception e)
            {

#if (DEBUG)
                ModelState.AddModelError("", "Ocurrio un error que no permite guardar el cliente, revisar " + e.Message);

#elif (!DEBUG)
                      ModelState.AddModelError("", "Ocurrio un error que no permite guardar el cliente, revisar" );
#endif
                result = View(clienteVm);
            }
            return result;
            //return RedirectToAction("Index");
        }

        public ActionResult BuscarPorNoIdentificacion(string noIdentificacion)
        {
            //Thread.Sleep(20000); para probar que sucede cuando el proceso no se realiza tan rapido, no se bloquea nada es decir si sucede un proceso asincrono sin hacer nada fuera de lo normal
            var search = new ClientesGetParams();
            this.pcpSetUsuarioAndIdNegocioTo(search);
            search.NoIdentificacion = noIdentificacion.RemoveAllButNumber();
            if (!string.IsNullOrEmpty(search.NoIdentificacion))
            {
                var result = BLLPrestamo.Instance.ClientesGet(search).FirstOrDefault();
                if (result != null)
                {
                    var data = new { Nombre = result.Nombres + " " + result.Apellidos, Codigo = result.Codigo };
                    Response.StatusCode = 200;
                    return Json(data, pcpIsUserAuthenticated ? JsonRequestBehavior.AllowGet : JsonRequestBehavior.DenyGet);
                }

            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        public IEnumerable<Cliente> GetClientes()
        {
            IEnumerable<Cliente> clientes;
            var getclientes = new ClientesGetParams();
            this.pcpSetUsuarioAndIdNegocioTo(getclientes);
            clientes = BLLPrestamo.Instance.ClientesGet(getclientes);
            return clientes;
        }
        private SeachResult<Cliente> getCliente(int id)
        {
            var searchCliente = new ClientesGetParams { IdCliente = id };
            pcpSetUsuarioAndIdNegocioTo(searchCliente);
            var cliente = BLLPrestamo.Instance.ClientesGet(searchCliente);
            var result = new SeachResult<Cliente>(BLLPrestamo.Instance.ClientesGet(searchCliente));
            return result;
        }

        /// <summary>
        /// Crea una instancia de ClienteVm a partir del parametro enviado
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        private ClienteModel CreateClienteVm(bool EsNuevo, Cliente cliente)
        {
            if (EsNuevo)
            {
                var newClienteVm = new ClienteModel(new Cliente());
                newClienteVm.Cliente.Codigo = "Nuevo";
                pcpSetUsuarioAndIdNegocioTo(newClienteVm.Cliente);
                FillReferencias(newClienteVm, newClienteVm.Referencias);
                return newClienteVm;
            }
            else
            {
                var clienteVm = new ClienteModel(cliente);
                clienteVm.Conyuge = cliente.InfoConyuge.ToType<Conyuge>();
                clienteVm.Direccion = cliente.InfoDireccion.ToType<Direccion>();
                clienteVm.InfoLaboral = cliente.InfoLaboral.ToType<InfoLaboral>();
                var referencias = cliente.InfoReferencia.ToType<List<Referencia>>();
                FillReferencias(clienteVm, referencias);
                pcpSetUsuarioTo(clienteVm.Cliente);
                var localidadDelCliente = BLLPrestamo.Instance.LocalidadGetFullName(clienteVm.Direccion.IdLocalidad);
                if (localidadDelCliente != null)
                {
                    clienteVm.InputRutaLocalidad = localidadDelCliente;
                    //localidadDelCliente.Nombre;
                }
                return clienteVm;
            }
        }

        private static void FillReferencias(ClienteModel clienteVm, List<Referencia> referencias)
        {
            Prestamo pre = new Prestamo();
            for (int i = 0; i < 5; i++)
            {
                if (referencias.Count > i)
                {
                    clienteVm.Referencias.Add(referencias[i]);
                }
                else
                {
                    clienteVm.Referencias.Add(new Referencia());
                    //clienteVm.Referencias[i] = new Referencia();
                }
            }
        }
    }


    public class SeachResult<T>
    {
        public bool DatosEncontrados { get; private set; } = false;
        public IEnumerable<T> DataList
        {
            get;
            private set;
        }

        public SeachResult(IEnumerable<T> data)
        {
            this.DatosEncontrados = (data != null & data.Count() > 0);
            if (DatosEncontrados)
                DataList = data;
            else
                DataList = new List<T>();
        }
    }

}