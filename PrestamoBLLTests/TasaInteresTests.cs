using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLLTests
{
    [TestClass()]
    public class BLLPrestamoTests
    {
        /// <summary>
        /// Consulta mediante el Bll el objeto que almacena las tasas de interes
        /// </summary>
        [TestMethod()]

        public void GetTasaInteresTest()
        {
            var result = BLLPrestamo.Instance.TasasInteresGet(new TasaInteresGetParams { IdNegocio = 1 });
            Assert.IsTrue(result.Count() > 0);
        }
        /// <summary>
        /// insertar data mediante el BLL para el objeto TasaInteres
        /// </summary>
        [TestMethod()]
        public void insUpdTasaInteresTest()
        {
            var error = new Exception();
            var OperacionExitosa = true;
            var tasaInteres = new TasaInteres { Codigo = "B05", InteresMensual = 2.5M, Usuario = "TestProject", IdNegocio = 1 };
            var searchData = new TasaInteresGetParams { Codigo = "B05", IdNegocio = -1 };
            var result = BLLPrestamo.Instance.TasasInteresGet(searchData);
            if (result.Count() != 0)
            {
                tasaInteres.idTasaInteres = result.First().idTasaInteres;
            }
            try
            {
                BLLPrestamo.Instance.TasaInteresInsUpd(tasaInteres);
            }
            catch (Exception e)
            {
                error = e;
                OperacionExitosa = false;

            }
            Assert.IsTrue(OperacionExitosa, error.Message);
        }

        [TestMethod()]
        public void GetTiposMorasTest()
        {
            var result = BLLPrestamo.Instance.TiposMorasGet(new TipoMoraGetParams { IdNegocio = 1 });
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
            var result = BLLPrestamo.Instance.TiposMorasGet(searchData);
            if (result.Count() != 0)
            {
                tipoMora.IdTipoMora = result.First().IdTipoMora;
            }
            try
            {
                BLLPrestamo.Instance.TipoMoraInsUpd(tipoMora);
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