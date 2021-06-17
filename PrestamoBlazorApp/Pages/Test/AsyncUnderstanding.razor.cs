using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Test
{
    public partial class AsyncUnderstanding : BaseForCreateOrEdit
    {
        [Inject]
        TestService testService { get; set; }
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }



        private async Task TestAsyncOk()
        {
            var elapseTime = new Stopwatch();
            elapseTime.Start();
            await SweetMessageBox($"Realizando las tareas");
            var tarea1 = testService.GetTest01(10);
            var tarea2 = testService.GetTest02(5);
            var tarea3 = testService.GetTest03(15);

            var tarea4 = testService.GetTest01(10);
            var tarea5 = testService.GetTest02(5);
            var tarea6 = testService.GetTest03(15);
            var tareas = new List<Task> { tarea1, tarea2, tarea3, tarea4, tarea5, tarea6  };
            
            while (tareas.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tareas);
                if (finishedTask == tarea1)
                {
                    var result = tarea1.Result;
                    await NotifyMessageBox($"termine la tarea 1 me devolvio el valor {result}");
                }
                else if (finishedTask == tarea2)
                {
                    var result = tarea2.Result;
                    await NotifyMessageBox($"termine la tarea 2 me devolvio el valor {result}");
                }
                else if (finishedTask == tarea3)
                {
                    var result = tarea3.Result;
                    await NotifyMessageBox($"termine la tarea 3 me devolvio el valor {result}");
                }
                else if (finishedTask == tarea4)
                {
                    var result = tarea4.Result;
                    await NotifyMessageBox($"termine la tarea 4 me devolvio el valor {result}");
                }
                else if (finishedTask == tarea5)
                {
                    var result = tarea5.Result;
                    await NotifyMessageBox($"termine la tarea 5 me devolvio el valor {result}");
                }
                else if (finishedTask == tarea6)
                {
                    var result = tarea6.Result;
                    await NotifyMessageBox($"termine la tarea 6 me devolvio el valor {result}");
                }
                tareas.Remove(finishedTask);
            }
            elapseTime.Stop();
            await SweetMessageBox($"async se tardo {elapseTime.ElapsedMilliseconds / 1000}");
        }

        private async Task TestAsyncBad()
        {
            var elapseTime = new Stopwatch();
            elapseTime.Start();
            await SweetMessageBox($"Realizando las tareas");
            await getTest01();
            await NotifyMessageBox($"termine la tarea 1");
            await getTest02();
            await NotifyMessageBox($"termine la tarea 2");
            await getTest03();
            await NotifyMessageBox($"termine la tarea 3");
            await getTest01();
            await NotifyMessageBox($"termine la tarea 1");
            await getTest02();
            await NotifyMessageBox($"termine la tarea 2");
            await getTest03();
            await NotifyMessageBox($"termine la tarea 3");
            elapseTime.Stop();
            await SweetMessageBox($"async se tardo {elapseTime.ElapsedMilliseconds / 1000}");
        }

        private async Task getTest03()
        {
            await testService.GetTest02(5);
        }

        private async Task getTest02()
        {
            await testService.GetTest02(15);
        }

        private async Task getTest01()
        {
            await testService.GetTest01(10);
        }
    }
}
