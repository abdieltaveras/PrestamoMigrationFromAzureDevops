using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class RoleVM
    {
        public Role Role { get; set; }
        public IEnumerable<Role> ListaRoles { get; set; } = new List<Role>();
        public IEnumerable<Operacion> Operaciones { get; set; } = new List<Operacion>();
        public dynamic ListaOperaciones { get; set; }

    }
}