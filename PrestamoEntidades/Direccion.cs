using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
{
    //public class TipoLocalidad : BaseCatalogo
    //{
    //    public int IdTipoLocalidad { get; set; } = 0;
    //    /// <summary>
    //    /// Indica el tipo de localidad que esta puede ser padre
    //    /// Ejemlo la localidad Tipo Provincia es padre de municipios
    //    /// </summary>
    //    public int LocalidadPadre { get; set; }
    //    public override int GetId() => this.IdTipoLocalidad;
    //}

    //public class Localidad : BaseInsUpd
    //{
    //    [Required]
    //    [Display(Name = "Estatus")]
    //    public bool Activo { get; set; } = true;
    //    [Required(ErrorMessage = "el campo {0} es requerido")]
    //    public string Codigo { get; set; } = string.Empty;
    //    public int IdLocalidad { get; set; } = 0;
    //    public int IdTipoLocalidad { get; set; } = 0;
    //    public int IdLocalidadPadre { get; set; } = 0;
    //    public string Nombre { get; set; } = string.Empty;
    //    public override int GetId() => this.IdLocalidad;
    //}

    public class Direccion: BaseInsUpd
    {
        public int IdDireccion {get;set;}
        [Display(Name ="Localidad")]

        [Range(1,99999999,ErrorMessage ="No ha seleccionado ninguna localidad")]
        public int IdLocalidad { get; set; }
        [Required(ErrorMessage = "campo {0} requerido, no puede estar vacio")]
        /// <summary>
        /// Nombre de la calle incluyendo el numero de la vivienda
        /// </summary>
        public string Calle { get; set; } = string.Empty;
        
        [Display(Name = "Codigo Postal")]
        public string CodigoPostal { get; set; } = string.Empty;
        /// <summary>
        /// las coordenadas dadas por google sobre la ubicacion gps de la direccion
        /// </summary>
        [Display(Name = "Coordenadas GPS")]
        public string CoordenadasGPS { get; set; } = string.Empty;
        [Display(Name = "Otros Detalles")]
        [Required(ErrorMessage = "campo requerido, no puede estar vacio")]
        /// <summary>
        /// Para detallar la direccion esta proximo a la iglesia La Santidad
        /// Doblando por la banca GranPremio detras de la escuela Los Genios
        /// </summary>
        public string Detalles { get; set; } = string.Empty;
        
    }
}
