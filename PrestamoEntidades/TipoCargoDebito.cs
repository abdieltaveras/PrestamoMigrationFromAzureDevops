using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    /// <summary>
    /// para definir los tipos de debitos Cargo por intimacion, etc.
    /// </summary>
    public class TipoCargoDebito : BaseInsUpdGenericCatalogo
    {
        public int? IdTipoCargo { get; set; }

        public override int GetId()
        {
            return (int)IdTipoCargo;
        }

        protected override void SetIdForConcreteObject()
        {
            this.IdTipoCargo = this.IdRegistro;
        }

        public override void SetPropertiesNullToRemoveFromSqlParam()
        {
            this.IdTipoCargo = null;
        }
    }

    public class CodigoCargoDebitoParams : BaseGetParams
    {
        public string Codigo { get; set; } = string.Empty;
    }
}
