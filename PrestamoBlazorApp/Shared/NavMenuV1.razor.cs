﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using MudBlazor;
using PrestamoBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public partial class NavMenuV1
    {
        [Inject]
        IWebHostEnvironment env { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }
        [Inject]
        private IJSRuntime jsRuntime { get; set; }
        [Inject]
        ISnackbar Snackbar { get; set; }

        private JsInteropUtils jsInterop { get; set; } = new JsInteropUtils();
        private bool IsDevelopment => env.EnvironmentName == "Development";
        public NavMenuV1()
        {

        }
        //protected override void OnInitialized()  {}
        private void navigateTo(string linkUrl)
        {
            navManager.NavigateTo(linkUrl, true);
        }

        private async Task NotImplementedMessage() => await Task.Run(() => Snackbar.Add("Opcion no implementada o revisar la url y asignarla", Severity.Warning));



    }
}
