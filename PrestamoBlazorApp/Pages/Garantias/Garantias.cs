﻿using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Garantias
{
    public partial class Garantias : BaseForCreateOrEdit
    {
        [Inject]
        IJSRuntime jsRuntime { get; set; }
        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        [Inject]
        GarantiasService GarantiasService { get; set; }
        IEnumerable<Garantia> garantias { get; set; } = new List<Garantia>();
        [Parameter]
        public Garantia Garantia { get; set; } 
        bool loading = false;
        GarantiaGetParams SearchGarantia { get; set; } = new GarantiaGetParams();
        void Clear() => garantias = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Garantia = new Garantia();
        }
        protected override async Task OnInitializedAsync()
        {
            await GetGarantiasWithPrestamos();
            //garantias = await GarantiasService.GetWithPrestamo(new BuscarGarantiaParams { IdNegocio = 1, Search = ""});
        }
        async Task GetGarantias()
        {
            //loading = true;
            await BlockPage();
            var param = new GarantiaGetParams { IdNegocio = 1,IdGarantia = 1 };
            garantias = await GarantiasService.Get(param);
            await UnBlockPage();
            //loading = false;
        }
        async Task GetGarantiasWithPrestamos()
        {
            //loading = true;
            await BlockPage();
            garantias = await GarantiasService.GetWithPrestamo(new BuscarGarantiaParams { IdNegocio = 1, Search = "" });
            await UnBlockPage();
            //loading = false;
        }

        //async Task GetAll()
        //{
        //    loading = true;
        //    Garantias = await GarantiasService.GetAll();
        //    loading = false;
        //}

        //async Task SaveGarantia()
        //{
        //    await GarantiasService.SaveGarantia(this.Garantia);
        //}
        //void CreateOrEdit(int idGarantia = -1)
        //{
        //    if (idGarantia > 0)
        //    {
        //        this.Garantia = garantias.Where(m => m.IdGarantia == idGarantia).FirstOrDefault();
        //    }
        //    else
        //    {
        //        this.Garantia = new Garantia();
        //    }
        //    JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        //}
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
