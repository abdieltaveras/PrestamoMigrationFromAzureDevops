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
using System.Reflection;

namespace PrestamoBLL
{
    partial class BLLPrestamo
    {
        
        public void InsUpdDebitoMaestro(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {
            var ctas = cuotas.Cast<CuotaMaestroConDetallesCxC>();
            var fcta = ctas.FirstOrDefault();
            var cuotasMaestroDT = ctas.ToDataTable();
            //cuotasMaestroDT.Columns.Remove("DetallesCargosJson");
            //var columns = cuotasMaestroDT.Columns;
            //var cuotasMaestroDT2 = cuotasMaestro2.ToDataTablePcp<CuotaMaestro>();
            var detallesList = new List<IDetalleDebitoCxC>();
            //var data = new cuotasParam { cuotasMaestra = cuotasMaestroDT };
            var data2 = new { maestroCxC = cuotasMaestroDT };
            var sqlParams = SearchRec.ToSqlParams(data2);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spInsUpdMaestroCxCPrestamo", sqlParams);
        }

        internal class DetalleCargo : DetalleCargoCxC
        {
            public int IdTransaccion { get; set; }
            public int IdTransaccionMaestro { get; set; }
            public Guid IdReferenciaMaestro { get; set; }
            public Guid IdReferenciaDetalle { get; set; }

        }

        public void InsUpdDetallesCargos(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {
            List<DetalleCargo> detalles = new List<DetalleCargo>();
            foreach ( var c in cuotas)
            {
                var cta = c as CuotaMaestroConDetallesCxC;
                var idReferenciaMaestro = Guid.NewGuid();
                // Simulando el stored procedure
                var idTransMaestro = new Random().Next(1000, 10000);
                cta.GetDetallesCargos().ForEach(item =>

                detalles.Add(new DetalleCargo {  
                      Balance = item.Balance, Monto = item.Monto, CodigoCargo = item.CodigoCargo, IdReferenciaDetalle = Guid.NewGuid(), IdReferenciaMaestro= idReferenciaMaestro, IdTransaccionMaestro= idTransMaestro }));

            }
            var data2 = new { detallesCargos = detalles.ToDataTable() };
            var sqlParams = SearchRec.ToSqlParams(data2);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.SpInsUpdDetallesDrCxC", sqlParams);
        }
        internal class CuotaMaestroConDetalleJsonTestConversion : CuotaMaestroSinDetallesCxC
        {
            public string DetalleCargosJson { get; private set; }
            public void SetDetallesCargos(IEnumerable<DetalleCargoCxC> detallesCargos)
            {
                
                this.DetalleCargosJson = JsonConvert.SerializeObject(detallesCargos);
            }
            public IEnumerable<DetalleCargoCxC> GetDetallesCargos()
            {
                var detallesCargos = new List<DetalleCargoCxC>();
                if (!DetalleCargosJson.IsNullOrEmpty())
                {
                    var detalles = JsonConvert.DeserializeObject<List<DetalleCargoCxC>>(DetalleCargosJson);
                    detallesCargos = detalles;
                }
                return detallesCargos;
            }
        }

        public void TestTVInsUpdDebitoMaestro2(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {
            var ctas = cuotas.Cast<CuotaMaestroConDetallesCxC>();
            var cuotasMaestroDT = ctas.ToDataTable();
            cuotasMaestroDT.Columns.Remove("DetallesCargosJson");
            //cuotasMaestroDT.Columns.Remove("Fecha");
            var columns = cuotasMaestroDT.Columns;
            //var cuotasMaestroDT2 = cuotasMaestro2.ToDataTablePcp<CuotaMaestro>();
            var detallesList = new List<IDetalleDebitoCxC>();


            //var data = new cuotasParam { cuotasMaestra = cuotasMaestroDT };
            var data2 = new { cuotasMaestra = cuotasMaestroDT };
            var sqlParams = SearchRec.ToSqlParams(data2);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spTestTVP", sqlParams);
        }
        class testObj { public DateTime Fecha { get; set; } = DateTime.Now; }
        public void TestTVToDataTable()
        {
            var test = new List<testObj>();
            test.Add(new testObj());
            var cuotasMaestroDT = test.ToDataTable();
            var data2 = new { cuotasMaestra = cuotasMaestroDT };
            var sqlParams = SearchRec.ToSqlParams(data2);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spTestTVP", sqlParams);
        }

        public void TryJsonDeserialization(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {

            var ct1 = new CuotaMaestroConDetalleJsonTestConversion() { Fecha=DateTime.Now };
            var detalles = new List<DetalleCargoCxC>();
            detalles.Add(new DetalleCargoCxC() { CodigoCargo="CA", Monto=1000, Balance=1000 });
            detalles.Add(new DetalleCargoCxC() { CodigoCargo = "INT", Monto = 100, Balance = 100 }); ;
            var result = JsonConvert.SerializeObject(detalles);
            ct1.SetDetallesCargos(detalles);
            var ct2 = new CuotaMaestroConDetalleJsonTestConversion();
            var cts = new List<CuotaMaestroConDetalleJsonTestConversion> { ct1, ct2 };
            
            var ctsJson = JsonConvert.SerializeObject(cts);
            
            
            var dese2 = JsonConvert.DeserializeObject<List<CuotaMaestroConDetalleJsonTestConversion>>(ctsJson);

            

            var cuotasMaestros = cuotas.Cast<CuotaMaestroSinDetallesCxC>();
            var cuotasMaestroToJson = JsonConvert.SerializeObject(cuotasMaestros);
            var reverse = JsonConvert.DeserializeObject<List<CuotaMaestroSinDetallesCxC>>(cuotasMaestroToJson);
            var cuotasMaestroDT = cuotasMaestros.ToDataTablePcp<CuotaMaestroSinDetallesCxC>();
            //var cuotasMaestro2 = cuotas.Cast<CuotaMaestro>();
            //var cuotasMaestroDT2 = cuotasMaestro2.ToDataTablePcp<CuotaMaestro>();

            var detallesList = new List<IDetalleDebitoCxC>();
            
            var detallesDT = detallesList.ToDataTable();
            //var data = new cuotasParam { cuotasMaestra = cuotasMaestroDT };
            var cuotasYDetallesCargo = new { cuotasMaestra = cuotasMaestroDT, detallesCargo=detallesDT };
            var sqpParams = SearchRec.ToSqlParams(cuotasYDetallesCargo);
            
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spInsUpdCuotasMaestroDetalles", sqpParams);
        }

    }

    public static class ExtMethod
    {
        public static DataTable ToDataTable<@Type>(this IEnumerable<@Type> list, Dictionary<string, Func<object, object>> convertMap = null, List<string> skipList = null)
        {
            DataTable result = new DataTable();
            PropertyInfo[] properties = typeof(@Type).GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                if (skipList != null)
                {
                    if (skipList.Contains(pi.Name))
                    {
                        continue;
                    }
                }
                var newCol = new DataColumn(pi.Name);

                if (pi.PropertyType.Name == "DateTime")
                {
                    newCol.DataType = System.Type.GetType("System.DateTime");
                }
                result.Columns.Add(newCol);
            }
            foreach (var item in list)
            {
                var newRow = result.NewRow();
                foreach (PropertyInfo pi in properties)
                {
                    if (skipList != null)
                    {
                        if (skipList.Contains(pi.Name))
                        {
                            continue;
                        }
                    }
                    if (convertMap != null)
                    {
                        if (convertMap.ContainsKey(pi.Name))
                        {
                            newRow[pi.Name] = convertMap[pi.Name](pi.GetValue(item, null));
                            continue;
                        }
                    }
                    newRow[pi.Name] = pi.GetValue(item, null);
                }
                result.Rows.Add(newRow);
            }
            return result;
        }
    }
}
