using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PrestamoBLL
{

    
    public class PrestamoBuilder 
    {
        List<Cliente> clientes = new List<Cliente>();
        List<Codeudor> codeudores = new List<Codeudor>();
        List<Garantia> garantias = new List<Garantia>();
        Prestamo prestamoInProgress = new Prestamo();

        /// <summary>
        /// recibe un prestamo para validarlo y hacer todo el proceso necesario de calculos antes de enviarlo
        /// a la base de datos.
        /// </summary>
        /// <param name="prestamo"></param>
        public void SetPrestamo(Prestamo prestamo)
        {
            SetFechaDeEmision(prestamo.FechaEmision);
            SetClasificacion(prestamo.IdClasificacion);
            SetAmortizacion(prestamo.IdAmortizacion);
            SetRenovacion(prestamo.NoPrestamoARenovar);
            SetClientes(prestamo.Clientes);
            SetGarantias(prestamo.Garantias);
            SetCodeuDores(prestamo.Codeudores);
            SetMontoAPrestar(prestamo.DineroPrestado, prestamo.IdDivisa);
            SetGastDeCierre(prestamo.TasaGastoDeCierre, prestamo.GastoDeCierreEsDeducible, prestamo.SumarGastoDeCierreALasCuotas, prestamo.CargarInteresAlGastoDeCierre);
            SetTasaInteres(prestamo.IdTasaDeInteres);
            SetPeriodoYDuracion(prestamo.IdPeriodo, prestamo.CantidadDePeriodo);
            SetMoras(prestamo.IdTipoMora);
            SetAcomodarFecha(prestamo.FechaInicioPrimeraCuota);
        }
        public void AddCliente(Prestamo prestamo, Cliente cliente)
        {
            
            if (cliente.IdCliente > 0)
            {
                prestamo.Clientes.Add(cliente);
            }
            else
            {
                throw new NullReferenceException("el id del cliente a agregar no es valido debe ser mayor que cero");
            }
        }
        public void AddCodeudor(Prestamo prestamo, Codeudor codeudor)
        {

            if (codeudor.IdCodeudor > 0)
            {
                prestamo.Codeudores.Add(codeudor);
            }
            else
            {
                throw new NullReferenceException("el id del cliente a agregar no es valido debe ser mayor que cero");
            }
        }
        private void AddGarantia(Prestamo prestamo, Garantia garantia)
        {
            if (garantia.IdGarantia > 0)
            {
                prestamo.Garantias.Add(garantia);
            }
            else
            {
                throw new NullReferenceException("el id del cliente a agregar no es valido debe ser mayor que cero");
            }
            garantias.Add(garantia);
        }
        public void SetMontoAPrestar(decimal monto, int idDivisa)
        {
            validateRange(monto, 1, 0, "dinero prestado");
            //validateRange(idDivisa, 1, 0, "La divisa");
            prestamoInProgress.DineroPrestado = monto;
            prestamoInProgress.IdDivisa = idDivisa;
        }

        private void SetFechaEmision(DateTime fechaEmision)
        {
            /// validar que la fecha de emision este dentro del rango permitido
            prestamoInProgress.FechaEmision = fechaEmision;
        }

        private void validateRange(decimal valor, decimal minimo, decimal maximo, string propiedad)
        {
            if (valor <= minimo)
            {
                throw new ArgumentOutOfRangeException($"El valor de {propiedad} e menor al valor minimo aceptado el cual es {minimo}");
            }
            if (maximo>0 || valor > maximo)
            {
                throw new ArgumentOutOfRangeException($"El valor de {propiedad} es mayor que el valor maximo aceptado el cual es {minimo}");
            }
        }
        
        private void setPeriodo(int idPeriodo, int cantidadPeriodo)
        {
            prestamoInProgress.IdPeriodo = idPeriodo;
            prestamoInProgress.CantidadDePeriodo = cantidadPeriodo;
        }
        private void chkFiel()
        { 
            
        }
        private void CargarGastoDeCierre(float tasaGastoDeCiere = 0, bool esDeducible=false)
        {
            prestamoInProgress.TasaGastoDeCierre = tasaGastoDeCiere;
            prestamoInProgress.GastoDeCierreEsDeducible = esDeducible;
        }
        
        private int SetAcomodarFecha(DateTime? fechaInicioPrimeraCuota)
        {
            // aqui debe tomar la fecha y debe actualizar entonces la propiedad  [FechaInicioCalculoPrestamo]
            throw new NotImplementedException();
        }

        private void SetMoras(int idTipoMora)
        {
            throw new NotImplementedException();
        }

        private void SetPeriodoYDuracion(int idPeriodo, int cantidadDePeriodo)
        {
            prestamoInProgress.FechaVencimiento = CalcularFechaVencimiento();
        }

        private DateTime CalcularFechaVencimiento()
        {
            throw new NotImplementedException();
        }

        private void SetTasaInteres(int idTasaDeInteres)
        {
            throw new NotImplementedException();
        }

        private void SetGastDeCierre(float tasaGastoDeCierre, bool EsDeducible, bool sumargastoDeCierreALasCuotas, bool cargarInteresAlGastoDeCierre )
        {
            this.prestamoInProgress.TasaGastoDeCierre = tasaGastoDeCierre;
            prestamoInProgress.GastoDeCierreEsDeducible = EsDeducible;
            prestamoInProgress.CargarInteresAlGastoDeCierre = cargarInteresAlGastoDeCierre;
            prestamoInProgress.SumarGastoDeCierreALasCuotas = sumargastoDeCierreALasCuotas;
        }

        

        private void SetCodeuDores(List<Codeudor> codeudores)
        {

            foreach (var item in codeudores)
            {
                AddCodeudor(prestamoInProgress, item);
                prestamoInProgress.IdGarantias.Add(item.IdCodeudor);
            }
        }

        private void SetGarantias(List<Garantia> garantias)
        {
            foreach (var item in garantias)
            {
                AddGarantia(prestamoInProgress, item);
                prestamoInProgress.IdGarantias.Add(item.IdGarantia);
            }
        }
        private void SetCodeudores(List<Codeudor> codeudores)
        {
            foreach (var item in codeudores)
            {
                AddCodeudor(prestamoInProgress, item);
                prestamoInProgress.IdGarantias.Add(item.IdCodeudor);
            }
        }
        private void SetClientes(List<Cliente> clientes)
        {
            foreach (var item in clientes)
            {
                AddCliente(prestamoInProgress,item);
                prestamoInProgress.IdClientes.Add(item.IdCliente);
            }
        }

        private void SetRenovacion(string noPrestamoARenovar)
        {
            this.prestamoInProgress.NoPrestamoARenovar = noPrestamoARenovar;
        }

        private void SetAmortizacion(int idAmortizacion)
        {
            prestamoInProgress.IdAmortizacion = idAmortizacion;
        }

        private void SetClasificacion(int idClasificacion)
        {
            // seria bueno analizar que impida que en un prestamo inmobiliario no ponga un vehiculo o incluso que la clasificacion la tome por las garantias
            this.prestamoInProgress.IdClasificacion = idClasificacion;
            
        }

        private void SetFechaDeEmision(DateTime fechaEmision)
        {
            // la fecha debe estar dentro del rango permitido
            if (fechaEstaEnRangoPermitido(fechaEmision))
            {
                this.prestamoInProgress.FechaEmision = fechaEmision;
            }
        }

        private bool fechaEstaEnRangoPermitido(DateTime fechaEmision)
        {
            return true;
        }

        public Prestamo Build()
        {
            return null;
        }
    }

    public partial class BLLPrestamo
    {

        public int PrestamoInsUpd(Prestamo prestamo)
        {
            return 0;
        }

        public int CalcularCuotasAlPrestamo(Prestamo prestamo)
        {
            return 0;
        }
    }
}
