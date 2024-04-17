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
