using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLLTests
{
    [TestClass()]
    public class EquipoTest
    {
        [TestMethod()]
        public void RegistrarEquipo_Codigo_NotEmpty()
        {
            EquipoIdYCodigo result = InsertEquipo("Probando Caja 1", "Probando");
            Assert.IsTrue(result.IdEquipo != 0 && !string.IsNullOrEmpty(result.Codigo), $"se esperaba valores para IdEquipo y se obtuvo {result.IdEquipo} y para Copdigo y se obtuvo {result.Codigo}");
        }

        private static EquipoIdYCodigo InsertEquipo(string nombreEquipo, string descripcion = "Probando")
        {
            var eq = new Equipo() { IdNegocio = 1, Nombre = nombreEquipo, Descripcion = descripcion, Usuario = TestInfo.Usuario };
            var result = BLLPrestamo.Instance.EquipoInsUpd(eq);
            //var result = BLLPrestamo.Equipo_Operaciones.RegistrarEquipo(eq);
            return result;
        }

        [TestMethod()]
        public void ModificarRegistroEquipoTest()
        {
            var result = BLLPrestamo.Instance.EquiposGet(new EquiposGetParam());
            var ExpectedNombre =  Guid.NewGuid().ToString();
            string nombreActual = string.Empty;
            if (result != null)
            {
                var data = result.First();
                data.Nombre = ExpectedNombre;
                BLLPrestamo.Instance.EquipoInsUpd(data);
                result = BLLPrestamo.Instance.EquiposGet(new EquiposGetParam {IdEquipo=data.IdEquipo } );
                nombreActual= result.First().Nombre;
            }
            Assert.IsTrue(ExpectedNombre == nombreActual, $"Se esperaba {ExpectedNombre} y se recibio {nombreActual}");
        }
        [TestMethod]
        public void DesvicularEquipoTest()
        {

            var result = BLLPrestamo.Instance.EquiposGet(new EquiposGetParam());
            Equipo data = null;
            if (result != null)
            {
                    data = result.Where(eq => !eq.EstaDesvinculado).FirstOrDefault();
                data = result.First();
                    BLLPrestamo.Instance.EquipoDesvincular(new EquiposGetParam2 { IdEquipo = data.IdEquipo, Usuario = TestInfo.Usuario });
                    data = BLLPrestamo.Instance.EquiposGet(new EquiposGetParam {IdEquipo= data.IdEquipo, Usuario= data.Usuario}).FirstOrDefault();
            }
            Assert.IsTrue(data.EstaDesvinculado, $"Se esperaba que devolviera true y se recibio {data.EstaDesvinculado}");
        }

        [TestMethod]
        public void BloquearEquipo_trueTest()
        {
            var result = BLLPrestamo.Instance.EquiposGet(new EquiposGetParam());
            Equipo data = null;
            if (result != null)
            {
                data = result.Where(eq => !eq.EstaBloqueado).FirstOrDefault();
                BLLPrestamo.Instance.EquipoBloquearAcceso(new EquiposGetParam2 { IdEquipo = data.IdEquipo, Usuario = TestInfo.Usuario });
                data = BLLPrestamo.Instance.EquiposGet(new EquiposGetParam { IdEquipo = data.IdEquipo, Usuario = data.Usuario }).FirstOrDefault();
            }
            Assert.IsTrue(data.EstaBloqueado, $"Se esperaba que devolviera true y se recibio {data.EstaBloqueado}");
        }
        [TestMethod()]
        public void GetEquiposExecuteCountGreaterThenZero()
        {
            InsertEquipo("intagsa Romana Prestamo 1", "para operaciones de prestamo");
            var result = BLLPrestamo.Instance.EquiposGet(new EquiposGetParam());
            Assert.IsTrue(result.Count() > 0, $"se esperaba valor mayor que 0 y se recibio {result.Count()}");
        }
        public void GetEquiposExecuteMensajeErrorEmpty()
        {
            var tInfo = new TestInfo();
            try
            {
                var result = BLLPrestamo.Instance.EquiposGet(new EquiposGetParam());
            }
            catch (Exception e)
            {
                tInfo.MensajeError = e.Message;
            }

            Assert.IsTrue(string.IsNullOrWhiteSpace(tInfo.MensajeError), $"se esperaba que no hubiesen mensajes y se recibio {tInfo.MensajeError}");
        }
        [TestMethod()]
        public void ConfirmarRegistro_Execute_ConfirmadoEqualTrue_andIdEquipoEqualToFirstFound()
        {
            var result = BLLPrestamo.Instance.EquiposGet (new EquiposGetParam()).FirstOrDefault();
            int idEquipo = -1;
            if (result != null)
            {
                idEquipo = result.IdEquipo;
                BLLPrestamo.Instance.EquipoConfirmarRegistro(new EquiposGetParam2 { IdEquipo = result.IdEquipo, Usuario = TestInfo.Usuario });
                result = BLLPrestamo.Instance.EquiposGet(new EquiposGetParam() { IdEquipo = result.IdEquipo }).FirstOrDefault(); 
             
            }
            else
            {
                result = new Equipo();
            }
            Assert.IsTrue(result.EstaConfirmado && result.IdEquipo==idEquipo, $"se esperaba true y se recibio false");
        }
    }
}