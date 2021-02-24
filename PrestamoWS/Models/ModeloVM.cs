using Microsoft.AspNetCore.Mvc.Rendering;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoWS.Models
{
    public class ModeloVM
    {
        public Modelo Modelo { get; set; }
        public IEnumerable<Marca> ListaMarcas { get; set; }
        public SelectList ListaSeleccionMarcas { get; set; }
        public IEnumerable<ModeloWithMarca> ListaModelos { get; set; }
    }
}
