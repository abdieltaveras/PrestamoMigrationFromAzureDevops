using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
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

    

    public interface IInfoDeudaPrestamo {
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


    public interface IInfoClienteDrCr 
    {
        int IdCliente { get; }

        string Codigo { get; }

        string Nombres { get; }

        string Apellidos { get; }

        string NombreDocumentoIdentidad { get; }

        string NumeracionDocumentoIdentidad { get; }

        string TelefonoMovil { get; }

        string TelefonoCasa { get; }

        string TelefonoTrabajo1 { get; }
        string TelefonoTrabajo2 { get; }

        string Imagen1FileName { get; }

        string Imagen2FileName { get; }

        string OtrosDetalles { get; }
    }


    public interface IInfoGarantia 
    { 
        int IdGarantia { get; }

        string TipoGarantia { get;  }

        string Numeracion { get; }

        string OtrosDetalles { get; }

    }
    public interface IPrestamoConDetallesParaCreditosyDebitos
    {
        IInfoPrestamoDrCr infoPrestamo { get; }
        IInfoClienteDrCr infoCliente { get;  }
        IInfoGarantia infoGarantia { get;  }
        IInfoDeudaPrestamo InfoDeuda { get;  }
    }
}
