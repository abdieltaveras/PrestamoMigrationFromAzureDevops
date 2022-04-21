using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class TipoSexo : BaseInsUpdGenericCatalogo
    {
        public virtual int? IdTipoSexo { get; set; }

        public override int GetId() => (int)this.IdTipoSexo;

        protected override void SetIdForConcreteObject()
        {
            this.IdTipoSexo = this.IdRegistro;
        }

        public override void SetPropertiesNullToRemoveFromSqlParam()
        {
            this.IdTipoSexo = null;
        }
    }

    public class TipoSexoGetParams : BaseCatalogoGetParams
    {
        public int IdTipoSexo { get; set; } = -1;
    }
}
