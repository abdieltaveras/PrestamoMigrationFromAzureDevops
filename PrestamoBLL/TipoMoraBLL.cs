using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class TipoMoraBLL : BaseBLL
    {

        public TipoMoraBLL(int idLocalidadNegocioLoggedIn, string loginName) : base(idLocalidadNegocioLoggedIn, loginName) { }


        public IEnumerable<TipoMora> GetTiposMoras(TipoMoraGetParams  searchParam)
        {
            return this.Get<TipoMora>( "spGetTiposMora", searchParam);
        }
        public int InsUpdTipoMora(TipoMora insUpdParam)
        {
            this.SetIdLocalidadNegocioAndUsuario(insUpdParam);
            return this.InsUpd("spInsUpdTipoMora",insUpdParam);
        }
        
        public void CancelTipoMora(TipoMoraDelParams delParam)
        {
            //this.SoftDelete("SpDeleteTipoMora", delParam);
            //BllAcciones.CancelData<TipoMoraDelParams>(delParam, "SpDeleteTipoMora");
            //PrestamosDB.ExecSelSP("SpDeleteTipoMora", SearchRec.ToSqlParams(delParam));
        }

        public void DeleteTipoMora(TipoMoraDelParams delParam)
        {
            this.SoftDelete("spDelTipoMora", delParam);

            //DBPrestamo.ExecSelSP("spDelTipoMora", SearchRec.ToSqlParams(delParam));

        }
    }
}
