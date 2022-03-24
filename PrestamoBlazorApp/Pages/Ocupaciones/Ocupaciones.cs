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
        
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
