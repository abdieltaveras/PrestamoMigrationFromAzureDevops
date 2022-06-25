using PcpSoft.System;
using PrestamoEntidades;
using System;

namespace PrestamoModelsForFrontEnd
{
    public class DivisionTerritorialTreeItem : ITreeItem
    {

        public int TreeItemId { get; }

        public int? TreeItemParentId { get; }

        public string TreeItemText { get; }

        public DivisionTerritorialTreeItem(Territorio territorio)
        {
            this.TreeItemId = territorio.IdDivisionTerritorial;
            this.TreeItemParentId = territorio.IdLocalidadPadre > 0 ? territorio.IdLocalidadPadre : null;
            //this.TreeItemParentId = territorio.IdDivisionTerritorialPadre >0 ? territorio.IdDivisionTerritorialPadre : null;
            this.TreeItemText = territorio.Nombre;
        }

        public override string ToString() => $"{this.TreeItemId} {this.TreeItemText} {this.TreeItemParentId}";
    }
}
