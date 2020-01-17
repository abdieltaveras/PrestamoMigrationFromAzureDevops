using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Exceptions
{
    public class FormException : Exception
    {
        public FormException(string message) : base(message)
        {

        }
    }
}