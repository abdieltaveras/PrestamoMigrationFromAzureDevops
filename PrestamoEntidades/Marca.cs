﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Marca : BaseInsUpdCatalogo
    {
        public int? IdMarca { get; set; } = 0;

        public override int GetId() => (int)this.IdMarca;
        

        //public override void SetPropertiesNullToRemoveFromSqlParam()
        //{
        //    this.IdMarca = null;
        //}

        //protected override void SetIdForConcreteObject()
        //{
        //    //this.IdMarca = IdRegistro;
        //}
    }
    public class MarcaGetParams : BaseGetParams
    {
        public int IdMarca { get; set; } = -1;
    }

    
}
