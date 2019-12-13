﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class InfoLaboral
    {
        /// <summary>
        /// Nombre de la empresa, negocio, etc donde labora
        /// </summary>
        
        [Required(ErrorMessage = "campo requerido, no puede estar vacio")]
        [Display(Name = "Nombre Lugar donde Trabaja")]
        public string Nombre
        { get; set; } = string.Empty;
        [Display(Name = "Posicion o Puesto")]
        [Required(ErrorMessage = "campo requerido, no puede estar vacio")]
        public string Puesto { get; set; } = string.Empty;
        [Display(Name = "Fecha que inicio")]
        public DateTime FechaInicio { get; set; } = new DateTime(1900, 1, 1);
        [Display(Name = "Telefono 1")]
        public string NoTelefono1 { get; set; } = string.Empty;
        [Display(Name = "Telefono 2")]
        public string NoTelefono2 { get; set; } = string.Empty;
        [Required(ErrorMessage = "campo requerido, no puede estar vacio")]
        [Display(Name = "Direccion donde  labora")]
        public string Direccion { get; set; } = string.Empty;
        [Display(Name = "Escriba aqui otros detalles de interes")]
        public string Notas { get; set; } = string.Empty;
        
    }
    
}
