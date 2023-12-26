namespace PrestamoBlazorApp.Services
{
    public interface ISiteResourcesService
    {
        string CandidatoOrOrganizationImage { get; }
        string CanditatoNameOrorganizationName { get; }
        string ConjuntoGruposName { get; }
        string DirectorPrincipalUnidadGrupoName { get; }
        string DirigenteConjuntoGruposName { get; }
        string OrganizacionPoliticaImage { get; }
        string OrganizacionPoliticaName { get; }
        string PcProgImage { get; }
        string ProvinciaPrincipalUnidadGrupoName { get; }
        string UnidadDeGrupoName { get; }
        string LoggedOutValue { get; }

         string LoggedOutKey { get; }

        
    }
}