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
    public class ComentariosTests
    {
        public ComentariosTests()
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
                var parametros = new Comentario
                {
                    IdTransaccion = 1,
                    TablaOrigen = eTablasOrigen.Clientes.ToString(),
                    Detalle = "Detalle De Prueba",
                    Usuario = "Luis"
                };
                new ComentarioBLL(1,parametros.Usuario).InsUpd(parametros);
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
                var parametros = new StatusGetParams
                {
                };
                var datos = BLLPrestamo.Instance.GetStatus(parametros);
            }
            catch (Exception e)
            {
                result = e.Message + e.StackTrace;
                throw;
            }
        }

    }
}
