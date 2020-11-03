using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class RedesSociales 
        //:BaseCatalogo
    {

        [DisplayName("Red Social")]
        public int Tipo { get; set; }
        [DisplayName("Nombre de Usuario")]
        public string NombreUsuario { get; set; }

        [DisplayName("Url Del Perfil")]
        public string PerfilUrl { get; set; }

        //public override int GetId()
        //{
        //    throw new NotImplementedException();
        //}
    }

}
