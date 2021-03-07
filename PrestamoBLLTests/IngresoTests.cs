using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System.Data;

namespace PrestamoBLLTests
{
    /// <summary>
    /// Summary description for IngresoTests
    /// </summary>
    [TestClass]
    public class IngresoTests
    {
        public IngresoTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        public string MensajeEr = string.Empty;
        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
           
            var searchIngresoParams = new IngresoGetParams { IdPrestamo = 1};
            var datosPrestamo = BLLPrestamo.Instance.GetIngresos(searchIngresoParams);
            decimal montoabonado = 100;
            decimal balance = 0;
            try
            {
                foreach (var item in datosPrestamo)
                {
                    balance = item.Balance;
                    decimal monto_original_cuota = item.Monto_Original_Cuota;
                    decimal deudacuota = monto_original_cuota - balance;
                    if (deudacuota == 0)
                    {
                        MensajeEr = "Esta Cuota está saldada";
                    }
                    else if(montoabonado > deudacuota)
                    {
                        MensajeEr = "No puede abonar mas de lo que debe a esta cuota \n El maximo que puede abonar es: " + deudacuota.ToString();
                    }
                    else
                    {
                        balance = montoabonado + item.Balance;
                        var datos = new Ingreso
                        {
                            IdPrestamo = item.IdPrestamo,
                            IdCuota = item.IdCuota,
                            Num_Cuota = item.Num_Cuota,
                            Monto_Original_Cuota = item.Monto_Original_Cuota,
                            Monto_Abonado = montoabonado,
                            Balance = balance,
                            Usuario = "Luis"
                        };
                        
                        BLLPrestamo.Instance.InsUpdIngreso(datos);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MensajeEr = ex.Message;
                throw;
            }

            Assert.IsTrue(MensajeEr == string.Empty, MensajeEr);
            
        }
    }
}
