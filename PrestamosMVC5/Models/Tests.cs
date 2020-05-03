using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    /// <summary>
    ///  Class for testing checkbox behavior
    /// </summary>
    public class TestCheckBox
    {
        public bool Activo { get; set; } = true;
        public bool Bloqueado { get; set; } = true;
    
    }

    public class Item
    {

        public string Nombre { get; set; } = "Cualquier valor";
    }

}