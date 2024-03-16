using System;

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
        decimal TotalOrig { get; }
        bool MenorOIgualALaFecha(DateTime fecha);
        bool Vencida(DateTime fecha);
    }
}