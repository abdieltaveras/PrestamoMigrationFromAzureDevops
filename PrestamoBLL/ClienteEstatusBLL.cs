using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class ClienteEstatusBLL : BaseBLL
    {
        public ClienteEstatusBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName) { }


        public IEnumerable<ClienteEstatusGet> Get(ClienteEstatusGetParams searchParam)
        {
            return this.Get<ClienteEstatusGet>("spGetClienteEstatus", searchParam);
        }
        public int InsUpd(ClienteEstatus insUpdParam)
        {
            return this.InsUpd("spInsUpdClienteEstatus", insUpdParam);
        }

        //public IEnumerable<Estatus> ListEstatus()
        //{
        //    return this.Get<Estatus>("spListEstatus", new PrestamoEntidades.EstatusGetParams());
        //}
    }
}
