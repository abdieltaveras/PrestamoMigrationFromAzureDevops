using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{

    public abstract class BaseForCreateOrEdit : CommonBase
    {
        [Inject]
        public IDialogService svrDialogService { get; set; }
        public DialogOptions dialogOptions { get; set; } = new DialogOptions { FullWidth = true, CloseOnEscapeKey = true, CloseButton = true, MaxWidth = MaxWidth.Medium };

        protected bool IsFormShowErrors;
        protected string[] FormFieldErrors { get; set; }
        protected bool IsFormSuccess { get; set; }
        protected MudForm form = null;

        //[Inject]
        //protected SetParametrosService setParametros { get; set; }
        protected bool saving { get; set; }
        protected bool EnableReadCodigo { get; set; } = true;
        protected string TextoForActivo { get; set; } = "Si";

        /// <summary>
        /// To show notifications
        /// </summary>
        /// <param name="mesasageDelayInMilliseconds"> the time message will last showing</param>
        /// <param name="watingTimeBeforeContinueExecution">as an async method yue set here if you want some time to be awaited before leveing this method and continue execution</param>
        /// <returns></returns>


        protected async Task Handle_GetData(Func<Task> _action, string redirectTo = @"/")
        {

            loading = true;
            try
            {
                await _action();
            }
            catch (Exception e)
            {
                await NotifyMessageBySnackBar("Ocurrio un error que no permitio obtener los datos solicitados", Severity.Error);
                // await SweetMessageBox("Ha ocurrido algun error " + e.Message, icon: "error", redirectTo, 5000);
            }
            loading = false;
         
         
        }

        protected async Task Handle_SaveData(Func<Task> _action, Func<Task> _OnSuccess = null, Func<Task> _OnFail = null, bool blockPage = false, string redirectTo = "", MudDialogInstance mudDialogInstance = null)
        {
            if (form!=null)
            {
                await form.Validate();
                if (form.IsValid == false)
                {
                    await NotifyMessageBySnackBar("Revisar hay errores en el formulario", Severity.Warning);
                    return;
                }
            }
          

            if (blockPage) { await BlockPage(); }
            await Handle_Funct(() => SetOverlay(true));
            try
            {

                saving = true;
                await _action();
                saving = false;
                if (blockPage) { await UnBlockPage(); }
                if (_OnSuccess == null)
                {
                    if (mudDialogInstance == null)
                    {
                        await NotifyMessageBySnackBar("Los datos no pudieron ser Guardados", Severity.Warning);
                    }
                    else
                    {
                        await NotifyMessageBySnackBar("Datos Guardados Correctamente", Severity.Success);
                        mudDialogInstance.Close();
                    }
                }
                else
                {
                    await _OnSuccess();
                    if (!string.IsNullOrEmpty(redirectTo))
                    {
                        await NavigateTo(redirectTo);
                    }
                }
            }
            catch (Exception e)
            {
                await NotifyMessageBySnackBar("Los datos no pudieron ser Guardados", Severity.Warning);
                //if (e.Message.ToUpper().Contains("JAVASCRIPT INTEROP", StringComparison.InvariantCultureIgnoreCase) == false && e.Message.ToLower().Contains("cannot read properties of null", StringComparison.InvariantCultureIgnoreCase) == false)
                //{
                //    //await SweetMessageBox("Error al ejecutar listado: " + ex.Message, Severity.Error);

                //    if (blockPage) { await UnBlockPage(); }
                //    if (_OnFail == null)
                //    {
                //        var mens1 = redirectTo != "" ? "regresale al listado" : "";
                //        await SweetMessageBox($"Lo siento error al guardar los datos error {e.Message} {mens1}", "error", redirectTo, 10000);
                //        // await SweetMessageBox($"Lo siento error al guardar los datos error {e.Message } {e.InnerException.Message} regresale al listado", "error", redirectoTo, 10000);
                //    }
                //    else
                //    {
                //        await _OnFail();
                //    }
                //}
            }
            if (blockPage) { await UnBlockPage(); }
            await Handle_Funct(() => SetOverlay(false));
         
        }

        protected MudBlazor.Color SetColorForCheckBox(bool value) => value ? Color.Success : Color.Default;


    }


    public static class ConstForCreateOrEdit
    {
        public const string RegistroGuardado = "Registro Guardado, gracias, regresare al listado";
    }


}
