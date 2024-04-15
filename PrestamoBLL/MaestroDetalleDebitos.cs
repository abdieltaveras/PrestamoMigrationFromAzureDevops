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
using PrestamoBLL.Models;
using System.Runtime.InteropServices;
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


        public IEnumerable<ICxCDebitoPrestamo> GetCuotasMaestroDetalles(int idNegocio, int idLocalidad, int idPrestamo)
        {
            var result = GetMaestroDetallesDr(idNegocio, idLocalidad, idPrestamo, "CT", 'D');
            return result;
        }


        private IEnumerable<DebitoPrestamoConDetallesForBLL> GetMaestroDetallesDr(int idNegocio, int idLocalidad, int idPrestamo, string codigoTipoTransaccion, char tipoDrCr)
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
            var result = ConvertToDebitoPrestamoViewModel(cuotasMaestras, cuotasDetalles);
            return result;
        }
        private static IEnumerable<DebitoPrestamoConDetallesForBLL> ConvertToDebitoPrestamoViewModel(IEnumerable<MaestroDrConDetalles> cuotasMaestras)

        {

            List<IDetalleDebitoCxC> detallesCargos = new List<IDetalleDebitoCxC>();
            cuotasMaestras.ForEach(cta =>
                {
                    detallesCargos.AddRange(cta.GetDetallesCargos());
                }
            );
                
            var debitosViewModel = new List<DebitoPrestamoConDetallesForBLL>();
            foreach (var ctaM in cuotasMaestras)
            {
                var items = detallesCargos.Where(dc => dc.IdTransaccionMaestro == ctaM.IdTransaccion);
                ctaM.SetDetallesCargos(items);
            }

            foreach (var item in cuotasMaestras)
            {
                debitosViewModel.Add(DebitoPrestamoConDetallesForBLL.Create(item));
            }

            return debitosViewModel;
        }

        private static IEnumerable<DebitoPrestamoConDetallesForBLL> ConvertToDebitoPrestamoViewModel(List<MaestroDrConDetalles> cuotasMaestras, IEnumerable<DetalleCargoCxC> cuotasDetalles)
        {
            var detallesCargos = cuotasDetalles
                .GroupBy(item => item.IdTransaccionMaestro);

            var debitosViewModel = new List<DebitoPrestamoConDetallesForBLL>();
            foreach (var ctaM in cuotasMaestras)
            {
                var items = detallesCargos.Where(dc => dc.Key == ctaM.IdTransaccion).SelectMany(item => item);
                ctaM.SetDetallesCargos(items);
            }

            foreach (var item in cuotasMaestras)
            {
                debitosViewModel.Add(DebitoPrestamoConDetallesForBLL.Create(item));
            }

            return debitosViewModel;
        }


        /// <summary>
        /// crear el maestros y sus detalles pero solo a nivel de memoria
        /// </summary>
        /// <param name="idPrestamo"></param>
        /// <param name="infGenCuotas"></param>
        public IEnumerable<IMaestroDebitoConDetallesCxC> CreateCuotasPrestamoInMemory(int idPrestamo, IInfoGeneradorCuotas infGenCuotas)
        {
            var result = GeneradorDeCuotas.CreateCuotasMaestroDetalle(idPrestamo, infGenCuotas);
            return result;
        }

        public void InsCuotasPrestamos(int idPrestamo, IInfoGeneradorCuotas infGenCuotas)
        {
            var cuotas = CreateCuotasPrestamoInMemory(idPrestamo, infGenCuotas);
            InsDebitoMaestroDetalle(cuotas);
        }

        /// <summary>
        /// recibiendo el listado de cargos generar el maestro y detalle de los mismos por separado
        /// </summary>
        /// <param name="debitos"></param>
        /// <returns></returns>
        internal static DrMaestroDetalle CreateDrMaestroYDetalles(IEnumerable<IMaestroDebitoConDetallesCxC> debitos)
        {
            var maestro = debitos.Cast<MaestroDrConDetalles>();
            var detalles = CreateDetallesDr(debitos);
            var result = new DrMaestroDetalle(maestro, detalles);
            return result;

        }
        internal void InsDebitoMaestroDetalle(IEnumerable<IMaestroDebitoConDetallesCxC> debitos)
        {
            var result = CreateDrMaestroYDetalles(debitos);
            var ctasMaestroCxC = result.MaestrosDr;
            var detalles = result.DetallesDr;
            var dataParams = new { maestroCxC = ctasMaestroCxC.ToDataTable(), detallesCargos = detalles.ToDataTable(), crearTablas = 1 };
            var sqlParams = SearchRec.ToSqlParams(dataParams);
            //BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spTestCreateTmpCxC", sqlParams);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.spInsMaestroDetalleDrCxCPrestamo", sqlParams);
            //var ctasMaestroCxC2 = debitos.Cast<IMaestroDebitoConDetallesCxC>();
            //var cuotasMaestroDT = ctasMaestroCxC.ToDataTable();
            //var detallesDT = detalles.ToDataTable();
        }


        public IEnumerable<DebitoPrestamoConDetallesViewModel> ProyectarCuotasPrestamos(int idPrestamo, IInfoGeneradorCuotas infGenCuotas)
        {
            var cuotas1 = CreateCuotasPrestamoInMemory(idPrestamo, infGenCuotas);

            var cuotas = cuotas1.Cast<MaestroDrConDetalles>();
            //var detalles = cuotas.ForEach(cuota =>
            //{
            //    var detalles = cuota.GetDetallesCargos();

            //});

            var result = ConvertToDebitoPrestamoViewModel(cuotas);
            return result;
                
        }

        /// <summary>
        /// metodo tan solo de prueba, para probar generar los detalles de los cargos no se usara para realmente exponerlo
        /// </summary>
        /// <param name="cuotas"></param>
        /// <param name="clave"></param>
        /// 

        public void InsUpdDetallesCargos(int idPrestamo, IInfoGeneradorCuotas infGenCuotas, int clave)
        {
            if (clave != 8131438) return;
            var cuotas = CreateCuotasPrestamoInMemory(idPrestamo, infGenCuotas);
            var detalles = CreateDetallesDr(cuotas).Take(4);
            var detallesCuota = detalles.Cast<DetalleCargoCxC>();
            var idTransaccionMaestro = 1436;
            detallesCuota.ForEach(cta => { cta.IdTransaccionMaestro = idTransaccionMaestro; });
            var data2 = new { detallesCargos = detallesCuota.ToDataTable(), idTransaccionMaestro = idTransaccionMaestro };
            var sqlParams = SearchRec.ToSqlParams(data2);
            BLLPrestamo.DBPrestamo.ExecReaderSelSP("dbo.SpInsDetallesDrCxC", sqlParams);
        }

        internal static List<IDetalleDebitoCxC> CreateDetallesDr(IEnumerable<IMaestroDebitoConDetallesCxC> cuotas)
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
        /// <summary>
        /// se hizo una mejora en el codigo para corregir una falla cuando se envia un table type que contiene un campo DateTime
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="list"></param>
        /// <param name="convertMap"></param>
        /// <param name="skipList"></param>
        /// <returns></returns>
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
