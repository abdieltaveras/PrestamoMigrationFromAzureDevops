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
    public class LocalidadesTest
    {
        public class LocalidadTree
        {
            public List<Localidad> LocalidadesHijas { get; } = new List<Localidad>();

            public Localidad LocalidadPadre { get; }
            public LocalidadTree(Localidad localidadPadre)
            {
                this.LocalidadPadre = localidadPadre;
            }
            public void add(Localidad localidad)
            {
                this.LocalidadesHijas.Add(localidad);
            }
        }

        List<Localidad> Localidades = new List<Localidad>();
        [TestMethod()]
        public void GetLocalidadesTest()
        {
            var localidades = BLLPrestamo.Instance.GetLocalidades(new LocalidadGetParams());

            Localidades = localidades.ToList();
            SetLocalidadesHija(Localidades.FirstOrDefault());
        }

        private void SetLocalidadesHija(Localidad localidad)
        {
            var any = Localidades.Any(item => item.IdLocalidadPadre == localidad.IdLocalidad);
            if (any)
            {
                var localidadPadre = new LocalidadTree(localidad);
                var hijas = Localidades.Where(item => item.IdLocalidadPadre == localidad.IdLocalidad);
                foreach (var item in hijas)
                {
                    localidadPadre.add(item);
                    SetLocalidadesHija(item);         
                }
            }
        }
    }
}