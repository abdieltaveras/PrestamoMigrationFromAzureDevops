using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace PrestamoBLL
{
    public static class Utils
    {
        public static int GetIdFromDataTable(DataTable objectContainingId)
        {
            return Convert.ToInt32(objectContainingId.Rows[0][0]);
        }
        public static string SaveFiles(string path, HttpPostedFileBase file, string fileName="")
        {
            var files = new List<HttpPostedFileBase>();
            files.Add(file);
            var result = Utils.SaveFiles(path, files, fileName);
            return result.FirstOrDefault();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static IEnumerable<string> SaveFiles(string path, IEnumerable<HttpPostedFileBase> files, string fileName = "")
        {
            List<string> fileNames = new List<string>();
            var index = 0;
            files.ToList().ForEach(
                    f =>
                    {
                        try
                        {
                            var filename= SaveFile(path, f, fileName);
                            fileNames.Add(filename);
                        }
                        catch (Exception e)
                        {
                            fileNames.Add($"Error {e.Message}");
                        }
                        index++;
                    }
                );
            return fileNames;
        }


        public static string SaveFile(string path, HttpPostedFileBase file, string fileName = "")
        {
            string _fileName = string.Empty;
            if (file == null) return _fileName;
            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(fileName))
            {
                _fileName = Guid.NewGuid().ToString() + extension;
            }
            else
            {
                _fileName = fileName + extension; 
            }
            var fullpath = Path.Combine(path, _fileName);
            try
            {
                file.SaveAs(fullpath);
            }
            catch (Exception e)
            {
                throw e;
            }
            return _fileName;
        }

        /// <summary>
        /// Recibe un string64base y lo convierte en una imagen el string contiene el texto inicial "data:Image/jpeg;base64,"
        /// el metodo lo quita para crear la imagen de base64 string
        /// </summary>
        /// <param name="basestring"></param>
        /// <returns></returns>
        public static Image convertBase64ToImage(string basestring, string fullPath)
        {
            int lengthtoremove = "data:Image/jpeg;base64,".Length;
            string imgbasestring = basestring.Remove(0, lengthtoremove);
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(imgbasestring);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
                image.Save(fullPath);
            }
            
            return image;
        }

        public static string SaveFile(string path, string base64Image)
        {
            
            string fileName = string.Empty;
            var extension = "jpeg";
            fileName = Guid.NewGuid().ToString() +"."+ extension;
            var fullpath = Path.Combine(path, fileName);
            
            int lengthtoremove = "data:Image/jpeg;base64,".Length;
            string imgbasestring = base64Image.Remove(0, lengthtoremove);
            File.WriteAllBytes(fullpath, Convert.FromBase64String(imgbasestring));
 
            //var imagen = convertBase64ToImage(base64Image, fullpath);
            return fileName;
        }
    }
}
