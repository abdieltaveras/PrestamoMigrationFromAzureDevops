using PcpUtilidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    /// <summary>
    /// Enumeration get instances by acccxv essing its statics members
    /// </summary>
    public class CatalogoName : Enumeration
    {
        public string IdColumnName { get; }
        public string TableName { get; }
        public static CatalogoName Generico = new(-1, nameof(Generico), "Generico", "IdGenerico");
        public static CatalogoName Ocupacion = new(1, nameof(Ocupacion), "tblOcupaciones","IdOcupacion");
        public static CatalogoName Color = new(2, nameof(Color),"tblColores","IdColor");
        public static CatalogoName TipoSexo = new(3, nameof(TipoSexo), "tblTipoSexos", "IdTipoSexo");
        public static CatalogoName TipoTelefono = new(4, nameof(TipoTelefono), "tblTiposTelefonos", "IdTipoTelefono");
        private CatalogoName(int id, string name, string tableName, string idColumnName) : base(id, name)
        {
            this.IdColumnName = idColumnName;
            this.TableName = tableName;
        }
    }

}