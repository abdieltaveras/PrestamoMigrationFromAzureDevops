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
    public class CommonBase : ComponentBase
    {
        protected bool loading { get; set; } = false;
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        protected JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        public virtual async Task SweetAlertSuccess(string message, string redirectTo = "")
        {
            if (message == null || message == string.Empty)
            {
                message = ConstForCreateOrEdit.RegistroGuardado;
            }
            await JsInteropUtils.SweetAlertSuccess(jsRuntime, message, redirectTo);
            //await Task.Delay(watingTimeBeforeContinueExecution);
        }
        public virtual async Task SweetMessageBox(string message = "", string icon = "info", string redirectTo = "", int delayMilliSeconds = 1500)
        {
            await JsInteropUtils.SweetMessageBox(jsRuntime, message, icon, redirectTo, delayMilliSeconds);
            //await Task.Delay(watingTimeBeforeContinueExecution);
        }
        public virtual async Task BlockPage()
        {
            await JsInteropUtils.BlockPage(jsRuntime);
            //await Task.Delay(watingTimeBeforeContinueExecution);
        }
        public virtual async Task UnBlockPage()
        {
            await JsInteropUtils.UnBlockPage(jsRuntime);
            //await Task.Delay(watingTimeBeforeContinueExecution);
        }
        public virtual async Task OnDeleteConfirm()
        {
            throw new NotImplementedException();
        }

    }

    public abstract class BaseForList : CommonBase
    {

        // agregar logica para los listados como es OnAgregar, OnDelete, etc

        public async void Handle_GetData(Func<Task> _action)
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
                await SweetMessageBox("Ha ocurrido algun error " + e.Message, icon: "info", @"\",5000);
            }
        }
        
    }

    public abstract class BaseForCreateOrEdit : CommonBase
    {

        protected bool saving { get; set; }
        protected bool disableCodigo { get; set; } = true;
        protected string TextoForActivo { get; set; } = "Si";
        
        /// <summary>
        /// To show notifications
        /// </summary>
        /// <param name="mesasageDelayInMilliseconds"> the time message will last showing</param>
        /// <param name="watingTimeBeforeContinueExecution">as an async method yue set here if you want some time to be awaited before leveing this method and continue execution</param>
        /// <returns></returns>
        public virtual async Task  OnGuardarNotification(string message = ConstForCreateOrEdit.RegistroGuardado, string redirectTo = "")
        {
            await SweetAlertSuccess(message, redirectTo);
        }

        public async void Handle_SaveData(Func<Task> _action)
        {
            try
            {
                saving = true;
                await BlockPage();
                await _action();
                await UnBlockPage();
                saving = false;
            }
            catch (Exception e)
            {
                await SweetMessageBox($"Lo siento error al guardar los datos error {e.Message} regresale al listado", "info", @"/Clientes", 5000);
                
            }
        }

    }

    public static class ConstForCreateOrEdit
    { 
        public const string RegistroGuardado = "Registro Guardado, gracias, regresare al listado";
    }
}