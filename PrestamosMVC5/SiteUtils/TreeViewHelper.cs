using PrestamoEntidades;
using System.Collections.Generic;
using System.Linq;

namespace PrestamosMVC5.SiteUtils
{

    public class Element
    {
        public string Nombre { get; set; }
        public Element Padre { get; set; }
        public int Id { get; set; }
    }

    public static class ElementData
    {
        public static IEnumerable<Element> CreateTree()
        {
            var root = new Element { Nombre = "Pais", Id = 1 };
            var element1 = new Element { Nombre = "Provincia", Id = 2, Padre = root };
            var element2 = new Element { Nombre = "Distrito Nacional", Id = 3, Padre = root };
            var element11 = new Element { Nombre = "Municipio", Id = 4, Padre = element1 };
            var element111 = new Element { Nombre = "Sector", Id = 5, Padre = element11 };
            var element112 = new Element { Nombre = "Distrito Municipal", Id = 6, Padre = element11 };
            return new List<Element> {root, element1, element2, element11, element111, element112};
        }
    }

    public class DivisionTerritorialTree
    {

        public List<Element> ElementsForTree { get; set; } = new List<Element>();
        private IEnumerable<Territorio> Territorios;
        public DivisionTerritorialTree(IEnumerable<Territorio> territorios)
        {
            this.Territorios = territorios;
            var root = getRoot();
            GetHijos(root);
        }

        private Element getRoot()
        {
            var root = new Territorio();
            foreach (var current in this.Territorios)
            {
                var tienePadre = this.Territorios.Where(div => div.IdLocalidadPadre == current.IdTipoLocalidad)==null;
                if (!tienePadre)
                {
                    root = current;
                    break;
                }
            }
            Element rootElem = new Element { Nombre = root.Nombre, Id = root.IdTipoLocalidad };
            ElementsForTree.Add(rootElem);
            return rootElem;
        }
        private void  GetHijos(Element padre)
        {
            var hijos = this.Territorios.Where(terr => terr.IdLocalidadPadre == padre.Id);
            foreach (var item in hijos)
            {
                Element elem = new Element { Nombre = item.Nombre, Id = item.IdTipoLocalidad, Padre = padre };
                ElementsForTree.Add(elem);
                GetHijos(elem);
            }
        }
    }
}
