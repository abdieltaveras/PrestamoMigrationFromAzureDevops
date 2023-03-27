using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{

    public class PagoResult
    {
        public List<string> ErrorMessages { get; internal set; }
        internal PagoResult() { }
        internal PagoResult AddErrorMessage(string errorMessage)
        {
            this.ErrorMessages.Add(errorMessage);
            return this;
        }

    }

    public class AplicarPagoAPrestamo : BaseBLL
    {
        private int IdPrestamo { get; set; }

        private DateTime Fecha { get; set; }

        private string Usuario { get; set; }

        private decimal MontoAAplicar { get; set; }

        private Prestamo Prestamo { get; set; }

        private PrestamoBLLC PrestamoBLLC { get; set; }

        private List<string> ErrorMessages { get; set; }
        private AplicarPagoAPrestamo(int idprestamo, DateTime fecha, string nombreUsuario, PositiveDecimal montoPagado, int idLocalidadNegocio) : base(idLocalidadNegocio, nombreUsuario)
        {
            this.IdPrestamo = idprestamo;
            this.Fecha = fecha;
            this.Usuario = nombreUsuario;
            this.MontoAAplicar = montoPagado;
        }

        private PagoResult ProcesarPago()
        {
            //GetPrestamo();
            if (Fecha <= Prestamo.FechaEmisionReal)
            {
                return new PagoResult().AddErrorMessage("Lo siento la fecha de esta transaccion no puede ser menor o igual a la fecha real de creacion del prestamo");
            }
            GetCXC();

            return new PagoResult();
        }
        private void GetDeuda()
        {
            var result = PrestamoBLLC.GetPrestamos(new PrestamosGetParams { idPrestamo = IdPrestamo }).FirstOrDefault();
            this.Prestamo = result;
        }

        private void GetCXC()
        {
            PrestamoBLLC.GetCXC(this.IdPrestamo, DateTime.Now);
        }

        public void AplicarCredito(int idprestamo, DateTime fecha, PositiveDecimal montoPagado, int idLocalidadNegocio, string nombreUsuario)
        {
            var pago = new AplicarPagoAPrestamo(idprestamo,fecha,  nombreUsuario, montoPagado, idLocalidadNegocio);
            GetDeuda();
        }
    }
}
