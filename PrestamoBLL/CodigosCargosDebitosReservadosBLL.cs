using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLL
{
    public class CodigosCargosDebitosReservadosBLL:BaseBLL
    {
        public CodigosCargosDebitosReservadosBLL(int idLocalidadNegocio, string loginName) : base(idLocalidadNegocio, loginName)
        {
            
        }

        public IEnumerable<CodigosCargosDebitosReservados> Get(CodigosCargosGetParams searchParam)
        {
            return this.Get<CodigosCargosDebitosReservados>("spGetCodigosCargos", searchParam);
        }
        public int InsUpd(CodigosCargosDebitosReservados insUpdParam)
        {
            insUpdParam.IdLocalidadNegocio = this.IdLocalidadNegocioLoggedIn;
            insUpdParam.Usuario = this.LoginName;
            var result = this.InsUpd("spInsUpdCodigosCargosDebitosReservados", insUpdParam);
            return result;
        }
    }
}
