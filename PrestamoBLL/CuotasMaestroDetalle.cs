using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PcpUtilidades;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    partial class BLLPrestamo
    {
        
        public void InsUpdDebitoMaestro(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {
            var cuotasMaestros  = cuotas.Cast<CuotaMaestroSinDetallesCxC>();
            var cuotasMaestroDT = cuotasMaestros.ToDataTablePcp<CuotaMaestroSinDetallesCxC>();
            //var cuotasMaestro2 = cuotas.Cast<CuotaMaestro>();
            //var cuotasMaestroDT2 = cuotasMaestro2.ToDataTablePcp<CuotaMaestro>();
            var detallesList = new List<IDetalleDebitoCxC>();
            cuotas.ToList().ForEach(cta => 
                detallesList.AddRange(cta.Detalles));

            //var data = new cuotasParam { cuotasMaestra = cuotasMaestroDT };
            var data2 = new { cuotasMaestra = cuotasMaestroDT };
            var sqpParams = SearchRec.ToSqlParams(data2);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spInsUpdCuotasMaestro", sqpParams);
        }

        internal class CtMSinD : CuotaMaestroSinDetallesCxC
        {
            public string DetalleCargosJson { get; set; }

            private List<DetalleCargoCxC> DetalleCargos { get;  set; } = new List<DetalleCargoCxC>();
            public void ConvertDetallesToJson(string detallesText)
            {
                if (!detallesText.IsNullOrEmpty())
                {
                    var detalles = JsonConvert.DeserializeObject<List<DetalleCargoCxC>>(detallesText);
                    this.DetalleCargos = detalles;
                }
                //detalles.ForEach(det => { 
                //    var detalles = JsonConvert.DeserializeObject<List<DetalleDebitoCxC>>(det);
                //    this.Detalles = detalles;
                //});
            }
            public IEnumerable<DetalleCargoCxC> GetDetallesCargos() => DetalleCargos;
        }

        
        public void InsUpdJsonDebitoMaestroDetalle(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {

            var ct1 = new CtMSinD();
            var detalles = new List<DetalleCargoCxC>();
            detalles.Add(new DetalleCargoCxC());
            detalles.Add(new DetalleCargoCxC());
            ct1.DetalleCargosJson = detalles.ToJSON().ToString();

            var ct2 = new CtMSinD();
            var cts = new List<CtMSinD> { ct1, ct2 };
            var ctsJson = JsonConvert.SerializeObject(cts);
            
            
            var dese2 = JsonConvert.DeserializeObject<List<CtMSinD>>(ctsJson);

            dese2.ForEach(ct => ct.ConvertDetallesToJson(ct.DetalleCargosJson));

            var cuotasMaestros = cuotas.Cast<CuotaMaestroSinDetallesCxC>();
            var cuotasMaestroToJson = JsonConvert.SerializeObject(cuotasMaestros);
            var reverse = JsonConvert.DeserializeObject<List<CuotaMaestroSinDetallesCxC>>(cuotasMaestroToJson);
            var cuotasMaestroDT = cuotasMaestros.ToDataTablePcp<CuotaMaestroSinDetallesCxC>();
            //var cuotasMaestro2 = cuotas.Cast<CuotaMaestro>();
            //var cuotasMaestroDT2 = cuotasMaestro2.ToDataTablePcp<CuotaMaestro>();

            var detallesList = new List<IDetalleDebitoCxC>();
            cuotas.ToList().ForEach(cta =>
                detallesList.AddRange(cta.Detalles));
            var detallesDT = detallesList.ToDataTable();
            //var data = new cuotasParam { cuotasMaestra = cuotasMaestroDT };
            var cuotasYDetallesCargo = new { cuotasMaestra = cuotasMaestroDT, detallesCargo=detallesDT };
            var sqpParams = SearchRec.ToSqlParams(cuotasYDetallesCargo);
            
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spInsUpdCuotasMaestroDetalles", sqpParams);
        }

    }
}
