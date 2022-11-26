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
            string result = "";
            try
            {
                try
                {
                    PrestamoEstatus param = new PrestamoEstatus
                    {
                        IdPrestamo = 1,
                        IdEstatus = 1,
                        IdLocalidadNegocio = 1,
                        IdNegocio = 1,
                        Usuario = "Luis",
                        Comentario = "Ninguno"
                    };
                    var id = new PrestamoEstatusBLL(1,"Luis").InsUpd(param);
                }
                catch (Exception e)
                {
                    throw new Exception("El cliente no pudo ser creado");

                }
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
                var param = new PrestamoEstatusGetParams
                {
                    IdPrestamo = 1
                };
                var datos = new PrestamoEstatusBLL(1,"Luis").Get(param);

            }
            catch (Exception e)
            {
                result = e.Message + e.StackTrace;
                throw;
            }
        }

    }
}
