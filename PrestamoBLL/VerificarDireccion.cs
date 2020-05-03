using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
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
