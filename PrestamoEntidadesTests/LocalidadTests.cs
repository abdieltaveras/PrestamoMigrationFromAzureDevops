using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades.Tests
{

    public class LocalidadSeleccionable: Localidad
    { 
        Localidad LocalidadPadre;
        public LocalidadSeleccionable(Localidad localidadPadre)
        {
            this.LocalidadPadre = localidadPadre;
        }

    }

    [TestClass()]
    public class LocalidadTests
    {

        [TestMethod()]
        public void GetIdTest()
        {
           
            var FullDescripcionSectorQuisqueya = 
                buildLocalidadFullDescripcion(LocalidadesDataSource.Quisqueya());
            Assert.Fail();
        }

        private string buildLocalidadFullDescripcion(Localidad localidad)
        {

            var nodo = localidad;
            string fullLocalidadText = string.Empty;
            do
            {
                fullLocalidadText += 
                $"[{getTipoLocalidadDescripcion(nodo.IdTipoLocalidad)}]  {nodo.Nombre} {','}";
                nodo = LocalidadesDataSource.Localidades().Where(loc => loc.IdLocalidad == nodo.IdLocalidadPadre).FirstOrDefault();
            } while (nodo!=null);
            return fullLocalidadText;
        }

        private string getTipoLocalidadDescripcion(int idTipoLocalidad)
        {
            var result = TiposLocalidadesDataSource.TiposLocalidadesRD().Where(loc => loc.IdTipoLocalidad == idTipoLocalidad).FirstOrDefault().Descripcion;
            return result;
        }
    }
}