using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.JSInterop;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public partial class InputImages2 : CommonBase
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

        [Parameter]
        public long MaxFileSize { get; set; } = 1024 * 1024 * 2;
        
        bool loadedImages { get; set; } = false;
        private string idFileInputName { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            idFileInputName = Guid.NewGuid().ToString();
            if (ListaImagenes.Count() > 0)
            {
                imagenes.AddRange(ListaImagenes);
                loadedImages = true;
            }
        }

        public async Task<string> UploadMedia(IBrowserFile file)
        {
            int mb = 2;
            byte[] buffer;
            
            using (Stream readStream = file.OpenReadStream(MaxFileSize))
            {
                var buf = new byte[readStream.Length];
                //var ms = new MemoryStream(buf);
                using (MemoryStream ms = new MemoryStream(buf))
                {
                    await readStream.CopyToAsync(ms);
                    buffer = ms.ToArray();
                }
            }
            return Convert.ToBase64String(buffer);
        }

        
        async Task OnInputFileChange(InputFileChangeEventArgs e)
        {

            loadedImages = false;

            var imageFiles = e.GetMultipleFiles();
            
            if (imageFiles.Count > ImageQty)
            {
                if (string.IsNullOrEmpty(MensajeLimiteImagenes))
                {
                    MensajeLimiteImagenes = $"eligio {imageFiles.Count} imagenes, y solo se permiten {this.ImageQty}";
                    await NotifyMessageBySnackBar(MensajeLimiteImagenes, Severity.Warning);
                }
                return;
            }
            var format = "image/png";
            string file64Base = string.Empty;
            foreach (var imageFile in imageFiles)
            {
                if (imageFile.Size > MaxFileSize)
                {
                    await NotifyMessageBySnackBar($"El archivo seleccionado es muy grande, debe elegirlo no mayor a {MaxFileSize/(1024*1024)} mb", Severity.Warning);
                    return;
                }
                else
                file64Base = await UploadMedia(imageFile);
            }

            //await NotifyMessageBySnackBar("pasando imagen a string", Severity.Warning);
            var imageDataUrl = $"data:{format};base64,{file64Base}";
            var imagen = new Imagen { Base64string = imageDataUrl, Grupo = this.GrupoImagen, NombreArchivo = Guid.NewGuid().ToString(), Agregar = true };
            imagenes.Add(imagen);
            await OnImageSet.InvokeAsync(imagen);
            loadedImages = true;
        }


        async Task OnInputFileChange2(InputFileChangeEventArgs e)
        {
            var imageFiles = e.GetMultipleFiles();
            if (imageFiles.Count > ImageQty)
            {
                if (string.IsNullOrEmpty(MensajeLimiteImagenes))
                {
                    MensajeLimiteImagenes = $"eligio {imageFiles.Count} imagenes, y solo se permiten {this.ImageQty}";
                    await NotifyMessageBySnackBar(MensajeLimiteImagenes, Severity.Warning);
                }
                return;
            }
            var format = "image/png";
            Imagen imagen = null;
            foreach (var imageFile in imageFiles)
            {
                byte[] buffer = null;
                bool bigSize = false;

                try
                {
                    bigSize = imageFile.Size > 500000;
                    var resizedImageFile =  bigSize ?
                        await imageFile.RequestImageFileAsync(format, 160, 140)
                        :
                        imageFile;

                    buffer = new byte[resizedImageFile.Size];
                    await resizedImageFile.OpenReadStream().ReadAsync(buffer);
                }
                catch (Exception ex)
                {
                    await NotifyMessageBySnackBar("Sucedio algo que impidio cargar la imagen" + ex.Message, Severity.Warning);
                    return;
                }


                var imageDataUrl = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                imagen = new Imagen { Base64string = imageDataUrl, Grupo = this.GrupoImagen, NombreArchivo = Guid.NewGuid().ToString(), Agregar = true };
                if (bigSize)
                {
                    var message = "La imagen elegida fue ajustada su tamaño, si no tiene buena calidad recomendamos elegir otra";
                    await NotifyMessageBySnackBar(message, Severity.Warning);
                }
            }
            imagenes.Add(imagen);
            await OnImageSet.InvokeAsync(imagen);
        }

        public static Image ScaleImage(Image image, int height)
        {
            double ratio = (double)height / image.Height;
            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            image.Dispose();
            return newImage;
        }
        void QuitarImagen(object _imagen)
        {
            var imagen = _imagen as Imagen;
            imagenes.Remove(imagen);
            OnImageRemove.InvokeAsync(imagen);
        }

    }

}

