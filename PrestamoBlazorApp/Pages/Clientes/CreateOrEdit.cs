using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PrestamoBlazorApp.Data;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class CreateOrEdit
    {
        [Inject]
        OcupacionesService ocupacionesService { get; set; }

        [Parameter]
        public int idCliente { get; set; }
        public Cliente cliente { get; set; }

        EventConsole console;
        string TextoForActivo { get; set; } = "Si";
        List<EnumModel> TiposIdentificacionPersonaList { get; set; }

        [Inject]
        ClientesService clientesService { get; set; }

        private IEnumerable<EnumModel> EstadosCiviles = EnumToList.GetEnumEstadosCiviles();
        private IEnumerable<EnumModel> TiposIdentificacion = EnumToList.GetEnumTiposIdentificacionPersona();

        private IEnumerable<Ocupacion> Ocupaciones { get; set; } = new List<Ocupacion>();

        private async Task<IEnumerable<Ocupacion>> GetOcupaciones()
        {
            var result = await ocupacionesService.GetOcupacionesAsync();
            return result;
        }

        bool disableCodigo { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            
            await base.OnInitializedAsync();
            this.cliente = new Cliente();
            this.cliente.Codigo = "Nuevo";
            Ocupaciones = await GetOcupaciones();
            //TiposIdentificacionPersonaList = EnumToAList.GetEnumTiposIdentificacionPersona();
        }

        private bool loading { get; set; }
        //async Task SaveCliente()
        async Task SaveCliente()
        {
            loading = true;
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

        
    }

}

