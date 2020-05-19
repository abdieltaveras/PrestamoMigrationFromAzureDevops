﻿using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public int InsUpdPrestamo(Prestamo prestamo)
        {
            PrestamoBuilder prToBuild = new PrestamoBuilder(prestamo);
            var result = prToBuild.Build();
            
            var prestamoParam2 = SearchRec.ToSqlParams(result);
            //var resultId = PrestamosDB.ExecSelSP("spInsUpdPrestamo",prestamoParam);
            var resultId = PrestamosDB.ExecSelSP("spInsUpdPrestamo", prestamoParam2);
            //var result = PrestamosDB.ExecSelSP("spInsUpdNegocio", _insUpdParam);
            //idResult = Utils.GetIdFromDataTable(result);
            var  id= Utils.GetIdFromDataTable(resultId);
            return id;
        }

        public IEnumerable<Prestamo> GetPrestamos(PrestamosGetParam searchParam)
        {
            //GetValidation(searchParam as BaseGetParams);
            
            var data = BllAcciones.GetData<Prestamo, PrestamosGetParam>(searchParam, "spGetPrestamos", GetValidation);
            return data;
        }

        public PrestamoConDetallesParaCreditosYDebitos GetPrestamoConDetalle(int idPrestamo, DateTime fecha)
        {
            if (idPrestamo <= 0 || fecha == null)
            {
                throw new NullReferenceException("el Id del prestamo enviado es invalido, o la fecha esta nula");
            }
            //GetValidation(searchParam as BaseGetParams    );
            var searchRec = SearchRec.ToSqlParams(new { idPrestamo = idPrestamo });
            var dr = PrestamosDB.ExecReaderSelSP("spGetPrestamoConDetalle", searchRec);
            InfoClienteDrCr infoCliente = new InfoClienteDrCr();
            InfoPrestamoDrCr infoPrestamo = new InfoPrestamoDrCr();
            
            while (dr.Read())
            {
                dr.DataReaderToType(out infoPrestamo);
                if (fecha <= infoPrestamo.FechaEmisionReal)
                {
                    throw new InvalidOperationException("La fecha enviada es menor a la fecha de emision del prestamo");
                }
                dr.DataReaderToType(out infoCliente);
            }
            var cuotas = new List<CuotaAmpliada>();
            if (dr.NextResult())
            {
                while (dr.Read())
                {
                    var cuota = new CuotaAmpliada();
                    dr.DataReaderToType(out cuota);
                    cuotas.Add(cuota);
                }
            }
            List<InfoGarantiaDrCr> infoGarantiasDrCr = new List<InfoGarantiaDrCr>();
            if (dr.NextResult())
            {
                while (dr.Read())
                {
                    InfoGarantiaDrCr infoGarantiaDrCr; 
                    dr.DataReaderToType(out infoGarantiaDrCr );
                    infoGarantiasDrCr.Add(infoGarantiaDrCr);
                }
            }

            // las garantia
            // los codeudores
            var PrestamoConDetalle = new PrestamoConDetallesParaCreditosYDebitos();
            PrestamoConDetalle.infoPrestamo = infoPrestamo;
            PrestamoConDetalle.infoCliente = infoCliente;
            PrestamoConDetalle.Cuotas = cuotas;
            PrestamoConDetalle.infoGarantias = infoGarantiasDrCr;
            PrestamoConDetalle.InfoDeuda = new InfoDeudaPrestamoDrCr(cuotas, fecha);
            return PrestamoConDetalle;
        }
    }
}
