using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLL
{
    public class CodigosCargosDebitosReservadosBLL:BaseBLL
    {
        public CodigosCargosDebitosReservadosBLL(int idLocalidadNegocio, string loginName) : base(idLocalidadNegocio, loginName)
        {
            
        }
        public class CodigosCargosDebitosReservadosClass
        {

            public const string Capital = "CA";
            public const string Interes = "INT";
            public const string InteresDespuesDeVencido = "INTDV";
            public const string Moras = "MOR";
            public const string InteresDelGastoDeCierre = "INTGC";
            public const string GastoDeCierre = "GC";
            public const string OtrosCargos = "OC";
            public const string InteresOtrosCargos = "INTOC";
            public static string GetNombre(string codigoCargo)
            {

                string nombre = string.Empty;
                switch (codigoCargo)
                {
                    case Capital: nombre = "Capital"; break;
                    case Interes: nombre = "Interes"; break;
                    case InteresDespuesDeVencido: nombre = "Interes despues de vencido"; break;
                    case Moras: nombre = "Moras (Cargos por atraso)"; break;
                    case GastoDeCierre: nombre = "Gasto de cierre"; break;
                    case InteresDelGastoDeCierre: nombre = "Interes del gasto de cierre"; break;
                    case OtrosCargos: nombre = "Otros  Cargos"; break;
                    case InteresOtrosCargos: nombre = "Interes Otros cargos"; break;
                    default: nombre = "el codigo indicado no esta registrado"; break;
                }
                return nombre;
            }
            public override string ToString() => "Codigos reservados para utilizar en los cargos";
            public static IEnumerable<string> GetCodigos()
            {
                // no se incluye otros cargos porque ese reservados para agrupar los cargos
                // creados por el usuario
                var codigosCargos = new List<string>()
            {
                Capital, Interes, InteresDespuesDeVencido, Moras, GastoDeCierre, InteresDelGastoDeCierre, InteresOtrosCargos
            };
                return codigosCargos;
            }
        }
        public IEnumerable<CodigosCargosDebitosReservados> Get(CodigosCargosGetParams searchParam)
        {
            List<CodigosCargosDebitosReservados> ls = new List<CodigosCargosDebitosReservados>();
            ls =  this.Get<CodigosCargosDebitosReservados>("spGetCodigosCargos", searchParam).ToList();
            var codes = CodigosCargosDebitosReservadosClass.GetCodigos();
            foreach (var code in codes)
            {
                ls.Add(new CodigosCargosDebitosReservados { Codigo = code, 
                    Nombre = CodigosCargosDebitosReservadosClass.GetNombre(code), Usuario="SISTEMA" });
            }
            return ls;
        }
        public int InsUpd(CodigosCargosDebitosReservados insUpdParam)
        {
            insUpdParam.IdLocalidadNegocio = this.IdLocalidadNegocioLoggedIn;
            insUpdParam.Usuario = this.LoginName;
            var result = this.InsUpd("spInsUpdCodigosCargosDebitosReservados", insUpdParam);
            return result;
        }
    }
}
