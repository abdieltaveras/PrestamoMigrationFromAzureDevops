using PrestamoBLL;
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
        public string MontoAPrestar { get; set; }
        public Prestamo Prestamo { get; set; }
        //[Display(Name ="Numeracion Identificacion del Cliente")]
        //public string _NumeroIdentificacion { get; set; } = string.Empty;
        [HiddenInput(DisplayValue = false)]
        public string infoCliente { get; set; }
        //[Display(Name = "No Identificacion de la garantia")]
        //public string _NumeracionGarantia { get; set; } = string.Empty;

        [Display(Name = "Prestamo a Renovar Numero")]
        public string NumeroPrestamoARenovar { get; set; } = string.Empty;
        [HiddenInput(DisplayValue = false)]
        public string infoGarantia { get; set; }
        [Display(Name = "Lleva gasto de cierre")]
        public bool LlevaGastoDeCierre { get; set; } = false;
        [Display(Name = "Desea Renovar Prestamo")]
        public bool IncluirRenovacion { get; set; } = false;
        public PrestamoVm()
        {
            this.Prestamo = new Prestamo();
        }

    }
}