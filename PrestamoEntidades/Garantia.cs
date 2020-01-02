using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Garantia
    {
        public int IdGarantia { get; set; }
        public int IdClasificacion { get; set; }
        public int IdTipo { get; set; }
        public int IdModelo { get; set; }
        public int IdMarca { get; set; }
        public string NoIdentificacion { get; set; }
        public int IdNegocio { get; set; }


        public DetalleGaratia Detalles { get; set; }
    }

    public class DetalleGaratia
    {
        // Mobiliarios
        public string Color { get; set; } = string.Empty;
        public string NoMaquina { get; set; } = string.Empty;
        public string Año { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;

        // Inmobiliarios
        public int IdLocalidad { get; set; }
        public string DetallesDireccion { get; set; } = string.Empty;
        public string Medida { get; set; } = string.Empty;

        // Datos en comun
        public bool UsoExclusivo { get; set; } = true; // Indica si se puede usar en varios (prestamos / documentos)
        public string Descripcion { get; set; } = string.Empty;
        public string InsertadoPor { get; set; } = string.Empty;
        public DateTime FechaInsertado { get; set; }
        public string ModificadoPor { get; set; } = string.Empty;
        public DateTime FechaModificado { get; set; }
        public string AnuladoPor { get; set; } = string.Empty;
        public DateTime FechaAnulado { get; set; }        
        // public int IdTasador { get; set; }
        // public int Valor { get; set; } = 0;
    }

    public class GarantiaInsUptParams
    {
        public int IdGarantia { get; set; }
        public int IdClasificacion { get; set; }
        public int IdTipo { get; set; }
        public int IdModelo { get; set; }
        public string NoIdentificacion { get; set; }
        public int IdNegocio { get; set; }

        //Aqui esta propiedad es string porque sera convertido en un JSON
        public string Detalles { get; set; }
    }

    public class BuscarGarantiaParams
    {
        public int IdNegocio { get; set; }
        public string Search { get; set; } = string.Empty;
    }

}
