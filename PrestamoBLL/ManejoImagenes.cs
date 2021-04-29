using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public class ManejoImagenes
    {
        private List<Imagen> Imagenes;
        private string DirectorioDeImagen { get; set; }

        private string InicioNombreImagen { get; set; }

        private ManejoImagenes(List<Imagen> imagenes, string directorioDeImagenes, string inicioNombreImagen)
        {
            this.Imagenes = imagenes;
            this.DirectorioDeImagen = directorioDeImagenes;
        }

        public static void ProcesarImagenes(List<Imagen> imagenes, string directorioDeImagenes, string inicioNombreImagen)
        {
            var instancia = new ManejoImagenes(imagenes, directorioDeImagenes, inicioNombreImagen);
            foreach (var item in instancia.Imagenes)
            {
                if (item.Agregar)
                {
                    var nombreArchivo = instancia.InicioNombreImagen + Guid.NewGuid().ToString();
                    try
                    {
                        var result = Utils.ConvertBase64ToImage(item.Base64string, instancia.DirectorioDeImagen, nombreArchivo);
                        item.NombreArchivo = result.Item1;
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }
                }
                if (item.Quitar)
                {
                    var fileName = instancia.DirectorioDeImagen + item.NombreArchivo;
                    FileInfo file = new FileInfo(fileName);
                    if (file.Exists)//check file exsit or not  
                    {
                        file.Delete();
                    }
                }
            }
        }
    }
}
