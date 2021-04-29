﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public class BaseForCreateOrEdit : ComponentBase
    {
        [Inject]
        protected  NavigationManager NavManager { get; set; }
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        protected JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
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
        public virtual async Task SweetAlertSuccess(string message, string redirectTo = "")
        {
            if (message == null || message == string.Empty)
            {
                message = ConstForCreateOrEdit.RegistroGuardado;
            }
           await JsInteropUtils.SweetAlertSuccess(jsRuntime, message,redirectTo);
            //await Task.Delay(watingTimeBeforeContinueExecution);
        }
        public virtual async Task SweetMessageBox(string message = "",string icon = "info", string redirectTo = "")
        {
            await JsInteropUtils.SweetMessageBox(jsRuntime, message,icon, redirectTo);
            //await Task.Delay(watingTimeBeforeContinueExecution);
        }
        public virtual async Task OnDeleteConfirm()
        {
            throw new NotImplementedException ();
        }

    }

    public static class ConstForCreateOrEdit
    { 
        public const string RegistroGuardado = "Registro Guardado, gracias, regresare al listado";
    }
}