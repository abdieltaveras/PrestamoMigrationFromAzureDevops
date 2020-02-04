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
                NoTelefono1 = "829-961-9141"
            };
            return conyuge1;
        }

        public static Direccion newDireccion()
        {
            var direccion = new Direccion
            {
                Calle = "Serapia no 3",
                IdLocalidad = 1,
                CoordenadasGPS="25263536",
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
        public static Cliente newCliente()
        {

            var cliente = new Cliente
            {
                Apellidos = "Taveras",
                Nombres = "Abdiel",
                Apodo = "Yeyo",
                IdNegocio = 1,
                Usuario = "TestProject",
                InfoConyuge = newConyuge().ToJson(),
            };
            return cliente;
        }
        public static ClientesGetParams SearchCliente()
        {
            return new ClientesGetParams() { };

        }
    }
    [TestClass()]
    public class ClienteTests
    {
        [TestMethod()]
        public void GetClientes_DoesNotThrowError()
        {
            var searchData = ClienteData.SearchCliente();
            searchData.IdNegocio = -1;
            var mensajeError = string.Empty;
            try
            {
                BLLPrestamo.Instance.ClientesGet(searchData);
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }
            Assert.IsTrue(string.IsNullOrEmpty(mensajeError),"fallo la rutina de buscar clientes mensaje de error "+mensajeError);
        }   

        [TestMethod()]
        public void insUpdClienteTest()
        {
            var error = new Exception();
            var OperacionExitosa = true;
            var cliente = ClienteData.newCliente();
            try { BLLPrestamo.Instance.ClientesInsUpd(cliente, ClienteData.newConyuge(), ClienteData.newInfoLaboral(), ClienteData.newDireccion()); }
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
            var gParam = new ClientesGetParams();
            gParam.IdNegocio = 0;
            var error = string.Empty;
            try
            {
                BLLPrestamo.Instance.ClientesGet(gParam);
            }
            catch (Exception e)
            {

                error = e.Message;
            }

            Assert.IsTrue(string.IsNullOrEmpty(error), error);
        }
    }
    
}