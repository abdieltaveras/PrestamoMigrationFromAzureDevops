using MudBlazor;
using PcpSoft.MudBlazorHelpers;
using PcpSoft.System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Models
{
    //public class TreeItemData : ITreeItemData
    //{
    //    public ITreeItemData Parent { get; private set; }
        

    //    public string Text { get; set; } = string.Empty;

    //    public string Icon { get; set; } = string.Empty;

    //    public int? IdTreeItem { get; set; }

    //    public bool IsExpanded { get; set; }

    //    public bool HasChild => TreeItems.Any();

    //    public bool IsChecked { get; set; }
    //    public TreeItemData()
    //    {

    //    }
    //    public HashSet<ITreeItemData> TreeItems { get; set; } = new HashSet<ITreeItemData>();

        
    //    public TreeItemData(string text, string icon, int? idTreeItem = null)
    //    {
    //        Text = text;
    //        Icon = icon;
    //        IdTreeItem = idTreeItem;
    //    }
    //    public void AddChild(TreeItemData item)
    //    {
    //        item.Parent = this;
    //        this.TreeItems.Add(item);
    //    }

    //    public bool HasPartialChildSelection()
    //    {
    //        int iChildrenCheckedCount = (from c in TreeItems where c.IsChecked select c).Count();
    //        return HasChild && iChildrenCheckedCount > 0 && iChildrenCheckedCount < TreeItems.Count();
    //    }
    //}

    //public class MudBlazorTreeBuilder : IMudBlazorTreeBuilder
    //{
    //    public HashSet<ITreeItemData> TreeItems { get; set; } = new HashSet<ITreeItemData>();

    //    private IEnumerable<ITreeNode> TreeNodes { get; set; }

    //    public MudBlazorTreeBuilder(IEnumerable<ITreeNode> treeNodes)
    //    {
    //        this.TreeNodes = treeNodes.ToList();
    //    }
    //    public async Task<HashSet<ITreeItemData>> GetTreeItems()
    //    {
    //        await BuildTreeItems(this.TreeNodes, null);
    //        return this.TreeItems;
    //    }

    //    private async Task BuildTreeItems(IEnumerable<ITreeNode> treeNodes, ITreeItemData mbTreeNode)
    //    {
    //        var treeNode = mbTreeNode as TreeItemData;
    //        foreach (var node in treeNodes)
    //        {

    //            var mudNode = new TreeItemData(node.NodeText, Icons.Material.Filled.Directions, node.NodeId) { IsExpanded = true };

    //            if (mbTreeNode == null)
    //            {
    //                TreeItems.Add(mudNode);
    //            }
    //            else
    //            {
    //                (mbTreeNode as TreeItemData).AddChild(mudNode);
    //            }
    //            await BuildTreeItems(node.ChildNodes, mudNode);
    //        }
    //    }
    //}

}
