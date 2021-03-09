﻿using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Pages
{
    public partial class Marcas
    {
        [Inject]
        MarcasService marcasService { get; set; }
        IEnumerable<Marca> marcas;
        [Parameter]
        public Marca Marca { get; set; } 
        bool loading = false;
        void Clear() => marcas = null;
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    this.Marca = new Marca();
        //}

        async Task GetMarcasByParam()
        {
            loading = true;
            var getAzul = new MarcaGetParams { IdMarca = 4 };
            marcas = await marcasService.GetMarcasAsync(getAzul);
            loading = false;
        }

        async Task GetAll()
        {
            loading = true;
            marcas = await marcasService.GetAll();
            loading = false;
        }

        async Task SaveMarca()
        {
            await marcasService.SaveMarca(this.Marca);
        }

        void RaiseInvalidSubmit()
        {
            
        }
    }
}
