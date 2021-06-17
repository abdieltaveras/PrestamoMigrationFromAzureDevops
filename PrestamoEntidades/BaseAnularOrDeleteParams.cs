using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
{
    public class BaseAnularOrDeleteParams : BaseUsuario
    {
        [Required]
        public virtual int Id { get; set; }
    }

    
}
