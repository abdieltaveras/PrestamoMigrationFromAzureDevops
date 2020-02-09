using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PrestamosMVC5.Controllers
{
    [AuthorizeUser]
    public class ClientesController : ControllerBasePcp
    {
        // GET: Clientes
        
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
            // task: error cuando busca cliente con usuario autenticado dice negocio <= 0
            // task falla la autencitacion del administrador con contrasena pcp20200123 y siendo pcp20200124
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
                }
                else
                {
                    model.MensajeError = "Lo siento no encontramos datos para su peticion";
                }
            }
            return View(model);
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult CreateOrEdit(ClienteModel clienteVm)
        {
            ActionResult result;
            try
            {
                pcpSetUsuarioAndIdNegocioTo(clienteVm.Cliente);
                BLLPrestamo.Instance.ClientesInsUpd(clienteVm.Cliente, clienteVm.Conyuge, clienteVm.InfoLaboral, clienteVm.Direccion);
                var mensaje = "Sus datos fueron guardados correctamente, Gracias";
                result = RedirectToAction("CreateOrEdit", new { id = -1, mensaje = mensaje });
            }
            catch (Exception e)
            {
                clienteVm.MensajeError = "Ocurrio un error que no permite guardar el cliente, revisar";
                result = View(clienteVm);
            }
            return result;
            //return RedirectToAction("Index");
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
                return newClienteVm;
            }
            else
            {
                var clienteVm = new ClienteModel(cliente);
                clienteVm.Conyuge = cliente.InfoConyuge.ToType<Conyuge>();
                clienteVm.Direccion = cliente.InfoDireccion.ToType<Direccion>();
                clienteVm.InfoLaboral = cliente.InfoLaboral.ToType<InfoLaboral>();
                pcpSetUsuarioTo(clienteVm.Cliente);
                var localidadDelCliente = BLLPrestamo.Instance.LocalidadesGet(new LocalidadGetParams { IdLocalidad = clienteVm.Direccion.IdLocalidad }).FirstOrDefault();
                if (localidadDelCliente != null)
                {
                    clienteVm.NombreLocalidad = localidadDelCliente.Nombre;
                }
                return clienteVm;
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