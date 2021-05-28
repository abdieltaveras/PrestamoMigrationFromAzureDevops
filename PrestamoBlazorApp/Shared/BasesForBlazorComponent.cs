using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PrestamoBlazorApp.Shared
{
    public abstract class CommonBase : ComponentBase
    {
        protected bool loading { get; set; } = false;
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject]
        internal IJSRuntime jsRuntime { get; private set; }

        private int executionCounter { get; set; }
        protected virtual async Task SweetAlertSuccess(string message, string redirectTo = "")
        {
            if (message == null || message == string.Empty)
            {
                message = ConstForCreateOrEdit.RegistroGuardado;
            }
            await JsInteropUtils.SweetAlertSuccess(jsRuntime, message, redirectTo);
            //await Task.Delay(watingTimeBeforeContinueExecution);
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
        protected virtual async Task OnDeleteConfirm()
        {
            throw new NotImplementedException();
        }

        protected async Task CountAndShowExecutionTime(string proceso)
        {
            executionCounter++;
            await NotifyMessageBox(proceso + "veces ejecutado " + executionCounter);
        }
    }

    public abstract class BaseForSearch : CommonBase
    {
        protected async void Handle_GetDataForSearch(Func<Task> _action)
        {
            try
            {
                //loading = true;
                //await BlockPage();
                await _action();
                StateHasChanged();
                //await UnBlockPage();
                //loading = false;
            }
            catch (Exception e)
            {
                await SweetMessageBox("Ha ocurrido algun error " + e.Message, icon: "info", null, 5000);
            }
        }
    }


    public abstract class BaseForList : CommonBase
    {

        // agregar logica para los listados como es OnAgregar, OnDelete, etc

        protected async Task Handle_GetDataForList(Func<Task> _action, string redirectTo="")
        {
            try
            {
                //loading = true;
                //await BlockPage();
                await _action();
                //StateHasChanged();
                //await UnBlockPage();
                //loading = false;
            }
            catch (Exception e)
            {
                var redirect = redirectTo == "" ? @"\" : redirectTo;
                await SweetMessageBox("Ha ocurrido algun error " + e.Message, icon: "info", redirect,5000);
            }
        }
        
    }

      public abstract class BaseForCreateOrEdit : CommonBase
    {
        [Inject]
        protected SetParametrosService setParametros { get; set; }
        protected bool saving { get; set; }
        protected bool disableCodigo { get; set; } = true;
        protected string TextoForActivo { get; set; } = "Si";

        /// <summary>
        /// To show notifications
        /// </summary>
        /// <param name="mesasageDelayInMilliseconds"> the time message will last showing</param>
        /// <param name="watingTimeBeforeContinueExecution">as an async method yue set here if you want some time to be awaited before leveing this method and continue execution</param>
        /// <returns></returns>
        protected virtual async Task  OnGuardarNotification(string message = ConstForCreateOrEdit.RegistroGuardado, string redirectTo = "")
        {
            await SweetAlertSuccess(message, redirectTo);
        }

        protected async Task Handle_GetData(Func<Task> _action, string redirectTo=@"/")
        {
            
            try
            {
                await _action();
            }
            catch (Exception e)
            {
                await SweetMessageBox("Ha ocurrido algun error " + e.Message, icon: "error", redirectTo , 5000);
            }
        }

        protected async Task Handle_SaveData(Func<Task> _action, Func<Task> _OnSuccess = null, Func<Task> _OnFail = null, bool blockPage=false, string redirectTo = "")
        {
            if (blockPage) { await BlockPage(); }
            try
            {
                saving = true;
                await _action();
                saving = false;
                if (blockPage) { await UnBlockPage(); }
                if (_OnSuccess == null)
                {
                    await SweetMessageBox("Datos Guardados Correctamente", "success", redirectTo);
                }
                else
                {
                    await _OnSuccess();
                }
            }
            catch (Exception e)
            {
                if (blockPage) { await UnBlockPage(); }
                if (_OnFail == null)
                {
                    var mens1 = redirectTo != "" ? "regresale al listado" : "";
                    await SweetMessageBox($"Lo siento error al guardar los datos error {e.Message} {mens1}", "error", redirectTo, 10000);
                    // await SweetMessageBox($"Lo siento error al guardar los datos error {e.Message } {e.InnerException.Message} regresale al listado", "error", redirectoTo, 10000);
                }
                else
                {
                    await _OnFail();
                }
            }
            
        }
    }

    public static class ConstForCreateOrEdit
    { 
        public const string RegistroGuardado = "Registro Guardado, gracias, regresare al listado";
    }
}