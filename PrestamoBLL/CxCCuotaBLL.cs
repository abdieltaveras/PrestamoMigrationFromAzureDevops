using PrestamoEntidades;
using System.Net.NetworkInformation;

namespace PrestamoBLL
{
    public class CxCCuotaBLL : CxCCuota 
    {
        public new decimal BceInteresDelGastoDeCierre { get; internal set; } = 0;
        public new decimal BceGastoDeCierre { get; internal set; } = 0;
        public new decimal BceInteres { get; internal set; } = 0;
        public new decimal BceCapital { get; internal set; } = 0;
        public override decimal BceGeneral => BceCapital + BceInteres + BceGastoDeCierre + BceInteresDelGastoDeCierre; // + BceOtrosCargos??0
        public override string ToString()
        {
            return $"CuotaMaestroConDetallesCxC No {Numero} Fecha {Fecha} Total {TotalOrig} Capital {Capital} Interes {Interes} G/C {GastoDeCierre} Int G/C {InteresDelGastoDeCierre}";
        }
    }

}
