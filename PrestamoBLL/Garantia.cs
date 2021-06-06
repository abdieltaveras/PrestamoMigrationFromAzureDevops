using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<GarantiaConMarcaYModelo> SearchGarantias(string search)
        {
            var searchParams = new SearchRec();
            searchParams.AddParam("search", search);
            //searchParams.AddParam("usuario", searchParam.Usuario);
            var result = DBPrestamo.ExecReaderSelSP<GarantiaConMarcaYModelo>("spBuscarGarantias", searchParams.ToSqlParams());
            return result;
        }
        public IEnumerable<GarantiaConMarcaYModelo> GetGarantias(GarantiaGetParams searchParam)
        {
            GetValidation(searchParam as BaseGetParams);
            return BllAcciones.GetData<GarantiaConMarcaYModelo, GarantiaGetParams>(searchParam, "spGetGarantias", GetValidation);
        }

        public IEnumerable<GarantiaConMarcaYModeloYPrestamos> SearchGarantiaConDetallesDePrestamos(BuscarGarantiaParams searchParam)
        {
            
            var search_Param = SearchRec.ToSqlParams(searchParam);
            var dr = DBPrestamo.ExecReaderSelSP("spBuscarGarantiasConPrestamos", search_Param);
            var GarantiaConPrestamo = new GarantiaConMarcaYModeloYPrestamos();
            var GarantiasConPrestamos = new List<GarantiaConMarcaYModeloYPrestamos>();
            while (dr.Read())
            {
                dr.DataReaderToType(out GarantiaConPrestamo);
                GarantiasConPrestamos.Add(GarantiaConPrestamo);
            }

            var dataGarPrestamo = new dataGarantiaPrestamo();
            if (dr.NextResult())
            {
                while (dr.Read())
                {
                    dr.DataReaderToType(out dataGarPrestamo);
                    var index = GarantiasConPrestamos.FindIndex(data => data.IdGarantia == dataGarPrestamo.idGarantia);
                    if (index >= 0)
                    {
                        GarantiasConPrestamos[index].IdPrestamos.Add(dataGarPrestamo.idPrestamo);
                        GarantiasConPrestamos[index].PrestamosNumero.Add(dataGarPrestamo.prestamoNumero);
                    }
                }
            }
            return GarantiasConPrestamos;
        }

        internal class dataGarantiaPrestamo
        {
            public int idGarantia { get; set; }
            public int idPrestamo { get; set; }
            public string prestamoNumero { get; set; }
        }
        public void InsUpdGarantia(Garantia insUpdParam)
        {
            BllAcciones.InsUpdData<Garantia>(insUpdParam, "spInsUpdGarantias");
        }

        public bool GarantiasTienenPrestamosVigentes(IEnumerable<int> idGarantias)
        {
            
            var result = IdGarantiasConPrestamos(idGarantias);
            return (result!=null);
        }

        public IEnumerable<Garantia> GetGarantiasByPrestamo(int idPrestamo)
        {
            var searchParam = SearchRec.ToSqlParams(new { idPrestamo = idPrestamo });
            var result = DBPrestamo.ExecReaderSelSP<Garantia>("spGetGarantiasByprestamo", searchParam);
            return result;
        }
        public IEnumerable<GarantiasConPrestamo> IdGarantiasConPrestamos(IEnumerable<int> idGarantias)
        {
            var tpIdGarantias = new List<tpIdGarantia>();
            var result = idGarantias.Select(data => new tpIdGarantia() { IdGarantia = data });
            //tpIdGarantias.Add(new tpIdGarantia { IdGarantia = idGarantias[ });
            //tpIdGarantias.Add(new tpIdGarantia { IdGarantia = 3 });
            var idGarantiasDataTable = result.ToDataTable();
            var searchParam = SearchRec.ToSqlParams(new { idGarantias = idGarantiasDataTable });
            var result2 = DBPrestamo.ExecReaderSelSP<GarantiasConPrestamo>("spGarantiasConPrestamosVigentes", searchParam);
            return result2;
        }
    }
    public class tpIdGarantia
    {
        public int IdGarantia { get; set; }
    }

    public class GarantiasConPrestamo
    { 
        public int idGarantia { get; internal set; }
        public int idPrestamo { get; internal set; }
        
        public int prestamoNumero { get; internal set; }
    }
}
