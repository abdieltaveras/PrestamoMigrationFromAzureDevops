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
    public class MaestroDetallerDr
    {
        public static void GetCuotasMaestroDetalles(int idNegocio, int idLocalidad, int idPrestamo)
        {
            GetMaestroDetallesDr(idNegocio, idLocalidad, idPrestamo, "CT", 'D');
        }


        class CuotaConDetalles : CuotaMaestroSinDetallesCxC
        {
            public IEnumerable<DetalleCargoCxC> Detalles { get; set; }
        }
        public class CuotasConDetallesDto 
        { 
            IEnumerable<CuotaConDetalles> CuotasConDetalles { get; set; }
            internal static void CreateMaestroDetalleCuotas(IEnumerable<CuotaMaestroSinDetallesCxC> cuotasMaestro, IEnumerable<DetalleCargoCxC> cetalles)
            {
                var cuotasConDetalles = new List<CuotaConDetalles>();
                foreach (var item in cuotasMaestro)
                {
                    cuotasConDetalles.Add()
                }
            }
            //public override string ToString()
            //{

                //string detallesCargos = string.Empty;
                //this.Detalles.ForEach(cargo =>
                //{
                //    detallesCargos = detallesCargos + $"{CodigosCargosDebitos.GetNombreCargo(cargo.CodigoCargo)+ ","} {cargo.Monto} {cargo.Balance}";
                //});
                //return detallesCargos
            //}
        }

        private static void GetMaestroDetallesDr(int idNegocio, int idLocalidad, int idPrestamo, string codigoTipoTransaccion, char tipoDrCr)
        {


            var getParams = new { idNegocio = idNegocio, idLocalidad = idLocalidad, idPrestamo = idPrestamo, codigoTipoTransaccion = codigoTipoTransaccion, tipoDrCr };
            var sqlParams = SearchRec.ToSqlParams(getParams);
            var drResult = BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spGetMaestroYDetallesCxC", sqlParams);

            var cuotasMaestras = new List<CuotaMaestroSinDetallesCxC>();
            var cuotaMaestra = new CuotaMaestroConDetallesCxC();
            while (drResult.Read())
            {
                drResult.DataReaderToType(out cuotaMaestra);
                cuotasMaestras.Add(cuotaMaestra);
            }
            var cuotasDetalles = new List<DetalleCargoCxC>();
            var detalle = new DetalleCargoCxC();
            while (drResult.NextResult())
            {
                while (drResult.Read())
                {
                    drResult.DataReaderToType(out detalle);
                    cuotasDetalles.Add(detalle);
                }
            }


            cuotasMaestras.ForEach(ct=> 
            ct.IdTransaccion)
            {

            }
            var result = 1;

        }
    }
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

        public void InsUpdDebitoMaestroDetalle(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {
            var ctasMaestroCxC = cuotas.Cast<CuotaMaestroConDetallesCxC>();
            List<DetalleCargo> detalles = CreateDetallesDr(cuotas);
            var idReferenciaMaestro = ctasMaestroCxC.FirstOrDefault().IdReferencia;
            var ctasPorReferencia = detalles.Where(cta => cta.IdReferenciaMaestro == idReferenciaMaestro);
            var fcta = ctasMaestroCxC.FirstOrDefault();
            var cuotasMaestroDT = ctasMaestroCxC.ToDataTable();
            //cuotasMaestroDT.Columns.Remove("DetallesCargosJson");
            //var columns = cuotasMaestroDT.Columns;
            //var cuotasMaestroDT2 = cuotasMaestro2.ToDataTablePcp<CuotaMaestro>();
            var detallesList = new List<IDetalleDebitoCxC>();
            //var data = new cuotasParam { cuotasMaestra = cuotasMaestroDT };
            var data2 = new { maestroCxC = cuotasMaestroDT, detallesCargos= detalles.ToDataTable()};
            var sqlParams = SearchRec.ToSqlParams(data2);
            //BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spTestCreateTmpCxC", sqlParams);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spInsUpdMaestroDetalleDrCxCPrestamo", sqlParams);
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
            List<DetalleCargo> detalles = CreateDetallesDr(cuotas);
            var data2 = new { detallesCargos = detalles.ToDataTable() };
            var sqlParams = SearchRec.ToSqlParams(data2);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.SpInsUpdDetallesDrCxC", sqlParams);
        }

        private static List<DetalleCargo> CreateDetallesDr(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {
            List<DetalleCargo> detalles = new List<DetalleCargo>();
            foreach (var c in cuotas)
            {
                var cta = c as CuotaMaestroConDetallesCxC;
                // Simulando el stored procedure
                cta.GetDetallesCargos().ForEach(item =>

                detalles.Add(new DetalleCargo
                {
                    Balance = item.Balance,
                    Monto = item.Monto,
                    CodigoCargo = item.CodigoCargo,
                    IdReferenciaDetalle = Guid.NewGuid(),
                    IdReferenciaMaestro = cta.IdReferencia,

                }));

            }

            return detalles;
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
