using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    partial class BLLPrestamo
    {

        public IEnumerable<VerificadorDireccion> VerificardorDireccionGet(VerificadorDireccionGetParams searchParam)
        {
            return BllAcciones.GetData<VerificadorDireccion, VerificadorDireccionGetParams>(searchParam, "spGetVerificadoresDireccion", GetValidation);
        }
    }
}
