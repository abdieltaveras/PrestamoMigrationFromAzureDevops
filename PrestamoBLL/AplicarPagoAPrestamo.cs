using DevBox.Core.DAL.SQLServer;
using Microsoft.Extensions.Primitives;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
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
        private decimal TotalPagoCapital { get; set; }
        private decimal TotalPagoInteres { get; set; }

        private decimal TotalPagoGastoDeCierre {get;set;}

        private decimal TotalPagoInteresGastoDeCierre { get; set; }

        private OtrosPagos TotalOtrosPagos { get; set; }

        private List<DetalleCargoCxC> DetallesCargosCXC { get; set; } = new List<DetalleCargoCxC>();

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
                CreateDetalleCargo(cuota);
                var cargos = DetallesCargosCXC;
                AplicaPagoACuota(cuota);
                CreateDetallePagoCuota(cuota);
            }
        }

        private void CreateDetalleCargo(CxCCuotaBLL cuota)
        {
            CreateDetalleCargoPorTipo("MaestroDrConDetalles", "CAP", cuota.idCuota, cuota.Capital, cuota.BceCapital);
            CreateDetalleCargoPorTipo("MaestroDrConDetalles", "INT", cuota.idCuota, cuota.Interes, cuota.BceInteres);
            CreateDetalleCargoPorTipo("MaestroDrConDetalles", "GC", cuota.idCuota, cuota.GastoDeCierre, cuota.BceGastoDeCierre);
            CreateDetalleCargoPorTipo("MaestroDrConDetalles", "INTGC", cuota.idCuota, cuota.InteresDelGastoDeCierre, cuota.BceInteresDelGastoDeCierre);
        }

        private void CreateDetalleCargoPorTipo(string tipoDebito, string codigoCargo, int idTransaccion, decimal monto, decimal balance)
        {
            var detalle = new DetalleCargoCxC { TipoDebito = tipoDebito , CodigoCargo = codigoCargo, IdTransaccion = idTransaccion, Monto = monto , Balance = balance };
            DetallesCargosCXC.Add(detalle);
        }

        private void AplicaPagoACuota(CxCCuotaBLL cuota)
        {
            cuota.BceInteresDelGastoDeCierre = AplicaPago(cuota.BceInteresDelGastoDeCierre);
            cuota.BceGastoDeCierre = AplicaPago(cuota.BceGastoDeCierre);
            cuota.BceCapital = AplicaPago(cuota.BceCapital);
            cuota.BceInteres = AplicaPago(cuota.BceInteres);

        }

        private void CreateDetallePagoCuota(CxCCuotaBLL cuota)
        {

            
        }
        
        private class DetalleCargoCxC
        {

            public string TipoDebito { get; set; }
            public int IdTransaccion { get; set; }
            public string CodigoCargo { get; set; }
            public decimal Monto { get; set; }
            public decimal Balance { get; set; }

            public override string ToString() => $"Tipo {TipoDebito} Cargo {CodigoCargo} Monto Original {Monto} Balance {Balance}";
        }
        private class MontoAplicado
        {
            InfoAccion idCargo { get; set; }
            string TipoDebito { get; set; } //Cuota, Debitos, Moras, Cargo Por Interes, 
            string TipoCargo { get; set; } 
            decimal Monto { get; set; }
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
