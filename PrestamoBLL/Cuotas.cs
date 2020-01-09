using emtSoft.DAL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public void insUpdCuotas(IEnumerable<Cuota> cuotas)
        {
            var cuotasDataTable = cuotas.ToDataTable();
            try
            {
                /// preparar el parametro a enviar
                /// las propiedades deben estar en el mismo orden que en el tipo de sql server
                /// debe crear un parametro anonimo que coincida el nombre del parametro
                /// y asignarle un objeto datatable
                var _insUpdParam = SearchRec.ToSqlParams(new { cuotas = cuotasDataTable });
                Database.AdHoc(ConexionDB.Server).ExecSelSP("spInsUpdCuotas", _insUpdParam);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }
    }
}
