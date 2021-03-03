using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class Catalogo : BaseCatalogo
    {
        public int Id { get; set; } = 0;
        public string IdTabla { get; set; } = string.Empty;
        public string NombreTabla { get; set; }

        public override int GetId()
        {
            throw new NotImplementedException();
        }
    }

    public class ToggleStatusCatalogo : BaseAnularParams
    {
        public string IdTabla { get; set; } = string.Empty;
        public string NombreTabla { get; set; }
        public bool Activo { get; set; }
    }

    public class DelCatalogo : BaseAnularParams
    {
        /// <summary>
        /// el id del registro a borrar
        /// </summary>
        public string IdRegistro { get; set; } = string.Empty;
        /// <summary>
        /// El nombre del campo o columna que hara la comparacion
        /// </summary>
        public string NombreColumna { get; internal set; } 
        /// <summary>
        /// el nombre de la tabla que se ejecutara la anulacion
        /// </summary>
        public string NombreTabla { get; set; }
    }

    public class SearchCatalogoParams
    {
        public string TextToSearch { get; set; } = "";
        public string TableName { get; set; } = string.Empty;
        public int IdNegocio { get; set; } = -1;
    }
}
