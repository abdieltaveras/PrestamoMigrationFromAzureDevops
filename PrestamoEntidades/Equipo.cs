using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Equipo : BaseInsUpd
    {
        public int IdEquipo { get; set; }
        public int IdNegocio { get; set; }
        [IgnoreOnParams]
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime UltimoAcceso { get; set; } = InitValues._19000101;

        [IgnoreOnParams]

        /// <summary>
        /// hasta que un equipo no sea confirmado no podra ser utilizado
        /// </summary>

        public DateTime FechaBloqueado { get; set; } = InitValues._19000101;

        [IgnoreOnParams]
        public bool BloqueadoPor { get; set; }

        public string AccesadoPor { get; set; }
        [IgnoreOnParams]
        /// <summary>
        /// hasta que un equipo no sea confirmado no podra ser utilizado
        /// </summary>

        public DateTime FechaConfirmado { get; set; } = InitValues._19000101;
        [IgnoreOnParams]
        public bool ConfirmadoPor { get; set; }
        [IgnoreOnParams]
        public bool EstaConfirmado
        {
            get
            {
                return (FechaConfirmado.CompareTo(InitValues._19000101) > 0);
            }
        }
        [IgnoreOnParams]
        public bool EstaBloqueado
        {
            get
            {
                return ( FechaBloqueado.CompareTo(InitValues._19000101) > 0);
            }
        }
        [IgnoreOnParams]
        public bool EstaDesvinculado
        {
            get
            {
                var result = FechaAnulado.CompareTo(InitValues._19000101);
                return result > 0;
            }
        }
    }
    public class EquiposGetParam : BaseGetParams
    {
        public int IdEquipo { get; set; } = -1;
        public string Codigo { get; set; } = string.Empty;
    }

}
