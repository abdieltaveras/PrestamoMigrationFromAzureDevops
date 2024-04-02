using System;
using System.Collections.Generic;

namespace PrestamoEntidades
{
    public interface ICxCDebitoPrestamo
    {
        string NumeroTransaccion { get;  }
        DateTime Fecha { get;  }
        decimal Capital { get;  }
        decimal Interes { get;  }
        decimal GastoDeCierre { get;  }
        decimal InteresDelGastoDeCierre { get;  }
        decimal OtrosCargos { get;  }
        decimal InteresOtrosCargos { get;  }
        decimal InteresDespuesDeVencido { get;  }
        decimal TotalOrig { get; }
        List<IDetalleDebitoCxC> DetallesOtrosCargos { get; set; }

        bool MenorOIgualALaFecha(DateTime fecha);
        bool Vencida(DateTime fecha);
    }
}