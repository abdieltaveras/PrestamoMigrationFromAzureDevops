using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class RedSocial : BaseCatalogo
    {
        public int IdRedSocial { get; set; }

        public override int GetId()
        {
            return this.IdRedSocial;
        }
    }

    public class RedSocialGetParams : BaseGetParams
    {
        public int IdRedSocial{ get; set; } = -1;
    }
}
