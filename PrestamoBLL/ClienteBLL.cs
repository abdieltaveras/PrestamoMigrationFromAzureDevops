using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBLL
{

    public class ClienteBLL : BaseBLL
    {
        public ClienteBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName) { }


        public IEnumerable<Cliente> GetClientes(ClienteGetParams searchParam, bool convertToObj, string directorioDeImagen = "")
        {
   
            SetUsuario(searchParam);
            var spName = "spGetClientes";
            return this.Get<Cliente>(spName, searchParam);
        }


        public int InsUpdCliente(Cliente insUpdParam)
        {
           
            SetIdLocalidadNegocioAndUsuario(insUpdParam);
            insUpdParam.RemoveAllButNumber();
            var imagesToRemove = insUpdParam.ImagenesObj.Where(item => item.Quitar).ToList();
            imagesToRemove.ForEach(item => insUpdParam.ImagenesObj.Remove(item));
            insUpdParam.ConvertObjToJson();
            var spName = "spInsUpdCliente";
            return this.InsUpd(spName, insUpdParam);
        }

        public bool DeleteDivisionTerritorial(int idRegistro, string motivo)
        {
             return this.SoftDeleteUsingCommonSP(idRegistro, "IdCliente",motivo, "tblClientes");
        }

        public void AnularClientes(ClienteDelParams delParam)
        {
            DBPrestamo.ExecSelSP("spDelCliente", SearchRec.ToSqlParams(delParam));
        }

        public IEnumerable<Cliente> SearchCliente(int Option, string SearchText)
        {
            return  this.Get<Cliente>("spBuscarClientes", new { TextToSearch = SearchText }); 
        }
        public IEnumerable<Cliente> ReporteClientes(BaseReporteParams searchParam)
        {
            var param = SearchRec.ToSqlParams(searchParam);
            var resultSet = this.Get<Cliente>("spRptClientes", param);
            return resultSet;
        }
        public IEnumerable<Cliente> SearchClientesByProperties(eOpcionesSearchCliente Option, string Value)
        {
            bool isDefined = Enum.IsDefined(typeof(eOpcionesSearchCliente), Option);
            ClienteGetParams param = new ClienteGetParams();
            if (isDefined)
            {
                eOpcionesSearchCliente enumOp = Option;
                switch (enumOp)
                {
                    case eOpcionesSearchCliente.NoIdentificacion:
                        param.NoIdentificacion = Value;
                        break;
                    case eOpcionesSearchCliente.Nombres:
                        param.Nombres = Value;
                        break;
                    case eOpcionesSearchCliente.Apellidos:
                        param.Apellidos = Value;
                        break;
                    default:
                        break;
                }
            }
            SetUsuario(param);
            var spName = "spGetClientes";
            return this.Get<Cliente>(spName, param);
        }
        public IEnumerable<Cliente> SearchClienteByColumn(string SearchText, string Table, string Column, string OrderBy = "") 
        {
            return this.Get<Cliente>("spSearchTableByColunm",new
            {
                SearchText,
                Table,
                Column,
                OrderBy
            });
            //return this.Get<Cliente>("spSearchTableByColunm", SearchRec.ToSqlParams(new
            //{
            //    SearchText = SearchText,
            //    Tabla = Tabla,
            //    Columna = Columna,
            //    OrderBy = OrderBy
            //}));
        }
        //public static IEnumerable<> SearchClienteByColumn<TResult>(string SearchText, string Tabla, string Columna, string OrderBy = "") where TResult : class
        //{
        //    return DBPrestamo.ExecReaderSelSP<TResult>("spSearchTableByColunm", SearchRec.ToSqlParams(new
        //    {
        //        SearchText = SearchText,
        //        Tabla = Tabla,
        //        Columna = Columna,
        //        OrderBy = OrderBy
        //    }));
        //}
    }

}
