using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class PeriodosTests
    {
        TestInfo tinfo = new TestInfo();
        [TestMethod()]
        public void InsUpdPeriodoTest()
        {
            var periodo = new Periodo { Codigo = "MesTest", IdNegocio=6, PeriodoBase = PeriodoBase.Mes, Nombre = "Prueba Mes", Usuario = tinfo._Usuario };

            Func<bool> condicion = () => tinfo.MensajeError == string.Empty;
            try
            {
                BLLPrestamo.Instance.InsUpdPeriodo(periodo);
            }
            catch (Exception e)
            {
                tinfo.MensajeError = e.Message;
            }

            Assert.IsTrue(condicion(), tinfo.MensajeError);
        }

        [TestMethod()]
        public void GetPeriodoTest()
        {
            
            Func<bool> condicion = () => tinfo.MensajeError == string.Empty;
            try
            {
                GetPeriodo();
            }
            catch (Exception e)
            {
                tinfo.MensajeError = e.Message;
            }

            Assert.IsTrue(condicion(), tinfo.MensajeError);
        }
        private int GetPeriodo()
        {
            var periodo = BLLPrestamo.Instance.GetPeriodos(new PeriodoGetParams { IdNegocio = 6, Codigo = "MES" }).FirstOrDefault();
            return periodo.idPeriodo;
        }
        [TestMethod()]
        public void GetInvalidIdPeriodoTest()
        {
            var searchPeriodo = new PeriodoGetParams { idPeriodo = 1526 };
            var periodo = BLLPrestamo.Instance.GetPeriodos(searchPeriodo).FirstOrDefault();
            Assert.IsTrue(periodo == null, "el id invalido retorno un valor diferente de null");
        }

    }
    
}