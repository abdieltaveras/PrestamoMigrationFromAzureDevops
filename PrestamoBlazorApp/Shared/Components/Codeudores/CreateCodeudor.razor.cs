using Microsoft.AspNetCore.Components;
using PcpUtilidades;
using PrestamoBlazorApp.Models;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using PrestamoValidaciones;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Codeudores
{
    public partial class CreateCodeudor : BaseForCreateOrEdit
    {
        // servicios

        [Inject]
        OcupacionesService ocupacionesService { get; set; }
        [Inject]
        LocalidadesService localidadService { get; set; }
        [Inject]
        CodeudoresService CodeudoresService { get; set; }

        // parametros
        [Parameter]
        public int IdCodeudor { get; set; }

        List<Imagen> FotosRostro { get; set; } = new List<Imagen>();

        List<Imagen> FotosDocIdentificacion { get; set; } = new List<Imagen>();
        // miembros
        string searchSector = string.Empty;

        public Codeudor Codeudor { get; set; } = new Codeudor();
        Conyuge conyuge { get; set; } = new Conyuge();

        DireccionModel direccion { get; set; } = new DireccionModel();

        InfoLaboral infoLaboral { get; set; } = new InfoLaboral();

        EventConsole console;

        List<EnumModel> TiposIdentificacionPersonaList { get; set; }

        private IEnumerable<Ocupacion> Ocupaciones { get; set; } = new List<Ocupacion>();

        bool LoadedFotos = false;

        private async Task<IEnumerable<Ocupacion>> GetOcupaciones()
        {
            var result = await ocupacionesService.Get(new OcupacionGetParams());
            return result;
        }
        List<Referencia> referencias = new List<Referencia>();




        protected override async Task OnInitializedAsync()
        {

            await Handle_GetData(prepareModel, @"/Codeudores");
            //await prepareModel();
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }

     

        private void UpdateEstadoCivil(int value)
        {
            Codeudor.IdEstadoCivil = value;
        }

        private async Task prepareModel()
        {
            Ocupaciones = await GetOcupaciones();
            LoadedFotos = false;
            if (IdCodeudor != 0)
            {
                var codeudores = await CodeudoresService.GetCodeudoresAsync(new CodeudorGetParams { IdCodeudor = IdCodeudor }, true);
                this.Codeudor = codeudores.FirstOrDefault();
            }
            if (this.Codeudor == null || IdCodeudor <= 0)
            {
                Codeudor = new Codeudor();
                //this.Codeudor = new Codeudor
                //{
                //    IdLocalidadNegocio=1,
                //    Codigo = "Nuevo",
                //    Nombres = "a1",
                //    Apellidos = "a2",
                //    Apodo = "a3",
                //    IdTipoIdentificacion = 1,
                //    NoIdentificacion = "000-0000000-1",
                   
                //    InfoLaboralObj = new InfoLaboral { Direccion = "d1", Nombre = "d2" },
                //    InfoDireccionObj = new Direccion
                //    {
                //        IdLocalidad = 5,
                //        Calle = "Calle emilio prud home #5",
                //        Latitud = 18.43056,
                //        Longitud = -68.98449
                //    }
                //};
                //clinica coral Lat = 18.43190, Lng = -68.98503;
            }
            else
            {
              
                this.infoLaboral = Codeudor.InfoLaboralObj;
                this.direccion = Codeudor.InfoDireccionObj.ToJson().ToType<DireccionModel>(); ;
                var localidad = await localidadService.Get(new LocalidadGetParams { IdLocalidad = this.direccion.IdLocalidad });
                this.direccion.selectedLocalidad = localidad.FirstOrDefault().Nombre;
            }
            FilterImagesByGroup();
            LoadedFotos = true;
            //StateHasChanged();
        }

        private void FilterImagesByGroup()
        {
            FotosRostro.Clear();
            FotosDocIdentificacion.Clear();
            Codeudor.ImagenesObj.ForEach(item =>
            {
                if (!item.Quitar)
                {
                    if (item.Grupo == TiposFotosPersonas.Rostro.ToString()) FotosRostro.Add(item);
                    if (item.Grupo == TiposFotosPersonas.DocIdentificacion.ToString()) FotosDocIdentificacion.Add(item);
                }
            });

        }

       
        async Task SaveCodeudor()
        {
            await Handle_SaveData(SaveData, () => OnSaveNotification(redirectTo: @"\Codeudores"), null, false);
        }

        private async Task<bool> SaveData()
        {
            direccion.IdLocalidad = 1;
            direccion.IdLocalidadNegocio = 1;
            this.Codeudor.InfoDireccionObj = direccion;
            this.Codeudor.InfoLaboralObj = infoLaboral;
            try
            {
                await CodeudoresService.Post(this.Codeudor);
            }
            catch (ValidationObjectException e)
            {
                await JsInteropUtils.NotifyMessageBox(jsRuntime, $"Lo siento error al guardar los datos mensaje recibido {e.Message}");
            }
            return true;
        }

        private void SetImages(Imagen imagen)
        {
            Codeudor.ImagenesObj.Add(imagen);
            FilterImagesByGroup();
        }



        private void RemoveImages(Imagen imagen)
        {
            var index = this.Codeudor.ImagenesObj.IndexOf(imagen);
            this.Codeudor.ImagenesObj[index].Quitar = true;
            this.Codeudor.ImagenesObj.Where(img => img.NombreArchivo == imagen.NombreArchivo).FirstOrDefault().Quitar = true;
        }

    }
}
