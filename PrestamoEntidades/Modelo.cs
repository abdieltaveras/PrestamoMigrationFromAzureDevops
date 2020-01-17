using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Modelo
    {
        public int IdModelo { get; set; } = 0;
        public int IdMarca { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public int IdNegocio { get; set; } = 0;
    }
    public class ModeloGetParams
    {
        public int IdModelo { get; set; } = -1;
        public int IdMarca { get; set; } = -1;
        public int IdNegocio { get; set; } = -1;
    }
    public class ModeloInsUpdParams : Modelo
    {
        public string InsertadoPor { get; set; } = string.Empty;
    }

    public class ModeloWithMarca : Modelo
    {
        public string NombreMarca { get; set; } = string.Empty;
    }
}
