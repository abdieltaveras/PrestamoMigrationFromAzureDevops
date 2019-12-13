using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class TiposLocalidadesDataSource
    {
        public class TipoLocalidad : BaseCatalogo
        {
            public int IdTipoLocalidad { get; set; } = 0;
            /// <summary>
            /// Indica el tipo de localidad que esta puede ser padre
            /// Ejemlo la localidad Tipo Provincia es padre de municipios
            /// </summary>
            public int LocalidadPadre { get; set; }
            public override int GetId() => this.IdTipoLocalidad;
        }


        public static List<TipoLocalidad> TiposLocalidadesRD()
        {
            var Pais = new TipoLocalidad
            {
                IdNegocio = 1,
                IdTipoLocalidad = 1,
                Descripcion = "Pais",
                Codigo = "Pais",
                LocalidadPadre = 0
            };

            var Provincia = new TipoLocalidad { IdNegocio = 1, IdTipoLocalidad = 2, Descripcion = "Provincia", Codigo = "Provincia", LocalidadPadre = Pais.IdTipoLocalidad };

            var Municipio = new TipoLocalidad { IdNegocio = 1, IdTipoLocalidad = 3, Descripcion = "Municipio", Codigo = "Municipio", LocalidadPadre = Provincia.IdTipoLocalidad };

            var SectorOBarrio = new TipoLocalidad { IdNegocio = 1, IdTipoLocalidad = 4, Descripcion = "Sector o Barrio", Codigo = "Sector", LocalidadPadre = Municipio.IdTipoLocalidad };

            var tiposLocalidades = new List<TipoLocalidad>();
            tiposLocalidades.Add(Pais);
            tiposLocalidades.Add(Provincia);
            tiposLocalidades.Add(Municipio);
            tiposLocalidades.Add(SectorOBarrio);
            return tiposLocalidades;
        }
    }


    public class LocalidadesDataSource
    {

        public static List<Localidad> Localidades()
        {
            var PaisRD = new Localidad
            {
                IdTipoLocalidad = TiposLocalidadesDataSource.TiposLocalidadesRD().Where(loc => loc.Codigo == "Pais").FirstOrDefault().IdTipoLocalidad,
                IdLocalidadPadre = 0,
                IdLocalidad = 1,
                IdNegocio = 1,
                Nombre = "Republica Dominicana"

            };
            var ProvinciaLaRomana = new Localidad
            {
                IdTipoLocalidad = TiposLocalidadesDataSource.TiposLocalidadesRD().Where(loc => loc.Codigo == "Provincia").FirstOrDefault().IdTipoLocalidad,
                IdLocalidadPadre = PaisRD.IdLocalidad,
                IdLocalidad = 2,
                IdNegocio = 1,
                Nombre = "La Romana"
            };
            var MunicipioLaRomana = new Localidad
            {
                IdTipoLocalidad = TiposLocalidadesDataSource.TiposLocalidadesRD().Where(loc => loc.Codigo == "Municipio").FirstOrDefault().IdTipoLocalidad,
                IdLocalidadPadre = ProvinciaLaRomana.IdLocalidad,
                IdLocalidad = 3,
                IdNegocio = 1,
                Nombre = "La Romana"
            };
            var MunicipioVillaHermosa = new Localidad
            {
                IdTipoLocalidad = TiposLocalidadesDataSource.TiposLocalidadesRD().Where(loc => loc.Codigo == "Municipio").FirstOrDefault().IdTipoLocalidad,
                IdLocalidadPadre = ProvinciaLaRomana.IdLocalidad,
                IdLocalidad = 4,
                IdNegocio = 1,
                Nombre = "VillaHermosa"
            };

            var BarrioQuisqueya = new Localidad
            {
                IdTipoLocalidad = TiposLocalidadesDataSource.TiposLocalidadesRD().Where(loc => loc.Codigo == "Sector").FirstOrDefault().IdTipoLocalidad,
                IdLocalidadPadre = MunicipioLaRomana.IdLocalidad,
                IdLocalidad = 5,
                IdNegocio = 1,
                Nombre = "Ensanche Quisqueya"
            };
            var BarrioLaLechooza = new Localidad
            {
                IdTipoLocalidad = TiposLocalidadesDataSource.TiposLocalidadesRD().Where(loc => loc.Codigo == "Sector").FirstOrDefault().IdTipoLocalidad,
                IdLocalidadPadre = MunicipioVillaHermosa.IdLocalidad,
                IdLocalidad = 6,
                IdNegocio = 1,
                Nombre = "La Lechoza"
            };
            var localidades = new List<Localidad>();
            localidades.Add(PaisRD);
            localidades.Add(ProvinciaLaRomana);
            localidades.Add(MunicipioVillaHermosa);
            localidades.Add(MunicipioLaRomana);
            localidades.Add(BarrioQuisqueya);
            localidades.Add(BarrioLaLechooza);
            return localidades;
        }

        public static Localidad Quisqueya() => LocalidadesDataSource.Localidades().Where(loc => loc.IdLocalidad == 5).FirstOrDefault();
        public static Localidad LaLechoza() => LocalidadesDataSource.Localidades().Where(loc => loc.IdLocalidad == 6).FirstOrDefault();

    }


}
