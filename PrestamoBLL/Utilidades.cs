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
        public static string SaveFile(string path, HttpPostedFileBase file)
        {
            string fileName = string.Empty;
            if (file == null) throw new NullReferenceException(); //return fileName;
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
