using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamosMVC5.SiteUtilsTests
{
    [TestClass()]
    public class TerritorioTest
    {
        [TestMethod()]
        public void DivisionTerritorialTreeTest()
        {
            var search = new DivisionSearchParams { IdDivisionTerritorial = 2, IdNegocio = 1 };
            var result = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(search);
            var divTerritorialTree = new DivisionTerritorialTree(result);
            Assert.IsTrue(true, "nada");
        }
    }
}