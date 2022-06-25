using MudBlazor;
using PcpSoft.System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TreeItemData
{
    public TreeItemData Parent { get; private set; }
    public string Text { get; set; }

    public string Icon { get; set; }

    public int? IdTreeItem { get; set; }

    public bool IsExpanded { get; set; }

    public bool HasChild => TreeItems.Any();

    public bool IsChecked { get; set; }
    public TreeItemData()
    {
            
    }
    public HashSet<TreeItemData> TreeItems { get; set; } = new HashSet<TreeItemData>();

    private TreeItemData(string text, string icon, int? idTreeItem = null)
    {
        Text = text;
        Icon = icon;
        IdTreeItem = idTreeItem;
    }
    private void AddChild(TreeItemData item)
    {
        item.Parent = this;
        this.TreeItems.Add(item);
    }

    public async Task BuilBlazorTree(IEnumerable<ITreeNode> treeNodes, TreeItemData mbTreeNode)
    {

        foreach (var node in treeNodes)
        {

            var mudNode = new TreeItemData(node.NodeText, Icons.Filled.Directions, node.NodeId) { IsExpanded = true };

            if (mbTreeNode == null)
            {
                TreeItems.Add(mudNode);
            }
            else
            {
                mbTreeNode.AddChild(mudNode);
            }
            await BuilBlazorTree(node.ChildNodes, mudNode);

        }
    }

    public bool HasPartialChildSelection()
    {
        int iChildrenCheckedCount = (from c in TreeItems where c.IsChecked select c).Count();
        return HasChild && iChildrenCheckedCount > 0 && iChildrenCheckedCount < TreeItems.Count();
    }

}

