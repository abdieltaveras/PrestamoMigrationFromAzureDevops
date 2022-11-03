using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class ComentarioBLL : BaseBLL
    {
        public ComentarioBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName) { }


        public IEnumerable<Comentario> Get(ComentarioGetParams searchParam)
        {
            return this.Get<Comentario>("spGetComentario", searchParam);
        }
        public int InsUpd(Comentario insUpdParam)
        {
            return this.InsUpd("spInsUpdComentario", insUpdParam);
        }

        //public IEnumerable<Estatus> ListEstatus()
        //{
        //    return this.Get<Estatus>("spListEstatus", new PrestamoEntidades.EstatusGetParams());
        //}
    }
}
