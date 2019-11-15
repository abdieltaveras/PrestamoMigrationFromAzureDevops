using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
{
    public abstract class BaseDeleteParams : BaseUsuario
    {
        [Required]
        public bool Borrado { get; set; } = true;
    }
    
}
