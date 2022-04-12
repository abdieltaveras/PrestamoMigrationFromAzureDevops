using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    //public class Catalogo : BaseCatalogo
    //{
    //    public int Id { get; set; }
    //    public string IdTabla { get; set; } = string.Empty;
    //    public string NombreTabla { get; set; }

    //    public override int GetId()
    //    {
    //        throw new NotImplementedException();
    //    }

    //}

    public class CatalogoInsUpd : BaseInsUpdGenericCatalogo
    {
        
        public override int GetId()
        {
            return IdRegistro;
        }

        protected override void SetId() { }
        /// <summary>
        /// the purpuso of this is to use it when needs to remove the concrete Id no the IdRegistsroPeoprety from object
        /// </summary>
        public override void SetPropertiesNullToRemoveFromSqlParam() { }
        public override string ToString()
        {
            return $"{Nombre} Codigo {Codigo}";
        }
    }

    public class ToggleStatusCatalogo : BaseAnularOrDeleteParams
    {
        public string IdTabla { get; set; } = string.Empty;
        public string NombreTabla { get; set; }
        public bool Activo { get; set; }
    }

    public class AnularCatalogo : BaseAnularOrDeleteParams
    {
        /// <summary>
        /// el id del registro a borrar
        /// </summary>
        public string IdRegistro { get; set; } = string.Empty;
        /// <summary>
        /// El nombre del campo o columna que hara la comparacion
        /// </summary>
        //public string NombreColumna { get;  set; } 
        ///// <summary>
        ///// el nombre de la tabla que se ejecutara la anulacion
        ///// </summary>
        //public string NombreTabla { get; set; }
    }

    public class SearchCatalogoParams
    {
        public string TextToSearch { get; set; } = "";
        public string TableName { get; set; } = string.Empty;
        public int IdNegocio { get; set; } = -1;
    }
    public class CatalogoGetParams : BaseCatalogoGetParams  {  }

    public class CatalogoReportGetParams : BaseCatalogoGetParams
    {
        /// <summary>
        /// The id value to search
        /// </summary>
        public int ReportType { get; set; } = -1;
        public CatalogoGetParams CatalogoGParams { get; set; }
    }



}
