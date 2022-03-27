using DevBox.Core.Classes.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIClient.Pages.Components;

namespace PrestamoBlazorApp.Shared.Components.Base
{
    public partial class ListBaseUsingObjects : ComponentBase
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public List<DataGridViewColumn> Columns { get; set; }
        [Parameter] public IEnumerable<object> ObjectList { get; set; }
        [Parameter] public List<DataGridViewToolbarButton> ToolbarButtons { get; set; }
        [Parameter] public bool Outlined { get; set; } = true;
        [Parameter] public bool Dense { get; set; } = true;
        [Parameter] public bool Hover { get; set; } = true;
        [Parameter] public bool Striped { get; set; } = true;
        [Parameter] public bool Bordered { get; set; }
        [Parameter] public bool AllowMultiSelection { get; set; }
        [Parameter] public bool Immediate { get; set; } = false;
        [Parameter] public Action<object> OnDelete { get; set; }
        [Parameter] public Action<object> OnEdit { get; set; }
        [Parameter] public Func<object, bool> FilterFunc { get; set; }

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
        bool outlined = true;
        bool dense = true;
        bool hover = true;
        bool striped = true;
        bool bordered = false;
        string title = "";
        string searchString = "";
        object selectedItem = null;
        List<DataGridViewToolbarButton> toolbarButtons { get; set; } = new List<DataGridViewToolbarButton>();
        bool rowSelected => ObjectList.Count() > 0 && (selectedItem != null
                                                       || (allowMultiSelection && selectedItems.Count > 0));
        private HashSet<object> selectedItems = new HashSet<object>();
        private IEnumerable<object> objectList = new List<object>();
        private Action<object> onDelete = (obj) => { };
        private Action<object> onEdit = (obj) => { };
        private Func<object, bool> filterFunc;
        private List<DataGridViewColumn> columns = new List<DataGridViewColumn>();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            title = Title;
            filterFunc = FilterFunc;
            columns = Columns;
            allowMultiSelection = AllowMultiSelection;
            outlined = Outlined;
            dense = Dense;
            hover = Hover;
            striped = Striped;
            bordered = Bordered;
            toolbarButtons = ToolbarButtons ?? new List<DataGridViewToolbarButton>();
            objectList = ObjectList ?? new List<object>();
        }
        string getText(object context, DataGridViewColumn col)
        {
            if (context.IsNull()) return "";
            var text = context.GetFieldValue(col.ColumnName) ?? null;
            if (text.IsNull()) return "";
            switch (col.ColumnType)
            {
                case DataGridViewColumnTypes.CheckBox:
                    text = Convert.ToBoolean(text) ? "Sí" : "No";
                    break;
                case DataGridViewColumnTypes.Date:
                    text = Convert.ToDateTime(text).ToShortDateString();
                    break;
                case DataGridViewColumnTypes.Time:
                    text = Convert.ToDateTime(text).ToShortTimeString();
                    break;
                case DataGridViewColumnTypes.DateTime:
                    text = Convert.ToDateTime(text).ToString();
                    break;
                case DataGridViewColumnTypes.Money:
                    text = Convert.ToDecimal(text).ToString("C2");
                    break;
                case DataGridViewColumnTypes.Number:
                    text = Convert.ToInt32(text).ToString("N");
                    break;
                case DataGridViewColumnTypes.Text:
                default:
                    text = text.ToString();
                    break;
            }
            return text.ToString();
        }
    }
    
}
