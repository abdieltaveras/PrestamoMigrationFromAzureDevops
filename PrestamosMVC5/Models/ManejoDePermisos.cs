using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class ManejoDePermisos
    {
        public string CodigoFuncion { get; } = string.Empty;
        public string CodigoRole { get; } = string.Empty;

        public ManejoDePermisos(string CodigoFuncion, string CodigoRole)
        {
            this.CodigoFuncion = CodigoFuncion;
            this.CodigoRole = CodigoRole;
        }

        public NivelDePermiso TienePermiso()
        {
            //Se consulta la tabla de permiso de roles o la cache de ese usuario para mejorar el rendimiento
            return new NivelDePermiso();
        }
    }

    public class NivelDePermiso
    {
        public bool TienePermiso { get; set; } = false;
        public bool PuedeVer { get; set; } = false;
        public bool PuedeCrear { get; set; } = false;
        public bool PuedeModificar { get; set; } = false;
        public bool PuedeAnular { get; set; } = false;
    }
}