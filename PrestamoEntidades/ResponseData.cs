using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class ResponseData
    {
        public object Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public ResponseData(object data, bool success, string message, int statusCode )
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
            Success = success;
        }
        public ResponseData(bool success, string message,  int statusCode )
        {
            Data = new {};
            StatusCode = statusCode;
            Message = message;
            Success = success;
        }
        public ResponseData(object data, string message ="")
        {
            Data = data;
            StatusCode = 200;
            Message = message;
            Success = true;
        }
    }
    public class ResponseDataFE<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
