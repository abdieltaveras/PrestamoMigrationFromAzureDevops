using PcpUtilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrestamoEntidades;

namespace PrestamoValidaciones
{
    public static class Validaciones
    {

        public static Validator<Prestamo> ForPrestamo001()
        {
            var prestamoValidator =
                    Validator<Prestamo>.Empty
                    .IsNotValidWhen(p => p.AcomodarFechaALasCuotas, "Por ahora no aceptamos acomodar la fecha", ValidationOptions.StopOnFailure)
                    .IsNotValidWhen(p => p.IdTipoAmortizacion != (int)TiposAmortizacion.No_Amortizable_cuotas_fijas, "Por ahora solo trabajamos con la amortizacion No Amortizable Cuotas Fijas")
                    .IsNotValidWhen(p => p == null, "El prestamo no puede estar nulo", ValidationOptions.StopOnFailure)
                    .IsValidWhen(p => p.MontoPrestado >= 0, "El monto a prestar no puede ser menor a 0 (cero)")
                    .IsValidWhen(p => p.IdCliente > 0, "Debe establecer un cliente")
                    .IsValidWhen(p => p.IdClasificacion > 0, "Debe elegir una clasificacion valida", ValidationOptions.StopOnFailure)
                    .IsNotValidWhen(p => !p.LlevaGarantia && p.IdGarantias.Count() > 0, "la clasificacion elegida no lleva garantias y se le establecio una debe cambiar la clasificacion")
                    .IsNotValidWhen(p => p.LlevaGarantia && p.IdGarantias.Count() <= 0, "Debe establecer garantia");
            return prestamoValidator;
        }

        public static Validator<Cliente> ForCliente001()
        {
            var validator =
                    Validator<Cliente>.Empty
                    .IsNotValidWhen(cl => string.IsNullOrEmpty(cl.InfoDireccionObj.Detalles),"Es obligatorio escribir detalles a la direccion");
            return validator;
        }
    }


}
