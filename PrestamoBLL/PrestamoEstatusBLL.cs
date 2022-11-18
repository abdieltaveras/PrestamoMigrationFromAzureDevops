using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class PrestamoEstatusBLL : BaseBLL
    {
        public PrestamoEstatusBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName) { }


        public IEnumerable<PrestamoEstatusGet> Get(PrestamoEstatusGetParams searchParam)
        {
            return this.Get<PrestamoEstatusGet>("spGetPrestamoEstatus", searchParam);
        }
        public int InsUpd(PrestamoEstatus insUpdParam)
        {
            return this.InsUpd("spInsUpdPrestamoEstatus", insUpdParam);
        }

        //public IEnumerable<Estatus> ListEstatus()
        //{
        //    return this.Get<Estatus>("spListEstatus", new PrestamoEntidades.EstatusGetParams());
        //}
    }
}
