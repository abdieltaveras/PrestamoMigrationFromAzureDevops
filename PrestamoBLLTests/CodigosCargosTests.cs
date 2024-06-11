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
    public class CodigosCargosTests
    {
        public CodigosCargosTests()
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
                    CodigosCargosDebitos param = new CodigosCargosDebitos
                    {
                        Nombre = "Prueba",
                        Descripcion="Desc Orueba",
                        IdLocalidadNegocio = 1,
                        IdNegocio = 1,
                        Usuario = "Luis",
                    };
                    var id = new CodigosCargosDebitosReservadosBLL(1,"Luis").InsUpd(param);
                }
                catch (Exception e)
                {
                    throw new Exception("El cargo no pudo ser creado");

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
                var param = new CodigosCargosGetParams
                {
                    IdCodigoCargo = 1
                };
                var datos = new CodigosCargosDebitosReservadosBLL(1,"Luis").Get(param);

            }
            catch (Exception e)
            {
                result = e.Message + e.StackTrace;
                throw;
            }
        }

    }
}
