using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Ocupacion : BaseInsUpdGenericCatalogo
    {
        public int? IdOcupacion { get; set; } 


        public override int GetId()
        {
            return (int)IdOcupacion;
        }

        protected override void SetId()
        {
            this.IdOcupacion = this.IdRegistro;
        }

        public override void SetPropertiesNullToRemoveFromSqlParam()
        {
            this.IdOcupacion = null;
        }
    }

    public class OcupacionGetParams : BaseGetParams
    {
        public int IdOcupacion { get; set; } = -1;
        
    }
}
