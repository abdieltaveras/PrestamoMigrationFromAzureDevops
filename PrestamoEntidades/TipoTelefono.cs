using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{

    public class TipoTelefono : BaseInsUpdGenericCatalogo
    {
        public int? IdTipoTelefono { get; set; } = 0;

        public override int GetId()
        {
            return (int)IdTipoTelefono;
        }

        protected override void SetIdForConcreteObject()
        {
            this.IdTipoTelefono = this.IdRegistro;
        }

        public override void SetPropertiesNullToRemoveFromSqlParam()
        {
            this.IdTipoTelefono = null;
        }
    }


    public class TipoTelefonoGetParams : BaseCatalogoGetParams
    {
        public int IdTipoTelefono { get; set; } = -1;
    }
}
