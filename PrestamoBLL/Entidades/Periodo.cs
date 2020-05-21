using System.ComponentModel.DataAnnotations;

namespace PrestamoBLL.Entidades
{
    public enum PeriodoBase { Dia=1, Semana, Quincena, Mes, Ano}
    public class Periodo : BaseCatalogo
    {
        public int idPeriodo { get; set; } = 0;
        // el valor numerico del interes 10%, 4%, etc
        public PeriodoBase PeriodoBase { get; set; } = PeriodoBase.Mes;
        public int MultiploPeriodoBase { get; set; } = 1;
        public bool RequiereAutorizacion { get; set; } = false;

        public override int GetId() => this.idPeriodo;
        
    }
    public class PeriodoGetParams : BaseGetParams
    {
        public int idPeriodo { get; set; } = -1;
     
        public string Codigo { get; set; } = string.Empty;
        
        public int Activo { get; set; } = -1;
        public int RequiereAutorizacion { get; set; } = -1;
    }
    public class PeriodoDelParams : BaseAnularParams
    {

    }

}
