using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public static class SLFactory
    {
        private static IEnumerable<SelectListItem> GetEnumSelectList<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<int>().Select(e => new SelectListItem()
            {
                Text = Enum.GetName(typeof(T), e).Replace("_", " "),
                Value = e.ToString()
            })).ToList();
        }
        public static SelectList ForEnum<T>() => new SelectList(GetEnumSelectList<T>(), "Value", "Text");
    }

    public static class SelectItems
    {
        public static SelectList TiposCargoMora => SLFactory.ForEnum<TiposCargosMora>();

        public static SelectList CalcularMoraPor => SLFactory.ForEnum<CalcularMoraPor>();

        public static SelectList AplicarMorasAl => SLFactory.ForEnum<AplicarMoraAl>();
        public static SelectList TiposIdentificacion => SLFactory.ForEnum<TiposIdentificacionCliente>();
        public static SelectList TiposTelefonos => SLFactory.ForEnum<TiposTelefono>();
        public static SelectList Sexos => SLFactory.ForEnum<Sexo>();
        public static SelectList EstadosCiviles => SLFactory.ForEnum<EstadoCivil>();
        public static SelectList OpcionesBusquedaCliente => SLFactory.ForEnum<EnumBuscarClientePor>();
        public static SelectList OpcionesBusquedaCatalogo => SLFactory.ForEnum<EnumBuscarCatalogosPor>();

        public static SelectList Negocios => _Negocios();
        
        public static SelectList Ocupaciones => _Ocupaciones();

        private static SelectList _Negocios()
        {
            var negocios = BLLPrestamo.Instance.GetNegocios(new NegociosGetParams {IdNegocio=-1 });
            var result = negocios.OrderBy(ord => ord.NombreComercial).Select(e => new SelectListItem()
            {
                Text = e.NombreComercial,
                Value = e.IdNegocio.ToString()
            }).ToList();
            return new SelectList(result, "Value", "Text");
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