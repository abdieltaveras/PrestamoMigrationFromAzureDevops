﻿using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Clasificaciones
{
    public partial class Clasificaciones : BaseForCreateOrEdit
    {
        [Parameter]
        public Catalogo Catalogo { get; set; } = new Catalogo { NombreTabla = "tblclasificaciones", IdTabla = "idclasificacion" };
        [Parameter]
        public CatalogoGetParams CatalogoGetParams { get; set; } = new CatalogoGetParams { NombreTabla = "tblclasificaciones", IdTabla = "idclasificacion" };
        //[Inject]
        //ClasificacionesService ClasificacionesService { get; set; }
        //IEnumerable<Clasificacion> clasificaciones { get; set; } = new List<Clasificacion>();
        //[Parameter]
        //public Clasificacion Clasificacion { get; set; } 
        //bool loading = false;
        //ClasificacionesGetParams SearchClasificacion { get; set; } = new ClasificacionesGetParams();
        //void Clear() => clasificaciones = null;
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    this.Clasificacion = new Clasificacion();
        //}
        //protected override async Task OnInitializedAsync()
        //{
        //    clasificaciones = await ClasificacionesService.Get(new ClasificacionesGetParams { IdNegocio = 1});
        //}
        //async Task GetClasificaciones()
        //{
        //    loading = true;
        //    var param = new ClasificacionesGetParams { IdNegocio = 1 };
        //    clasificaciones = await ClasificacionesService.Get(param);
        //    loading = false;
        //}

        //async Task GetAll()
        //{
        //    loading = true;
        //    clasificaciones = await ClasificacionesService.GetAll();
        //    loading = false;
        //}

        //async Task SaveClasificacion()
        //{
        //    await BlockPage();
        //    await ClasificacionesService.SaveClasificacion(this.Clasificacion);
        //    await UnBlockPage();
        //    await SweetMessageBox("Guardado Correctamente", "success", "");
        //}
        //async Task CreateOrEdit(int idClasificacion = -1)
        //{
        //    await BlockPage();
        //    if (idClasificacion > 0)
        //    {
        //        var datos = await ClasificacionesService.Get(new ClasificacionesGetParams { IdClasificacion = idClasificacion });
        //        this.Clasificacion = datos.FirstOrDefault();
        //    }
        //    else
        //    {
        //        this.Clasificacion = new Clasificacion();
        //    }
        //    await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        //    await UnBlockPage();
        //}
        //void RaiseInvalidSubmit()
        //{

        //}
    }
}
