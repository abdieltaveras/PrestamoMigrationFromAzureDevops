using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Localizador : BaseInsUpdCatalogo
    {
        public int IdLocalizador { get; set; } = 0;
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefonos { get; set; }


        public override int GetId()
        {
            return IdLocalizador;
        }
    }

    public class LocalizadorGetParams
    {
        public int IdLocalizador { get; set; } = -1;
    }
}
