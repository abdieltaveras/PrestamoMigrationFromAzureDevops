using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Territorios
{
    public partial class Territorios : BaseForList
    {
        
        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        [Inject]
        DivisionTerritorialService TerritoriosService { get; set; }
        
        IEnumerable<DivisionTerritorial> TiposTerritorios { get; set; } = new List<DivisionTerritorial>();

        private DivisionTerritorial SelectedItem1 = null;

        public DivisionTerritorial Territorio { get; set; }

        void Clear() => TiposTerritorios = new List<DivisionTerritorial>();
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Territorio = new DivisionTerritorial();
            //this.Ocupacion = new Ocupacion();
        }
        protected override async Task OnInitializedAsync()
        {
            TiposTerritorios = await TerritoriosService.GetTiposDivisionTerritorial();
            //new DivisionTerritorialGetParams());
            // await JsInteropUtils.Territorio(jsRuntime);
        }
        public async Task VerTerritorios(string id)
        {
            await JsInteropUtils.Territorio(jsRuntime,id);
        }
        async Task SaveTerritorio()
        {
            try
            {
                if (this.Territorio.IdDivisionTerritorialPadre <= 0 || this.Territorio.IdDivisionTerritorialPadre <= 0)
                {
                    await OnSaveNotification("Error Al Guardar, llene todos los campos");
                }
                else
                {
                    await BlockPage();
                    await TerritoriosService.SaveDivisionTerritorial(this.Territorio);
                    await UnBlockPage();
                    await SweetMessageBox("Datos Guardados", "success", "/Territorios");
                    //await OnGuardarNotification();
                    //NavManager.NavigateTo("/Territorios");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
          


        }
        void RaiseInvalidSubmit()
        {

        }

        int territorioSelected = -1;
        int localidadPadreSelected = -1;
        //bool permiteCalleChecked = false;
        private async void selectedTerritorio(ChangeEventArgs args)
        {
            territorioSelected = Convert.ToInt32(args.Value.ToString());

            //if (territorioSelected > 0)
            //{
            await VerTerritorios(territorioSelected.ToString());
            this.Territorio.IdDivisionTerritorialPadre = territorioSelected;

            //}
            //else
            //{
            //    this.Territorio.IdDivisionTerritorialPadre = -1;
            //}
        }
        private void selectedLocalidadPadre(ChangeEventArgs args)
        {
            localidadPadreSelected = Convert.ToInt32(args.Value.ToString());

            //if (localidadPadreSelected > 0)
            //{
            this.Territorio.IdDivisionTerritorialPadre = localidadPadreSelected;
            //    }
            //else
            //{
            //    this.Territorio.IdLocalidadPadre = -1;
            //}
        }
        private void checkPermiteCalle(ChangeEventArgs args)
        {
            this.Territorio.PermiteCalle = Convert.ToBoolean(args.Value);
        }

        async Task CreateOrEdit(int id) { }

        private bool FilterFunc(DivisionTerritorial element)
        {
            return true;
        }
    }
}
