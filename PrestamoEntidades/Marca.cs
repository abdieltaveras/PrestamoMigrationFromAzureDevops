using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Marca : BaseInsUpdCatalogo
    {
        public int IdMarca { get; set; } = 0;

        public override int GetId()
        {
            throw new NotImplementedException();
        }
    }
    public class MarcaGetParams : BaseGetParams
    {
        public int IdMarca { get; set; } = -1;
    }

    // TODO: Resolver con el modelo
    //public class MarcaInsUpdParams : Marca
    //{
    //    public string InsertadoPor { get; set; } = string.Empty;         
    //}
}
