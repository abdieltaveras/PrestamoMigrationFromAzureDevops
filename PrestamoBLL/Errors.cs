using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    internal class ErrorsNumbers
    {
        private static readonly object padlock = new object();

        public static string sqlDupCode => "Valor Duplicado en columna";

        public static string sqlNotFoundProcedure => "El stored procedure indicado no fue encontrado";
    }

    public class ErrorCodes
    {
        public string ErrorCode { get; private set; }

        public string Descripcion { get; private set; }

        public ErrorCodes()
        {

        }

    }
    [Serializable]
    public class BllException : Exception
    {
        public string ErrorCode { get; private set; }
        //public BllException() { }
        public BllException(string message, int number) : base(message) { }
        public BllException(string message, int number, Exception inner) : base(message, inner) { }
        protected BllException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
