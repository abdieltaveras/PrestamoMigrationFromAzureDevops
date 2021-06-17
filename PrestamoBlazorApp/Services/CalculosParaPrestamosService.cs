using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{

    public class CalculosParaPrestamosService
    {
        public IEnumerable<Cuota>  GenerarCuotas(IInfoGeneradorCuotas info)
        {
            var generadorCuotas = CuotasConCalculo.GetGeneradorDeCuotas(info);
            var cuotas = generadorCuotas.GenerarCuotas();
            return cuotas;
        }

        public void CalculateTasaInteresPorPeriodo(decimal tasaInteresMensual, Periodo periodo)
        {
            var data = BLLPrestamo.Instance.CalcularTasaInteresPorPeriodos(tasaInteresMensual, periodo);
        }

    }
}
