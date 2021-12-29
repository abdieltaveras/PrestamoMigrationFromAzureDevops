using Microsoft.AspNetCore.Components;
using PcpUtilidades;
using PrestamoBlazorApp.Models;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using PrestamoValidaciones;
using Radzen;
using Radzen.Blazor;
using System;
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
        ClientesService clientesService { get; set; }

        // parametros
        [Parameter]
        public int idCliente { get; set; }

        List<Imagen> FotosRostroCliente { get; set; } = new List<Imagen>();

        List<Imagen> FotosDocIdentificacion { get; set; } = new List<Imagen>();
        // miembros
        string searchSector = string.Empty;

        public Cliente cliente { get; set; } = new Cliente();
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

            await Handle_GetData(prepareModel, @"/Clientes");
            //await prepareModel();
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

        }

        private void UpdateTieneConyuge(bool value)
        {
            cliente.TieneConyuge = value;
        }

        private void UpdateEstadoCivil(int value)
        {
            //NotifyMessageBox("estado civil actualizado");
            cliente.IdEstadoCivil = value;
        }

        private async Task prepareModel()
        {
            Ocupaciones = await GetOcupaciones();
            LoadedFotos = false;
            if (idCliente != 0)
            {
                var clientes = await clientesService.GetClientesAsync(new ClienteGetParams { IdCliente = idCliente }, true);
                this.cliente = clientes.FirstOrDefault();
            }
            if (this.cliente == null || idCliente <= 0)
            {
                this.cliente = new Cliente
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
                this.conyuge = cliente.InfoConyugeObj;
                this.infoLaboral = cliente.InfoLaboralObj;
                this.direccion = cliente.InfoDireccionObj.ToJson().ToType<DireccionModel>(); ;
                var localidad = await localidadService.Get(new LocalidadGetParams { IdLocalidad = this.direccion.IdLocalidad });
                this.direccion.selectedLocalidad = localidad.FirstOrDefault().Nombre;
            }
            SetReferencias(cliente.InfoReferenciasObj);
            FilterImagesByGroup();
            LoadedFotos = true;
            //StateHasChanged();
        }

        private void FilterImagesByGroup()
        {
            FotosRostroCliente.Clear();
            FotosDocIdentificacion.Clear();
            cliente.ImagenesObj.ForEach(item =>
            {
                if (!item.Quitar)
                {
                    if (item.Grupo == TiposFotosCliente.RostroCliente.ToString()) FotosRostroCliente.Add(item);
                    if (item.Grupo == TiposFotosCliente.DocIdentificacion.ToString()) FotosDocIdentificacion.Add(item);
                }
            });

        }

        private void SetReferencias(List<Referencia> infoReferenciasObj)
        {
            for (int i = 0; i < 5; i++)
            {
                var referencia = new Referencia { Tipo = (int)EnumTiposReferencia.Personal };

                if ((i + 1) <= infoReferenciasObj.Count())
                {
                    referencia = infoReferenciasObj[i];
                }
                referencias.Add(referencia);
            }
        }

        //async Task SaveCliente()
        async Task SaveCliente()
        {
            await Handle_SaveData(SaveData, () => OnSaveNotification(redirectTo: @"\Clientes"), null, false);
        }

        private async Task<bool> SaveData()
        {
            this.cliente.InfoConyugeObj = conyuge;
            this.cliente.InfoReferenciasObj = referencias;
            this.cliente.InfoDireccionObj = direccion;
            this.cliente.InfoLaboralObj = infoLaboral;

            var result = Validaciones.ForCliente001().Validate(cliente);
            var validacionesFallidas = result.Where(item => item.Success == false);
            var MensajesValidacionesFallida = string.Join(", ", validacionesFallidas.Select((item, i) => (i + 1) + "-" + item.Message + Environment.NewLine));
            if (validacionesFallidas.Count() > 0)
            {
                SweetMessageBox("Se han encontrado errores" + Environment.NewLine + MensajesValidacionesFallida, "error", "", 5000);
                return false;
            }
            try
            {
                //todo: validationresult https://www.c-sharpcorner.com/UploadFile/20c06b/using-data-annotations-to-validate-models-in-net/

                await clientesService.SaveCliente(this.cliente);

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

        private void SetImages(Imagen imagen)
        {
            cliente.ImagenesObj.Add(imagen);
            FilterImagesByGroup();
        }



        private void RemoveImages(Imagen imagen)
        {
            var index = this.cliente.ImagenesObj.IndexOf(imagen);
            this.cliente.ImagenesObj[index].Quitar = true;
            this.cliente.ImagenesObj.Where(img => img.NombreArchivo == imagen.NombreArchivo).FirstOrDefault().Quitar = true;
        }

    }
}
