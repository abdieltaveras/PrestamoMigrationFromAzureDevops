using System.ComponentModel.DataAnnotations;

namespace PrestamoBLL.Entidades
{
    public class BaseAnularParams : BaseUsuario
    {
        [Required]
        public virtual int Id { get; set; }
    }

    public class BaseAnularParams2 : BaseUsuario
    {
        [Required]
        public virtual int IdRegistroValor { get; set; }
    }
}
