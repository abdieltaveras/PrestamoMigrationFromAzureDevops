using Microsoft.AspNetCore.Mvc;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoWS.Controllers
{
    public abstract class CatalogoController<@CatalogoType> : ControllerBasePrestamoWS  where CatalogoType: BaseInsUpdGenericCatalogo
    {

        private CatalogoName CatalogoName { get; }

        public CatalogoController(CatalogoName catalogoName)
        {
            this.CatalogoName = catalogoName;
        }
        
        protected IActionResult PostBase([FromBody] BaseInsUpdGenericCatalogo catalogoInsUPd) => SaveCatalogo(catalogoInsUPd);
        protected IEnumerable<@CatalogoType> GetBase([FromQuery] BaseCatalogoGetParams getParams) => BLLPrestamo.Instance.GetCatalogos<CatalogoType>(CatalogoName, getParams);
        protected void DeleteBase(BaseCatalogoDeleteParams catalogoDelParams) => BLLPrestamo.Instance.DeleteCatalogo(CatalogoName, catalogoDelParams);
        public abstract IActionResult Post([FromBody] @CatalogoType  catalogoInsUpd);
        public abstract IEnumerable<@CatalogoType> Get([FromQuery] BaseCatalogoGetParams getParams);
        public abstract void Delete(BaseCatalogoDeleteParams catalogoDelParams);

        private IActionResult SaveCatalogo(BaseInsUpdGenericCatalogo catalogo)
        {
            
            try
            {
                catalogo.Usuario = this.LoginName;
                catalogo.IdNegocio = 1;
                BLLPrestamo.Instance.InsUpdCatalogo(CatalogoName, catalogo);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("El cliente no pudo ser creado" + e.Message);

            }
        }

    }
}
