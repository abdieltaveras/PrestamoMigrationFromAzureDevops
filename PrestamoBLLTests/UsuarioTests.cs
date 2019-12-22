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
    public class UsuarioTests
    {
        string errorMensaje = string.Empty;

        [TestMethod()]
        public void InsUpdUsuario()
        {

            var usr = new Usuario
            {
                NombreRealCompleto = "Abdiel Taveras Angomas",
                LoginName = "abdiel",
                Contraseña = "Pcp46232",
                Telefono1 = "829-961-9141",
                Usuario = "UsuarioTest",
                CorreoElectronico ="abdieltaveras@hotmail.com", 
                
                
            };
            try
            {
                BLLPrestamo.Instance.InsUpdUsuario(usr);
            }
            catch (Exception e)
            {
                errorMensaje = e.Message;
            }

            Assert.IsTrue(string.IsNullOrEmpty(errorMensaje), errorMensaje);
        }
    }
}