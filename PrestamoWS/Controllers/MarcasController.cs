﻿using Microsoft.AspNetCore.Mvc;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamoWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MarcasController : Controller
    {
        [HttpGet]
        public Marca Get()
        {
            return new Marca();
        }
        [HttpGet]
        public void SaveGet()
        {
            MarcaVM datos = new MarcaVM();
            //Hay que agregar el controller
            datos.ListaMarcas = BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = 1 });

            //return View("CreateOrEdit", datos);
        }
        [HttpPost]
        public void SavePost(Marca marca)
        {
            //marca.IdNegocio = 1;
            //marca.InsertadoPor = "Bryan";
            //this.pcpSetUsuarioAndIdNegocioTo(marca);
            BLLPrestamo.Instance.InsUpdMarca(marca);
            //return RedirectToAction("CreateOrEdit");
        }
    }
}
