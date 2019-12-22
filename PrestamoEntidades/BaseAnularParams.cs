using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
{
    public class BaseAnularParams : BaseUsuario
    {
        [Required]
        public virtual int id { get; set; }

    }
    
}
