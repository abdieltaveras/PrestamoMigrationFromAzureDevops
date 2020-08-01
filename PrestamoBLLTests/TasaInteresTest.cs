﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class TasaInteresTest
    {
        [TestMethod()]
        public void CalcularTasaInterePorPeriodoTest()
        {
            var search = new PeriodoGetParams(); 
            var periodos = BLLPrestamo.Instance.GetPeriodos(search);

            var diario = periodos.Where(per => per.Codigo == "DIA").FirstOrDefault();
            var semanal = periodos.Where(per => per.Codigo == "SEM").FirstOrDefault();
            var quincenal = periodos.Where(per => per.Codigo == "QUI").FirstOrDefault();
            var mensual = periodos.Where(per => per.Codigo == "MES").FirstOrDefault();

            
            var resultDiario = BLLPrestamo.Instance.CalcularTasaInterePorPeriodo(10, diario);
            var resultQuincenal = BLLPrestamo.Instance.CalcularTasaInterePorPeriodo(10, quincenal);
            var resultSemanal = BLLPrestamo.Instance.CalcularTasaInterePorPeriodo(10, semanal);
            var resultMensual = BLLPrestamo.Instance.CalcularTasaInterePorPeriodo(10, mensual);
            Assert.Fail();
        }
    }
}