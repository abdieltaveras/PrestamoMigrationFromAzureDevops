using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class AplicarPagoTest
    {
        [TestMethod()]
        public void AplicarPago_Test()
        {
            // primero buscar un prestamo
            var idLocalidadNegocio = TestUtils.GetIdLocalidadNegocio();
            var usuario = TestUtils.Usuario;
            var prestamoResult = ConfigurationManager.AppSettings["prestamoAplicarPago"];
            var prestamoNo = PrestamoBllUtils.PadLeftPrestamo(prestamoResult);
                //Utils.GenPrestamoNumero(prestamoResult);

            var prestamos = new PrestamoBLLC(idLocalidadNegocio, usuario).GetPrestamos(new PrestamosGetParams { PrestamoNumero = prestamoNo });
            var prestamo = prestamos.FirstOrDefault();        
            
                var apPago = new AplicarPagoAPrestamo(idLocalidadNegocio, usuario);
                    apPago.AplicarPago(prestamo.IdPrestamo, DateTime.Now, 1500);
            
            Assert.Fail();
        }
    }
}