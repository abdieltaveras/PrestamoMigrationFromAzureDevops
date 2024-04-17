using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class TiposMoraTests
    {

        [TestMethod()]
        public void GetTiposMorasTest()
        {
            new TipoMoraBLL(1, TestUtils.Usuario);
            var result = new TipoMoraBLL(1, TestUtils.Usuario).GetTiposMoras(new TipoMoraGetParams { IdNegocio = 1 });
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
            var result = new TipoMoraBLL(1, TestUtils.Usuario).GetTiposMoras(searchData);
            if (result.Count() != 0)
            {
                tipoMora.IdTipoMora = result.First().IdTipoMora;
            }
            try
            {
                new TipoMoraBLL(1, TestUtils.Usuario).InsUpdTipoMora(tipoMora);
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
