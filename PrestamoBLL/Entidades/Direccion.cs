using System.ComponentModel.DataAnnotations;

namespace PrestamoBLL.Entidades
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

        [Range(1,99999999,ErrorMessage ="Es necesario establecer una localidad")]
        public int IdLocalidad { get; set; }
        [Required(ErrorMessage = "ingrese informacion en de la calle")]
        /// <summary>
        /// Nombre de la calle incluyendo el numero de la vivienda
        /// </summary>
        public string Calle { get; set; } = string.Empty;
        
        [Display(Name = "Codigo Postal")]
        public string CodigoPostal { get; set; } = string.Empty;
        /// <summary>
        /// las coordenadas dadas por google sobre la ubicacion gps de la direccion
        /// </summary>
        
        public float Latitud { get; set; } 
        
        public float Longitud { get; set; }
        [Display(Name = "Otros Detalles")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Digite detalles a la direccion")]
        /// <summary>
        /// Para detallar la direccion esta proximo a la iglesia La Santidad
        /// Doblando por la banca GranPremio detras de la escuela Los Genios
        /// </summary>
        public string Detalles { get; set; } = string.Empty;
        
    }
}
