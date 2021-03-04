using System.ComponentModel.DataAnnotations;

namespace PrestamoBLL.Entidades
{
    public class BaseAnularOrDeleteParams : BaseUsuario
    {
        [Required]
        public virtual int Id { get; set; }
    }

    
}
