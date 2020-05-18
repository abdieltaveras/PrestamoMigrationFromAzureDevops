using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public int InsUpdPrestamo(Prestamo prestamo)
        {
            PrestamoBuilder prToBuild = new PrestamoBuilder(prestamo);
            var result = prToBuild.Build();
            
            var prestamoParam2 = SearchRec.ToSqlParams(result);
            //var resultId = PrestamosDB.ExecSelSP("spInsUpdPrestamo",prestamoParam);
            var resultId = PrestamosDB.ExecSelSP("spInsUpdPrestamo", prestamoParam2);
            //var result = PrestamosDB.ExecSelSP("spInsUpdNegocio", _insUpdParam);
            //idResult = Utils.GetIdFromDataTable(result);
            var  id= Utils.GetIdFromDataTable(resultId);
            return id;
        }

        public IEnumerable<Prestamo> GetPrestamos(PrestamoGetParam searchParam)
        {
            //GetValidation(searchParam as BaseGetParams);
            
            var data = BllAcciones.GetData<Prestamo, PrestamoGetParam>(searchParam, "spGetPrestamos", GetValidation);
            return data;
        }

        public IPrestamoConDetallesParaCreditosyDebitos GetPrestamoConDetalle(int idPrestamo)
        {
            //GetValidation(searchParam as BaseGetParams    );
            var searchRec = SearchRec.ToSqlParams(new { idPrestamo = idPrestamo });
            var dr = PrestamosDB.ExecReaderSelSP("spGetPrestamoConDetalle", searchRec);
            IInfoClienteDrCr infoCliente = new InfoClienteDrCr();
            IInfoPrestamoDrCr infoPrestamo = new InfoPrestamoDrCr();
            IInfoDeudaPrestamo infoDeuda = new InfoDeudaPrestamo();
            IInfoGarantia infoGarantia = null;
            while (dr.Read())
            {
                //var valor = dr["idPrestamo"];
                //var obj = Convert.ToInt32(valor == null ? 0 : Convert.ToInt32(valor));
                //infoCliente = DataRowToInfoCliente(dr);
                infoCliente = DataRowToInfo(dr, infoCliente);
                //infoPrestamo = DataRowToInfoPrestamoDrCr(dr);
                //infoGarantia = DataRowToInfoGarantia(dr);
                //infoDeuda = DataRowToInfoDeuda(dr);
            }

            //tomar los datos de infoprestamo
            //Tomar los datos de infocliente
            // tomar los datos de infoGarantia
            // tomar los datos de infoDeuda

            var PrestamoConDetalle = new PrestamoConDetallesParaCreditosYDebitos();
            PrestamoConDetalle.infoPrestamo = infoPrestamo;
            PrestamoConDetalle.infoCliente = infoCliente;
            PrestamoConDetalle.InfoDeuda = infoDeuda;
            PrestamoConDetalle.infoGarantia = infoGarantia;
            return PrestamoConDetalle;

            
        }

        private @type DataRowToInfo<@type>(IDataReader dr, @type obj)
        {
            foreach (var pi in obj.GetType().GetProperties())
            {
                if (pi.CanWrite)
                {
                    string propName = pi.Name;
                    if (dr.HasColumn(propName))
                    {
                        var propValue = dr[propName];
                        pi.SetValue(obj, Convert.ChangeType(propValue, pi.PropertyType), null);
                    }
                }
            }
            return obj;
        }

        private IInfoDeudaPrestamo DataRowToInfoDeuda(IDataReader dr)
        {
            var info = new InfoDeudaPrestamo();
            foreach (var pi in info.GetType().GetProperties())
            {
                if (pi.CanWrite)
                {
                    string propName = pi.Name;
                    if (dr.HasColumn(propName))
                    {
                        var propValue = dr[propName];
                        pi.SetValue(info, Convert.ChangeType(propValue, pi.PropertyType), null);
                    }
                }
            }
            return info;
        }

        private IInfoGarantia DataRowToInfoGarantia(IDataReader dr)
        {
            return null;
        }

        private static IInfoPrestamoDrCr DataRowToInfoPrestamoDrCr(IDataReader dr)
        {
            var info = new InfoPrestamoDrCr();
            foreach (var pi in info.GetType().GetProperties())
            {
                if (pi.CanWrite)
                {
                    string propName = pi.Name;
                    if (dr.HasColumn(propName))
                    {
                        var propValue = dr[propName];
                        pi.SetValue(info, Convert.ChangeType(propValue, pi.PropertyType), null);
                    }
                }
            }
            return info;
        }

        private static IInfoClienteDrCr DataRowToInfoCliente(IDataReader dr)
        {
            var info = new InfoClienteDrCr();
            foreach (var pi in info.GetType().GetProperties())
            {
                if (pi.CanWrite)
                {
                    string propName = pi.Name;
                    if (dr.HasColumn(propName))
                    {
                        var propValue = dr[propName];
                        pi.SetValue(info, Convert.ChangeType(propValue, pi.PropertyType), null);
                    }
                }
            }
            return info;
        }
    }
}
