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

        private PositiveDecimal MontoAAplicar { get; set; }

        private Prestamo Prestamo { get; set; }

        private PrestamoBLLC PrestamoBLLC { get; set; }

        public bool OcurrioUnError => PagoResult.ErrorMessages.Any();
        PagoResult PagoResult { get; set; }
        private AplicarPagoAPrestamo(int idprestamo, DateTime fecha, string nombreUsuario, PositiveDecimal montoPagado, int idLocalidadNegocio) : base(idLocalidadNegocio, nombreUsuario)
        {
            this.IdPrestamo = idprestamo;
            this.Fecha = fecha;
            this.Usuario = nombreUsuario;
            this.MontoAAplicar = montoPagado;
            montoPagado = 5 +3;
        }

        private void ProcesarPago()
        {
            GetPrestamo();
            if (Fecha < Prestamo.FechaEmisionReal)
            {
                 PagoResult.AddErrorMessage("Lo siento la fecha de esta transaccion no puede ser menor la fecha real de creacion del prestamo");
            }
            GetCXC();
        }

        private void GetPrestamo()
        {
            var result = PrestamoBLLC.GetPrestamos(new PrestamosGetParams { idPrestamo = this.IdPrestamo });
            this.Prestamo = result.FirstOrDefault();
            if (this.Prestamo == null)
                PagoResult.AddErrorMessage("El prestamo indicado no existe");
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

        public void AplicarPago(int idPrestamo, DateTime fecha, PositiveDecimal montoPagado, int idLocalidadNegocio, string nombreUsuario)
        {
            var pago = new AplicarPagoAPrestamo(idPrestamo,fecha,  nombreUsuario, montoPagado, idLocalidadNegocio);
            GetDeuda();
        }

        public void AplicarPago(string prestamoNumero, DateTime fecha, PositiveDecimal montoPagado, int idLocalidadNegocio, string nombreUsuario)
        {
            var result = PrestamoBLLC.GetPrestamos(new PrestamosGetParams { PrestamoNumero = prestamoNumero });
            this.Prestamo = result.FirstOrDefault();
            GetDeuda();
        }

        private void probando_stash() { }
    }
}
