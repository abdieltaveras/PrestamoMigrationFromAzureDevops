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
    public partial class ListBaseForCrud<TItem> : ComponentBase where TItem : class
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public List<DataGridViewToolbarButton> ToolbarButtons { get; set; }
        [Parameter] public TItem SelectedItem { get; set; } = null;

    }

}
