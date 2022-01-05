using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        
        public IEnumerable<Codeudor> GetCodeudores(CodeudorGetParams  searchParam, bool convertToObj, string directorioDeImagen = "")
        {
            
            GetValidation(searchParam as BaseGetParams);
            var result= BllAcciones.GetData<Codeudor, CodeudorGetParams>(searchParam, "spGetCodeudores", GetValidation);
            if (convertToObj)
            {
                result.ToList().ForEach(cl => cl.ConvertJsonToObj(directorioDeImagen));
            }
            return result;
        }
        public int InsUpdCodeudor(Codeudor insUpdParam)
        {
            insUpdParam.RemoveAllButNumber();
            var imagesToRemove = insUpdParam.ImagenesObj.Where(item => item.Quitar).ToList();
            imagesToRemove.ForEach(item => insUpdParam.ImagenesObj.Remove(item));
            insUpdParam.ConvertObjToJson();
            var sqlParams = SearchRec.ToSqlParams(insUpdParam);
            var result = BllAcciones.InsUpdData<Codeudor>(insUpdParam, "spInsUpdCodeudor");
            return result;
        }

        public void InsUpdCodeudor(Codeudor codeudor, InfoLaboral infoLaboral, Direccion infoDireccion)
        {
            FixPropertiesCodeudor(codeudor, infoLaboral);
            convertToJsonCodeudor(codeudor, infoLaboral, infoDireccion);
            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(codeudor);
                DBPrestamo.ExecSelSP("spInsUpdCodeudor", ref _insUpdParam);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        private static void convertToJsonCodeudor(Codeudor codeudor, InfoLaboral infoLaboral, Direccion infoDireccion)
        {
            codeudor.InfoLaboral = infoLaboral.ToJson();
            codeudor.InfoDireccion = infoDireccion.ToJson();
        }

        private static void FixPropertiesCodeudor(Codeudor codeudor,  InfoLaboral infoLaboral)
        {
            codeudor.TelefonoCasa = StringMeth.RemoveAllButNumber(codeudor.TelefonoCasa);
            codeudor.TelefonoMovil = StringMeth.RemoveAllButNumber(codeudor.TelefonoMovil);
            codeudor.NoIdentificacion = StringMeth.RemoveAllButNumber(codeudor.NoIdentificacion);
            infoLaboral.NoTelefono1 = StringMeth.RemoveAllButNumber(infoLaboral.NoTelefono1);
            infoLaboral.NoTelefono2 = StringMeth.RemoveAllButNumber(infoLaboral.NoTelefono2);
            codeudor.TelefonoCasa = StringMeth.ConvertNullStringToEmpty(codeudor.TelefonoCasa);
            codeudor.TelefonoMovil = StringMeth.ConvertNullStringToEmpty(codeudor.TelefonoMovil);
        }

        public void AnularCodeudor(CodeudorDelParams delParam)
        {
            DBPrestamo.ExecSelSP("spDelCodeudor", SearchRec.ToSqlParams(delParam));
        }

        public IEnumerable<Codeudor> SearchCodeudor(BuscarCodeudorParams searchParam)
        {
            return BllAcciones.GetData<Codeudor, BuscarCodeudorParams>(searchParam, "spBuscarCodeudores", GetValidation);
        }
        public IEnumerable<Codeudor> ReporteCodeudores(BaseReporteParams searchParam)
        {
            var param = SearchRec.ToSqlParams(searchParam);
            var resultSet = DBPrestamo.ExecReaderSelSP<Codeudor>("spRptCodeudores", param);
            return resultSet;
        }
    }
}
