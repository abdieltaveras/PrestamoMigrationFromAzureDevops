using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLLTests
{
    [TestClass()]
    public class TerritorioTest
    {
        [TestMethod()]
        public void TerritorioBuscarComponentesDivisionesTerritorialesTest()
        {
            var search = new DivisionSearchParams {IdDivisionTerritorial=2, IdNegocio=1  };
            var result = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(search);
            Assert.IsTrue(result.Count()>0,"no se econtro dato para la division territorial");
        }
    }
}