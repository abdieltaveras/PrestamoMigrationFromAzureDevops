using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Data;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL.Entidades;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class CreateOrEditCliente
    {

        [Inject]
        OcupacionesService ocupacionesService { get; set; }

        string searchSector = string.Empty;
        string selectedLocalidad = "Ninguna";
        [Parameter]
        public string idCliente { get; set; }
        public Cliente cliente { get; set; } = new Cliente();
        Conyuge conyuge { get; set; } = new Conyuge();

        Direccion direccion { get; set; }  = new Direccion();

        InfoLaboral infoLaboral { get; set; } = new InfoLaboral();

        EventConsole console;
        string TextoForActivo { get; set; } = "Si";
        List<EnumModel> TiposIdentificacionPersonaList { get; set; }

        [Inject]
        ClientesService clientesService { get; set; }

        
        
        private IEnumerable<Ocupacion> Ocupaciones { get; set; } = new List<Ocupacion>();

        private async Task<IEnumerable<Ocupacion>> GetOcupaciones()
        {
            var result = await ocupacionesService.GetOcupacionesAsync();
            return result;
        }
        List<Referencia> referencias = new List<Referencia>();

        Referencia referencia1 = new Referencia();
        Referencia referencia2 = new Referencia();
        Referencia referencia3 = new Referencia();
        bool disableCodigo { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            var _idCliente = Convert.ToInt32(idCliente);
            Ocupaciones = await GetOcupaciones();
            if ( _idCliente != 0)
            {
                var clientes = await clientesService.GetClientesAsync(new ClienteGetParams { IdCliente = _idCliente,ConvertJsonToObj=true });
                this.cliente = clientes.FirstOrDefault();
            }
            prepTestData();
            await base.OnInitializedAsync();
        }

        private void prepTestData()
        {
            if (this.cliente.IdCliente == 0)
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
                InfoDireccionObj = new Direccion { Calle = "c3", Latitud = 1, Longitud = 2 }
                };
            }
            this.conyuge = cliente.InfoConyugeObj;
            this.infoLaboral = cliente.InfoLaboralObj;
            this.direccion = cliente.InfoDireccionObj;
            referencia1 = new Referencia { Tipo = (int)EnumTiposReferencia.Personal , NombreCompleto="r1"};
            referencia2 = new Referencia { Tipo = (int)EnumTiposReferencia.Comercial, NombreCompleto ="r2" };
            referencia3 = new Referencia { Tipo = (int)EnumTiposReferencia.Familiar, NombreCompleto ="r3" };
            referencias.Add(referencia1);
            referencias.Add(referencia2);
            referencias.Add(referencia3);


        }

        private bool loading { get; set; }
        //async Task SaveCliente()
        async Task SaveCliente()
        {
            //todo: validationresult https://www.c-sharpcorner.com/UploadFile/20c06b/using-data-annotations-to-validate-models-in-net/
            loading = true;
            this.cliente.InfoConyugeObj = conyuge;
            this.cliente.InfoReferenciasObj = referencias;
            this.cliente.InfoDireccionObj = direccion;
            this.cliente.InfoLaboralObj = infoLaboral;
            await clientesService.SaveCliente(this.cliente);
            loading = false;
        }


        void OnChange(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
            var selectedValue = Convert.ToInt32(str);
            //console.Log($"{name} value changed to {str}");
        }

        void OnInputFileChange(InputFileChangeEventArgs e)
        {
            var imageFiles = e.GetMultipleFiles();
        }
        
        private void SetImages(IList<string> images)
        {
            this.cliente.ImagesForCliente = images;
        }

        protected void Handle_ConyugeChange(Conyuge conyuge)
        {
            this.cliente.InfoConyugeObj = conyuge;
        }

        SearchDireccion searchDireccion { get; set; } = new SearchDireccion();
        
    }
    public class SearchDireccion
    {
        public string SearchSector { get; set; } = string.Empty;

        public string SelectedLocalidad { get; set; } = string.Empty;
    }

    
}

