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
        Func<CatalogoInsUpd, Func<Task>, Task> ShowEditorForAdd { get; }
        Func<CatalogoInsUpd, Func<Task>, Task> ShowEditorForEdit { get; }
        Func<CatalogoInsUpd, Func<Task>, Task> ShowEditorForDelete { get; }
        
        Func<Task> UpdateList { get; }
        
        private CommonActionsForCatalogo()
        {

        }
        public CommonActionsForCatalogo(Func<CatalogoInsUpd, Func<Task>, Task> showEditorForAdd, Func<CatalogoInsUpd, Func<Task>, Task> showEditorForEdit, Func<CatalogoInsUpd, Func<Task>, Task> showEditorForDelete, Func<Task> updateList)
        {
            ShowEditorForAdd = showEditorForAdd;
            ShowEditorForDelete = showEditorForDelete;
            ShowEditorForEdit = showEditorForEdit;
            UpdateList = updateList;
         
        }
        public async Task BtnAddClick(CatalogoInsUpd obj)
        {
            await ShowEditorForAdd(new CatalogoInsUpd(), this.UpdateList);
        }
        public async Task BtnEdtClick(CatalogoInsUpd obj)
        {
            if (obj != null)
            {
                await ShowEditorForEdit(obj, this.UpdateList);
            }
        }
        public async Task BtnDelClick(CatalogoInsUpd obj)
        {

            await ShowEditorForDelete(obj, UpdateList);
            //await UpdateList();
        }
    }
}
