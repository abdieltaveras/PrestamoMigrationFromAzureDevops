using DevBox.Core.DAL.SQLServer;
using PcpUtilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    // clase para manejar imagenes
    public class Imagen
    {
        public string Grupo { get; set; }
        /// <summary>
        /// el nombre del archivo 
        /// </summary>
        public string NombreArchivo { get; set; }

        /// <summary>
        /// El archivo base64
        /// </summary>

        //[IgnoreOnParams]
        public string Base64string { get; set; }

        /// <summary>
        /// para indicar que la imagen es nueva  y se debe agregar
        /// </summary>
        //[IgnoreOnParams]
        public bool Agregar { get; set; } = false;

        /// <summary>
        /// para indicar si la imagen debe o no ser quitada
        /// </summary>
        //[IgnoreOnParams]
        public bool Quitar { get; set; } = false;

        public void ConvertNombreArchivoToBase64String(string directorio)
        {
            if (!string.IsNullOrEmpty(directorio))
            {
                var fileName = directorio + NombreArchivo;
                this.Base64string= Utils.ConvertFileToBase64(fileName);
            }
        }
        

        public override string ToString()
        {
            return $"{Grupo} {NombreArchivo} {Agregar}";
        }

        public string ConvertToJson()
        {
            var obj = new { Grupo = Grupo, NombreArchivo = NombreArchivo };
            return obj.ToJson();

        }
    }

}
