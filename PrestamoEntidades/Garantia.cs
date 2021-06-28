using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PrestamoEntidades
{
    public enum TiposClasificacionGarantia { Inmobiliaria=1, Mobiliaria}
    public class InfoGarantiaDrCr
        //: IInfoGarantiaDrCr
    {
        public int IdGarantia { get;  set; }
        public int IdClasificacion { get;  set; }

        public string Clasificacion => Enum.GetName(typeof(TiposClasificacionGarantia), (TiposClasificacionGarantia)IdClasificacion);
        public string NumeracionGarantia { get;  set; } = string.Empty;
        public string NombreMarca { get;  set; } = string.Empty;
        public string NombreModelo { get;  set; } = string.Empty;
        public string NombreTipoGarantia { get;  set; } = string.Empty;
        public string OtrosDetalles { get;  set; } = string.Empty;
        public string Detalles { get;  set; } = string.Empty;
        public string Imagen1FileName { get;  set; }

        public string Imagen2FileName { get;  set; }
        public string Imagen3FileName { get;  set; }

        public string Imagen4FileName { get; set; }
        //public DetalleGarantia DetallesForJson { get;  set; }
        

        public DetalleGarantia GetDetallesGarantia()=> this.Detalles.ToType<DetalleGarantia>();

        public override string ToString() =>
            $"{this.NombreTipoGarantia} {this.NombreMarca} {this.NombreModelo} ";
        
    }
    public class Garantia : BaseInsUpd
    {
        public int IdGarantia { get; set; } = -1;
        public int IdClasificacion { get; set; } = -1;
        public int IdTipoGarantia { get; set; } = -1;
        public int IdModelo { get; set; } = -1;
        public int IdMarca { get; set; } = -1;
        public string Imagen1FileName { get; set; } 
        public string Imagen2FileName { get; set; } 
        public string Imagen3FileName { get; set; }
        public string Imagen4FileName { get; set; }
        [IgnoreOnParams]
        public IEnumerable<string> ImagesForGaratia { get; set; }
        //[IgnoreOnParams]
        //public IEnumerable<string> ImagesForGaratiaEntrantes { get; set; }
        //[Required(false, "Debe ingresar un numero de identificacion","",Type.Missing)]
        //[StringLength(2, ErrorMessage = "El numero de identidad debe ser menor a {1} caracteres")]
        //[StringLength(3)]
        [Required]
        public string NoIdentificacion { get; set; } = string.Empty;
        [IgnoreOnParams]
        public DetalleGarantia DetallesJSON { get; set; } 
        public string Detalles { get; set; } = string.Empty;
        
    }
    public class GarantiaConMarcaYModelo : Garantia
    {
        public string NombreMarca { get;  set; }
        public string NombreModelo { get;  set; }

        public string NombreColor { get; set; }
    }

    public class GarantiaConMarcaYModeloYPrestamos : Garantia
    {
        public string NombreMarca { get;  set; }
        public string NombreModelo { get;  set; }

        public bool TienePrestamosVigentes => IdPrestamos.Count() > 0;

        public List<int> IdPrestamos { get;  set; } = new List<int>();

        public List<string> PrestamosNumero { get;  set; } = new List<string>();

        public string ListaPrestamosVigentes => string.Join(", ", PrestamosNumero);
    }

    public class DetalleGarantia : BaseInsUpd
    {
        // Mobiliarios
        public string Color { get; set; } = string.Empty;
        public string Chasis { get; set; } = string.Empty;
        public string NoMaquina { get; set; } = string.Empty;
        public string Ano { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;

        // Inmobiliarios
        public int IdLocalidad { get; set; }
        public string DetallesDireccion { get; set; } = string.Empty;
        public string Medida { get; set; } = string.Empty;
        // Datos en comun
        public bool UsoExclusivo { get; set; } = false; // Indica si se puede usar en varios (prestamos / documentos)
        public string Descripcion { get; set; } = string.Empty;
        // public int IdTasador { get; set; }
        public int Valor { get; set; } = 0;

        //public string InsertadoPor { get; set; } = string.Empty;
        //public DateTime FechaInsertado { get; set; }
        //public string ModificadoPor { get; set; } = string.Empty;
        //public DateTime FechaModificado { get; set; }
        //public string AnuladoPor { get; set; } = string.Empty;
        //public DateTime FechaAnulado { get; set; }        
    }

    public class GarantiaInsUptParams
    {
        public int IdGarantia { get; set; }
        public int IdClasificacion { get; set; }
        public int IdTipo { get; set; }
        public int IdModelo { get; set; }
        public int IdMarca { get; set; }
        public string NoIdentificacion { get; set; }
        public int IdNegocio { get; set; }
        public string Imagen1FileName { get; set; } = string.Empty;
        public string Imagen2FileName { get; set; } = string.Empty;
        public string Imagen3FileName { get;  set; } = string.Empty;
        public string Imagen4FileName { get;  set; } = string.Empty;
        //Aqui esta propiedad es string porque sera convertido en un JSON
        public string Detalles { get; set; }
    }

    public class BuscarGarantiaParams : BaseGetParams
    {
        public string Search { get; set; } = string.Empty;
    }

    public class GarantiaGetParams : BaseGetParams
    //: BaseGetParams
    {
        public int IdGarantia { get; set; } = -1;
        public string NoIdentificacion { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
    }

    public class GarantiasConPrestamo
    {
        public int idGarantia { get; set; }
        public int idPrestamo { get; set; }

        public string prestamoNumero { get; set; }
    }


}
