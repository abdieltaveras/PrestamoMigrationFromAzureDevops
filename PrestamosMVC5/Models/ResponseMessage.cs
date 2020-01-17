using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class ResponseMessage
    {

        public static string TYPE_ERROR = "error";
        public static string TYPE_SUCCESS = "success";
        public static string TYPE_WARNING = "warning";

        public string Tipo { get; set; }
        public string Mensaje { get; set; }

    }
}