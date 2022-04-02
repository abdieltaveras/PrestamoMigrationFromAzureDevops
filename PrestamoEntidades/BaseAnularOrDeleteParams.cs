using DevBox.Core.DAL.SQLServer;
using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
{
    public class BaseAnularOrDeleteParams : BaseUsuario
    {
        public virtual int Id { get; set; }
    }

    public class BaseCatalogoDeleteParams : BaseUsuario
    {
        public string NombreTabla { get; set; } = string.Empty;
        public string IdNombreColumna { get; set; } = string.Empty;
        public int IdRegistro { get; set; }
    }
}
