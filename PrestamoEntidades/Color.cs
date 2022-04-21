using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Color : BaseInsUpdGenericCatalogo
    {
        public virtual int? IdColor { get; set; } 

        public override int GetId() => (int)this.IdColor;

        protected override void SetIdForConcreteObject()
        {
            this.IdColor = this.IdRegistro;
        }

        public override void SetPropertiesNullToRemoveFromSqlParam()
        {
            this.IdColor = null;
        }
    }
    public class ColorGetParams : BaseGetParams 
    {
        public int IdColor { get; set; } = -1;

    }
}
