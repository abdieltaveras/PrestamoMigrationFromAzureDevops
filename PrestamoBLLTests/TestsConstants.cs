using PrestamoEntidades;
using System.Linq;


namespace PrestamoBLL.Tests
{
    public static class TestsConstants
    {

        public static string Usuario => "TestUsr";

        public static int IdLocalidadNegocioForFirstLocalidadNegocio => BLLPrestamo.Instance.GetLocalidadesNegocio(new LocalidadNegociosGetParams()).FirstOrDefault().IdLocalidadNegocio;

        public static int AnyIdLocalidadNegocio => -1;
    }
}