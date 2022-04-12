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

        public static IEnumerable<ButtonForToolBar<TType>> StandarCrudToolBarButtons<TType>(ICrudStandardButtonsAndActions<TType> BtnsWithActions)
        {
            var buttons = new List<ButtonForToolBar<TType>>()
            {
            new ButtonForToolBar<TType>() { Color = MudBlazor.Color.Success, Icon = Icons.Filled.AddCircle, Text = "Nuevo", OnClick = BtnsWithActions.BtnAddClick, IsEnabled = BtnsWithActions.BtnAddEnabled, Show = BtnsWithActions.BtnAddShow() },
            new ButtonForToolBar<TType>() { Color = MudBlazor.Color.Secondary, Icon = Icons.Filled.Edit, Text = "Modificar", OnClick = BtnsWithActions.BtnEdtClick, IsEnabled = BtnsWithActions.BtnEdtEnabled, Show = BtnsWithActions.BtnEdtShow() },
            new ButtonForToolBar<TType>() { Color = MudBlazor.Color.Error, Icon = Icons.Filled.Delete, Text = "Eliminar", OnClick = BtnsWithActions.BtnDelClick, IsEnabled = BtnsWithActions.BtnDelEnabled, Show = BtnsWithActions.BtnDelShow() }
            };
            return buttons;
        }

        public static  bool FilterFuncForCatalogo(object obj, string searchValue)
        {
            var element = (CatalogoInsUpd)obj;
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
