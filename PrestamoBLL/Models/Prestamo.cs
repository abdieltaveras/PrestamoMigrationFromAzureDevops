using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrestamoBLL.Models
{
    internal class PrestamoInsUpdParam : Prestamo
    {
        public IEnumerable<IMaestroDebitoConDetallesCxC> CargosMaestro = new List<IMaestroDebitoConDetallesCxC>();


        public IEnumerable<IDetalleDebitoCxC> CargosDetalles = new List<IDetalleDebitoCxC>();

        //public PrestamoInsUpdParam(IEnumerable<CxCCuotaForSqlType> cuotas)
        //{
        //    this._CuotasList = cuotas;
        //}

        //todo fix
        public PrestamoInsUpdParam()
        {

        }

        public DataTable GarantiasDT => this.IdGarantias.Select(gar => new { idGarantia = gar }).ToDataTable();
        // this._Garantias.ToDataTable();
        public DataTable CodeudoresDT => this.IdCodeudores.Select(cod => new { idCodeudor = cod }).ToDataTable();

        public DataTable CargosMaestrosDT => this.CargosMaestro.ToDataTable();

        public DataTable CargosDetallesDT => this.CargosDetalles.ToDataTable();
    }
    
}
