using PrestamoBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class RedSocialVM
    {
        public RedSocial RedSocial { get; set; }
        public IEnumerable<RedSocial> ListaRedesSociales { get; set; }
    }
}