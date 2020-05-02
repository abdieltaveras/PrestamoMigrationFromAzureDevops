using emtSoft.DAL;
using PrestamoEntidades;
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
        public void addCliente(Cliente cliente)
        {
            
            if (cliente.IdCliente > 0)
            {
                clientes.Add(cliente);
            }
            else
            {
                throw new NullReferenceException("el id del cliente a agregar no es valido debe ser mayor que cero");
            }
        }

        public void AddMontoAPrestar(decimal monto, int idDivisa)
        {
            prestamoInProgress.IdDivisa = idDivisa;
            prestamoInProgress.DineroPrestado = monto;
        }

        private void SetFechaEmision(DateTime fechaEmision)
        {
            prestamoInProgress.FechaEmision = fechaEmision;
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
        private void AddGarantias(Garantia garantia)
        {
            garantias.Add(garantia);
        }

        public void SetPrestamoInfoAndValidate(Prestamo prestamo)
        {
            /*
            proceso a realizar
            SetFechaDeEmision
            SetClasificacion
            SetAmortizacion
            SetRenovacion
            SetClientes
            Setgarantias 
            SetCodeuDores
            SetMontoAPrestar
            SetGastDeCierre
            SetTasaInteres
            SetPeriodos
            SetMoras
            SetAcomodarFecha
            */
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
    }
}
