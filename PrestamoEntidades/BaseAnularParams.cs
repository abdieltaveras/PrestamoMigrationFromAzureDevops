using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
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
