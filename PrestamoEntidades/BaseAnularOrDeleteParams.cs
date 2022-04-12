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
        public int IdRegistro { get; set; }
    }
}
