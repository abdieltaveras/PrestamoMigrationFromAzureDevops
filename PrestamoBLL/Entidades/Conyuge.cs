using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class Conyuge
    {
        [Required(ErrorMessage = "campo {0} requerido, no puede estar vacio")]
        public string Nombres { get; set; } = string.Empty;
        public string Apodo { get; set; } = string.Empty;
        [Required(ErrorMessage = "campo {0} requerido, no puede estar vacio")]
        public string Apellidos { get; set; } = string.Empty;
        [Display(Name = "Telefono")]
        public string NoTelefono1 { get; set; } = string.Empty;
        [Display(Name = "Donde Labora")]
        public string LugarTrabajo { get; set; } = string.Empty;
        [Display(Name = "Telefono del Trabajajo")]
        public string TelefonoTrabajo { get; set; } = string.Empty;
        [Display(Name = "Direccion donde trabaja")]
        public string DireccionLugarTrabajo { get; set; } = string.Empty;
        [Display(Name = "Tipo Identificacion")]
        public int IdTipoIdentificacion { get; set; } = 1;
        [Display(Name = "Numeracion Identificacion")]
        public string NoIdentificacion { get; set; } = string.Empty;
        [Display(Name = "Otros detalles")]
        public string Notas { get; set; } = string.Empty;
    }
    
}
