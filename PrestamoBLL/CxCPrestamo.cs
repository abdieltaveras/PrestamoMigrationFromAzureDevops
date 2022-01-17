using PrestamoEntidades;
using System;
using System.Collections.Generic;

namespace PrestamoBLL
{
    public class CxCPrestamo
    {
        public void GetCuentaPorCobrar(int idPrestamo, DateTime Fecha)
        {

            // debo buscar todos los cargos tanto en cuotas como en 
            // Simulacion
            List<CxCCuota> Cuotas = new List<CxCCuota>();
            //Cuotas = CxCCuota
        }

    }
}
