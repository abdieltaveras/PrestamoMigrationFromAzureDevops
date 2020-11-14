using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using emtSoft.DAL;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System.Linq;

namespace PrestamoBLLTests
{
    /// <summary>
    /// Summary description for StatusTests
    /// </summary>
    [TestClass]
    public class StatusTests
    {
        public StatusTests()
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

        [TestMethod]
        public void InsUpdStatusMethod()
        {
            //
            // TODO: Add test logic here
            //
            string result = "";
            try
            {
                var parametros = new Status
                {
                    IdStatus = 1,
                    IdTipoStatus = 2,
                    Concepto ="Garantia Desaparecida",
                    Estado = false,
                    Usuario = "Luis"
                };
                BLLPrestamo.Instance.InsUpdStatus(parametros);
            }
            catch (Exception e)
            {
                result = e.Message  + e.StackTrace;
                throw;
            }
        }

        [TestMethod]
        public void GetStatusMethod()
        {
            //
            // TODO: Add test logic here
            //
            string result = "";
            try
            {
                var parametros = new StatusGetParams
                {
                };
                var datos = BLLPrestamo.Instance.GetStatus(parametros);
            }
            catch (Exception e)
            {
                result = e.Message + e.StackTrace;
                throw;
            }
        }
        public class SeachResult<T>
        {
            public bool DatosEncontrados { get; private set; } = false;
            public IEnumerable<T> DataList
            {
                get;
                private set;
            }

            public SeachResult(IEnumerable<T> data)
            {
                this.DatosEncontrados = (data != null & data.Count() > 0);
                if (DatosEncontrados)
                    DataList = data;
                else
                    DataList = new List<T>();
            }
        }

    }
}
