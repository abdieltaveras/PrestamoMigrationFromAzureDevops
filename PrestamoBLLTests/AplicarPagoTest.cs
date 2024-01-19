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
            if (prestamos.Any())
            {
                Prestamo prestamo  = null;
                int counter = 0;
                foreach (var p in prestamos) {
                    prestamo = p;
                    counter++;
                        if (counter > 1) break;
                }
                var apPago = new AplicarPagoAPrestamo(idLocalidadNegocio, usuario);
                    apPago.AplicarPago(prestamo.IdPrestamo, DateTime.Now, 1500);
            }
            Assert.Fail();
        }
    }
}