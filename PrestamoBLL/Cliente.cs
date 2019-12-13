using emtSoft.DAL;
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
        
        public IEnumerable<Cliente> GetClientes(ClientesGetParams  searchParam)
        {
            IEnumerable<Cliente> result=new List<Cliente>();
            try
            {
                var searchSqlParams = SearchRec.ToSqlParams(searchParam);
                result = Database.AdHoc(ConexionDB.Server).ExecReaderSelSP<Cliente>("spGetClientes", searchSqlParams);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return result;
        }
        public void insUpdCliente(Cliente insUpdParam)
        {
            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(insUpdParam);
                Database.AdHoc(ConexionDB.Server).ExecSelSP("spInsUpdCliente", _insUpdParam);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }
        
        public void insUpdCliente(Cliente cliente, Conyuge infoConyuge, InfoLaboral infoLaboral, Direccion infoDireccion)
        {
            FixProperties(cliente, infoConyuge, infoLaboral);
            convertToJson(cliente, infoConyuge, infoLaboral, infoDireccion);
            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(cliente);
                Database.AdHoc(ConexionDB.Server).ExecSelSP("spInsUpdCliente", _insUpdParam);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        private static void convertToJson(Cliente cliente, Conyuge infoConyuge, InfoLaboral infoLaboral, Direccion infoDireccion)
        {
            cliente.InfoConyuge = infoConyuge.ToJson();
            cliente.InfoLaboral = infoLaboral.ToJson();
            cliente.InfoDireccion = infoDireccion.ToJson();
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

        public void DeleteCliente(ClienteDelParams delParam)
        {
            Database.AdHoc(ConexionDB.Server).ExecSelSP("spDelCliente", SearchRec.ToSqlParams(delParam));
        }
    }
}
