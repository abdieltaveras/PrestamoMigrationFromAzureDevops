using Microsoft.AspNetCore.Components;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Codeudores
{
    public partial class GeneralesCodeudorUI
    {

        [Parameter]
        public Codeudor Codeudor { get; set; }
        [Parameter]
        public IEnumerable<BaseInsUpdGenericCatalogo> Ocupaciones { get; set; } = new List<Ocupacion>();

        [Parameter]
        public bool AllowInputCodigo { get; set; } = false;

        private int _idEstadoCivil;

        private int IdEstadoCivil
        {
            get => _idEstadoCivil;
            set
            {
                _idEstadoCivil = value;
                OnEstadoCivilChange.InvokeAsync(value);
            }
        }

        [Parameter]
        public IEnumerable<Imagen> FotosRostro { get; set; }
        [Parameter]
        public IEnumerable<Imagen> FotosDocIdentificacion { get; set; }
        [Parameter]
        public EventCallback<Imagen> SetImages { get; set; }

        [Parameter]
        public EventCallback<bool> OnTieneConyugeChange { get; set; }

        [Parameter]
        public EventCallback<int> OnEstadoCivilChange { get; set; }
        [Parameter]
        public EventCallback<Imagen> RemoveImages { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //TieneConyuge = Codeudor.TieneConyuge;
            IdEstadoCivil = Codeudor.IdEstadoCivil;
        }
    }
}
