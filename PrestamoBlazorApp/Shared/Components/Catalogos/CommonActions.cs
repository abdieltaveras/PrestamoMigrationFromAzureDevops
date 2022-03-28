using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Catalogos
{
    public class CommonActions
    {
        public virtual bool BtnAddEnabled(object obj) => true;
        public virtual bool BtnEdtEnabled(object obj) => obj != null;
        public virtual bool BtnDelEnabled(object obj) => obj != null;
        public virtual bool BtnAddShow() => true;
        public virtual bool BtnEdtShow() => true;
        public virtual bool BtnDelShow() => true;
    }
    
}
