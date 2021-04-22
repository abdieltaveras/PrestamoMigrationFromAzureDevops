using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class JsInteropUtils
    {
        
        public static Task Alert(IJSRuntime JsRuntime, object message) => Task.Run(async () => await JsRuntime.InvokeAsync<object>("Alert", new object[] { message }));
        public static Task ShowModal(IJSRuntime JsRuntime,string id) => Task.Run(async () => await JsRuntime.InvokeAsync<string>("ShowModal", new string[] { id }));
        public  static Task Reload(IJSRuntime JsRuntime, bool force) => Task.Run(async () => await JsRuntime.InvokeAsync<bool>("Reload", force));
        
        public static Task<bool> Confirm(IJSRuntime JsRuntime, string message) => Task.Run(async () => await JsRuntime.InvokeAsync<bool>("Confirm", message));

        public static Task<bool> Notification(IJSRuntime jsRuntime, string message, int delayInMilliSeconds=5000) => Task.Run(async () => await jsRuntime.InvokeAsync<bool>("Notification", message, delayInMilliSeconds));

        public static Task SetInputMaskByElemId(IJSRuntime JsRuntime, string elemId, string mask) => Task.Run(async () => await JsRuntime.InvokeVoidAsync("SetInputMaskByElem",elemId, mask));

        public static Task SetInputMask(IJSRuntime JsRuntime) => Task.Run(async () => await JsRuntime.InvokeVoidAsync("SetInputMask"));
        public static Task Territorio(IJSRuntime JsRuntime, string Id) => Task.Run(async () => await JsRuntime.InvokeAsync<string>("DivisionTerritorial", new string[] { Id }));
        //public static Task<bool> Territorio(IJSRuntime jsRuntime) => Task.Run(async () => await jsRuntime.InvokeAsync<string>("BuscarComponentesDeDivisionTerritorial"));
    }
}
