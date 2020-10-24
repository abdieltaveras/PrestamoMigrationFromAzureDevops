using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class RedesSociales :BaseCatalogo
    {
        public int Tipo { get; set; }
        public string NombreUsuario { get; set; }
        public string PerfilUrl { get; set; }

        public override int GetId()
        {
            throw new NotImplementedException();
        }
    }
    public class RedesSocialesGetParams : BaseGetParams
    {
        public int IdRedesSociales { get; set; } = -1;
    }
}
