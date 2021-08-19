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
        public static Task CloseModal(IJSRuntime JsRuntime, string id) => Task.Run(async () => await JsRuntime.InvokeAsync<string>("CloseModal", new string[] { id }));
        public  static Task Reload(IJSRuntime JsRuntime, bool force) => Task.Run(async () => await JsRuntime.InvokeAsync<bool>("Reload", force));
        
        public static Task<bool> Confirm(IJSRuntime JsRuntime, string message) => Task.Run(async () => await JsRuntime.InvokeAsync<bool>("Confirm", message));
        public static Task<bool> GoToUrl (IJSRuntime JsRuntime, string url) => Task.Run(async () => await JsRuntime.InvokeAsync<bool>("GoToUrl", url));

        public static Task<bool> NotifyMessageBox(IJSRuntime jsRuntime, string message, int delayInMilliSeconds=5000) => Task.Run(async () => await jsRuntime.InvokeAsync<bool>("Notification", message, delayInMilliSeconds));

        public static Task ConsoleLog(IJSRuntime jsRuntime, object _object) => Task.Run(async () => await jsRuntime.InvokeAsync<object>("ConsoleLog", new object[] { _object}));
        public static Task<bool> SweetAlertSuccess(IJSRuntime jsRuntime, string message,string redirectTo = "") => Task.Run(async () => await jsRuntime.InvokeAsync<bool>("sweetAlertSuccess", message,redirectTo));
        public static Task<bool> SweetMessageBox(IJSRuntime jsRuntime, string message,string icon, string redirectTo = "", int delayMilliSeconds = 1500) => Task.Run(async () => await jsRuntime.InvokeAsync<bool>("SweetMessageBox", message,icon, redirectTo, delayMilliSeconds));
        public static Task<int> SweetConfirm(IJSRuntime jsRuntime,string title,string DenyButtonText="") => Task.Run(async () => await jsRuntime.InvokeAsync<int>("SweetConfirm",title,DenyButtonText));
        public static Task<bool> SweetConfirmWithIcon(IJSRuntime jsRuntime, string title, string text,string ConfirmButtonText = "Ok") => Task.Run(async () => await jsRuntime.InvokeAsync<bool>("SweetConfirmWithIcon", title,text, ConfirmButtonText));
        public static Task<bool> BlockPage(IJSRuntime jsRuntime) => Task.Run(async () => await jsRuntime.InvokeAsync<bool>("BlockPage"));
        public static Task<bool> UnBlockPage(IJSRuntime jsRuntime) => Task.Run(async () => await jsRuntime.InvokeAsync<bool>("UnBlockPage"));
        public static Task SetInputMaskByElemId(IJSRuntime JsRuntime, string elemId, string mask) => Task.Run(async () => await JsRuntime.InvokeVoidAsync("SetInputMaskByElem",elemId, mask));

        public static Task SetInputMask(IJSRuntime JsRuntime) => Task.Run(async () => await JsRuntime.InvokeVoidAsync("SetInputMask"));
        public static Task Territorio(IJSRuntime JsRuntime, string Id) => Task.Run(async () => await JsRuntime.InvokeAsync<string>("DivisionTerritorial", new string[] { Id }));
        public static Task SearchLocalidad(IJSRuntime JsRuntime) => Task.Run(async () => await JsRuntime.InvokeAsync<string>("searchLocalidad"));
        //public static Task<bool> Territorio(IJSRuntime jsRuntime) => Task.Run(async () => await jsRuntime.InvokeAsync<string>("BuscarComponentesDeDivisionTerritorial"));
    }
}
