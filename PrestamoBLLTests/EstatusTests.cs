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
    public class EstatusTests
    {
        public EstatusTests()
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
            string result = "";
            try
            {
                var parametros = new Estatus
                {
                    Name = "Estatus Nuevo",
                    Description="Descripcion",
                    IsActivo = true,
                    IsImpedirHacerPrestamo = false,
                    IsImpedirPagoEnCaja= false,
                    IsNotPrintOnReport = false,
                    IsRequiereAutorizacionEnCaja = false
                };
                int InsResult = new EstatusBLL(1,"Luis").InsUpd(parametros);
            }
            catch (Exception e)
            {
                result = e.Message  + e.StackTrace;
                throw;
            }
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
                var parametros = new EstatusGetParams
                {
                    IdEstatus = -1
                };
                var datos = new EstatusBLL(1, "Luis").Get(parametros);
            }
            catch (Exception e)
            {
                result = e.Message + e.StackTrace;
                throw;
            }
        }

    }
}
