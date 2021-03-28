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
    public class ComponentBasePcp : ComponentBase
    {
        [Inject]
        protected  NavigationManager NavManager { get; set; }
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        protected JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();

        public virtual void OnGuardarNotification()
        {
            JsInteropUtils.Notification(jsRuntime, "Registro Guardado, gracias, regresare al listado");

        }
        public virtual void OnGuardarNotification(string message, int delayInMilliseconds = 5000)
        {
            JsInteropUtils.Notification(jsRuntime, message, delayInMilliseconds);

        }

    }
}