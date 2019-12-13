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
            return (Enum.GetValues(typeof(T)).Cast<int>().Select(e => new SelectListItem() { 
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

        //public static SelectList TiposTelefono => SLFactory.ForEnum<TiposTelefono>();
        //public static SelectList Sexos => SLFactory.ForEnum<Sexo>();
        //public static SelectList EstadosCiviles => SLFactory.ForEnum<EstadoCivil>();
        //public static SelectList TiposIdentificacion => SLFactory.ForEnum<TiposIdentificacion>();
        //public static SelectList OpcionesBusquedaCliente => SLFactory.ForEnum<EnumBuscarClientePor>();
        //public static SelectList OpcionesBusquedaCatalogo => SLFactory.ForEnum<EnumBuscarCatalogosPor>();
    }
}