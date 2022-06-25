using PcpSoft.System;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoModelsForFrontEnd
{
    public class LocalidadTreeItem : ITreeItem
    {
        public int TreeItemId { get; }

        public int? TreeItemParentId { get; }

        public string TreeItemText { get; }

        public LocalidadTreeItem(Localidad localidad)
        {
            this.TreeItemId = localidad.IdLocalidad;
            this.TreeItemParentId = localidad.IdLocalidadPadre > 0 ? localidad.IdLocalidadPadre : null;
            this.TreeItemText = localidad.Nombre;
        }

        public override string ToString() => $"{this.TreeItemId} {this.TreeItemText} {this.TreeItemParentId}";
    }
}
