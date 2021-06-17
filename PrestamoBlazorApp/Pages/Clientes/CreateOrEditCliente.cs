using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using PcpUtilidades;
using PrestamoBlazorApp.Data;
using PrestamoBlazorApp.Models;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL;
using PrestamoEntidades;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class CreateOrEditCliente : BaseForCreateOrEdit
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

            await Handle_GetData(prepareModel,@"/Clientes");
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
                var clientes = await clientesService.GetClientesAsync(new ClienteGetParams { IdCliente = idCliente, ConvertJsonToObj = true });
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
                this.direccion = Utils.ToDerived<Direccion, DireccionModel>(cliente.InfoDireccionObj);
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
            await Handle_SaveData(SaveData, () => OnGuardarNotification(redirectTo: @"\Clientes"), null, false);
        }

        private async Task<bool> SaveData()
        {
            //todo: validationresult https://www.c-sharpcorner.com/UploadFile/20c06b/using-data-annotations-to-validate-models-in-net/
            this.cliente.InfoConyugeObj = conyuge;
            this.cliente.InfoReferenciasObj = referencias;
            this.cliente.InfoDireccionObj = direccion;
            this.cliente.InfoLaboralObj = infoLaboral;
            await clientesService.SaveCliente(this.cliente);
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

