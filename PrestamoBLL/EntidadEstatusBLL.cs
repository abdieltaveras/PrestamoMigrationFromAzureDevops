using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class EntidadEstatusBLL : BaseBLL
    {
        public EntidadEstatusBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName) { }


        public IEnumerable<EntidadEstatus> Get(EntidadEstatusGetParams searchParam)
        {
            return this.Get<EntidadEstatus>("spGetEntidadEstatus", searchParam);
        }
        public int InsUpd(EntidadEstatus insUpdParam)
        {
            return this.InsUpd("spInsUpdEntidadEstatus", insUpdParam);
        }


    }
}
