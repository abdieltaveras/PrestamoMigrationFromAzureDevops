using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class ErrorInfo
    {
        public string Description { get; set; }
        public string Solution { get; set; }

        public string UrlLink { get; set; }
    }
    public static class ViewErrors
    {
        /// <summary>
        /// View Missing Parameters
        /// </summary>
        /// <returns></returns>
        static ErrorInfo vmp01()
        {
            ErrorInfo errorInfo = new ErrorInfo
            {
                Description = "Faltan parametros que no pueden estar nulos",
                Solution = "Revisar que cualquier llamada a este componente o vista le asigne valor a los parametross requeridos",
            };
            return errorInfo;
        }
    }
}
