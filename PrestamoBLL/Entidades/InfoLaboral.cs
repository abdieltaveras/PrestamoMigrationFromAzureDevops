using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class InfoLaboral
    {
        /// <summary>
        /// Nombre de la empresa, negocio, etc donde labora
        /// </summary>
        
        [Required(ErrorMessage = "ingrese el nombre del lugar de trabajo")]
        [Display(Name = "Nombre Lugar donde Trabaja")]
        public string Nombre
        { get; set; } = string.Empty;
        [Display(Name = "Posicion o Puesto")]
        [Required(ErrorMessage = "campo requerido, no puede estar vacio")]
        public string Puesto { get; set; } = string.Empty;
        [Display(Name = "Fecha que inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaInicio { get; set; } = InitValues._19000101;
        [Display(Name = "Telefono 1")]
        public string NoTelefono1 { get; set; } = string.Empty;
        [Display(Name = "Telefono 2")]
        public string NoTelefono2 { get; set; } = string.Empty;
        [Required(ErrorMessage = "ingrese la direccion donde labora")]
        [Display(Name = "Direccion donde  labora")]
        public string Direccion { get; set; } = string.Empty;
        [Display(Name = "Escriba aqui otros detalles de interes")]
        public string Notas { get; set; } = string.Empty;
        
    }
    
}
