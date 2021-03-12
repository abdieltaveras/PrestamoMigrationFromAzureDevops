using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Base
{
    public partial class BaseComponent
    {
        [Inject]
        IJSRuntime JsRuntime { get; set; }

        Task Alert(object message) => Task.Run(async () => await JsRuntime.InvokeAsync<object>("Alert", new object[] { message }));
        Task ShowModal(string id) => Task.Run(async () => await JsRuntime.InvokeAsync<string>("ShowModal", new string[] { id }));
        Task Reload(bool force) => Task.Run(async () => await JsRuntime.InvokeAsync<bool>("Reload", force));
    }
}
