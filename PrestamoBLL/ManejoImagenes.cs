using HESRAM.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PcpUtilidades;
using PrestamoEntidades;
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

        public static List<string> GetImagen(string ImagePath, string ImageListJson )
        {
            #region Imagen
            List<string> list = new List<string>();
            //if (result.FirstOrDefault().Imagen1FileName != null)
            //{
                var listResult = JsonConvert.DeserializeObject<dynamic>(ImageListJson);
                foreach (var item in listResult)
                {
                    string imagen = Convert.ToString(item.Value);
                    //Obtenemos la ruta de la imagen
                    string path = ImagePath + item.Value;
                    //Evaluamos si existe la imagen
                    var ExisteImagen = System.IO.File.Exists(path);
                    if (ExisteImagen)
                    {
                        // Utilizamos la libreria HESRAM.Utils y obtenemos el imagebase64 de la ruta de la imagen
                        var imagepath = HConvert.GetImageBase64FromPath(path);
                        // creamos una lista para agregar nuestras bases
                        list.Add("data:image/jpg;base64," + imagepath);
                    }

                }

                IEnumerable<string> sendList = list;
                //garantias.FirstOrDefault().ImagesForGaratiaEntrantes = sendList;
                return sendList.ToList();
            //}

            //******************************************************//
            #endregion

        }
    }
}
