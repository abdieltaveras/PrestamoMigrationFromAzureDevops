using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLLTests
{
    [TestClass()]
    public class PrestamoTest
    {
        enum CalculoCuotas { PorPagaresNoAmortizable,  AmortizableSoloPagandoInteresPorPeriodo, AmortizableSoloPagandoInteresPorDia, AmortizablePorCuotas, AmortizableInteresFijo }

        public Dictionary<int, string> Clasificacion = new Dictionary<int, string>();

        [TestMethod()]
        public void PrestamoInsUpdTest()
        {

            // Debe tener informacion de si es inmobliario, mobiliario para que pueda determinar que tipo de garantia
            

            var cliente = new Cliente();
            var pre = new Prestamo
            {
                IdPrestamo = 1,
                FechaEmision = DateTime.Now,
                IdAmortizacion = (int)CalculoCuotas.PorPagaresNoAmortizable,
                IdClasificacion = GetClasificacion(),
                IdNegocio = 6,
                IdDivisa = 1, // equivale a la moneda nacional (siempre el codigo 1 es la moneda nacional del pais
                DineroPrestado = 10000,
                LlevaGastoDeCiere = false,
                IdTasaDeInteres = GetTasaDeInteres(),
                IdPeriodo = GetPeriodo(),
                CantidadDePeriodo = 5,
                IdTipoMora=GetTipoMora(),
            };
            pre.Clientes.Add(GetClientes());
            pre.Garantias.Add(GetGarantias());
            pre.Codeudores.Add(GetCodeudores());
        }

        private int GetClasificacion()
        {
            Clasificacion.Add(1, "Vehiculo");
            Clasificacion.Add(2, "Motores");
            Clasificacion.Add(3, "Personal");
            Clasificacion.Add(4, "Hipotecario");
            Clasificacion.Add(5, "Comercial");
            var itemDict = Clasificacion.Where(item => item.Value == "Vehiculo").FirstOrDefault();
            return itemDict.Key;
            
        }

        private Garantia GetGarantias()
        {
            throw new NotImplementedException();
        }

        public Codeudor GetCodeudores()
        {
            throw new NotImplementedException();
        }
        private Cliente GetClientes()
        {
            throw new NotImplementedException();
        }

        private int GetTipoMora()
        {
            throw new NotImplementedException();
        }

        private int GetPeriodo()
        {
            throw new NotImplementedException();
        }

        private int GetTasaDeInteres()
        {
            throw new NotImplementedException();
        }
    }
}