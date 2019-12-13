using System.ComponentModel.DataAnnotations;
namespace PrestamoEntidades
{
    public abstract class BaseDelParams : BaseUsuario
    {
        [Required]
        public int id { get; set; }
    }
}
