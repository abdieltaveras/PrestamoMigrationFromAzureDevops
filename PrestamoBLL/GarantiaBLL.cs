using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class GarantiaBLL:BaseBLL
    {
        public GarantiaBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName)
        {
        }

        public IEnumerable<GarantiaConMarcaYModelo> SearchGarantias(string search)
        {
            var searchParams = new SearchRec();
            searchParams.AddParam("search", search);
            //searchParams.AddParam("usuario", searchParam.Users);
            var result = this.Get<GarantiaConMarcaYModelo>("spBuscarGarantias", searchParams.ToSqlParams());
            return result;
        }
        public IEnumerable<GarantiaConMarcaYModelo> GetGarantias(GarantiaGetParams searchParam)
        {
            //GetValidation(searchParam as BaseGetParams);
            return this.Get<GarantiaConMarcaYModelo>( "spGetGarantias", searchParam);
        }

        public IEnumerable<GarantiaConMarcaYModeloYPrestamos> SearchGarantiaConDetallesDePrestamos(BuscarGarantiaParams searchParam)
        {
            // esto es para que no lo incluya
            //searchParam.Anulado = null;
            //var search_Param = SearchRec.ToSqlParams(searchParam);
            var search_Param = BLLPrestamo.SearchRecForGet(searchParam, new ImplicitParams { IncluirAnulados = 0, IncluirBorrados = 0 });
            var dr = DBPrestamo.ExecReaderSelSP("spBuscarGarantiasConPrestamos", search_Param); // aqui explota Luis
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
            this.InsUpd("spInsUpdGarantias",insUpdParam);
        }

        public bool GarantiasTienenPrestamosVigentes(IEnumerable<int> idGarantias)
        {
            
            var result = GarantiasConPrestamos(idGarantias);
            return (result!=null);
        }

        public IEnumerable<GarantiaConMarcaYModelo> GetGarantiasByPrestamo(int idPrestamo)
        {
            var searchParam = SearchRec.ToSqlParams(new { idPrestamo = idPrestamo });
            var result = DBPrestamo.ExecReaderSelSP<GarantiaConMarcaYModelo>("spGetGarantiasByprestamo", searchParam);
            return result;
        }
        public IEnumerable<GarantiasConPrestamo> GarantiasConPrestamos(IEnumerable<int> idGarantias)
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

    
}
