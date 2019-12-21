﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests

{
    [TestClass()]
    public class NegocioTests
    {
        string mensajeError = string.Empty;
        [TestMethod()]
        public void insUpdNegocioTest()
        {
            var negocio = NewInstanceNegocioIntagsa();

            //act
            try
            {
                InsUpdNegocio(negocio);
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }

        [TestMethod()]
        public void insUpdNegocioWithIdNegocioPadre()
        {

            mensajeError = string.Empty;
            //act
            try
            {
                createNegociosWithNegocioPadre();
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
            }
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }

        private static void createNegociosWithNegocioPadre()
        {
            var negocioPadre = GetNegocios(GetParamNegocioIntangsa()).FirstOrDefault();

            var negocio1 = NewInstanceNegocioIntagsa();
            negocio1.IdNegocioPadre = negocioPadre.IdNegocio;
            negocio1.NombreComercial = "Intagsa La Romana";
            negocio1.Codigo = "IntLR00";
            //InsUpdNegocio(negocio1);
            negocioPadre = GetNegocios(new NegociosGetParams {Codigo=negocio1.Codigo  , IdNegocio = -1 }).FirstOrDefault();
            var negocio2 = NewInstanceNegocioIntagsa();
            negocio2.IdNegocioPadre = negocioPadre.IdNegocio;
            negocio2.NombreComercial = "Intagsa La Romana Prestamos";
            negocio2.Codigo = "IntLR00-01";
            InsUpdNegocio(negocio2);
            var negocio3 = NewInstanceNegocioIntagsa();
            negocio3.IdNegocioPadre = negocioPadre.IdNegocio;
            negocio3.NombreComercial = "Intagsa La Romana Ventas";
            negocio3.Codigo = "IntLR00-02";
            InsUpdNegocio(negocio3);
        }

        private static void InsUpdNegocio(Negocio negocio)
        {
            BLLPrestamo.Instance.insUpdNegocio(negocio);
        }

        private static Negocio NewInstanceNegocioIntagsa()
        {
            var negocioInfo = new NegocioInfo { CorreoElectronico = "intagsa@hotmail.com", Direccion1 = "Prol. Gregorio Luperon no 12, Villa España", Direccion2 = "La Romana Rep. Dom.", Telefono1 = "809-813-1719" };
            var negocio = new Negocio { NombreComercial = "Intagsa", NombreJuridico = "Intagsa SRL", TaxIdNo = "112108236", Codigo="int001",OtrosDetalles = negocioInfo.ToJson(), Usuario = "TestProject" };
            return negocio;
        }

        [TestMethod()]
        public void GetNegociosTest()
        {
            var getParam = GetParamNegocioIntangsa();
            mensajeError = string.Empty;
            GetNegocioIntagsa(getParam);
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }

        [TestMethod()]
        public void GetNegociosHijosTest()
        {
            var negocioPadre = GetNegocios(GetParamNegocioIntangsa()).FirstOrDefault();
            var getParam = new NegociosGetParams { IdNegocioPadre = negocioPadre.IdNegocio, IdNegocio = -1 };
            mensajeError = string.Empty;
            var result = GetNegocios(getParam).FirstOrDefault();
            var mashijos = GetNegocios(new NegociosGetParams {IdNegocioPadre=result.IdNegocio, IdNegocio=-1});
            Assert.IsTrue(mensajeError == string.Empty, mensajeError);
        }

        [TestMethod()]
        public void FailInsertDuplicateCodigo()
        {
            var negocio = GetNegocios(GetParamNegocioIntangsa()).FirstOrDefault();
            negocio.IdNegocio = 0;
            negocio.Usuario = "NegocioTest";
            negocio.TaxIdNo = negocio.TaxIdNo + "xxx";
            mensajeError = string.Empty;
            try
            {
                InsUpdNegocio(negocio);
            }
            catch (Exception e)
            {
                mensajeError = e.Message;
                Assert.Fail(mensajeError);
            }
            Assert.IsTrue(string.IsNullOrEmpty(mensajeError));
        }
        private Negocio GetNegocioIntagsa(NegociosGetParams getParam)
        {
            IEnumerable<Negocio> negocios = new List<Negocio>();
            
            int intento = 0;
            do
            {
                intento++;
                try
                {
                    negocios = GetNegocios(getParam);
                }
                catch (Exception e)
                {
                    mensajeError = e.Message;
                }
                if (intento == 1 && negocios.Count() == 0)
                {
                    InsUpdNegocio(NewInstanceNegocioIntagsa());
                }
            } while ((intento == 1) || (negocios.Count() == 0));

            return negocios.FirstOrDefault();
        }

        private static IEnumerable<Negocio> GetNegocios(NegociosGetParams buscar)
        {
            return BLLPrestamo.Instance.GetNegocios(buscar);
        }

        private static NegociosGetParams GetParamNegocioIntangsa()=> new NegociosGetParams { NombreComercial = "Intagsa", IdNegocio = -1 };
    }
}