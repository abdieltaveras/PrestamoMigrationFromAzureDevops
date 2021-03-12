using PrestamoBLL ;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{

    public static class SelectItems
    {
        public static SelectList TiposCargoMora => SLFactory.ForEnum<TiposCargosMora>();
        public static SelectList TiposAmortizacion => SLFactory.ForEnum<TiposAmortizacion>();
        
        public static SelectList TiposMora(int idNegocio) => new SelectList(BLLPrestamo.Instance.TiposMorasGet(new TipoMoraGetParams {IdNegocio = idNegocio }), "IdTipoMora", "CodigoNombre");
        public static SelectList CalcularMoraPor => SLFactory.ForEnum<CalcularMoraPor>();
        public static SelectList AplicarMorasAl => SLFactory.ForEnum<AplicarMoraAl>();
        public static SelectList TiposIdentificacion => SLFactory.ForEnum<TiposIdentificacionCliente>();
        public static SelectList TiposTelefonos => SLFactory.ForEnum<TiposTelefono>();
        public static SelectList Sexos => SLFactory.ForEnum<Sexo>();
        public static SelectList EstadosCiviles => SLFactory.ForEnum<EstadosCiviles>();
        public static SelectList OpcionesBusquedaCliente => SLFactory.ForEnum<EnumBuscarClientePor>();
        public static SelectList OpcionesBusquedaCatalogo => SLFactory.ForEnum<EnumBuscarCatalogosPor>();
        public static SelectList TiposReferencias => SLFactory.ForEnum<EnumTiposReferencia>();
        public static SelectList TiposVinculos => SLFactory.ForEnum<EnumTiposVinculo>();
        public static SelectList Negocios(string usuario, int permitirOperaciones, int idNegocio) => new SelectList(BLLPrestamo.Instance.GetNegocios(new NegociosGetParams { IdNegocio = idNegocio, PermitirOperaciones = permitirOperaciones, Usuario = usuario }), "IdNegocio", "NombreComercial");

        public static SelectList Clasificaciones(int idNegocio) => new SelectList(BLLPrestamo.Instance.GetCatalogos(new BaseCatalogoGetParams { NombreTabla = "tblClasificaciones", IdTabla = "idClasificacion", IdNegocio = idNegocio }), "IdClasificacion", "Nombre");
        public static SelectList ListaTipoStatus => SLFactory.ForEnum<ListaTipoStatus>();
        public static SelectList Periodos(int idNegocio) => new SelectList(BLLPrestamo.Instance.GetPeriodos(new PeriodoGetParams { IdNegocio = idNegocio }), "IdPeriodo", "Nombre");

        /// <summary>
        /// muestra un listado de las tasas de interes muestra el codigo y la tasa
        /// </summary>
        /// <param name="idNegocio"></param>
        /// <returns></returns>
        public static SelectList TasasInteresMuestraCodigoYtasa(int idNegocio) => new SelectList(BLLPrestamo.Instance.TasasInteresGet(new TasaInteresGetParams { IdNegocio = idNegocio }), "IdTasaInteres", "CodigoTasa");
        /// <summary>
        /// muestra un listado de las tasas de interes pero solamente el codigo
        /// </summary>
        /// <param name="idNegocio"></param>
        /// <returns></returns>
        public static SelectList TasasInteresSoloCodigos(int idNegocio) => new SelectList(BLLPrestamo.Instance.TasasInteresGet(new TasaInteresGetParams { IdNegocio = idNegocio }), "IdTasaInteres", "CodigoTasa");

        // TasasInteresGet
        //public static SelectList NegociosMatrizRaiz() => new SelectList(BLLPrestamo.Instance.NegocioGetLosQueSonMatriz(), "IdNegocio", "NombreComercial");

        public static SelectList LocalidadesNegocios() => new SelectList(LocalidadesNegocios(), "key", "value");

        //public static SelectList NegociosOperacionalesForMatriz(int idNegocioPadre) => new SelectList(BLLPrestamo.Instance.GetNegocioYSusHijos(idNegocioPadre).Where(neg => neg.PermitirOperaciones), "IdNegocio", "NombreComercial");

        public static SelectList Lista12Meses => new SelectList(_12MesesNUmericos(), "key", "value");
        public static SelectList Ocupaciones => _Ocupaciones();
        private static Dictionary<int, string> _12MesesNUmericos()
        {
            var listaNumeros = new Dictionary<int, string>();
            for (int i = 1; i <= 10; i++)
            {
                var texto = i == 1 ? "-Mes" : "-Meses";
                listaNumeros.Add(i, i + texto);
            }
            return listaNumeros;
        }

        private static Dictionary<int, string> _LocalidadesNegociosFake()
        {
            var lista = new Dictionary<int, string>();
           lista.Add(1,"Romana");
            return lista;
        }
        private static SelectList CreateSelectList(List<Tuple<string, int>> items)
        {
            var result = items.OrderBy(ord => ord.Item1).Select(e => new SelectListItem()
            {
                Text = e.Item1,
                Value = e.Item2.ToString()
            }).ToList();
            return new SelectList(result, "Value", "Text");
        }
        private static SelectList _Ocupaciones()
        {
            var ocupaciones = new List<Tuple<string, int>>();
            var ocupacion = new Tuple<string, int>("Empleado Privado", 1);
            ocupaciones.Add(ocupacion);
            ocupacion = new Tuple<string, int>("Independiente", 2);
            ocupaciones.Add(ocupacion);
            ocupacion = new Tuple<string, int>("Motoconconcho", 3);
            ocupaciones.Add(ocupacion);
            ocupacion = new Tuple<string, int>("Abogado", 4);
            ocupaciones.Add(ocupacion);
            ocupacion = new Tuple<string, int>("Medico", 5);
            ocupaciones.Add(ocupacion);
            return CreateSelectList(ocupaciones); 
        }
    }
}