﻿using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class LocalidadNegocio : BaseInsUpd
    {

        public int IdLocalidadNegocioPadre { get; set; }
        [IgnoreOnParams]
        /// <summary>
        /// store Guid number
        /// </summary>
        public string Codigo { get;  set; } = string.Empty; // si esta propiedad está con intertal, no puede visualizarse el dato al hacer un GET
        /// <summar
        /// <summary>
        /// El nombre registrado legalmente
        /// </summary>
        public string NombreJuridico { get; set; } = string.Empty;
        /// <summary>
        /// El nombre comercial con lo que lo conoce el mercado (los clientes)
        /// </summary>
        public string NombreComercial { get; set; } = string.Empty;


        /// Prefijo que usaran las transacciones realizada por dicha localidadNegocio
        /// ejemplo una localidad llamada intagsa sosua podria ser el valor SOS 
        /// las 3 primeras letras de  Sosua
        /// </summary>
        public string PrefijoTransacciones { get;  set; } = string.Empty;

        public string PrefijoPrestamo { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public bool Bloqueado { get; set; } = false;
        public bool PermitirOperaciones { get; set; } = true;
        public string TaxIdNacional { get; set; } = string.Empty;

        public string TaxIdLocalidad { get; set; } = string.Empty;


        
        /// <summary>
        /// esta propiedad no va en la tabla
        /// </summary>
        [IgnoreOnParams]
        public LocalidadNegocioOtrosDetalles OtrosDetallesObj
        {
            get { return _OtrosDetalles.ToType<LocalidadNegocioOtrosDetalles>(); }
            set
            {
                _OtrosDetalles = value.ToJson();
            }
        }

        private string _OtrosDetalles { get; set; }
        
        /// <summary>
        /// esta propiedad es un holder solamente no es para accesarla, es la que se usa en la tabla de la base de datos
        /// </summary>
        public string OtrosDetalles
        {
            get { return _OtrosDetalles; }
            internal set
            {
                _OtrosDetalles = value;
            } 
        }


        public string Logo { get; set; } = string.Empty;
    }



    public class LocalidadNegociosGetParams : BaseGetParams
    {
        public int Opcion { get; set; } = -1;
        //public string Codigo { get; set; } = string.Empty;
        //public string NombreJuridico { get; set; } = string.Empty;

        //public string NombreComercial { get; set; } = string.Empty;

        //public string TaxIdNo { get; set; } = string.Empty;

        public int PermitirOperaciones { get; set; } = -1;

    }

    /// <summary>
    /// otras informaciones relacionadas al negocio
    /// </summary>
    public class LocalidadNegocioOtrosDetalles
    {

        public string Direccion { get; set; } //= string.Empty;

        public string Calle { get; set; } = string.Empty;

        [Phone]
        public string Telefono1 { get; set; } = string.Empty;

        [Phone]
        public string Telefono2 { get; set; } = string.Empty;
        public string Slogan { get; set; } = string.Empty;

        public string CorreoElectronico { get; set; } = string.Empty;

    }
}
