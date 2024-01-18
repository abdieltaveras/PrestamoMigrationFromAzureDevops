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
    public class AplicarPagoTest
    {
        [TestMethod()]
        public void AplicarPago_Test()
        {
            // primero buscar un prestamo
            var idLocalidadNegocio = TestInfo.GetIdLocalidadNegocio();
            var usuario = TestInfo.Usuario;
            var prestamos = new PrestamoBLLC(idLocalidadNegocio, usuario).GetPrestamos(new PrestamosGetParams());
            var prestamo = prestamos.FirstOrDefault();
            var apPago = new AplicarPagoAPrestamo(idLocalidadNegocio, usuario);
                apPago.AplicarPago(prestamo.PrestamoNumero,DateTime.Now,1500, idLocalidadNegocio, usuario);

            Assert.Fail();
        }
    }
}