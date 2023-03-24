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
        public PagoResult Result { get; set; }
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

        private decimal MontoPagado { get; set; }

        private Prestamo Prestamo { get; set; }

        private PrestamoBLLC PrestamoBLLC { get; set; }

        private List<string> ErrorMessages { get; set; }
        public AplicarPagoAPrestamo(int idprestamo, DateTime Fecha, string nombreUsuario, PositiveDecimal MontoPagado, int idLocalidadNegocio) : base(idLocalidadNegocio, nombreUsuario)
        {
            this.IdPrestamo = idprestamo;
            this.Fecha = Fecha;
            this.Usuario = nombreUsuario;
            this.MontoPagado = MontoPagado;
        }

        private PagoResult ProcesarPago()
        {
            GetPrestamo();
            if (Fecha <= Prestamo.FechaEmisionReal)
            {
                return new PagoResult().AddErrorMessage("Lo siento la fecha de esta transaccion no puede ser menor o igual a la fecha real de creacion del prestamo");
            }
            GetCXC();

            return new PagoResult();
        }
        private void GetPrestamo()
        {
            PrestamoBLLC = new PrestamoBLLC(this.IdLocalidadNegocioLoggedIn, this.LoginName);
            var result = PrestamoBLLC.GetPrestamos(new PrestamosGetParams { idPrestamo = IdPrestamo }).FirstOrDefault();
            this.Prestamo = result;
        }

        private void GetCXC()
        {
            PrestamoBLLC.GetCXC(this.IdPrestamo,DateTime.Now);
        }

        private void aplicarPago()
        {

        }
    }
}
