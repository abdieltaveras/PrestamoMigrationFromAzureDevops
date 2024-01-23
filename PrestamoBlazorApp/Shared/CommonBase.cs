using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using PrestamoEntidades.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using PrestamoBlazorApp.Providers;
using DevBox.Core.Identity;
using System.Linq;
using DevBox.Core.Access;
using PrestamoBlazorApp.Shared.Layout.Components.Menu;

namespace PrestamoBlazorApp.Shared
{
    public abstract class CommonBase : ComponentBase
    {
        [Inject] AuthenticationStateProvider _authenticationStateProvider { get; set; }
        //[Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private TokenAuthenticationStateProvider _TokenState { get; set; }
        [Inject] private ActionsManagerService actionsManagerService { get; set; }
        [Inject] private UserManagerService userManagerService { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject]
        internal IJSRuntime jsRuntime { get; private set; }
        [Inject]
        ISnackbar Snackbar { get; set; }
        [Inject]
        protected IWebHostEnvironment Env { get; set; }
        protected bool loading { get; set; } = false;

        protected virtual async Task SweetAlertSuccess(string message, string redirectTo = "")
        {
            if (message == null || message == string.Empty)
            {
                message = ConstForCreateOrEdit.RegistroGuardado;
            }
            //await Task.Delay(watingTimeBeforeContinueExecution);
        }
        protected async Task<DevBox.Core.Access.Action> AuthorizeViewActions()
        {

            DevBox.Core.Access.Action act = new DevBox.Core.Access.Action();
            var principal = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var usr = new User(principal.User);
            if (principal.User.Identity.IsAuthenticated)
            {

                IEnumerable<CoreUser> users = new CoreUser[0];
                string message = string.Empty;
                try
                {
                    users = await userManagerService.GetUser(usr.ID);
                }
                catch (Exception e)
                {
                    message = e.Message;

                }
                var userFound = users.FirstOrDefault();
                var actions = (users.FirstOrDefault()?.Actions ?? ActionList.Empty).Filter(ActionListFilters.Allowed);

                actionsManagerService.Actions = actions;
                var a = MenuDictionary.MenuDictionaryData();

                foreach (var item in a)
                {
                    if (item.Url.Replace("/", "") == NavManager.Uri.Replace(NavManager.BaseUri, ""))
                    {
                        foreach (var ac in actions)
                        {
                            if (ac.Value == item.Value)
                            {
                                actionsManagerService.SetCurrentAction(ac);
                                return ac;
                                //navigationManager.NavigateTo(item.Url);
                                //StateHasChanged();
                            }
                        }
                    }
                }
            }
            return null;
        }


        protected async Task NotifyMessageBySnackBar(string message, Severity severity)
        {
            await Task.Run(() => Snackbar.Add(message, severity));
        }
        /// <summary>
        /// alert using javascript alert method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="icon"></param>
        /// <param name="redirectTo"></param>
        /// <param name="delayMilliSeconds"></param>
        /// <returns></returns>
        protected virtual async Task Alert(string message)
        {
            await JsInteropUtils.Alert(jsRuntime, message);
        }
        protected virtual async Task BlockPage()
        {
            await JsInteropUtils.BlockPage(jsRuntime);
            //await Task.Delay(watingTimeBeforeContinueExecution);
        }
        protected virtual async Task UnBlockPage()
        {
            await JsInteropUtils.UnBlockPage(jsRuntime);
            //await Task.Delay(watingTimeBeforeContinueExecution);
        }
        //protected virtual async Task OnlyNumberInput(string inputName)
        //{
        //    await JsInteropUtils.OnlyNumberInput(jsRuntime, inputName);
        //}
        protected virtual async Task FocusOnInput(string inputName)
        {
            await JsInteropUtils.FocusOnInput(jsRuntime, inputName);
        }
        
        protected virtual async Task<bool> SweetConfirmWithIcon(string title,string text ="", string ConfirmedButtonText = "Ok")
        {
            var a = true; //await JsInteropUtils.SweetConfirmWithIcon(jsRuntime, title,text, ConfirmedButtonText);
            return a;
            //throw new NotImplementedException();
        }
        protected virtual async Task<bool> OnDeleteConfirm(string title,string text = "")
        {
           return await SweetConfirmWithIcon(title,text);
            //throw new NotImplementedException();
        }
        protected virtual async Task<bool> FichaDetalleDrCr(List<DetalleDrCrImpresionDocumento> datos)
        {
            string datosJson = JsonConvert.SerializeObject(datos);
            return await JsInteropUtils.FichaDetalleDrCr(jsRuntime,datosJson);
            //throw new NotImplementedException();
        }
        protected virtual async Task<bool> GenerarReciboIngreso(DetalleDrCrImpresionDocumento datos)
        {
            string datosJson = JsonConvert.SerializeObject(datos);
            return await JsInteropUtils.GenerarReciboIngreso(jsRuntime, datosJson);
            //throw new NotImplementedException();
        }
        protected virtual async Task<bool> SetOverlay(bool value)
        {
            return await JsInteropUtils.HSetOverlay(jsRuntime, value);
            //throw new NotImplementedException();
        }

        public async Task Handle_Funct(Func<Task> _action)
        {
            //await HSetOverlay(true);
            try
            {

                await _action();

            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().Contains("JAVASCRIPT INTEROP", StringComparison.InvariantCultureIgnoreCase) == false && ex.Message.ToLower().Contains("cannot read properties of null", StringComparison.InvariantCultureIgnoreCase) == false)
                {
                    //await SweetMessageBox("Error al ejecutar listado: " + ex.Message, Severity.Error);
                }
            }
            //await HSetOverlay(false);
        }

        protected virtual async Task OnSaveNotification(string message = ConstForCreateOrEdit.RegistroGuardado, string redirectTo = "")
        {
            await SweetAlertSuccess(message, redirectTo);
        }

        protected bool IsDevelopmentEnvironment => Env.IsDevelopment();

        protected virtual async Task NavigateTo(string url)
        {
            if (String.IsNullOrWhiteSpace(url) == false)
            {
                await Task.Run(() => NavManager.NavigateTo(url));
            }
        }
    }
}