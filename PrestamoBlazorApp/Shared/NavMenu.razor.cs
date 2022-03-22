﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public partial class NavMenu
    {
        [Inject]
        IWebHostEnvironment env { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }
        [Inject]
        private IJSRuntime jsRuntime { get; set; }
        private JsInteropUtils jsInterop { get; set; } = new JsInteropUtils();
        private bool IsDevelopment => env.EnvironmentName == "Development";
        public NavMenu()
        {

        }
        //protected override void OnInitialized()  {}
        private void navigateTo(string linkUrl)
        {
            navManager.NavigateTo(linkUrl, true);
        }

        
    }
}
