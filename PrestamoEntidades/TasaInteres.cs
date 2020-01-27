using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
{
    public class TasaInteres : BaseCatalogo
    {
        public int idTasaInteres { get; set; } = 0;
        // el valor numerico del interes 10%, 4%, etc
        public decimal InteresMensual { get; set; } = 0;
        public bool RequiereAutorizacion { get; set; } = false;

        
        
    }
    public class TasaInteresGetParams : BaseGetParams
    {
        public int idTasaInteres { get; set; } = -1;
        [MaxLength(10)]
        public string Codigo { get; set; } = string.Empty;
        public decimal InteresMensual { get; set; } = -1;
        public int Activo { get; set; } = -1;
        public int RequiereAutorizacion { get; set; } = -1;
    }
    public class TasaInteresDelParams : BaseAnularParams
    {

    }

}
