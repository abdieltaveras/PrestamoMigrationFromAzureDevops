using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PrestamoBlazorApp.Shared
{
    public abstract class CommonBase : ComponentBase
    {
        
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject]
        internal IJSRuntime jsRuntime { get; private set; }

        protected bool loading { get; set; } = false;

        protected virtual async Task SweetAlertSuccess(string message, string redirectTo = "")
        {
            if (message == null || message == string.Empty)
            {
                message = ConstForCreateOrEdit.RegistroGuardado;
            }
            await JsInteropUtils.SweetAlertSuccess(jsRuntime, message, redirectTo);
            //await Task.Delay(watingTimeBeforeContinueExecution);
        }

        protected virtual async Task NotifyNotImplementedAction()
        {
            await JsInteropUtils.SweetAlertSuccess(jsRuntime, "Accion no implementada aun");
        }
        protected virtual async Task SweetMessageBox(string message = "", string icon = "info", string redirectTo = "", int delayMilliSeconds = 1500)
        {
            await JsInteropUtils.SweetMessageBox(jsRuntime, message, icon, redirectTo, delayMilliSeconds);
        }
        protected virtual async Task NotifyMessageBox(string message, int delay=5000)
        {
             await JsInteropUtils.NotifyMessageBox(jsRuntime, message,delay);
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
        protected virtual async Task<int> SweetConfirm(string title, string DenyBtnText = "")
        {
            return await JsInteropUtils.SweetConfirm(jsRuntime, title, DenyBtnText);
            //throw new NotImplementedException();
        }
        protected virtual async Task<bool> SweetConfirmWithIcon(string title,string text ="", string ConfirmedButtonText = "Ok")
        {
          var a = await JsInteropUtils.SweetConfirmWithIcon(jsRuntime, title,text, ConfirmedButtonText);
            return a;
            //throw new NotImplementedException();
        }
        protected virtual async Task<bool> OnDeleteConfirm(string title,string text = "")
        {
           return await SweetConfirmWithIcon(title,text);
            //throw new NotImplementedException();
        }
        protected virtual async Task<bool> FichaDetalleDrCr(string datosJson)
        {
            return await JsInteropUtils.FichaDetalleDrCr(jsRuntime,datosJson);
            //throw new NotImplementedException();
        }


    }

    


    

    
}