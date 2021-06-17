using emtSoft.DAL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        /// <summary>
        /// Para registrar el equipo pero no actualiza el campo confirmado ni en la insersion
        /// ni en la actualicion esas son 2 operaciones apartes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // Registrar Equipo

        public EquipoIdYCodigo InsUpdEquipo(Equipo data)
        { 
            var _updParam = SearchRec.ToSqlParams(data);
            var resultSet = DBPrestamo.ExecReaderSelSP<EquipoIdYCodigo>("spInsUpdEquipo", _updParam);
            return resultSet.FirstOrDefault();
        }

        // GetEquipoRegistrado(CodigoEquipo)
        public IEnumerable<Equipo> GetEquipos(EquiposGetParam searchParam)
        {
            return BllAcciones.GetData<Equipo, EquiposGetParam>(searchParam, "spGetEquipos", GetValidation);
        }
        /// <summary>
        /// Confirma el registro para poder operar
        /// </summary>
        public void ConfirRegistroEquipo(EquiposGetParam2 searchParam)
        {
            var _searchParam = SearchRec.ToSqlParams(searchParam);
            DBPrestamo.ExecSelSP("SpConfirmarRegistro", _searchParam);
        }
        /// <summary>
        /// Para registrar un log de registro a este dispositivo  
        /// </summary>
        /// <param name="idEquipo"></param>
        /// <param name="usuario"></param>
        public void LogOperationsOfEquipo(EquiposGetParam2 getParam)
        {
            //todo: analizar esta implementacion si hacerlo aqui, o donde pues el objetivo sera llevar un control de quien ha accesado el sistema desde este equipo y a que hora lo hizo
            throw new NotImplementedException();
        }

        public void BloquearAccesoAEquipo(EquiposGetParam2 getParam)
        {
            var _searchParam = SearchRec.ToSqlParams(getParam);
            DBPrestamo.ExecSelSP("SpBloquearEquipo", _searchParam);
        }
        
        // Registrar acceso al equipo (el log)
        // Bloquear El Equipo
        public void DesvincularEquipo(EquiposGetParam2 getParam)
        {
            var anularRegistro = new AnulacionDeRegistro
            {
                IdRegistroNombreColumna = "IdEquipo",
                IdRegistroValor = getParam.IdEquipo,
                NombreTabla = "dbo.tblEquipos",
                Usuario = getParam.Usuario
            };
            var _updateData = SearchRec.ToSqlParams(anularRegistro);
            DBPrestamo.ExecSelSP("SpAnularRegistro", _updateData);
        }


        

        /// <summary>
        /// Solo tiene el idDelEquipo y el codigo
        /// </summary>
        public class EquipoIdYCodigo
        {
            public int IdEquipo { get;  internal set; }
            public string Codigo { get; internal set; }
        }
        /// <summary>
        /// Solo tiene el idDelEquipo y el codigo
        /// </summary>
        public class EquiposGetParam2
        {
            public int IdEquipo { get; set; }
            public string Usuario { get; set; }
        }
    }



}