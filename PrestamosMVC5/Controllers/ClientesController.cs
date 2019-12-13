﻿using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class ClientesController : Controller
    {
        private string getUsuario() => "Abdiel";
        private int getIdNegocio() => 1;
        // GET: Clientes
        public ActionResult Index()
        {
            var clientes = BLLPrestamo.Instance.GetClientes(new ClientesGetParams());
            return View(clientes);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CreateOrEdit(int id=-1, string mensaje="")
        {
            ClienteVM model = CreateClienteVm(true,null);
            model.MensajeError = mensaje;
            if (id!= -1)
            {
                // buscar el cliente
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

        
        private SeachResult<Cliente> getCliente(int id)
        {
            var cliente = BLLPrestamo.Instance.GetClientes(new ClientesGetParams { IdCliente = id });

            var result = new SeachResult<Cliente>(BLLPrestamo.Instance.GetClientes(new ClientesGetParams { IdCliente = id }));

            return result;
        }

        /// <summary>
        /// Crea una instancia de ClienteVm a partir del parametro enviado
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        private ClienteVM CreateClienteVm(bool EsNuevo, Cliente cliente)
        {
            if (EsNuevo)
            {
                var newClienteVm = new ClienteVM(new Cliente());
                newClienteVm.Cliente.Codigo = "Nuevo";
                newClienteVm.Cliente.Usuario = getUsuario();
                newClienteVm.Cliente.IdNegocio = getIdNegocio();
                return newClienteVm;
            }
            else
            {
                var clienteVm = new ClienteVM(cliente);
                clienteVm.Conyuge = cliente.InfoConyuge.ToType<Conyuge>();
                clienteVm.Direccion = cliente.InfoDireccion.ToType<Direccion>();
                clienteVm.InfoLaboral = cliente.InfoLaboral.ToType<InfoLaboral>();
                clienteVm.Cliente.Usuario = getUsuario();
                return clienteVm;
            }
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult CreateOrEdit(ClienteVM clienteVm)
        {
            ActionResult result;
            try
            {
                
                BLLPrestamo.Instance.insUpdCliente(clienteVm.Cliente, clienteVm.Conyuge, clienteVm.InfoLaboral, clienteVm.Direccion);
                var mensaje = "Sus datos fueron guardados correctamente, Gracias";
                result = RedirectToAction("CreateOrEdit", new { id = -1 , mensaje = mensaje}); 
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

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
