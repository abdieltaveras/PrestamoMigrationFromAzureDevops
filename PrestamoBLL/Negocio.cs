using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrestamoBLL
{
    //TODO: cambiar los nombres a los procedimientos segun lo acordado Entidad+Operacion ejemp GetNegocios a NegociosGet, eso debe hacerse cuando Bryan no este trabajando en nada en la rama y yo actualizarlo todo. luego de hacer esto devolverlo de inmediato para proseguir


    //todo revisar los procedimientos que no devuelvan data anulada, o si la devuelven que dicha dada no pueda ser modificada en ningun stored procedure es una condicion a poner en todos los sp
    public partial class BLLPrestamo
    {
        public IEnumerable<Negocio> GetNegocios(NegociosGetParams searchParam)
        {
            //ThrowErrorIfUsuarioEmptyOrNull(searchParam.Usuario);
            IEnumerable<Negocio> result = new List<Negocio>();
            try
            {
                var searchSqlParams = SearchRec.ToSqlParams(searchParam);
                result = DBPrestamo.ExecReaderSelSP<Negocio>("spGetNegocios", searchSqlParams);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
        public class negociosPadres
        {
            public int idNegocio { get; internal set; } = 0;
            public int idNegocioPadre { get; internal set; } = 0;
        }

        public IEnumerable<Negocio> GetNegocioySusPadres(int idNegocio)
        {
            IEnumerable<Negocio> result = new List<Negocio>();
            try
            {
                result = DBPrestamo.ExecReaderSelSP<Negocio>("spGetNegocioAndPadres", SearchRec.ToSqlParams(new { idNegocio = idNegocio }));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
            
        }
        /// <summary>
        /// Solo retorna el idNegocio y el idNegocioPadre no retorna mas informacion
        /// </summary>
        /// <param name="idNegocio"></param>
        /// <returns></returns>
        public IEnumerable<Negocio> GetNegocioYSusHijos(int idNegocio)
        {
            IEnumerable<Negocio> result = new List<Negocio>();
            try
            {

                result = DBPrestamo.ExecReaderSelSP<Negocio>("spGetNegociosAndHijos", SearchRec.ToSqlParams(new { idNegocio = idNegocio }));
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;


            //try
            //{
            //    var searchSqlParams = SearchRec.ToSqlParams(idNegocio);
            //    //$"select * from fnGetNegociosPadre({idNegocio})"
            //    using (var response = PrestamosDB.ExecQuery($"select * from fnGetNegocioAndHijos({idNegocio})", searchSqlParams))
            //    {
            //        while (response.Read())
            //        {
            //            var negocio = new Negocio{ IdNegocio = response.GetInt32(0), IdNegocioPadre = response[1].IsNull() ? 0 : response.GetInt32(1) };
            //            result.Add(negocio);
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    DatabaseError(e);
            //}
            //return result;
        }
        /// <summary>
        /// permite obtener el negocio matriz de un negocio hijo en especifico
        /// </summary>
        /// <param name="idNegocio"></param>
        /// <returns></returns>
        public int GetNegocioMatriz(int idNegocio)
        {
            var idNegocioResponse = 0;
            try
            {
                var searchSqlParams = SearchRec.ToSqlParams(idNegocio);
                var response = DBPrestamo.ExecEscalar($"select dbo.fnGetIdNegocioMatriz({idNegocio})");
                if (!response.IsNull())
                {
                    idNegocioResponse = Convert.ToInt32(response);
                }
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return idNegocioResponse;
        }
        /// <summary>
        /// Todos aquellos negocios que no tienen a ninguno de padre son los matriz Raiz
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Negocio> NegocioGetLosQueSonMatriz()
        {
            var result = new List<Negocio>();
            try
            {
                var searchSqlParams = SearchRec.ToSqlParams(0);
                //$"select * from fnGetNegociosPadre({idNegocio})"
                using (var response = DBPrestamo.ExecQuery("select IdNegocio, NombreComercial  from tblNegocios where IdNegocioPadre is NULL", searchSqlParams))
                {
                    while (response.Read())
                    {
                        var negocio = new Negocio();
                        negocio.IdNegocio = response.GetInt32(0);
                        negocio.NombreComercial = response.GetString(1);
                        result.Add(negocio);
                    }
                }
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
        
        public int NegocioinsUpd(Negocio insUpdParam)
        {
            InsUpdValidation(insUpdParam);
            var idResult = -1;
            var _insUpdParam = SearchRec.ToSqlParams(insUpdParam);
            try
            {
                var result = DBPrestamo.ExecSelSP("spInsUpdNegocio", _insUpdParam);
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
                NegocioinsUpd(negocio);
                return 1;
            }
            else
            {
                return 1;
            }
        }
    }
}


