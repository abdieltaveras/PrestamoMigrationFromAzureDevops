using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    
    public interface IInfoPrestamoDrCr
    {
        int IdPrestamo { get; }
        string PrestamoNumero { get; }
        decimal TotalPrestado { get; }
        DateTime FechaEmisionReal { get; }

        DateTime FechaEmisionParaCalculos { get; }

        DateTime FechaVencimiento { get; }
        
        string NombreTipoAmortizacion { get; }

        string NombreClasificacion { get; }

        string NombreTipoMora { get; }

        string NombrePeriodo { get; }
        string OtrosDetalles { get;  }
    }

    public interface IInfoDeudaPrestamoDrCr {
        int CantidadDeCuotas { get; }
        double CuotasAtrasadas { get;}
        double CuotasNoVencidas { get; }
        decimal TotalCapital { get; }
        decimal TotalInteres { get;  }
        decimal TotalInteresDespuesDeVencido { get; }
        decimal TotalMoras { get; }
        decimal TotalOtrosCargos { get; }
        string OtrosDetalles { get; }
        decimal DeudaTotal { get;  }
        decimal DeudaVencida { get;  }
        decimal DeudaNoVencida { get;  }
    }


    //public interface IInfoClienteDrCr 
    //{
    //    int IdCliente { get; }
    //    string CodigoCliente { get; }
    //    string InfoLaboral { get; }
    //    string Nombres { get; }
    //    string Apellidos { get; }
    //    int IdTipoIdentificacion { get; }

    //    string NombreDocumentoIdentidad { get; }

    //    string NumeracionDocumentoIdentidad { get; }

    //    string TelefonoMovil { get; }

    //    string TelefonoCasa { get; }

    //    string TelefonoTrabajo1 { get; }
    //    string TelefonoTrabajo2 { get; }

    //    string Imagen1FileName { get; }

    //    string Imagen2FileName { get; }

    //    string OtrosDetalles { get; }
    //}


    //public interface IInfoGarantiaDrCr 
    //{ 
    //    int IdGarantia { get; }

    //    string TipoGarantia { get;  }

    //    string NumeracionGarantia { get; }

    //    string OtrosDetalles { get; }

    //}
    //public interface IPrestamoConDetallesParaCreditosyDebitos
    //{
    //    InfoPrestamoDrCr infoPrestamo { get; }
    //    InfoClienteDrCr infoCliente { get;  }
    //    InfoGarantiaDrCr infoGarantia { get;  }
    //    InfoDeudaPrestamoDrCr InfoDeuda { get;  }
    //}
}
