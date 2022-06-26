using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcpSoft.System;
using System.Diagnostics;
using PrestamoModelsForFrontEnd;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class TerritorioTest
    {
        [TestMethod()]
        public void TerritorioBuscarComponentesDivisionesTerritorialesTest()
        {
            var search = new DivisionSearchParams {IdDivisionTerritorialPadre=2, IdNegocio=1  };
            var result = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(search);
            Assert.IsTrue(result.Count()>0,"no se econtro dato para la division territorial");
        }

        [TestMethod()]
        public void DivisionTerritoriosTreeTest()
        {
            var search = new DivisionSearchParams { IdDivisionTerritorialPadre = 2, IdNegocio = 1 };
            var DivisionTerritoriales = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(search);
            var div2 = BLLPrestamo.Instance.TerritorioDivisionesTerritorialesGet(new TerritorioGetParams());
            TreeBuilder divisionTerritorialTree = null;
            IEnumerable<ITreeNode> treeNodes = null;

            var treeItems = new List<ITreeItem>();
            string errorMessage = string.Empty;
            try
            {
                DivisionTerritoriales.First().IdLocalidadPadre=0; // esto es para hacerlo el nodo raiz
                foreach (var item in DivisionTerritoriales)
                {
                    var treeItem = new DivisionTerritorialTreeItem(item);
                    treeItems.Add(treeItem);
                }
                divisionTerritorialTree = new TreeBuilder(treeItems);
                treeNodes = divisionTerritorialTree.GetTreeNodes();
                treeNodes.DisplayTo((e) => Trace.WriteLine(e));
            }
            catch (Exception e)
            {
                errorMessage = e.Message;                
            }
            //DivisionTerritoriales.First().IdLocalidadPadre = 0;
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage), "no se pudo generar el tree para la division territorial");
        }
        
    }
}