using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.Classes.Access
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreOnToken : Attribute
    {
        public bool Ignorar { get; set; }
        public IgnoreOnToken()
        {
            this.Ignorar = true;
        }
        public IgnoreOnToken(bool Ignorar)
        {
            this.Ignorar = Ignorar;
        }
    }
}
