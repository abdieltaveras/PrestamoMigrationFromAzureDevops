using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace PrestamoBLL
{
    public class CxCPrestamo
    {
        public void GetCuentaPorCobrar(int idPrestamo, DateTime Fecha)
        {

            // debo buscar todos los cargos tanto en cuotas como en 
            // Simulacion
            //List<CxCCuota> Cuotas = new List<CxCCuota>();
            //Cuotas = CxCCuota
        }
        internal static int GetIdPrestamo(string prestamoNumero)
        {
            
            string query = $"Select idPrestamo from dbo.tblPrestamos where PrestamoNumero={prestamoNumero}";
            var resultIdPrestamo = BLLPrestamo.DBPrestamo.ExecEscalarCommand(query);
            var valor = System.Convert.ToInt32(resultIdPrestamo);
            return valor;
        }
        // todo fix
        //public static IEnumerable<CxCCuotaBLL> GetCuotas(int idPrestamo)
        //{
        //    // obtener el numero del prestamo y que pertenezca al negocio
        //    // obtener las cuotas y los debitos pendientes
        //    BLLValidations.ValueGreaterThanZero(idPrestamo, "Prestamo a consultar");
        //    var sqlParam = SearchRec.ToSqlParams(new { idPrestamo = idPrestamo });
        //    var result = BLLPrestamo.BllAcciones.GetData<CxCCuotaBLL>(sqlParam, "dbo.spGetCuotas");
        //    return result;
        //}
    }
}
