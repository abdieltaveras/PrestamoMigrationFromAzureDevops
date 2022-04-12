using DevBox.Core.Classes.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIClient.Pages.Components;

namespace PrestamoBlazorApp.Shared.Components.Base
{
    public partial class ToolBarButtons<TItem> : ComponentBase where TItem : class
    {
        //[Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public IEnumerable<ButtonForToolBar<TItem>> ToolbarButtons { get; set; }
        [Parameter] public TItem SelectedItem { get; set; } = null;

        private string SelectedItemText => (SelectedItem != null) ? @SelectedItem.ToString() : "Ninguna";
    }
}
