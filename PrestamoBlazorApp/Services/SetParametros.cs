using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{

    public class SetParametrosService
    {
        public async Task ForPrestamo(Prestamo prestamo)
        {
            // buscar en un servicio los parametros
            prestamo.InteresGastoDeCierre = 10;
            prestamo.MontoPrestado = 10000;
        }
    }
    
}
