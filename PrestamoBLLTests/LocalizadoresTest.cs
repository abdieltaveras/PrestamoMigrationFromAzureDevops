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
    public class Localizadores
    {
        [TestMethod()]
        public void GetLocalidadesTest()
        {
            var datos = new LocalizadorBLL(1, "Luis").Get(new LocalizadorGetParams());
        }

        [TestMethod()]
        public void InsUpdLocalidadesTest()
        {
            Localizador localizador = new Localizador
            {
                IdLocalidadNegocio = 1,
                IdNegocio = 1,
                Activo = true,
                Nombre = "Prueba Luis",
                Apellidos = "Apellidos Pruebas",
                Direccion = "Direccion prueba",
                Telefonos = "8297998545",
                Usuario = "Luis"
            };
            var datos = new LocalizadorBLL(1, "Luis").InsUpd(localizador);
        }
    }
}