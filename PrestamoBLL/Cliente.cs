using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        
        public IEnumerable<Cliente> GetClientes(ClienteGetParams  searchParam)
        {
            GetValidation(searchParam as BaseGetParams);
            var result= BllAcciones.GetData<Cliente, ClienteGetParams>(searchParam, "spGetClientes", GetValidation);
            return result;
        }
        public int InsUpdCliente(Cliente insUpdParam)
        {
            var result = BllAcciones.InsUpdData<Cliente>(insUpdParam, "spInsUpdUsuario");
            return result;
        }

        public void InsUpdClientes(Cliente cliente, Conyuge infoConyuge, InfoLaboral infoLaboral, Direccion infoDireccion, List<Referencia> infoReferencia)
        {
            FixProperties(cliente, infoConyuge, infoLaboral);
            convertToJson(cliente, infoConyuge, infoLaboral, infoDireccion, infoReferencia);
            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(cliente);
                DBPrestamo.ExecSelSP("spInsUpdCliente", _insUpdParam);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        private static void convertToJson(Cliente cliente, Conyuge infoConyuge, InfoLaboral infoLaboral, Direccion infoDireccion, List<Referencia> infoReferencia)
        {
            cliente.InfoConyuge = infoConyuge.ToJson();
            cliente.InfoLaboral = infoLaboral.ToJson();
            cliente.InfoDireccion = infoDireccion.ToJson();
            cliente.InfoReferencias = infoReferencia.FindAll(x => x.Tipo != 0).ToJson();
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
    }
}
