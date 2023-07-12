using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Modelo : BaseInsUpdCatalogo
    {
        public int IdModelo { get; set; } = 0;
        public int IdMarca { get; set; } = 0;

        public override int GetId()
        {
            throw new NotImplementedException();
        }
    }
    public class ModeloGetParams : BaseGetParams
    {
        public int IdModelo { get; set; } = -1;
        public int IdMarca { get; set; } = -1;
        public string Nombre { get; set; } = string.Empty;
    }

    
    public class ModeloWithMarca : Modelo
    {
        public string NombreMarca { get; set; } = string.Empty;
    }
}
