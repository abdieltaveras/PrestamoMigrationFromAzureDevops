using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    internal static class ConexionDB
    {
        internal static Database DBPrestamo => Database.AdHoc(Server);

        public static string Server
        {
            get
            {
                string connst = "Dataserver";
                #if DEBUG
                connst = "DataserverAbdiel";
                #endif
                return connst;
            }
        }
    }
}
