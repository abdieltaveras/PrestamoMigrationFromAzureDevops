using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.DivisionesTerritoriales
{
    public partial class TiposDivisionTerritorial : BaseForList
    {
        [Inject]
        DivisionTerritorialService TerritoriosService { get; set; }
        IEnumerable<DivisionTerritorial> TiposTerritorios { get; set; } = new List<DivisionTerritorial>();

        private int IdDivisionTerritorialSelected { get; set; }
        void Clear() => TiposTerritorios = new List<DivisionTerritorial>();

        protected override async Task OnInitializedAsync()
        {
            await GetTiposDivisionesTerritoriales();
            await base.OnInitializedAsync();
        }

        private async Task GetTiposDivisionesTerritoriales(bool stateChange = false)
        {
            TiposTerritorios = await TerritoriosService.GetTiposDivisionTerritorial();
            if (stateChange) StateHasChanged();
        }

        async Task CreateOrEdit(int id)
        {
            DialogParameters parameters = SetParametersToView(id,true);
            DialogSvr.Show<CrudTipoDivisionTerritorial>("", parameters, OptionsForDialog.SmallFullWidthCloseButtonCenter);
        }

        async Task CreateOrEditComponents(int id)
        {
            DialogParameters parameters = SetParametersToView(id,false);
            DialogSvr.Show<CrudComponentsDivisionTerritorial>("", parameters, OptionsForDialog.SmallFullWidthCloseButtonCenter);
        }

        private DialogParameters SetParametersToView(int id, bool addCallBack)
        {
            IdDivisionTerritorialSelected = id;
            DialogParameters parameters = new DialogParameters();
            parameters.Add("IdDivisionTerritorial", id);
            if (addCallBack)
            {
                var action = EventCallback.Factory.Create<bool>(this, (e) => GetTiposDivisionesTerritoriales(e));
                parameters.Add("HandleListUpdate", action);
            }
            return parameters;
        }
        private bool FilterFunc(DivisionTerritorial element)
        {
            
            return true;
        }
    }

    class oldCodeNoUsar
    {
        //public async Task VerTerritorios(string id)
        //{
        //    await JsInteropUtils.Territorio(jsRuntime,id);
        //}
        //async Task SaveTerritorio()
        //{
        //    try
        //    {
        //        if (this.Territorio.IdDivisionTerritorialPadre <= 0 || this.Territorio.IdDivisionTerritorialPadre <= 0)
        //        {
        //            await OnSaveNotification("Error Al Guardar, llene todos los campos");
        //        }
        //        else
        //        {
        //            await BlockPage();
        //            await TerritoriosService.SaveDivisionTerritorial(this.Territorio);
        //            await UnBlockPage();
        //            await SweetMessageBox("Datos Guardados", "success", "/Territorios");
        //            //await OnGuardarNotification();
        //            //NavManager.NavigateTo("/Territorios");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }



        //}
        //bool permiteCalleChecked = false;
        //private async void selectedTerritorio(ChangeEventArgs args)
        //{
        //    territorioSelected = Convert.ToInt32(args.Value.ToString());

        //    //if (territorioSelected > 0)
        //    //{
        //    await VerTerritorios(territorioSelected.ToString());
        //    this.Territorio.IdDivisionTerritorialPadre = territorioSelected;

        //    //}
        //    //else
        //    //{
        //    //    this.Territorio.IdDivisionTerritorialPadre = -1;
        //    //}
        //}
        //private void selectedLocalidadPadre(ChangeEventArgs args)
        //{
        //    localidadPadreSelected = Convert.ToInt32(args.Value.ToString());

        //    //if (localidadPadreSelected > 0)
        //    //{
        //    this.Territorio.IdDivisionTerritorialPadre = localidadPadreSelected;
        //    //    }
        //    //else
        //    //{
        //    //    this.Territorio.IdLocalidadPadre = -1;
        //    //}
        //}
        //private void checkPermiteCalle(ChangeEventArgs args)
        //{
        //    this.Territorio.PermiteCalle = Convert.ToBoolean(args.Value);
        //}

    }
}
