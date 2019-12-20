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
    [TestClass()]
    public class NegocioTests
    {
        string mensajeError = string.Empty;
        [TestMethod()]
        public void insUpdNegocioTest()
        {
            var negocioInfo = new NegocioInfo { CorreoElectronico = "intagsa@hotmail.com", Direccion1 = "Prol. Gregorio Luperon no 12, Villa España", Direccion2 = "La Romana Rep. Dom.", Telefono1 = "809-813-1719" };
            var negocio = new Negocio { NombreComercial = "Intagsa", NombreJuridico = "Intagsa SRL", TaxIdNo = "112108236", OtrosDetalles = negocioInfo.ToJson(), Usuario = "TestProject" };

            //act
            try
            {
                BLLPrestamo.Instance.insUpdNegocio(negocio);
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }

        [TestMethod()]
        public void GetNegociosTest()
        {
            var buscar = new NegociosGetParams { NombreComercial = "Intagsa",   };
            IEnumerable<Negocio> negocios = new List<Negocio>();
            try
            {
                negocios = BLLPrestamo.Instance.GetNegocios(buscar);
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }
            if (mensajeError==string.Empty && negocios.FirstOrDefault().NombreComercial != "Intagsa")
            {
                mensajeError = "No se encontro negocio con el nombre comercial Intagsa";
            }
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }
    }
}