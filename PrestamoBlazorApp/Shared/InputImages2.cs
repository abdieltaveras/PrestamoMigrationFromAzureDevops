using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public partial class InputImages2
    {

        [Inject]
        IJSRuntime JsRuntime { get; set; }

        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();

        List<Imagen> imagenes = new List<Imagen>();
        [Parameter]
        public string HeightInPx { get; set; } = "200px";


        [Parameter]
        public string GrupoImagen { get; set; }

        [Parameter]
        public IEnumerable<Imagen> ListaImagenes { get; set; } = new List<Imagen>();

        [Parameter]
        public string MensajeLimiteImagenes { get; set; } = string.Empty;

        [Parameter]
        public string Text { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<Imagen> OnImageSet { get; set; }

        [Parameter]
        public EventCallback<Imagen> OnImageRemove { get; set; }

        [Parameter]
        public int ImageQty { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (ListaImagenes.Count() > 0)
            {
                imagenes.AddRange(ListaImagenes);
            }
        }

        async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var imageFiles = e.GetMultipleFiles();
            if (imageFiles.Count > ImageQty)
            {
                if (string.IsNullOrEmpty(MensajeLimiteImagenes))
                {
                    MensajeLimiteImagenes = $"eligio {imageFiles.Count} imagenes, y solo se permiten {this.ImageQty}";
                    await JsInteropUtils.NotifyMessageBox(JsRuntime, MensajeLimiteImagenes);
                }
                return;
            }
            var format = "image/png";
            Imagen imagen = null;
            foreach (var imageFile in imageFiles)
            {

                
                var resizedImageFile = imageFile.Size > 20000 ? 
                    await imageFile.RequestImageFileAsync(format,100, 100)
                    :
                    imageFile;

                var buffer = new byte[resizedImageFile.Size];
                try
                {
                    await resizedImageFile.OpenReadStream().ReadAsync(buffer);
                }
                catch (Exception ex)
                {
                    await JsInteropUtils.SweetMessageBox(JsRuntime, "La imagen elegida fue ajustada su tamaño, si no tiene buena calidad recomendamos elegir otra","warning");
                }
                var imageDataUrl = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                imagen = new Imagen { Base64string = imageDataUrl, Grupo = this.GrupoImagen, NombreArchivo = Guid.NewGuid().ToString(), Agregar=true };
                if (imageFile.Size > 20000)
                {
                    //await JsInteropUtils.SweetMessageBox(JsRuntime, "La imagen elegida fue ajustada su tamaño, si no tiene buena calidad recomendamos elegir otra", "warning");
                    await JsInteropUtils.NotifyMessageBox(JsRuntime, "La imagen elegida fue ajustada su tamaño, si no tiene buena calidad recomendamos elegir otra",6000);
                }
            }
            imagenes.Add(imagen);
            await OnImageSet.InvokeAsync(imagen);
        }

        void QuitarImagen(object _imagen)
        {
            var imagen = _imagen as Imagen;
            imagenes.Remove(imagen);
            OnImageRemove.InvokeAsync(imagen);
        }

    }

}

