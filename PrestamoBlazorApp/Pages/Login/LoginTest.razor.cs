﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Login
{

    internal class Users
    {
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
    }
        public partial class LoginTest : BaseForCreateOrEdit
    {
        private Users users { get; set; }
    }

    

}


