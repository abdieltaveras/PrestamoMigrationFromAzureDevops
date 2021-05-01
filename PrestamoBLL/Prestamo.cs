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
            var resultId = DBPrestamo.ExecSelSP("spInsUpdPrestamo", prestamoParam2);
            //var result = PrestamosDB.ExecSelSP("spInsUpdNegocio", _insUpdParam);
            //idResult = Utils.GetIdFromDataTable(result);
            var  id= Utils.GetIdFromDataTable(resultId);
            return id;
        }

        public IEnumerable<PrestamoSearch> SearchPrestamos(PrestamosSearchParams searchParam)
        {
            //GetValidation(searchParam as BaseGetParams);

            IEnumerable<PrestamoSearch> data = null;
            searchParam.TextToSearch = searchParam.TextToSearch.Trim();
            if (searchParam.SearchType == 1)
            {
                data = BllAcciones.GetData<PrestamoSearch, PrestamosSearchParams>(searchParam, "spBuscarPrestamos", GetValidation);
            }
            else if (searchParam.SearchType == 2)
            {
                data = BllAcciones.GetData<PrestamoSearch, PrestamosSearchParams>(searchParam, "spBuscarPrestamosByCliente", GetValidation);
            }
            else if (searchParam.SearchType == 3)
            {
                data = BllAcciones.GetData<PrestamoSearch, PrestamosSearchParams>(searchParam, "spBuscarPrestamosByGarantia", GetValidation);
            }
            return data;
        }
        public IEnumerable<Prestamo> GetPrestamos(PrestamosGetParams searchParam)
        {
            //GetValidation(searchParam as BaseGetParams);
            searchParam.fechaEmisionRealDesde = searchParam.fechaEmisionRealDesde == null ? InitValues._19000101 : searchParam.fechaEmisionRealDesde;
            searchParam.fechaEmisionRealHasta = searchParam.fechaEmisionRealHasta == null ? InitValues._19000101 : searchParam.fechaEmisionRealDesde;
            var data = BllAcciones.GetData<Prestamo, PrestamosGetParams>(searchParam, "spGetPrestamos", GetValidation);
            return data;
        }

        public PrestamoConDetallesParaUIPrestamo GetPrestamoConDetalleForUIPrestamo(int idPrestamo)
        {
            if (idPrestamo <= 0)
            {
                throw new NullReferenceException("el Id del prestamo enviado es invalido, o la fecha esta nula");
            }
            
            //GetValidation(searchParam as BaseGetParams    );
            var searchRec = SearchRec.ToSqlParams(new { idPrestamo = idPrestamo });
            var dr = DBPrestamo.ExecReaderSelSP("spGetPrestamo", searchRec);
            Prestamo infoPrestamo = new Prestamo();
            InfoClienteDrCr infoCliente = new InfoClienteDrCr();
            while (dr.Read())
            {
                dr.DataReaderToType(out infoPrestamo);
                dr.DataReaderToType(out infoCliente);
            }
            
            List<InfoGarantiaDrCr> infoGarantiasDrCr = new List<InfoGarantiaDrCr>();
            if (dr.NextResult())
            {
                while (dr.Read())
                {
                    InfoGarantiaDrCr infoGarantiaDrCr;
                    dr.DataReaderToType(out infoGarantiaDrCr);
                    infoGarantiasDrCr.Add(infoGarantiaDrCr);
                }
            }
            // las garantia
            // los codeudores
            var PrestamoConDetalle = new PrestamoConDetallesParaUIPrestamo();
            PrestamoConDetalle.infoPrestamo = infoPrestamo;
            PrestamoConDetalle.infoCliente = infoCliente;
            PrestamoConDetalle.infoGarantias = infoGarantiasDrCr;
            return PrestamoConDetalle;
        }

        public PrestamoConDetallesParaCreditosYDebitos GetPrestamoConDetalle(int idPrestamo, DateTime fecha, bool ConvertDetallesGarantiaToJson = false)
        {
            if (idPrestamo <= 0)
            {
                throw new NullReferenceException("el Id del prestamo enviado es invalido, o la fecha esta nula");
            }
            if (fecha.IsNull())
            {
                fecha = DateTime.Now;
            }
            //GetValidation(searchParam as BaseGetParams    );
            var searchRec = SearchRec.ToSqlParams(new { idPrestamo = idPrestamo });
            var dr = DBPrestamo.ExecReaderSelSP("spGetPrestamoConDetalle", searchRec);
            InfoClienteDrCr infoCliente = new InfoClienteDrCr();
            InfoPrestamoDrCr infoPrestamo = new InfoPrestamoDrCr();
            
            while (dr.Read())
            {
                dr.DataReaderToType(out infoPrestamo);
                if (fecha <= infoPrestamo.FechaEmisionReal)
                {
                    throw new InvalidOperationException("La fecha enviada es menor a la fecha de emision del prestamo");
                }
                dr.DataReaderToType(out infoCliente);
            }
            var cuotas = new List<Cuota>();
            if (dr.NextResult())
            {
                while (dr.Read())
                {
                    var cuota = new Cuota();
                    dr.DataReaderToType(out cuota);
                    cuotas.Add(cuota);
                }
            }
            List<InfoGarantiaDrCr> infoGarantiasDrCr = new List<InfoGarantiaDrCr>();
            if (dr.NextResult())
            {
                while (dr.Read())
                {
                    InfoGarantiaDrCr infoGarantiaDrCr; 
                    dr.DataReaderToType(out infoGarantiaDrCr );
                    if (ConvertDetallesGarantiaToJson)
                    {
                        infoGarantiaDrCr.DetallesForJsonConvert();
                    }
                    infoGarantiasDrCr.Add(infoGarantiaDrCr);
                }
            }
            // las garantia
            // los codeudores
            var PrestamoConDetalle = new PrestamoConDetallesParaCreditosYDebitos();
            PrestamoConDetalle.infoPrestamo = infoPrestamo;
            PrestamoConDetalle.infoCliente = infoCliente;
            PrestamoConDetalle.Cuotas = cuotas;
            PrestamoConDetalle.infoGarantias = infoGarantiasDrCr;
            PrestamoConDetalle.InfoDeuda = new InfoDeudaPrestamoDrCr(cuotas, fecha);
            return PrestamoConDetalle;
        }
        public void AnularPrestamo(int idPrestamo)
        {
            throw new NotImplementedException();
        }
    }
}
