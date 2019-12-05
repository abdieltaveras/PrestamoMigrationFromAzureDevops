using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
{
    public abstract class BaseDelParams : BaseUsuario
    {
        [Required]
        public virtual int id { get; set; }

    }
    
}
