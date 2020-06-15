using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class CuotaTests
    {
        string mensajeError = string.Empty;
        [TestMethod()]
        public void insUpdCuotasTest()
        {
            
            mensajeError = string.Empty;
            try
            {
                //BLLPrestamo.Instance.CuotasinsUpd(CreateCuotas()); ;
            }
            catch (Exception e)
            {

                mensajeError = e.Message;
            }
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }

        //private IEnumerable<Cuota> CreateCuotas()
        //{
        //    List<Cuota> cuotas = new List<Cuota>();
        //    var cuota = new Cuota { 
        //        IdPrestamo = 1, 
        //        Fecha = DateTime.Now, 
        //        Capital = 100, 
        //        Interes = 100, 
        //        Numero = 1 };

        //    cuotas.Add(cuota);
        //    cuota.Fecha = cuota.Fecha.AddMonths(1);
        //    cuota.Numero = 2;
        //    cuotas.Add(cuota);
        //    cuota.Fecha = cuota.Fecha.AddMonths(2);
        //    cuota.Numero = 3;
        //    cuotas.Add(cuota);
        //    return cuotas;
        //}
    }
}