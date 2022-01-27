using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PrestamoBLL;
using Microsoft.VisualStudio;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class LocalidadNegocioTest
    {
        [TestMethod()]
        public void LocalidadNegocioInsUpdTest()
        {
            var localidadNegocio = new LocalidadNegocio()
            {
                NombreComercial = "intagsa",
                NombreJuridico = "Intagsa SRL",
              
                Usuario = "testUnit",
                TaxIdNacional = "112108236",
                OtrosDetallesObj = new LocalidadNegocioOtrosDetalles { Calle = "Gregorio Luperon no 12", CorreoElectronico = "intagsa@hotmail.com", Direccion = "La Romana, Villa España", Slogan = "Prestamos Baratos", Telefono1 = "809-813-1719" }

        };

            
            var error = string.Empty;
            try
            {
                BLLPrestamo.Instance.InsUpdLocalidadNegocio(localidadNegocio);
            }
            catch (Exception e)
            {
                error = e.Message;
                throw;
            }

            Assert.IsTrue(error == string.Empty, error);
        }

        [TestMethod()]
        public void GetLocalidadesNegociosTest()
        {
            var error = string.Empty;
            try
            {
                var result = BLLPrestamo.Instance.GetLocalidadesNegocio(new LocalidadNegociosGetParams());
            }
            catch (Exception e)
            {
                error = e.Message;
                throw;
            }

            Assert.IsTrue(error == string.Empty, error);
        }
    }

}