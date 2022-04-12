using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Base;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Catalogos
{

    public class CommonActionsForCatalogo : CommonActions, ICrudStandardButtonsAndActions<CatalogoInsUpd>
    {
        Action<CatalogoInsUpd> ShowEditorForAdd { get; }
        Action<CatalogoInsUpd> ShowEditorForEdit { get; }
        Action<CatalogoInsUpd> ShowEditorForDelete { get; }

        
        private CommonActionsForCatalogo()
        {

        }
        public CommonActionsForCatalogo(Action<CatalogoInsUpd> showEditorForAdd, Action<CatalogoInsUpd> showEditorForEdit, Action<CatalogoInsUpd> showEditorForDelete)
        {
            ShowEditorForAdd = showEditorForAdd;
            ShowEditorForDelete = showEditorForDelete;
            ShowEditorForEdit = showEditorForEdit;
         
        }
        public void BtnAddClick(CatalogoInsUpd obj)
        {
            ShowEditorForAdd(new CatalogoInsUpd());
        }
        public void BtnEdtClick(CatalogoInsUpd obj)
        {
            if (obj != null)
            {
                ShowEditorForEdit(obj);
            }
        }
        public void BtnDelClick(CatalogoInsUpd obj)
        {
            ShowEditorForDelete(obj);
        
        }
    }
}
