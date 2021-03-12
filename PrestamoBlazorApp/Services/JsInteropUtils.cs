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
            public Task Alert(IJSRuntime JsRuntime, object message) => Task.Run(async () => await JsRuntime.InvokeAsync<object>("Alert", new object[] { message }));
            public Task ShowModal(IJSRuntime JsRuntime,string id) => Task.Run(async () => await JsRuntime.InvokeAsync<string>("ShowModal", new string[] { id }));
            public  Task Reload(IJSRuntime JsRuntime, bool force) => Task.Run(async () => await JsRuntime.InvokeAsync<bool>("Reload", force));
    }
}
