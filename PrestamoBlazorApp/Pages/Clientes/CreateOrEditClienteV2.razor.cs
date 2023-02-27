using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpUtilidades;


using PrestamoBlazorApp.Models;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBlazorApp.Shared.Components.Forms.InputReferencia;
using PrestamoEntidades;
using PrestamoValidaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class CreateOrEditClienteV2 : BaseForCreateOrEdit
    {
        // servicios
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject] protected CatalogosServicesFactoryManager CatalogosFactory { get; set; }
        
        [Inject]
        LocalidadesService localidadService { get; set; }
        [Inject]
        ClientesService clientesService { get; set; }

        // parametros
        [Parameter]
        public int idCliente { get; set; }

        List<Imagen> FotosRostroCliente { get; set; } = new List<Imagen>();

        List<Imagen> FotosDocIdentificacion { get; set; } = new List<Imagen>();
        // miembros
        string searchSector = string.Empty;

        public Cliente Cliente { get; set; } = new Cliente();
        Conyuge Conyuge { get; set; } = new Conyuge();

        DireccionModel Direccion { get; set; } = new DireccionModel();
        Direccion InfoDireccion { get; set; } = new Direccion();


        InfoLaboral InfoLaboral { get; set; } = new InfoLaboral();

        
        
        List<EnumModel> TiposIdentificacionPersonaList { get; set; }

        private IEnumerable<BaseInsUpdGenericCatalogo> Ocupaciones { get; set; } = new List<Ocupacion>();

        bool LoadedFotos = false;

        private async Task<IEnumerable<BaseInsUpdGenericCatalogo>> GetOcupaciones()
        {
            var result = await CatalogosFactory.OcupacionesService.Get(new BaseCatalogoGetParams());
            return result;
        }
        List<Referencia> Referencias = new List<Referencia>();


        

        protected override async Task OnInitializedAsync()
        {

            await Handle_GetData(prepareModel,@"/Clientes");
            //await prepareModel();
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
                
        }

        private void UpdateTieneConyuge(bool value)
        {
            Cliente.TieneConyuge = value;
        }

        private void UpdateEstadoCivil(int value)
        {
            //NotifyMessageBox("estado civil actualizado");
            Cliente.IdEstadoCivil = value;
        }
        
        private async Task prepareModel()
        {
            Ocupaciones = await GetOcupaciones();
            LoadedFotos = false;
            if (idCliente != 0)
            {
                var clientes = await clientesService.GetClientesAsync(new ClienteGetParams { IdCliente = idCliente}, true);
                this.Cliente = clientes.FirstOrDefault();
            }
            if (this.Cliente == null || idCliente <= 0)
            {
                this.Cliente = new Cliente
                {
                    Codigo = "Nuevo",
                    Nombres = "a1",
                    Apellidos = "a2",
                    Apodo = "a3",
                    IdTipoIdentificacion = 1,
                    NoIdentificacion = "000-0000000-1",
                    InfoConyugeObj = new Conyuge { Nombres = "b1", Apellidos = "b2", DireccionLugarTrabajo = "b3" },
                    InfoLaboralObj = new InfoLaboral { Direccion = "d1", Nombre = "d2" },
                    InfoDireccionObj = new Direccion
                    {
                        IdLocalidad = 5,
                        Calle = "Calle emilio prud home #5",
                        Latitud = 18.43056,
                        Longitud = -68.98449
                    }
                };
                //clinica coral Lat = 18.43190, Lng = -68.98503;
            }
            else
            {
                this.Direccion = Cliente.InfoDireccion.ToType<DireccionModel>(); //Cliente.InfoDireccionObj;
                this.Conyuge = Cliente.InfoConyugeObj;
                this.InfoLaboral = Cliente.InfoLaboralObj;
                this.InfoDireccion = Cliente.InfoDireccion.ToType<Direccion>();
                var localidad = await localidadService.Get(new LocalidadGetParams { IdLocalidad = this.InfoDireccion.IdLocalidad });
                this.Direccion.selectedLocalidad = localidad.FirstOrDefault().Nombre;
            }
            Referencias = Cliente.InfoReferenciasObj;
            FilterImagesByGroup();
            LoadedFotos = true;
            //StateHasChanged();
        }

        private void FilterImagesByGroup()
        {
            FotosRostroCliente.Clear();
            FotosDocIdentificacion.Clear();
            Cliente.ImagenesObj.ForEach(item =>
            {
                if (!item.Quitar)
                {
                    if (item.Grupo == TiposFotosPersonas.Rostro.ToString()) FotosRostroCliente.Add(item);
                    if (item.Grupo == TiposFotosPersonas.DocIdentificacion.ToString()) FotosDocIdentificacion.Add(item);
                }
            });
        }



        async Task SaveCliente()
        {
            await Handle_Funct(() => SaveData());
        }

        private async Task<bool> SaveData()
        {
            this.Cliente.InfoConyugeObj = Conyuge;
            this.Cliente.InfoReferenciasObj = Referencias;
            this.Cliente.InfoDireccionObj = Direccion;
            this.Cliente.InfoLaboralObj = InfoLaboral;
            var result = Validaciones.ForCliente001().Validate(Cliente);
            var validacionesFallidas = result.Where(item => item.Success == false);
            var MensajesValidacionesFallida = string.Join(", ", validacionesFallidas.Select((item, i) => (i + 1) + "-" + item.Message + Environment.NewLine));
            if (validacionesFallidas.Count() > 0)
            {
                await SweetMessageBox("Se han encontrado errores" + Environment.NewLine + MensajesValidacionesFallida, "error", "", 5000);
                return false;
            }
            try
            {
                //todo: validationresult https://www.c-sharpcorner.com/UploadFile/20c06b/using-data-annotations-to-validate-models-in-net/

                await clientesService.SaveCliente(this.Cliente);
         
            }
            catch (ValidationObjectException e)
            {
                await JsInteropUtils.NotifyMessageBox(jsRuntime, $"Lo siento error al guardar los datos mensaje recibido {e.Message}");
            }
            return true;
        }

        //void OnChange(object value, string name)
        //{
        //    var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
        //    var selectedValue = Convert.ToInt32(str);
        //    //console.Log($"{name} value changed to {str}");
        //}
        //void OnInputFileChange(InputFileChangeEventArgs e)
        //{
        //    var imageFiles = e.GetMultipleFiles();
        //}


        //public async Task AddReferencia() // Boton Dinamico
        //{
        //    var parameters = new DialogParameters { ["Referencias"] = Referencias };
        //    DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium,FullWidth = true, CloseButton = true };
        //    var dialog = DialogService.Show<InputReferenciaV3>("Agregar Referencias", parameters, dialogOptions);
        //    var result = await dialog.Result;
        //}
        private async Task AddReferencia()
        {
            var parameters = new DialogParameters { ["Referencias"] = Referencias };
            DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
            var dialog = DialogService.Show<PrestamoBlazorApp.Shared.Components.Clientes.Referencias.ListInfoReferencias>("Listado de Referencias", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                //Tambien se puede Manejar la respuesta Aqui
                //Referencias = (Referencia)result.Data;
            }
        }
        private void SetImages(Imagen imagen)
        {
            Cliente.ImagenesObj.Add(imagen);
            FilterImagesByGroup();
        }



        private void RemoveImages(Imagen imagen)
        {
            var index = this.Cliente.ImagenesObj.IndexOf(imagen);
            this.Cliente.ImagenesObj[index].Quitar = true;
            this.Cliente.ImagenesObj.Where(img => img.NombreArchivo == imagen.NombreArchivo).FirstOrDefault().Quitar = true;
        }

    }
}

