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
        public static SelectList TiposLocalidad => SLFactory.ForEnum<TipoLocalidad>();
        public static SelectList TiposIdentificacion => SLFactory.ForEnum<TiposIdentificacionCliente>();
        public static SelectList TiposTelefonos => SLFactory.ForEnum<TiposTelefono>();
        public static SelectList Sexos => SLFactory.ForEnum<Sexo>();
        public static SelectList EstadosCiviles => SLFactory.ForEnum<EstadoCivil>();
        public static SelectList OpcionesBusquedaCliente => SLFactory.ForEnum<EnumBuscarClientePor>();
        public static SelectList OpcionesBusquedaCatalogo => SLFactory.ForEnum<EnumBuscarCatalogosPor>();

        public static SelectList Localidades => _Localidades();
        
        public static SelectList Ocupaciones => _Ocupaciones();

        private static SelectList _Localidades()
        {
            var localidades = new List<Tuple<string, string>>();
            var localidad = new Tuple<string, string>("Quisqueya","1");
            localidades.Add(localidad);
            localidad = new Tuple<string, string>( "Villa España","2");
            localidades.Add(localidad);
            localidad = new Tuple<string, string>( "La Lechoza","3");
            localidades.Add(localidad);

            var result = localidades.OrderBy(ord => ord.Item1).Select(e => new SelectListItem()
            {
                Text = e.Item1,
                Value = e.Item2
            }).ToList();
            return new SelectList(result, "Value", "Text");
        }
        private static SelectList _Ocupaciones()
        {
            var ocupaciones = new List<Tuple<string, string>>();
            var ocupacion = new Tuple<string, string>("Empleado Privado", "1");
            ocupaciones.Add(ocupacion);
            ocupacion = new Tuple<string, string>("Independiente", "2");
            ocupaciones.Add(ocupacion);
            ocupacion = new Tuple<string, string>("Motoconconcho", "3");
            ocupaciones.Add(ocupacion);
            ocupacion = new Tuple<string, string>("Abogado", "4");
            ocupaciones.Add(ocupacion);
            ocupacion = new Tuple<string, string>("Medico", "5");
            ocupaciones.Add(ocupacion);

            var result = ocupaciones.OrderBy(ord => ord.Item1).Select(e => new SelectListItem()
            {
                Text = e.Item1,
                Value = e.Item2
            }).ToList();
            return new SelectList(result, "Value", "Text");
        }
    }
}