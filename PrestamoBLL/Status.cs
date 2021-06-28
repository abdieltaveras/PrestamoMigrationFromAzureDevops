using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrestamoEntidades;
namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        
        public void InsUpdStatus(Status insUpdParam)
        {
            BllAcciones.InsUpdData<Status>(insUpdParam, "spInsUpdStatus");
        }

        public IEnumerable<Status> GetStatus(StatusGetParams searchParam)
        {
            return BllAcciones.GetData<Status, StatusGetParams>(searchParam, "spGetStatus", GetValidation);
        }
        
    }
}
