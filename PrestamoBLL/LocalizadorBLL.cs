using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class LocalizadorBLL : BaseBLL
    {
        public LocalizadorBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName) { }


        public IEnumerable<Localizador> Get(LocalizadorGetParams searchParam)
        {
            return this.Get<Localizador>("spGetLocalizadores", searchParam);
        }
        public int InsUpd(Localizador insUpdParam)
        {
            return this.InsUpd("spInsUpdLocalizador", insUpdParam);
        }

        //public IEnumerable<Estatus> ListEstatus()
        //{
        //    return this.Get<Estatus>("spListEstatus", new PrestamoEntidades.EstatusGetParams());
        //}
    }
}
