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


        private PositiveDecimal MontoAAplicar { get; set; }

        private Prestamo Prestamo { get; set; }

        //private PrestamoBLLC PrestamoBLLC { get; set; }

        public bool OcurrioUnError => PagoResult.ErrorMessages.Any();
        PagoResult PagoResult { get; set; }
        public AplicarPagoAPrestamo(int idLocalidadNegocio, string usuario) : base(idLocalidadNegocio, usuario) { }
        
        
        private void SetValue(int idprestamo, DateTime fecha, PositiveDecimal montoPagado)  
        {
            this.IdPrestamo = idprestamo;
            this.Fecha = fecha;
            this.MontoAAplicar = montoPagado;
            
        }

        private void ValidarFechaMayorEmisionPrestamo()
        {
            if (Fecha < Prestamo.FechaEmisionReal)
            {
                PagoResult.AddErrorMessage("Lo siento la fecha de esta transaccion no puede ser menor la fecha real de creacion del prestamo");
            }
        }

        /// <summary>
        /// buscar el prestamo indicado y le aplicara reglas de validacion
        /// </summary>
        private void GetPrestamo()
        {
            var searchParam = new PrestamosGetParams { idPrestamo = this.IdPrestamo };
            var result = new PrestamoBLLC(this.IdLocalidadNegocioLoggedIn, this.LoginName).GetPrestamos(searchParam);
            this.Prestamo = result.FirstOrDefault();
        }

        private IEnumerable<CxCCuota> GetCuotas()
        {
            var cuotas = CxCPrestamo.GetCuotas(this.IdPrestamo);
            return cuotas;
        }

        public void AplicarPago(int idPrestamo, DateTime fecha, PositiveDecimal montoPagado )
        {
            SetValue(idPrestamo, fecha, montoPagado);
            GetPrestamo();
            ValidarPrestamo();
            ValidarFechaMayorEmisionPrestamo();
            var cuotas = GetCuotas();
            decimal totalDeuda = 0;
            var atrasada = false;
            foreach (var cuota in cuotas)
            {
                totalDeuda += cuota.BceGeneral;
                if (cuota.Vencida(this.Fecha))
                {
                    var ultFechaMora = cuota.UltActFechaMora;
                }
            }
        }

        private void ValidarPrestamo()
        {
            if (this.Prestamo == null)
                PagoResult.AddErrorMessage("El prestamo indicado no existe");
        }

        public void  AplicarPago(string prestamoNumero, DateTime fecha, PositiveDecimal montoPagado, int idLocalidadNegocio, string usuario)
        {
            //var prestamoBLLC = new PrestamoBLLC(idLocalidadNegocio, usuario);
            var idPrestamo = CxCPrestamo.GetIdPrestamo(prestamoNumero);
            this.AplicarPago(idPrestamo, fecha, montoPagado);
            
        }
    }
}
