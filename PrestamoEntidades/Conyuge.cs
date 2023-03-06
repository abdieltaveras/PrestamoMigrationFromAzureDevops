using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Conyuge
    {
        //[Required(ErrorMessage = "debe ingresar el nombre del conyuge")]
        public string Nombres { get; set; } = string.Empty;
        public string Apodo { get; set; } = string.Empty;
        //[Required(ErrorMessage = "ingrese los apellidos")]
        public string Apellidos { get; set; } = string.Empty;
        
        public string TelefonoPersonal { get; set; } = string.Empty;
        
        public string LugarTrabajo { get; set; } = string.Empty;
        
        

        public string TelefonoTrabajo { get; set; } = string.Empty;
        
        public string DireccionLugarTrabajo { get; set; } = string.Empty;
        
        public int IdTipoIdentificacion { get; set; } = 1;
        
        public string NoIdentificacion { get; set; } = string.Empty;
        
        public string Notas { get; set; } = string.Empty;

        
    }
    
}
