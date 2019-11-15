using emtSoft.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PcProg.DAL;
using System.Web.Mvc;

namespace PrestamoEntidades
{
    public enum TiposCargosMora { Cargo_Fijo, Porciento }

    /// <summary>
    /// determina si el cargo se aplicara una vez vencida la cuota o se va a ir calculando dia a dia despues de vencida la cuota
    /// es decir por ejemplo una cuota es de 1000 y se le va a cargar 10%, se le cobra 100 pero si es por dia y solo tiene 13 dias
    /// atrasadas entonces se hace un calculo asi, los 100 pesos dividido 30 y multiplicado por 10
    /// </summary>
    public enum CalcularMoraPor
    {
        cada_dia_transcurrido_solo_desde_la_primera_cuota_vencida,
        cada_dia_transcurrido_por_cada_cuota_vencida,
        el_valor_acumulado_de_las_cuotas_vencidas
    }
    public enum AplicarMoraAl
    {
        Capital_intereses_y_moras,
        Capital_e_interes,
        Solo_al_interes
    }
    [Table("tblTiposMora", Schema = "pre")]
    public class TipoMora : BaseCatalogo
    {
        [KeyAttribute]
        public virtual int IdTipoMora { get; set; } = 0;
        public override int GetId() => this.IdTipoMora;
        [Required]
        //[StringLength(100)]
        [Display(Name = "Forma de calcular el cargo")]
        public virtual int TipoCargoMora { get; set; } = (int)TiposCargosMora.Porciento;
        [Display(Name = "Forma de hacer el calculo")]
        public virtual CalcularMoraPor FormaCargarMora { get; set; } = CalcularMoraPor.cada_dia_transcurrido_por_cada_cuota_vencida;

        [Display(Name = "para aplicarlo a")]
        public virtual AplicarMoraAl AplicarA { get; set; } = AplicarMoraAl.Capital_intereses_y_moras;
        [Display(Name = "aplicar cargo luego de x dias")]
        public virtual int DiasDeGracia { get; set; } = 0;

        [Display(Name = "Monto Fijo o Porcentaje a Aplicar")]
        public decimal MontoOPorcientoACargar { get; set; }

        //[Display(Name = "Desde")]
        //public decimal GargoDesde { get; set; }
        //[Display(Name = "Hasta")]
        //public decimal GargoHasta { get; set; }
        public override string ToString()
        {
            return Codigo + " " + Descripcion;
        }
    }
    [SpGetProcedure("SpGetTiposMora")]
    [Schema("pre")]
    public class TipoMoraGetParams : BaseGetParams
    {
        [KeyAttribute]
        public virtual int IdTipoMora { get; set; } = -1;
        public virtual string Codigo { get; set; } = string.Empty;
        public virtual string Descripcion { get; set; } = string.Empty;
    }
    [SpDelProcedure("spDelTipoMora")]
    [Schema("pre")]
    public class TipoMoraDelParams : BaseDeleteParams
    {
        [Required]
        public virtual int IdTipoMora
        {
            get; set;
        }

    }
}
