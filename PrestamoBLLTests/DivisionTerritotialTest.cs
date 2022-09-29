﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcpSoft.System;
using System.Diagnostics;


namespace PrestamoBLL.Tests
{

    [TestClass()]
    public class DivisionTerritotialTest
    {

        [TestMethod()]
        public void GetDivisionesTerritorial()
        {
            var getParams = new DivisionTerritorialGetParams();
            var result2 = new DivisionTerritorialBLL(TestsConstants.AnyIdLocalidadNegocio, TestsConstants.Usuario).GetDivisionesTerritoriales(getParams);

            Assert.IsTrue(result2.Count() > 0, "no se econtro dato para la division territorial");
        }

        [TestMethod()]
        public void GetDivisionesTerritorialById()
        {
            var getParams = new DivisionTerritorialGetParams() { idDivisionTerritorial = 8 };
            var result2 = new DivisionTerritorialBLL(TestsConstants.AnyIdLocalidadNegocio, TestsConstants.Usuario).GetDivisionesTerritoriales(getParams);

            Assert.IsTrue(result2.Count() > 0, "no se econtro dato para la division territorial");
        }

        [TestMethod()]
        public void GetDivisionTerritorialComponents()
        {
            var divisionTerritorialComponents = getDivisionesTerritorialesComponents();
            Assert.IsTrue(divisionTerritorialComponents != null, "No fue exitosa la operacion");
        }
        [TestMethod()]
        public void CreateDivisionTerritoriosTreeTest()
        {
            var divisionTerritorialComponents = getDivisionesTerritorialesComponents();
            TreeBuilder divisionTerritorialTree = null;
            IEnumerable<ITreeNode> treeNodes = null;
            string errorMessage = string.Empty;
            try
            {
                divisionTerritorialComponents.First().IdDivisionTerritorialPadre = 0; // esto es para hacerlo el nodo raiz
                var treeItems = divisionTerritorialComponents.Select(item =>
                                new TreeItem(item.IdDivisionTerritorial, item.IdDivisionTerritorialPadre, item.Nombre));
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

        private static IEnumerable<DivisionTerritorial> getDivisionesTerritorialesComponents()
        {

            //var result = BLLPrestamo.Instance.GetTiposDivisonTerritorial("test");
            var result = new DivisionTerritorialBLL(1, TestsConstants.Usuario).GetTiposDivisionTerritorial();
            //BLLPrestamo.Instance.GetTiposDivisonTerritorial("test");
            var idDivisionTerritorialSearchComponent = result.FirstOrDefault().IdDivisionTerritorial;
            var search = new DivisionTerritorialComponentsGetParams() { idDivisionTerritorial = result.FirstOrDefault().IdDivisionTerritorial };
            var DivisionTerritoriales = new DivisionTerritorialBLL(1, TestsConstants.Usuario).GetDivisionTerritorialComponents(search);
            return DivisionTerritoriales;
        }

        [TestMethod()]
        public void GetTiposDivisonTerritorialTest()
        {
            var result = new DivisionTerritorialBLL(TestsConstants.AnyIdLocalidadNegocio, TestsConstants.Usuario).GetTiposDivisionTerritorial();
            Assert.IsTrue(result != null, "por alguna razon no se ejecuto la busqueda");
        }

        [TestMethod()]
        public void SaveDivisionTerritorialTest()
        {

            var divTerr = new DivisionTerritorial { IdDivisionTerritorialPadre = null, Nombre = "Division Territorial Estados Unidos"};
            var resultId = new DivisionTerritorialBLL(TestsConstants.AnyIdLocalidadNegocio, TestsConstants.Usuario).InsUpdDivisionTerritorial(divTerr);
            Assert.IsTrue(resultId > 0, "No se pudo guardar el registro en division territorial ");
        }

        [TestMethod()]
        public void UpdateDivisionTerritorialTest()
        {

            var getParams = new DivisionTerritorialGetParams() { idDivisionTerritorial = 8 };
            var divisionTerritorialOriginal = new DivisionTerritorialBLL(TestsConstants.AnyIdLocalidadNegocio, TestsConstants.Usuario).GetDivisionesTerritoriales(getParams).FirstOrDefault();
            var activoExpectedValue = !divisionTerritorialOriginal.Activo;

            divisionTerritorialOriginal.Activo = activoExpectedValue;

            var resultId = new DivisionTerritorialBLL(TestsConstants.AnyIdLocalidadNegocio, TestsConstants.Usuario).InsUpdDivisionTerritorial(divisionTerritorialOriginal);

            var updated = new DivisionTerritorialBLL(TestsConstants.AnyIdLocalidadNegocio, TestsConstants.Usuario).GetDivisionesTerritoriales(getParams).FirstOrDefault();

            Assert.IsTrue(updated.Activo == activoExpectedValue, "Lo siento la actualizacion no se realizo");
        }

        [TestMethod()]
        public void DeleteDivisionTerritorialTest()
        {
            
            var borrado = new DivisionTerritorialBLL(TestsConstants.AnyIdLocalidadNegocio, TestsConstants.Usuario).DeleteDivisionTerritorial(8, "Era una prueba que se estaba haciendo");
            
            Assert.IsTrue(borrado,"no se pudo borrar el registro");
        }

        
    }
}