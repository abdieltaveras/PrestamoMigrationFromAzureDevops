using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    
    public class ClienteData
    {
        public static Conyuge newConyuge()
        {
            var conyuge1 = new Conyuge
            {
                Nombres = "Yocasta Rodriguez",
                Apellidos = "Rodriguez Castillo",
                LugarTrabajo = "Glipsy",
                DireccionLugarTrabajo = "Villa españa calle d no 5",
                TelefonoPersonal = "829-961-9141"
            };
            return conyuge1;
        }

        public static Direccion newDireccion()
        {
            var direccion = new Direccion
            {
                Calle = "Serapia no 3",
                IdLocalidad = 1,
                Detalles="Vive proximo a la puerta Oeste"
            };
            return direccion;
        }

        public static InfoLaboral newInfoLaboral()
        {
            var infoLaboral = new InfoLaboral
            {
                Direccion = "Gregorio Luperon no 112",
                FechaInicio = new DateTime(2001, 5, 3),
                Nombre = "Pc Prog",
                NoTelefono1 = "8095508455",
                NoTelefono2 = "8098131251"
            };
            return infoLaboral;
        }

        public static List<Referencia> newInfoReferencia()
        {
            var infoReferencia = new Referencia
            {
                Direccion = "Gregorio Luperon no 112",
                Telefono = "809-550-8455",
                Tipo = 1,
                Detalles = "es el negocio de computadoras"
            };
            var referencias = new List<Referencia> { infoReferencia};
            return referencias;
        }
        public static Cliente newCliente()
        {

            var cliente = new Cliente
            {
                Apellidos = "Taveras",
                Nombres = "Abdiel",
                Apodo = "Yeyo",
                IdNegocio = 1,
                Usuario = "TestProject",
            };
            return cliente;
        }
        public static ClienteGetParams SearchCliente()
        {
            return new ClienteGetParams() { };

        }
    }
    [TestClass()]
    public class ClienteTests
    {
        [TestMethod()]
        public void GetClientesWithoutConvertingJson()
        {
            var searhData = ClienteData.SearchCliente();
            searhData.IdNegocio = -1;
            var mensajeError = string.Empty;

            
            try
            {
                var result = new ClienteBLL(1,"testBll").GetClientes(searhData,false);
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }
            Assert.IsTrue(string.IsNullOrEmpty(mensajeError),"fallo la rutina de buscar clientes mensaje de error "+mensajeError);
        }

        [TestMethod()]
        public void GetClientesConvertingJson()
        {
            var searhData = ClienteData.SearchCliente();
            searhData.IdNegocio = -1;
            var mensajeError = string.Empty;

            searhData.IdCliente = 1;
            try
            {
                var result = new ClienteBLL(1, "testBll").GetClientes(searhData, true);
                var resultCliente = result.FirstOrDefault();
                var imagenes = (resultCliente is Cliente) ? resultCliente.ImagenesObj : null;
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }
            Assert.IsTrue(string.IsNullOrEmpty(mensajeError), "fallo la rutina de buscar clientes mensaje de error " + mensajeError);
        }

        [TestMethod()]
        public void insUpdClienteTest()
        {
            var error = new Exception();
            var OperacionExitosa = true;
            var cliente = ClienteData.newCliente();
            cliente.NoIdentificacion = DateTime.Now.ToString();
            try { new ClienteBLL(1,"testBll").InsUpdCliente(cliente); }
            catch (Exception e)
            {
                error = e;
                OperacionExitosa = false;
            }
            Assert.IsTrue(OperacionExitosa, error.Message);
        }
        [TestMethod()]
        public void ClientesGet_SetIdNegocioTOZero_ThrowError_Test()
        {
            var gParam = new ClienteGetParams();
            gParam.IdNegocio = 0;
            var ocurrioError = false;
            var error = string.Empty;
            try
            {
                new ClienteBLL(1,"testBll").GetClientes(gParam,false);
            }
            catch (Exception e)
            {
                error = e.Message;
                ocurrioError = true;
            }
            // debe fallar si el error 
            Assert.IsTrue(ocurrioError, error);
        }

        [TestMethod()]
        public void SearchClienteByColunmTest()
        {
            var ocurrioError = false;
            var error = string.Empty;
            try
            {
                var datos = new ClienteBLL(1, "testBll").SearchClienteByColumn("a","tblClientes","Nombres","Nombres");
            }
            catch (Exception e)
            {
                error = e.Message;
                ocurrioError = true;
            }
            // debe fallar si el error 
            Assert.IsTrue(ocurrioError, error);
        }

        [TestMethod()]
        public void SearchClientesByProperties()
        {
            var ocurrioError = false;
            var error = string.Empty;
            try
            {
                var a = (eOpcionesSearchCliente)9;
                var datos = new ClienteBLL(1, "testBll").SearchClientesByProperties(new ClienteGetParams { });
                //var datos = new ClienteBLL(1, "testBll").SearchClienteByColumn("a", "tblClientes", "Nombres", "Nombres");
            }
            catch (Exception e)
            {
                error = e.Message;
                ocurrioError = true;
            }
            // debe fallar si el error 
            Assert.IsTrue(ocurrioError, error);
        }
    }
    
}