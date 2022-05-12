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
        
        public IEnumerable<Cliente> GetClientes(ClienteGetParams  searchParam, bool convertToObj, string directorioDeImagen = "")
        {

            searchParam.Anulado = null;
            GetValidation(searchParam as BaseGetParams);
            
            var result= BllAcciones.GetData<Cliente, ClienteGetParams>(searchParam, "spGetClientes", GetValidation);
            if (convertToObj)
            {
                result.ToList().ForEach(cl => cl.ConvertJsonToObj(directorioDeImagen));
            }
            return result;
        }
        public int InsUpdCliente(Cliente insUpdParam)
        {
            insUpdParam.RemoveAllButNumber();
            var imagesToRemove = insUpdParam.ImagenesObj.Where(item => item.Quitar).ToList();
            imagesToRemove.ForEach(item => insUpdParam.ImagenesObj.Remove(item));
            insUpdParam.ConvertObjToJson();
            var sqlParams = SearchRec.ToSqlParams(insUpdParam);
            var result = BllAcciones.InsUpdData<Cliente>(insUpdParam, "spInsUpdCliente");
            return result;
        }

        public void InsUpdClientes(Cliente cliente, Conyuge infoConyuge, InfoLaboral infoLaboral, Direccion infoDireccion, List<Referencia> infoReferencia)
        {
            FixProperties(cliente, infoConyuge, infoLaboral);
            convertToJson(cliente, infoConyuge, infoLaboral, infoDireccion, infoReferencia);
            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(cliente);
                DBPrestamo.ExecSelSP("spInsUpdCliente", ref _insUpdParam);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        private static Cliente convertToJson(Cliente cliente, Conyuge infoConyuge, InfoLaboral infoLaboral, Direccion infoDireccion, List<Referencia> infoReferencia)
        {
            cliente.InfoConyuge = infoConyuge.ToJson();
            cliente.InfoLaboral = infoLaboral.ToJson();
            cliente.InfoDireccion = infoDireccion.ToJson();
            cliente.InfoReferencias = infoReferencia.FindAll(x => x.Tipo != 0).ToJson();
            return cliente;
        }

        private static void FixProperties(Cliente cliente, Conyuge infoConyuge, InfoLaboral infoLaboral)
        {
            cliente.TelefonoCasa = StringMeth.RemoveAllButNumber(cliente.TelefonoCasa);
            cliente.TelefonoMovil = StringMeth.RemoveAllButNumber(cliente.TelefonoMovil);
            cliente.NoIdentificacion = StringMeth.RemoveAllButNumber(cliente.NoIdentificacion);
            infoConyuge.NoIdentificacion = StringMeth.RemoveAllButNumber(infoConyuge.NoIdentificacion);
            infoConyuge.TelefonoTrabajo = StringMeth.RemoveAllButNumber(infoConyuge.TelefonoTrabajo);
            infoConyuge.NoTelefono1 = StringMeth.RemoveAllButNumber(infoConyuge.NoTelefono1);
            infoLaboral.NoTelefono1 = StringMeth.RemoveAllButNumber(infoLaboral.NoTelefono1);
            infoLaboral.NoTelefono2 = StringMeth.RemoveAllButNumber(infoLaboral.NoTelefono2);
            cliente.TelefonoCasa = StringMeth.ConvertNullStringToEmpty(cliente.TelefonoCasa);
            cliente.TelefonoMovil = StringMeth.ConvertNullStringToEmpty(cliente.TelefonoMovil);
        }

        public void AnularClientes(ClienteDelParams delParam)
        {
            DBPrestamo.ExecSelSP("spDelCliente", SearchRec.ToSqlParams(delParam));
        }

        public IEnumerable<Cliente> SearchCliente(BuscarClienteParams searchParam)
        {
            return BllAcciones.GetData<Cliente, BuscarClienteParams>(searchParam, "spBuscarClientes", GetValidation);
        }
        public IEnumerable<Cliente> ReporteClientes(BaseReporteParams searchParam)
        {
            var param = SearchRec.ToSqlParams(searchParam);
            var resultSet = DBPrestamo.ExecReaderSelSP<Cliente>("spRptClientes", param);
            return resultSet;
            //return BllAcciones.GetData<Cliente, BaseReporteParams>(searchParam, "spRptClientes",);
        }
       
    }
}
