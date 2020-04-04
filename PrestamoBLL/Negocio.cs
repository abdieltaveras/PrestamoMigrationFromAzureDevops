using emtSoft.DAL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
namespace PrestamoBLL
{
    //TODO: cambiar los nombres a los procedimientos segun lo acordado Entidad+Operacion ejemp GetNegocios a NegociosGet, eso debe hacerse cuando Bryan no este trabajando en nada en la rama y yo actualizarlo todo. luego de hacer esto devolverlo de inmediato para proseguir
    

    //todo revisar los procedimientos que no devuelvan data anulada, o si la devuelven que dicha dada no pueda ser modificada en ningun stored procedure es una condicion a poner en todos los sp
    public partial class BLLPrestamo
    {
        public IEnumerable<Negocio> GetNegocios(NegociosGetParams searchParam)
        {
            ThrowErrorIfUsuarioEmptyOrNull(searchParam.Usuario);
            IEnumerable<Negocio> result = new List<Negocio>();
            try
            {
                var searchSqlParams = SearchRec.ToSqlParams(searchParam);
                result = PrestamosDB.ExecReaderSelSP<Negocio>("spGetNegocios", searchSqlParams);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
        public void insUpdNegocio(Negocio insUpdParam)
        {
            InsUpdValidation(insUpdParam);
            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(insUpdParam);
                PrestamosDB.ExecSelSP("spInsUpdNegocio", _insUpdParam);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }
        /// <summary>
        /// Create a negocio if none negocio exist in table tblNegocios
        /// </summary>
        /// <param name="key"></param>
        /// <returns> 1 if succesfull 0 if fail </returns>
        public int NegocioCreateIfNotExist(string key)
        {
            if (key != "pcp46232") return 0;

            if (!ExistDataForTable("tblNegocios"))
            {
                var negocio = new Negocio
                {
                    NombreComercial = "Empresa Nueva",
                    Usuario = "InitSis",
                };
                insUpdNegocio(negocio);
                return 1;
            }
            else
            {
                return 1;
            }
        }
    }
}
