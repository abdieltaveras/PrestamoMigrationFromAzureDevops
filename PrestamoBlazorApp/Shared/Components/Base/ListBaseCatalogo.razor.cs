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
    public partial class ListBaseCatalogo : ComponentBase
    {
        [Parameter] public RenderFragment TableHeaderFragment { get; set; }
        [Parameter] public RenderFragment<Catalogo> RowTemplate { get; set; }
        [Parameter] public IEnumerable<Catalogo> Items { get; set; }

        [Parameter] public string Title { get; set; }
        [Parameter] public List<DataGridViewToolbarButton> ToolbarButtons { get; set; }
        [Parameter] public bool Outlined { get; set; } = true;
        [Parameter] public bool Dense { get; set; } = true;
        [Parameter] public bool Hover { get; set; } = true;
        [Parameter] public bool Striped { get; set; } = true;
        [Parameter] public bool Bordered { get; set; }
        [Parameter] public bool AllowMultiSelection { get; set; }
        [Parameter] public bool Immediate { get; set; } = false;

        [Parameter] public Func<Catalogo, bool> FilterFunc { get; set; }

        [Parameter] public string SearchPlaceHolder { get; set; } = "Search";

        [Parameter] public EventCallback<string> OnSearchValueChanged { get; set; }


        string _SearchValue;
        string SearchValue
        {
            get { return _SearchValue; }
            set
            {
                _SearchValue = value;
                Task.Run(async () => await OnSearchValueChanged.InvokeAsync(value));
            }
        }



        bool allowMultiSelection = false;
        

        Catalogo selectedItem = null;
        private HashSet<Catalogo> selectedItems = new HashSet<Catalogo>();

        bool rowSelected => Items.Count() > 0 && (selectedItem != null
                                                       || (allowMultiSelection && selectedItems.Count() > 0));
        
        
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
    }

}
