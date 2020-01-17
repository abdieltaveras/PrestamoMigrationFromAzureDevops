using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public class ModeloVM
    {
        public Modelo Modelo { get; set; }
        public IEnumerable<Marca> ListaMarcas { get; set; }
        public SelectList ListaSeleccionMarcas { get; set; }
        public IEnumerable<ModeloWithMarca> ListaModelos { get; set; }
    }
}