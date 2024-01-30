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
    public class NegociosBLLTest
    {
        [TestMethod()]
        public void CodigoNegocioDoesNotExistTest()
        {
            var errorMessage = string.Empty;
            bool codigoYaRegistrado = false;
            try
            {
                var negocioBll = new NegociosBLL(TestInfo.Usuario);
                var negocios = negocioBll.GetNegocios(new NegociosGetParams());
                
                var codigo = negocios.FirstOrDefault().Codigo;
                codigoYaRegistrado = new NegociosBLL(TestInfo.Usuario).CodigoYaRegistrado(codigo);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                throw;
            }

            Assert.IsTrue(codigoYaRegistrado,"debe informar que el codigo es duplicado");
        }
    }
}