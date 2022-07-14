using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using PrestamoEntidades;
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

        public IEnumerable<Cliente> SearchCliente(BuscarClienteParams searchParam)
        {
            return this.Get<Cliente>("spBuscarClientes", searchParam); 
        }
        public IEnumerable<Cliente> ReporteClientes(BaseReporteParams searchParam)
        {
            var param = SearchRec.ToSqlParams(searchParam);
            var resultSet = this.Get<Cliente>("spRptClientes", param);
            return resultSet;
        }
    }
}
