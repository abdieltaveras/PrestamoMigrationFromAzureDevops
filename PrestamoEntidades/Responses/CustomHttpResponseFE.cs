using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades.Responses
{
    public class CustomHttpResponseFE<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
    }
    public class CustomHttpResponse
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }

    }
}
