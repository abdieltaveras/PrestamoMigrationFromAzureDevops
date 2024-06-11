using DevBox.Core.Classes.Utils;
using Newtonsoft.Json;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PrestamoBLL.Models;

namespace PrestamoBLL
{
    internal abstract class DrCrPrestamosBuilder
    {

        private List<IDetalleDebitoCxC> Detalles { get; set; } = new List<IDetalleDebitoCxC>();
        private Guid IdReferenciaMaestro { get; set; }

        protected MaestroDrConDetalles Cargo { get; private set; }

        
        protected void CreateInstaceCuotaMaestra(DateTime fecha, int numero)
        {
            var cargo = new MaestroDrConDetalles
            {
                Fecha = fecha,
                // revisar este numero de transaccion como se va a generar
                NumeroTransaccion = numero.ToString(),
                // esto debe estar en la imprelmentacion mas concreta
                CodigoTipoTransaccion = GetTipoTransaccion()
            };
            IdReferenciaMaestro = cargo.IdReferencia;
            AddCargos();
            cargo.SetDetallesCargos(this.Detalles);
            this.Cargo = cargo;
        }

        protected abstract string GetTipoTransaccion();
        


        internal abstract void AddCargos();

        protected internal void AddCargo(string codigoCargo, decimal monto)
        {
            if (monto <= 0) return;
            var cargo = new DetalleCargoCxC
            {
                CodigoCargo = codigoCargo,
                Monto = monto,
                Balance = monto,
                IdReferenciaMaestro = this.IdReferenciaMaestro,
            };
            this.Detalles.Add(cargo);
        }

        //internal IMaestroDebitoConDetallesCxC CreateCuotaAndDetalle(DateTime fecha, int numero, decimal capital, decimal interes, decimal gastoDeCierre, decimal interesDelGastoDeCierre)
        //{
        //    CreateInstaceCuotaMaestra(fecha, numero);
        //    AddCargos();
        //    Cargo.SetDetallesCargos(this.Detalles);
        //}

    }

    
    internal class CargoGastoCierreSinFinanciamientoBuilder : DrCrPrestamosBuilder
    {
      
        Decimal MontoGastoDeCierre { get; set; }
        internal CargoGastoCierreSinFinanciamientoBuilder()
        {
        
        }
        internal IMaestroDebitoConDetallesCxC CreateCargoAndDetalle(DateTime fecha, decimal montoGastoDeCierre)
        {
            if (montoGastoDeCierre == 0) throw new NullReferenceException("valor no puede ser menor o igual a cero");

            this.MontoGastoDeCierre = montoGastoDeCierre;
            base.CreateInstaceCuotaMaestra(fecha, 000);
            return this.Cargo;

        }
        internal override void AddCargos()
        {
            AddCargo(CodigosCargosDebitosReservados.GastoDeCierre, MontoGastoDeCierre);
        }

        protected override string GetTipoTransaccion() => CodigosTiposTransaccionCxC.CargoInterno;
    }




    internal class CuotaPrestamoBuilder
    {
        private List<IDetalleDebitoCxC> Detalles { get; set; } = new List<IDetalleDebitoCxC>();
        private Guid IdReferenciaMaestro { get; set; }

        internal IMaestroDebitoConDetallesCxC CreateCuotaAndDetalle(DateTime fecha, int numero, decimal capital, decimal interes, decimal gastoDeCierre, decimal interesDelGastoDeCierre)
        {
            var cuota = new MaestroDrConDetalles
            {
                Fecha = fecha,
                NumeroTransaccion = numero.ToString(),
                CodigoTipoTransaccion = CodigosTiposTransaccionCxC.Cuota,

            };
            this.IdReferenciaMaestro = cuota.IdReferencia;

            AddCargo(CodigosCargosDebitosReservados.Capital, capital);
            AddCargo(CodigosCargosDebitosReservados.Interes, interes);
            AddCargo(CodigosCargosDebitosReservados.GastoDeCierre, gastoDeCierre);
            AddCargo(CodigosCargosDebitosReservados.InteresDelGastoDeCierre, interesDelGastoDeCierre);
            cuota.SetDetallesCargos(this.Detalles);
            return cuota;
        }

        private void AddCargo(string codigoCargo, decimal monto)
        {
            if (monto <= 0) return;
            var cargo = new DetalleCargoCxC
            {
                CodigoCargo = codigoCargo,
                Monto = monto,
                Balance = monto,
                IdReferenciaMaestro = this.IdReferenciaMaestro,
            };
            this.Detalles.Add(cargo);
        }
    }
}
