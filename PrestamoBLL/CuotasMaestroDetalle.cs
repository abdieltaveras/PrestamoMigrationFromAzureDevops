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
using System.Security.Cryptography.Xml;
using System.Runtime.CompilerServices;

namespace PrestamoBLL
{
    public class MaestroDetalleDebitosBLL
    {
        internal static Database DBPrestamo => ConexionDB.DBPrestamo;
        #region StaticBLL
        private static MaestroDetalleDebitosBLL _bll = null;
        public static MaestroDetalleDebitosBLL Instance
        {
            get
            {
                if (_bll == null)
                {
                    _bll = new MaestroDetalleDebitosBLL();
                }
                return _bll;
            }
        }

        #endregion StaticBLL

        
        public void GetCuotasMaestroDetalles(int idNegocio, int idLocalidad, int idPrestamo)
        {
            GetMaestroDetallesDr(idNegocio, idLocalidad, idPrestamo, "CT", 'D');
        }


        private IEnumerable<DebitoViewModel> GetMaestroDetallesDr(int idNegocio, int idLocalidad, int idPrestamo, string codigoTipoTransaccion, char tipoDrCr)
        {
            // buscar las cuotas
            var getParams = new { idNegocio = idNegocio, idLocalidad = idLocalidad, idPrestamo = idPrestamo, codigoTipoTransaccion = codigoTipoTransaccion, tipoDrCr };
            var sqlParams = SearchRec.ToSqlParams(getParams);
            var drResult = BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spGetMaestroYDetallesCxC", sqlParams);

            // crear el maestro sin detalles
            var cuotasMaestras = new List<MaestroDrConDetalles>();
            var cuotaMaestra = new MaestroDrConDetalles();
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

            var detallesCargos = cuotasDetalles
                .GroupBy(item => item.IdTransaccionMaestro);

            var debitos = new List<DebitoViewModel>();
            foreach (var ctaM in cuotasMaestras)
            {
                var items= detallesCargos.Where(dc => dc.Key == ctaM.IdTransaccion).SelectMany(item => item);
                ctaM.SetDetallesCargos(items);
            }

            foreach (var item in cuotasMaestras)
            {
                debitos.Add(DebitoViewModel.Create(item));
            }

            return debitos;
        }
        
        public void InsDebitoMaestroDetalle(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {
            var ctasMaestroCxC = cuotas.Cast<MaestroDrConDetalles>();
            var ctasMaestroCxC2 = cuotas.Cast<IMaestroDebitoConDetallesCxC>();
            var detalles = CreateDetallesDr(cuotas);
            var idReferenciaMaestro = ctasMaestroCxC.FirstOrDefault().IdReferencia;
            var ctasPorReferencia = detalles.Where(cta => cta.IdReferenciaMaestro == idReferenciaMaestro);
            var fcta = ctasMaestroCxC.FirstOrDefault();
            //fcta.DetallesCargos
            var cuotasMaestroDT = ctasMaestroCxC.ToDataTable();
            //cuotasMaestroDT.Columns.Remove("DetallesCargosJson");
            //var columns = cuotasMaestroDT.Columns;
            //var cuotasMaestroDT2 = cuotasMaestro2.ToDataTablePcp<CuotaMaestro>();
            var detallesList = new List<IDetalleDebitoCxC>();

            var detallesDT = detalles.ToDataTable();
            var data2 = new { maestroCxC = cuotasMaestroDT, detallesCargos = detallesDT, crearTablas = 1 };
            var sqlParams = SearchRec.ToSqlParams(data2);
            //BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spTestCreateTmpCxC", sqlParams);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spInsMaestroDetalleDrCxCPrestamo", sqlParams);
        }

        /// <summary>
        /// metodo tan solo de prueba, para probar generar los detalles de los cargos no se usara para realmente exponerlo
        /// </summary>
        /// <param name="cuotas"></param>
        /// <param name="clave"></param>
        public void InsUpdDetallesCargos(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas,int clave)
        {

            if (clave != 8131438) return;
            var detalles = CreateDetallesDr(cuotas).Take(4);
            var detallesCuota = detalles.Cast<DetalleCargoCxC>();
            var idTransaccionMaestro = 1436;
            detallesCuota.ForEach(cta => { cta.IdTransaccionMaestro = idTransaccionMaestro; });
            var data2 = new { detallesCargos = detallesCuota.ToDataTable(), idTransaccionMaestro = idTransaccionMaestro };
            var sqlParams = SearchRec.ToSqlParams(data2);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.SpInsDetallesDrCxC", sqlParams);
        }

        private static List<IDetalleDebitoCxC> CreateDetallesDr(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {
            List<IDetalleDebitoCxC> detalles = new List<IDetalleDebitoCxC>();
            cuotas.ForEach(cta => detalles.AddRange(cta.GetDetallesCargos()));
            return detalles;
        }

        

        public void TestTVInsUpdDebitoMaestro2(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
        {
            var ctas = cuotas.Cast<MaestroDrConDetalles>();
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
