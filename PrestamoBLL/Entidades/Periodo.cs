using emtSoft.DAL;
using System.ComponentModel.DataAnnotations;

namespace PrestamoBLL.Entidades
{
    public enum PeriodoBase { Dia=1, Semana, Quincena, Mes, Ano}
    public class Periodo : BaseCatalogo
    {
        public int idPeriodo { get; set; } = 0;
        // el valor numerico del interes 10%, 4%, etc
        [IgnorarEnParam]
        public PeriodoBase PeriodoBase { 
            get { return (PeriodoBase)IdPeriodoBase; } 
            set { IdPeriodoBase = (int)value; } 
        }

        public int IdPeriodoBase { get; internal set; } = 1;
        public int MultiploPeriodoBase { get; set; } = 1;
        [Display(Name = "Periodo base")]
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
