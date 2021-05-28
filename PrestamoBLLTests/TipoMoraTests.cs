using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class TipoMoraTests
    {
        [TestMethod()]
        public void GetTiposMorasTest()
        {
            var result = BLLPrestamo.Instance.GetTiposMoras(new TipoMoraGetParams { IdNegocio = 1 });
            var resultAsList = result.ToList();
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod()]
        public void insUpdTipoMoraTest()
        {
            var error = new Exception();
            var OperacionExitosa = true;
            var tipoMora = new TipoMora
            {
                Codigo = "P05",
                Usuario = "TestProject",
                IdNegocio = 1,
                TipoCargo = (int)TiposCargosMora.Porcentual,
                AplicarA = (int)AplicarMoraAl.Capital_intereses_y_moras,
                CalcularCargoPor = (int)CalcularMoraPor.cada_30_dias_transcurrido_por_cada_cuota_vencida,
                MontoOPorcientoACargar = 5.00M,
                DiasDeGracia = 4
            };
            var searchData = new TipoMoraGetParams { Codigo = "P05", IdNegocio = -1 };
            var result = BLLPrestamo.Instance.GetTiposMoras(searchData);
            if (result.Count() != 0)
            {
                tipoMora.IdTipoMora = result.First().IdTipoMora;
            }
            try
            {
                BLLPrestamo.Instance.InsUpdTipoMora(tipoMora);
            }
            catch (Exception e)
            {
                error = e;
                OperacionExitosa = false;
            }
            Assert.IsTrue(OperacionExitosa, error.Message);
        }
        
    }
}