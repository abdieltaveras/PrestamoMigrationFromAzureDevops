using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PrestamoBLL
{
    public static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static IEnumerable<string> SaveFiles(string path, IEnumerable<HttpPostedFileBase> files)
        {
            List<string> fileNames = new List<string>();
            var index = 0;
            files.ToList().ForEach(
                    f =>
                    {
                        try
                        {
                            var filename= SaveFile(path, f);
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
        public static string SaveFile(string path, HttpPostedFileBase file)
        {
            string fileName = string.Empty;
            if (file == null) return fileName;
            var extension = Path.GetExtension(file.FileName);
            fileName = Guid.NewGuid().ToString() + extension;
            var fullpath = Path.Combine(path, fileName);
            try
            {
                file.SaveAs(fullpath);
            }
            catch (Exception e)
            {
                throw e;
            }
            return fileName;
        }
    }
}
