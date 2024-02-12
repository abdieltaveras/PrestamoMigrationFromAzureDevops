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
        protected internal IActionResult PostBase([FromBody] BaseInsUpdGenericCatalogo catalogoInsUPd) => SaveCatalogo(catalogoInsUPd);
        protected internal IEnumerable<@CatalogoType> GetBase([FromQuery] BaseCatalogoGetParams getParams) => BLLPrestamo.Instance.GetCatalogos<CatalogoType>(CatalogoName, getParams);
        protected internal IActionResult DeleteBase(BaseCatalogoDeleteParams catalogoDelParams) => DeleteCatalog(catalogoDelParams);
        public abstract IActionResult Post([FromBody] @CatalogoType  catalogoInsUpd);
        public abstract IEnumerable<@CatalogoType> Get([FromQuery] BaseCatalogoGetParams getParams);
        public abstract IActionResult Delete(BaseCatalogoDeleteParams catalogoDelParams);

        private IActionResult DeleteCatalog(BaseCatalogoDeleteParams catalogoDelParams)
        {
            try 
            {
                BLLPrestamo.Instance.DeleteCatalogo(CatalogoName, catalogoDelParams);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo realizar la peticion de eliminar " + e.Message);
            }
        }
        private IActionResult SaveCatalogo(BaseInsUpdGenericCatalogo catalogo)
        {
            
            try
            {
                catalogo.Usuario = this.LoginName;
                catalogo.IdNegocio = this.IdNegocio;
                BLLPrestamo.Instance.InsUpdCatalogo(CatalogoName, catalogo);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("El catalogo no pudo ser creado" + e.Message);
            }
        }

    }
}
