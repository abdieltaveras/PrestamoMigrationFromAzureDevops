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

    public class CommonActionsForCatalogo : CommonActions, ICrudStandardButtonsAndActions<Catalogo>
    {
        Action<Catalogo> ShowEditorForAdd { get; }
        Action<Catalogo> ShowEditorForEdit { get; }
        Action<Catalogo> ShowEditorForDelete { get; }
        public CommonActionsForCatalogo()
        {

        }
        public CommonActionsForCatalogo(Action<Catalogo> showEditorForAdd, Action<Catalogo> showEditorForEdit, Action<Catalogo> showEditorForDelete)
        {
            ShowEditorForAdd = showEditorForAdd;
            ShowEditorForDelete = showEditorForDelete;
            ShowEditorForEdit = showEditorForEdit;
        }
        public void BtnAddClick(Catalogo obj)
        {
            ShowEditorForAdd(new Catalogo());
        }
        public void BtnEdtClick(Catalogo obj)
        {

            if (obj != null)
            {
                ShowEditorForEdit(obj);
            }
        }
        public void BtnDelClick(Catalogo obj)
        {
            ShowEditorForDelete(obj);
        }
    }
    
}
