using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class EstatusBLL : BaseBLL
    {
        public EstatusBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName) { }


        public IEnumerable<Estatus> Get(EstatusGetParams searchParam)
        {
            return this.Get<Estatus>("spGetEstatus", searchParam);
        }
        public int InsUpd(Estatus insUpdParam)
        {
            return this.InsUpd("spInsUpdEstatus", insUpdParam);
        }

        //public IEnumerable<Estatus> ListEstatus()
        //{
        //    return this.Get<Estatus>("spListEstatus", new PrestamoEntidades.EstatusGetParams());
        //}
    }
}
