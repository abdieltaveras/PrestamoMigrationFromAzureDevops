using System;
using CoreAccess = DevBox.Core.Access;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.Classes.UI
{
    public interface IUIAction
    {
        CoreAccess.Action Action { get; }
        Type[] GetComponents();
        Type[] GetServices();
    }
}
