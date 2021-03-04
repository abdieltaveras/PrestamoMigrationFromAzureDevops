using emtSoft.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestamoBLL.Entidades
{
    /// <summary>
    /// El cargo se aplicara como un monto fijo o como un porciento segun lo indique
    /// AplicarMoraAl
    /// </summary>
    public enum TiposCargosMora {
        Cargo_Fijo =1,
        Porcentual
    }

    /// <summary>
    /// determina si el cargo se aplicara una vez vencida la cuota o se va a ir calculando 
    /// dia a dia despues de vencida la cuota
    /// es decir por ejemplo una cuota es de 1000 y se le va a cargar 10%, 
    /// se le cobra 100 pero si es por dia y solo tiene 13 dias
    /// atrasadas entonces se hace un calculo asi, los 100 pesos dividido entre 30 y multiplicado por 10
    /// </summary>
    public enum CalcularMoraPor
    {
        cada_dias_transcurrido_desde_la_primera_cuota_vencida=1,
        cada_30_dias_transcurrido_por_cada_cuota_vencida,
        por_cada_cuota_vencida_por_periodos,
        el_valor_acumulado_de_las_cuotas_vencidas,
    }
    /// <summary>
    /// Indica a que se aplicara el calculo de mora
    /// </summary>
    public enum AplicarMoraAl
    {
        Capital_intereses_y_moras=1,
        Capital_e_interes,
        Solo_al_interes,
        Solo_al_capital,
        al_balance_de_la_cuota,
    }
    [Table("tblTiposMora", Schema = "pre")]
    public class TipoMora : BaseCatalogo
    {
        [KeyAttribute]
        public virtual int IdTipoMora { get; set; } = 0;
        
        [Required]
        //[StringLength(100)]
        [Display(Name = "Forma de calcular el cargo")]
        public virtual int TipoCargo { get; set; } = (int)TiposCargosMora.Porcentual;
        /// <summary>
        /// solo para usarse y obtener el enum del campo TipoCargo
        /// </summary>
        [IgnorarEnParam]
        [NotMapped]
        public TiposCargosMora TipoCargoEnum { get { return (TiposCargosMora)TipoCargo; } }
        
        [Display(Name = "Forma de hacer el calculo")]
        public virtual int CalcularCargoPor { get; set; } = (int)CalcularMoraPor.cada_30_dias_transcurrido_por_cada_cuota_vencida;
        //.cada_dia_transcurrido_por_cada_cuota_vencida;
        [IgnorarEnParam][NotMapped]
        public CalcularMoraPor CalcularCargoPorEnum { get { return (CalcularMoraPor)CalcularCargoPor; } }
        [Display(Name = "para aplicarlo a")]
        public virtual int AplicarA { get; set; } = (int)AplicarMoraAl.Capital_intereses_y_moras; 
        [IgnorarEnParam][NotMapped]
        public AplicarMoraAl AplicarAEnum { get { return (AplicarMoraAl)AplicarA; } }
        [Display(Name = "aplicar cargo luego de x dias")]
        public virtual int DiasDeGracia { get; set; } = 0;

        [Display(Name = "Monto Fijo o Porcentaje a Aplicar")]
        public decimal MontoOPorcientoACargar { get; set; }

        [Display(Name = "a partir del monto Cuota de")]
        public decimal MontoCuotaDesde { get; set; }
        [Display(Name = "Hasta el monto de cuota")]
        public decimal MontoCuotaHasta { get; set; }
        [IgnorarEnParam]
        public string CodigoNombre => this.Codigo + "-" + this.Nombre;
        public override int GetId()=>   this.IdTipoMora;
        

        public override string ToString()
        {
            return Codigo + " " + Nombre;
        }
    }
    
    public class TipoMoraGetParams : BaseGetParams
    {
        [KeyAttribute]
        public virtual int IdTipoMora { get; set; } = -1;
        [MaxLength(10)]
        public virtual string Codigo { get; set; } = string.Empty;
    }
    
    public class TipoMoraDelParams : BaseAnularOrDeleteParams
    {
        [Required]
        public override int Id
        {
            get; set;
        }
    }
}
