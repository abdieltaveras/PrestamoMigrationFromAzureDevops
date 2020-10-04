using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PrestamoBLL;
using System.Data.SqlClient;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class PrestamoTests
    {
        string errorMensaje = string.Empty;

        static internal Prestamo newPrestamo()
        {
            return new Prestamo();
        }

        [TestMethod()]
        public void getPrestamoConDetallesForUiPrestamo()
        {
            var id = 7;
            var searchResult = BLLPrestamo.Instance.GetPrestamoConDetalleForUIPrestamo(id);
            if (searchResult != null)
            {

                var Prestamo = searchResult.infoPrestamo;
                var infoCliente = searchResult.infoCliente.InfoDelCliente;
                var infoGarantia = searchResult.infoGarantias.FirstOrDefault().InfoVehiculo;
                var LlevaGastoDeCierre = (Prestamo.InteresGastoDeCierre > 0);
            }

        }
    }
}