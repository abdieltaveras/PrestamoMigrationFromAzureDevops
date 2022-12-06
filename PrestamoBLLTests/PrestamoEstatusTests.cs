using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PrestamoBLL;
using PrestamoEntidades;
using System.Linq;

namespace PrestamoBLL.Tests
{
    /// <summary>
    /// Summary description for StatusTests
    /// </summary>
    [TestClass]
    public class PrestamoEstatusTests
    {
        public PrestamoEstatusTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [TestMethod]
        public void InsUpdTest()
        {
            //
            // TODO: Add test logic here
            //
            string errorMessage = string.Empty;

            // Que me inserte un estaus  un prestamo 
            // no Buscar un prestamo
            // seleccionar un estatus


            
            
            //SeleccionameUnprestamo (Seleccioname el primer prestamo registrado)
            var prestamoList = new List<Prestamo>();
            var firsPrestamo = prestamoList.FirstOrDefault();
            //SeleccionameUnEstatus
            var EstatusList = new EstatusBLL(1, "BllTest").Get(new EstatusGetParams());
            var firstEstatus = EstatusList.FirstOrDefault();
            
            try
            {
                //AsignaleAlPrestamoElEstatusSeleccionado
                PrestamoEstatus param = new PrestamoEstatus
                {
                    IdPrestamo = firsPrestamo.IdPrestamo,
                    IdEstatus = firstEstatus.IdEstatus,
                    IdLocalidadNegocio = 1, // no es necesario
                    IdNegocio = 1,  //no es necesario
                    Usuario = "Luis",
                    Comentario = "Ninguno"
                };
                var id = new PrestamoEstatusBLL(1, "Luis").InsUpd(param);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage),  errorMessage);
        }

        [TestMethod]
        public void GetTest()
        {
            //
            // TODO: Add test logic here
            //
            string result = "";
            try
            {
                var param = new PrestamoEstatusGetParams
                {
                    IdPrestamo = 1
                };
                var datos = new PrestamoEstatusBLL(1, "Luis").Get(param);

            }
            catch (Exception e)
            {
                result = e.Message + e.StackTrace;
                throw;
            }
        }

    }
}
