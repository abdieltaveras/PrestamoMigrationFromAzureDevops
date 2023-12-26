using PrestamoBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class SiteResourcesService : ISiteResourcesService
    {
        public string PcProgImage => "";//PcProg64BaseImg.Logo;

        public string CandidatoOrOrganizationImage => "";//Eduard64BaseImg.ImagenCandidato;

        public string CanditatoNameOrorganizationName => "Eduard Espiritusanto";

        public string OrganizacionPoliticaName => "Fuerza del Pueblo";

        public string OrganizacionPoliticaImage => "";//Eduard64BaseImg.LogoOrganizacionPolitica;

        /// <summary>
        /// El nombre que se le da a una unidad del conjunto o grupo, no a un miembro sino a un solo grupo
        /// </summary>
        public string UnidadDeGrupoName => "Direccion de Base";
        /// <summary>
        /// El nombre que se le da a varios grupos que se agrupan juntos.
        /// </summary>


        public string ConjuntoGruposName => "Direccion Media";

        public string DirigenteConjuntoGruposName => "Director";

        public string DirectorPrincipalUnidadGrupoName => "Presidente";
        public string ProvinciaPrincipalUnidadGrupoName => "La Romana";

        public string LoggedOutKey=> "*7^%$wewe";

        public string LoggedOutValue=>"#2$%^klo";

    }
}
