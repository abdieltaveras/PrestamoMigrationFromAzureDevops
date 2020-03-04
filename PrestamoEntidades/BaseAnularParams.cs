using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
{
    public class BaseAnularParams : BaseUsuario
    {
        [Required]
        public virtual int Id { get; set; }
    }
}
