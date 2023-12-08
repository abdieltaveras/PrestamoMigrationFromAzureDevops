using DevBox.Core.Classes.UI;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    public class PublicAction : IUIAction
    {
        [Inject] public AreasEducativasService areasEducativasSvr { get; set; }
        public DevBox.Core.Access.Action Action => new DevBox.Core.Access.Action
        {
            DisplayName = "Áreas Educativas",
            Value = "AreasEducativas",
            Description = "Actualización de Áreas Educativas",
            GroupName = "Configuración"
        };
        public object[] GetComponents() => new object[] { };
        public object[] GetServices() => new object[] { areasEducativasSvr };
    }
}
