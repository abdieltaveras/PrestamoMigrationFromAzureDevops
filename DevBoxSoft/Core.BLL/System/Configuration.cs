﻿using DevBox.Core.Classes.Configuration;
using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.BLL.System
{
    public static class SysConfiguration
    {
        public static List<DiaFeriado> GetDiasFeriados(DiaFeriado searchRec)
        => GetDiasFeriados(searchRec, null, null, null);
        public static List<DiaFeriado> GetDiasFeriados(int Ano) => GetDiasFeriados(null, Ano, null, null);
        public static List<DiaFeriado> GetDiasFeriados(DateTime? desdeDia, DateTime? hastaDia)
        => GetDiasFeriados(null, null, desdeDia, hastaDia);
        private static List<DiaFeriado> GetDiasFeriados(DiaFeriado searchRec, int? Ano, DateTime? desdeDia, DateTime? hastaDia)
        => Database.DataServer.ExecReaderSelSP<DiaFeriado>("core.spGetDiasFeriados",
                                                           SearchRec.ToSqlParams(new { Ano = Ano < 1 ? DateTime.Today.Year : Ano, 
                                                                                       desdeDia, 
                                                                                       hastaDia }));
        public static void SaveDiaFeriado(DiaFeriado diaFeriado)
        => Database.DataServer.ExecSelSP("core.spInsUpdDiasFeriados", SearchRec.ToSqlParams(diaFeriado));
        public static void DelDiaFeriado(DiaFeriado diaFeriado)
        {
            SearchRec rec = new SearchRec();
            rec.AddParam("idDiaFeriado", diaFeriado.idDiaFeriado);
            Database.DataServer.ExecSelSP("core.spDelDiaFeriado", rec.ToSqlParams());
        }
    }
}
