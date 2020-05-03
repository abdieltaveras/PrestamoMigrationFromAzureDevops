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
        
        public IEnumerable<Cliente> ClientesGet(ClientesGetParams  searchParam)
        {
            GetValidation(searchParam as BaseGetParams);
            return BllAcciones.GetData<Cliente, ClientesGetParams>(searchParam, "spGetClientes", GetValidation);
        }
        public void ClientesInsUpd(Cliente insUpdParam)
        {
            BllAcciones.InsUpdData<Cliente>(insUpdParam, "spInsUpdUsuario");
        }

        public void ClientesInsUpd(Cliente cliente, Conyuge infoConyuge, InfoLaboral infoLaboral, Direccion infoDireccion, List<Referencia> infoReferencia)
        {
            FixProperties(cliente, infoConyuge, infoLaboral);
            convertToJson(cliente, infoConyuge, infoLaboral, infoDireccion, infoReferencia);
            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(cliente);
                PrestamosDB.ExecSelSP("spInsUpdCliente", _insUpdParam);
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
            cliente.InfoReferencia = infoReferencia.ToJson();
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

        public void ClientesAnular(ClienteDelParams delParam)
        {
            PrestamosDB.ExecSelSP("spDelCliente", SearchRec.ToSqlParams(delParam));
        }
    }
}
