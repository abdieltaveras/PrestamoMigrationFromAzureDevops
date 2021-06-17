using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Ocupaciones
{
    public partial class Ocupaciones : BaseForCreateOrEdit
    {
        
        
        [Inject]
        OcupacionesService OcupacionesService { get; set; }
        IEnumerable<Ocupacion> ocupaciones { get; set; } = new List<Ocupacion>();
        [Parameter]
        public Ocupacion Ocupacion { get; set; }

        [Parameter]
        public Catalogo Catalogo { get; set; } = new Catalogo { NombreTabla = "tblOcupaciones", IdTabla = "idocupacion" };
        [Parameter]
        public CatalogoGetParams CatalogoGetParams { get; set; } = new CatalogoGetParams { NombreTabla = "tblOcupaciones", IdTabla = "idocupacion" };
        //bool loading = false;
        //void Clear() => ocupaciones = null;
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    this.Ocupacion = new Ocupacion();
        //}
        //protected override async Task OnInitializedAsync()
        //{
        //    //await BlockPage();
        //    ocupaciones = await OcupacionesService.Get(new OcupacionGetParams());
        //    //await UnBlockPage();
        //}
        //async Task GetOcupaciones()
        //{
        //    loading = true;
        //    ocupaciones = await OcupacionesService.Get(new OcupacionGetParams());
        //    loading = false;
        //}

        //async Task GetAll()
        //{
        //    loading = true;
        //    ocupaciones = await OcupacionesService.GetAll();
        //    loading = false;
        //}

        //async Task SaveOcupacion()
        //{
        //    await Handle_SaveData(async() =>
        //    await OcupacionesService.SaveOcupacion(this.Ocupacion), null, null, blockPage:true);
        //    //await OnGuardarNotification();
        //}
        //async Task CreateOrEdit(int idOcupacion = -1)
        //{
        //    if (idOcupacion > 0)
        //    {
        //        await BlockPage();
        //        var ocupacion = await OcupacionesService.Get(new OcupacionGetParams { IdOcupacion = idOcupacion});
        //        this.Ocupacion = ocupacion.FirstOrDefault();
              
        //    }
        //    else
        //    {
        //        this.Ocupacion = new Ocupacion();
        //    }
        //    await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        //    await UnBlockPage();
        //}


        void RaiseInvalidSubmit()
        {
            
        }
    }
}
