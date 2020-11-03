﻿using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrestamoBLL.Entidades;
namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        
        public void InsUpdStatus(Status insUpdParam)
        {
            BllAcciones.InsUpdData<Status>(insUpdParam, "spInsUpdStatus");
        }

        public IEnumerable<Status> GetColores(StatusGetParams searchParam)
        {
            return BllAcciones.GetData<Status, StatusGetParams>(searchParam, "spGetStatus", GetValidation);
        }
        
    }
}
