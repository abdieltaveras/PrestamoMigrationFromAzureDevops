using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrestamoBLL
{
    //TODO: cambiar los nombres a los procedimientos segun lo acordado Entidad+Operacion ejemp GetNegocios a NegociosGet, eso debe hacerse cuando Bryan no este trabajando en nada en la rama y yo actualizarlo todo. luego de hacer esto devolverlo de inmediato para proseguir


    //todo revisar los procedimientos que no devuelvan data anulada, o si la devuelven que dicha dada no pueda ser modificada en ningun stored procedure es una condicion a poner en todos los sp
    public partial class BLLPrestamo
    {
        public IEnumerable<LocalidadNegocio> GetLocalidadesNegocio(LocalidadNegociosGetParams searchParam)
        {

            IEnumerable<LocalidadNegocio> result = new List<LocalidadNegocio>();
            try
            {
                var searchSqlParams = SearchRec.ToSqlParams(searchParam);
                result = DBPrestamo.ExecReaderSelSP<LocalidadNegocio>("spGetLocalidadesNegocio", searchSqlParams);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }


        /// <summary>
        /// texto para cuando no haya correo electronico en un campo que lo requiera y haya que ponerle un valor por defecto
        /// </summary>

        
        public int InsUpdLocalidadNegocio(LocalidadNegocio insUpdParam)
        {
            InsUpdValidation(insUpdParam);
            var idResult = -1;
            
            var _insUpdParam = SearchRec.ToSqlParams(insUpdParam);
            try
            {
                var result = DBPrestamo.ExecSelSP("spInsUpdLocalidadNegocio",  _insUpdParam);
                idResult = Utils.GetIdFromDataTable(result);
            }

            catch (Exception e)
            {
                DatabaseError(e);
            }
            return idResult;
        }

        /// <summary>
        /// Create a negocio if none negocio exist in table tblNegocios
        /// </summary>
        /// <param name="key"></param>
        /// <returns> 1 if succesfull 0 if fail </returns>
        
    }

}


