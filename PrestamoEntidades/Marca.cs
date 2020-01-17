using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Marca
    {
        public int IdMarca { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public int IdNegocio { get; set; } = 0;
    }
    public class MarcaGetParams
    {
        public int IdMarca { get; set; } = -1;
        public int IdNegocio { get; set; } = -1;
    }
    public class MarcaInsUpdParams : Marca
    {
        public string InsertadoPor { get; set; } = string.Empty;         
    }
}
