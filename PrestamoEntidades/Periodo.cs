using DevBox.Core.DAL.SQLServer;
using System.ComponentModel.DataAnnotations;

namespace PrestamoEntidades
{
    public enum PeriodoBase { Dia = 1, Semana, Quincena, Mes, Ano }
    public class Periodo : BaseCatalogo
    {
        public int idPeriodo { get; set; } = 0;
        // el valor numerico del interes 10%, 4%, etc
        [IgnoreOnParams]
        public PeriodoBase PeriodoBase
        {
            get { return (PeriodoBase)IdPeriodoBase; }
            set { IdPeriodoBase = (int)value; }
        }
        public int IdPeriodoBase { get;  set; } = 1;
        // public int IdPeriodoBase { get; internal set; } = 1;
        public int MultiploPeriodoBase { get; set; } = 1;


        [Display(Name = "Periodo base")]
        public bool RequiereAutorizacion { get; set; } = false;

        public override int GetId() => this.idPeriodo;

        [IgnoreOnParams]
        public int DiasDelPeriodo
        {
            get
            {
                var dias = 0;
                switch (PeriodoBase)
                {
                    case PeriodoBase.Dia:
                        dias = (1 * MultiploPeriodoBase);
                        break;
                    case PeriodoBase.Semana:
                        dias = (7 * MultiploPeriodoBase);
                        break;
                    case PeriodoBase.Quincena:
                        dias = (15 * MultiploPeriodoBase);
                        break;
                    case PeriodoBase.Mes:
                        dias = (30 * MultiploPeriodoBase);
                        break;
                    case PeriodoBase.Ano:
                        dias = (365 * MultiploPeriodoBase);
                        break;
                };
                return dias;
            }
        }
        public override string ToString()
        {
            //$"Id {this.idPeriodo} {this.Nombre} Periodo Base {this.PeriodoBase} {this.MultiploPeriodoBase} {DiasDelPeriodo}"
            return $"Id {this.idPeriodo} Nombre {this.Nombre} Duracion en dias {this.DiasDelPeriodo}";
        }

    }
    public class PeriodoGetParams : BaseGetParams
    {
        public int idPeriodo { get; set; } = -1;

        public string Codigo { get; set; } = string.Empty;

        public int Activo { get; set; } = -1;
        public int RequiereAutorizacion { get; set; } = -1;
    }
    public class PeriodoDelParams : BaseAnularOrDeleteParams
    {

    }

}
