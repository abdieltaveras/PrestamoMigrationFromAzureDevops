using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    [System.Diagnostics.DebuggerDisplay("{PositiveValue}")]
    public struct NotNullOrEmptyString 
    {
        private string m_value;

        private NotNullOrEmptyString(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentOutOfRangeException("value", "el valor no puede esta nulo o vacio");

            m_value = value;
        }

        public static implicit operator NotNullOrEmptyString(string temp)
        {
            return new NotNullOrEmptyString(temp);
        }

        public static implicit operator string(NotNullOrEmptyString c)
        {
            return c.m_value;
        }

        // operators for other numeric types...

        public override string ToString()
        {
            return m_value.ToString();
        }
    }
}
