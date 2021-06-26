using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoWS.Services
{
    public interface IPathProvider
    {
        string MapPath(string path);
    }

    public class PathProvider : IPathProvider
    {
        [Inject]
        private IWebHostEnvironment _webHostingEnvironment { get; set; }
        

        public string MapPath(string path)
        {
            var filePath = Path.Combine(_webHostingEnvironment.WebRootPath, path);
            return filePath;
        }
    }

}
