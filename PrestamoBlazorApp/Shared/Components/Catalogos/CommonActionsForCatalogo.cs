using DevBox.Core.Access;
using DevBox.Core.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.JSInterop;
using MudBlazor;
using PrestamoBlazorApp.Providers;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Auth;
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
        IDialogService _dialogService { get; set; }  
        private CommonActionsForCatalogo()
        {
            
        }
        public CommonActionsForCatalogo(Func<CatalogoInsUpd, Func<Task>, Task> showEditorForAdd, 
            Func<CatalogoInsUpd, Func<Task>, Task> showEditorForEdit, Func<CatalogoInsUpd, Func<Task>, Task> showEditorForDelete, Func<Task> updateList, DevBox.Core.Access.Action currentAction, IDialogService dialogService)
        {
            ShowEditorForAdd = showEditorForAdd;
            ShowEditorForDelete = showEditorForDelete;
            ShowEditorForEdit = showEditorForEdit;
            UpdateList = updateList;
            CurrentAction = currentAction;
            _dialogService = dialogService;
        }
        private async Task<IDialogReference> OpenAuthorizeDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = false, MaxWidth = MaxWidth.Small, FullWidth = true };
            return  _dialogService.Show<AuthorizeModal>("Requiere Authorizacion", options);
        }
        public async Task BtnAddClick(CatalogoInsUpd obj)
        {
            bool PermissionRequired = false;
            if (CurrentAction != null)
            {
                var subs = CurrentAction.SubActions.Where(m => m.Value.ToLower().Contains("agregar"));
                //return true;
                foreach (var item in subs)
                {
                    if (item.PermissionLevel == ActionPermissionLevel.PermissionRequired)
                    {
                        PermissionRequired = true;
                    }
                }
            }
            if (PermissionRequired)
            {
                var dialog = await OpenAuthorizeDialog();
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    if (Convert.ToBoolean(result.Data) == true)
                    {
                        await ShowEditorForAdd(new CatalogoInsUpd(), this.UpdateList);
                    }
                }
            }
            else
            {
                await ShowEditorForAdd(new CatalogoInsUpd(), this.UpdateList);
            }
        }
        public async Task BtnEdtClick(CatalogoInsUpd obj)
        {
            if (obj != null)
            {
                bool PermissionRequired = false;
                if (CurrentAction != null)
                {
                    var subs = CurrentAction.SubActions.Where(m => m.Value.ToLower().Contains("editar"));
                    //return true;
                    foreach (var item in subs)
                    {
                        if (item.PermissionLevel == ActionPermissionLevel.PermissionRequired)
                        {
                            PermissionRequired = true;
                        }
                    }
                }
                if (PermissionRequired)
                {
                    var dialog = await OpenAuthorizeDialog();
                    var result = await dialog.Result;
                    if (!result.Canceled)
                    {
                        if (Convert.ToBoolean(result.Data) == true)
                        {
                            await ShowEditorForEdit(obj, this.UpdateList);
                        }
                    }
              
                }
                else
                {
                    await ShowEditorForEdit(obj, this.UpdateList);
                }
            }
        }
        public async Task BtnDelClick(CatalogoInsUpd obj)
        {
            bool PermissionRequired = false;
            if (CurrentAction != null)
            {
                var subs = CurrentAction.SubActions.Where(m => m.Value.ToLower().Contains("eliminar"));
                //return true;
                foreach (var item in subs)
                {
                    if (item.PermissionLevel == ActionPermissionLevel.PermissionRequired)
                    {
                        PermissionRequired = true;
                    }
                }
            }
            if (PermissionRequired)
            {
                var dialog = await OpenAuthorizeDialog();
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    if (Convert.ToBoolean(result.Data) == true)
                    {
                        await ShowEditorForDelete(obj, UpdateList);
                    }
                }

            }
            else
            {
                await ShowEditorForDelete(obj, UpdateList);
            }
        
            //await UpdateList();
        }
        public virtual bool BtnAddEnabled(object obj)
        {

            return true;
        }
        public  bool BtnEdtEnabled(object obj) => obj != null;
        public  bool BtnDelEnabled(object obj) => obj != null;
        public bool BtnAddShow()
        {
            //TODO:Al recargar la pagina se va el valor de la accion... hay que crear un middelware
            //que asigne el valor de la accion actual nuevamente en base a la url,
            //si este tiene acceso a dicha url
            //if(CurrentAction != null)
            //{
            //    var subs = CurrentAction.SubActions.Where(m => m.Value.ToLower().Contains("agregar"));
            //    //return true;
            //    foreach (var item in subs)
            //    {
            //        if (item.PermissionLevel == ActionPermissionLevel.Allow)
            //        {
            //            return true;
            //        }
            //    }
            //}

            //return false;
            return true;
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
