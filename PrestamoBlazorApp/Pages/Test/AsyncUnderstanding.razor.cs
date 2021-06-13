using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL.Entidades;
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
            var tarea1 = getTest01();
            var tarea2 = getTest02();
            var tarea3 = getTest03();
            var tareas = new List<Task> { tarea1, tarea2, tarea3 };
            
            while (tareas.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tareas);
                if (finishedTask == tarea1)
                {
                    await NotifyMessageBox($"termine la tarea 1");
                }
                else if (finishedTask == tarea2)
                {
                    await NotifyMessageBox($"termine la tarea 2");
                }
                else if (finishedTask == tarea3)
                {
                    await NotifyMessageBox($"termine la tarea 3");
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
