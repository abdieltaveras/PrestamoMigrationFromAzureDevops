using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PrestamoBLL;
using PrestamoEntidades;
using System.Linq;

namespace PrestamoBLL.Tests
{
    /// <summary>
    /// Summary description for StatusTests
    /// </summary>
    [TestClass]
    public class BaseBLLTests
    {
        public BaseBLLTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        [TestMethod]
        public void GetSearchTableByColumn()
        {
            //
            // TODO: Add test logic here
            //
            string result = "";
            try
            {
                var parametros = new EstatusGetParams
                {
                    IdEstatus = -1
                };
                //var datos = new BaseBLL();
            }
            catch (Exception e)
            {
                result = e.Message + e.StackTrace;
                throw;
            }
        }

    }
}
