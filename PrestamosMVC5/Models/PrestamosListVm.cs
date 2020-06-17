using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public class PrestamosListVm
    {
        [HiddenInput]
        public int idPrestamo { get; set; }
        public string PrestamoNumero { get; set; }
        public string Fecha { get; set; }
        public string NombreCliente { get; set; }
        public decimal MontoPrestado { get; set; }
    }

    public class PrestamoVm
    {
        public string MensajeError { get; internal set; }
        public Prestamo Prestamo { get; set; } = new Prestamo();
        [Display(Name ="Numeracion Identificacion del Cliente")]
        public string NumeroIdentificacion { get; set; } = string.Empty;
        [HiddenInput(DisplayValue = false)]
        public string infoCliente { get; set; }
        [Display(Name = "No Identificacion de la garantia")]
        public string NumeracionGarantia { get; set; } = string.Empty;
        [HiddenInput(DisplayValue =false)]
        public string infoGarantia { get; set; }
        [Display(Name ="Lleva gasto de cierre")]
        public bool LlevaGastoDeCierre { get; set; } = false;
        [Display(Name = "probando si desea ver el json result al hacer post")]
        public bool MostrarJsonResult { get; set; } = false;
        public PrestamoVm()
        {
            
        }

    }
}