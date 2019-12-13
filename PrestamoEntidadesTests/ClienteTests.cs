using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace PrestamoEntidades.Tests
{
    public class ClienteData
    {
        public static Conyuge newConyuge()
        {
            var conyuge1 = new Conyuge
            {
                Nombres = "Yocasta Rodrigues",
                Apellidos = "Rodriguez Castillo",
                LugarTrabajo = "Glipsy",
                DireccionLugarTrabajo = "Villa españa calle d no 5",
                NoTelefono1 = "829-961-9141"
            };
            return conyuge1;
        }
        public static Cliente newCliente()
        {

            var cliente = new Cliente
            {
                Apellidos = "Taveras",
                Nombres = "Abdiel",
                Apodo = "Yeyo",
                IdNegocio = 1,
                Usuario = "abdiel",
                InfoConyuge = ClienteData.newConyuge().ToJson(),
            };
            return cliente;
        }
    }
    [TestClass()]
    public class ClienteTests
    {
        [TestMethod()]
        public void WhenConyugeStringTestEmpty()
        {
            var cliente = new Cliente();
            var conyugeText = cliente.InfoConyuge;
            var conyugeJson = conyugeText.ToType<Conyuge>();

        }

        [TestMethod()]
        public void WhenConyugeIsNotEmpty()
        {
            var cliente = new Cliente();
            var Conyuge = conyugeData.conyuge.ToJson();
            var conyugeText = cliente.InfoConyuge;
        }
        [TestMethod()]
        public void InfoClienteTest()
        {
            Assert.Fail();
        }

    }

    public static class conyugeData
    {
        public static Conyuge conyuge = new Conyuge
        {
            Nombres = "Yocasta Rodriguez",
            Apellidos = "Rodriguez Castillo",
            NoIdentificacion = "1",
            LugarTrabajo = "Glipsy",
            DireccionLugarTrabajo = "Villa espana",
            NoTelefono1 = "829-961-9141",
            TelefonoTrabajo = "809-556-8976",
            IdTipoIdentificacion = (int)TiposIdentificacionCliente.Cedula
        };
    }
    [TestClass()]
    public class ConyugeTests
    {



        [TestMethod()]
        public void ConvertToJson()
        {
            var fromConyugeToJson = conyugeData.conyuge.ToJson();
            var fromJsonToConyuge = fromConyugeToJson.ToType<Conyuge>();
            Assert.AreEqual(conyugeData.conyuge.Nombres, fromJsonToConyuge.Nombres, "los objetos no son iguales algoi fallo revise");
        }
        [TestMethod()]
        public void ConvertToXml()
        {
            var conyugeXml = conyugeData.conyuge.ToXml();
            var dserialFromXml = conyugeXml.ToType<Conyuge>(ExtMethJsonAndXml.ConversionType.ToXmlFormat);
            Assert.AreEqual(conyugeData.conyuge.Nombres, dserialFromXml.Nombres, "los objetos no son iguales algo fallo revise");
        }
    }
}