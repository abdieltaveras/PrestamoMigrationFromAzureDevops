using MudBlazor;
using PrestamoBlazorApp.Shared.Components.Base;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Catalogos
{

    public static class Factory
    {

        public static IEnumerable<ToolbarButtonForMud<TType>> StandarCrudToolBarButtons<TType>(ICrudStandardButtonsAndActions<TType> view)
        {
            var buttons = new List<ToolbarButtonForMud<TType>>()
            {
            new ToolbarButtonForMud<TType>() { Color = MudBlazor.Color.Success, Icon = Icons.Filled.AddCircle, Text = "Nuevo", OnClick = view.BtnAddClick, IsEnabled = view.BtnAddEnabled, Show = view.BtnAddShow() },
            new ToolbarButtonForMud<TType>() { Color = MudBlazor.Color.Secondary, Icon = Icons.Filled.Edit, Text = "Modificar", OnClick = view.BtnEdtClick, IsEnabled = view.BtnEdtEnabled, Show = view.BtnEdtShow() },
            new ToolbarButtonForMud<TType>() { Color = MudBlazor.Color.Error, Icon = Icons.Filled.Delete, Text = "Eliminar", OnClick = view.BtnDelClick, IsEnabled = view.BtnDelEnabled, Show = view.BtnDelShow() }
            };
            return buttons;
        }

        public static  bool FilterFuncForCatalogo(object obj, string searchValue)
        {
            var element = (Catalogo)obj;
            if (string.IsNullOrWhiteSpace(searchValue))
                return true;
            if (element.Nombre.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo != null)
            {
                if (element.Codigo.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }

}
