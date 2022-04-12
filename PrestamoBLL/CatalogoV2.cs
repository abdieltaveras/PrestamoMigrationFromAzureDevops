using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{

    public partial class BLLPrestamo
    {


        public void InsUpdCatalogo(CatalogoName catalogoName,  BaseInsUpdGenericCatalogo insUpdParams)
        {
            ThrowErroWhenNullCatalogName(catalogoName);
            insUpdParams.SetPropertiesNullToRemoveFromSqlParam(); // to remove innecesary param IdOcupacion, Idcolor, etc.
            var param = insUpdParams;
            var sqlParams = CreateSqlParams(catalogoName, insUpdParams).ToArray();
            DBPrestamo.ExecReaderSelSP("spInsUpdCatalogo", sqlParams);
            //BllAcciones.InsUpdData(insUpdParams, "spInsUpdCatalogo");
        }

        public IEnumerable<CatalogoType> GetCatalogos<CatalogoType>(CatalogoName catalogoName, BaseCatalogoGetParams getParams) where CatalogoType: BaseInsUpdGenericCatalogo
        {
            ThrowErroWhenNullCatalogName(catalogoName);
            var sqlParams = CreateSqlParams(catalogoName, getParams).ToArray();
            var result = DBPrestamo.ExecReaderSelSP<CatalogoType>("spGetCatalogosV2", sqlParams);
            return result;
        }

        public void AnularCatalogo(AnularCatalogo delParams)
        {
            throw new NotImplementedException();
            //BllAcciones.CancelData(delParams, "spDelCatalogo");
        }

        public void DeleteCatalogo(CatalogoName catalogoName, BaseCatalogoDeleteParams delParams)
        {
            ThrowErroWhenNullCatalogName(catalogoName);
            var sqlParams = CreateSqlParams(catalogoName, delParams).ToArray();
            DBPrestamo.ExecReaderSelSP("spDelCatalogo", sqlParams);
        }

        private static void ThrowErroWhenNullCatalogName(CatalogoName catalogName)
        {
            if (catalogName == null)
            {
                throw new NullReferenceException("Necesito un objeto que me indique el nombre del catalogo y su columna id");
            }
        }

        public IEnumerable<SqlParameter> CreateSqlParams<@Type>(CatalogoName catalogName, @Type getParams) 
        {
            var sqlParams1 = SearchRec.ToSqlParams(getParams);
            var sqlParams2 = AddParamsForCatalogoName(catalogName, sqlParams1);
            return sqlParams2;
        }

        private IEnumerable<SqlParameter> AddParamsForCatalogoName(CatalogoName catalogName, IEnumerable<SqlParameter> sqlParams)
        {
            var sqlParamsList = sqlParams.ToList();
            var newParams = new SearchRec();
            newParams.AddParam("TableName", catalogName.TableName);
            newParams.AddParam("IdColumnName", catalogName.IdColumnName);
            sqlParamsList.AddRange(newParams.ToSqlParams());
            return sqlParamsList;
        }

        private IEnumerable<SqlParameter> RemoveParam(IEnumerable<SqlParameter> sqlParamsList,string parameterName)
        {
            var result = sqlParamsList.ToList();
            result.RemoveAll(item => item.ParameterName == parameterName);
            return result;
        }
    }
}
