using DevBox.Core.Access;
using DevBox.Core.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Providers;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Base;
using PrestamoBlazorApp.Shared.Layout.Components.Menu;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Catalogos
{

    //public class CommonActionsForCatalogo : CommonActions, ICrudStandardButtonsAndActions<CatalogoInsUpd>
    public class CommonActionsForCatalogo : ICrudStandardButtonsAndActions<CatalogoInsUpd>
    {
        
        Func<CatalogoInsUpd, Func<Task>, Task> ShowEditorForAdd { get; }
        Func<CatalogoInsUpd, Func<Task>, Task> ShowEditorForEdit { get; }
        Func<CatalogoInsUpd, Func<Task>, Task> ShowEditorForDelete { get; }
        
        Func<Task> UpdateList { get; }
        private DevBox.Core.Access.Action CurrentAction { get; set; }
        [Inject] AuthenticationStateProvider _authenticationStateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private TokenAuthenticationStateProvider _TokenState { get; set; }
        [Inject] private ActionsManagerService actionsManagerService { get; set; }
        [Inject] private UserManagerService userManagerService { get; set; }
        private CommonActionsForCatalogo()
        {

        }
        public CommonActionsForCatalogo(Func<CatalogoInsUpd, Func<Task>, Task> showEditorForAdd, Func<CatalogoInsUpd, Func<Task>, Task> showEditorForEdit, Func<CatalogoInsUpd, Func<Task>, Task> showEditorForDelete, Func<Task> updateList, DevBox.Core.Access.Action currentAction)
        {
            ShowEditorForAdd = showEditorForAdd;
            ShowEditorForDelete = showEditorForDelete;
            ShowEditorForEdit = showEditorForEdit;
            UpdateList = updateList;
            CurrentAction = currentAction;
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
        public virtual bool BtnAddEnabled(object obj)
        {

            return true;
        }
        public  bool BtnEdtEnabled(object obj) => obj != null;
        public  bool BtnDelEnabled(object obj) => obj != null;
        public  bool BtnAddShow()
        {
            //TODO:Al recargar la pagina se va el valor de la accion... hay que crear un middelware
            //que asigne el valor de la accion actual nuevamente en base a la url,
            //si este tiene acceso a dicha url
            if(CurrentAction != null)
            {
                var subs = CurrentAction.SubActions.Where(m => m.Value.ToLower().Contains("agregar"));
                return true;
                //foreach (var item in subs)
                //{
                //    if (item.PermissionLevel == ActionPermissionLevel.Allow)
                //    {
                //        return true;
                //    }
                //}
            }

            return false;
        }
        public virtual bool BtnEdtShow()
        {
            if (CurrentAction != null)
            {
                var subs = CurrentAction.SubActions.Where(m => m.Value.ToLower().Contains("editar"));
                foreach (var item in subs)
                {
                    if (item.PermissionLevel == ActionPermissionLevel.Allow)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public virtual bool BtnDelShow()
        {
            if (CurrentAction != null)
            {
                var subs = CurrentAction.SubActions.Where(m => m.Value.ToLower().Contains("eliminar"));
                foreach (var item in subs)
                {
                    if (item.PermissionLevel == ActionPermissionLevel.Allow)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    
    }
}
