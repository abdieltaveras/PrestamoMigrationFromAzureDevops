using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Globalization;
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


        private decimal MontoAAplicar { get; set; }

        private Prestamo Prestamo { get; set; }

        //private PrestamoBLLC PrestamoBLLC { get; set; }

        public bool OcurrioUnError => PagoResult.ErrorMessages.Any();
        PagoResult PagoResult { get; set; }
        private IEnumerable<CxCCuotaBLL> Cuotas { get; set; }
        private decimal PendientePorAplicar { get; set; }

        public AplicarPagoAPrestamo(int idLocalidadNegocio, string usuario) : base(idLocalidadNegocio, usuario) { }


        private void SetValue(int idprestamo, DateTime fecha, decimal montoPagado)
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


        private IEnumerable<CxCCuotaBLL> GetCuotas()
        {
            var cuotas = CxCPrestamo.GetCuotas(this.IdPrestamo);
            return cuotas;
        }

        public void AplicarPago(int idPrestamo, DateTime fecha, decimal montoPagado)
        {
            SetValue(idPrestamo, fecha, montoPagado);
            GetPrestamo();
            ValidarPrestamo();
            ValidarFechaMayorEmisionPrestamo();
            Cuotas = GetCuotas();
            var deudaCuotas = Cuotas.Sum(c => c.BceGeneral);
            var moras = CalcularMora();
            var otrDebitos = GetOtrosDebitos();
            var totalDeuda = deudaCuotas + moras + otrDebitos;
            try
            {
                var result = (totalDeuda < montoPagado);
                if (result)
                {
                    this.PagoResult.AddErrorMessage("El monto de la deuda es menor que el monto a aplicar");
                    return;
                }

            }
            catch (Exception e)
            {
                throw;
            }

            aplicarPagoADeuda();
        }

        private void aplicarPagoADeuda()
        {

            PendientePorAplicar = MontoAAplicar;
            //var cuotas = this.Cuotas.ToArray();
            //for (int i = 0; i < cuotas.Count() || PendientePorAplicar>0; i++)
            //{

            //    var cuota = cuotas[i];
            //    cuota.BceInteresDelGastoDeCierre = AplicaPago(cuota.BceInteresDelGastoDeCierre);
            //    cuota.BceGastoDeCierre = AplicaPago(cuota.BceGastoDeCierre);
            //    cuota.BceCapital = AplicaPago(cuota.BceCapital);
            //    cuota.BceInteres = AplicaPago(cuota.BceInteres);
            //    var balance = cuota.BceGeneral;
                

            //} 
            foreach (var c in Cuotas)
            {
                if (PendientePorAplicar == 0) break;
                var cuota = c;
                cuota.BceInteresDelGastoDeCierre = AplicaPago(cuota.BceInteresDelGastoDeCierre);
                cuota.BceGastoDeCierre = AplicaPago(c.BceGastoDeCierre);
                cuota.BceCapital = AplicaPago(cuota.BceCapital);
                cuota.BceInteres = AplicaPago(cuota.BceInteres);
            }
        }

        private decimal AplicaPago(decimal bceValue)
        {
            if (bceValue == 0) return 0;

            if (bceValue >= PendientePorAplicar)
            {
                bceValue = bceValue - PendientePorAplicar;
                PendientePorAplicar = 0;
            }
            if (bceValue < PendientePorAplicar)
            {
                PendientePorAplicar -= bceValue;
                bceValue = 0;
            }
            return bceValue;
        }

        private decimal GetOtrosDebitos()
        {
            return 0;
        }

        private decimal CalcularMora()
        {
            foreach (var c in Cuotas) { }
            return 0;
        }

        private void ValidarPrestamo()
        {
            if (this.Prestamo == null)
                PagoResult.AddErrorMessage("El prestamo indicado no existe");
        }

        public void AplicarPago(string prestamoNumero, DateTime fecha, decimal montoPagado, int idLocalidadNegocio, string usuario)
        {
            //var prestamoBLLC = new PrestamoBLLC(idLocalidadNegocio, usuario);
            var idPrestamo = CxCPrestamo.GetIdPrestamo(prestamoNumero);
            this.AplicarPago(idPrestamo, fecha, montoPagado);

        }
    }

}
