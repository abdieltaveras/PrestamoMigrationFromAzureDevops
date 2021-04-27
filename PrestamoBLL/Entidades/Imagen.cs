using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    // clase para manejar imagenes
    public class Imagen
    {
        public string Grupo { get; set; }

        public string NombreArchivo { get; set; }

        public string Base64string { get; set; }

        public bool NewImagen { get; set; } = false;

        public bool Quitar { get; set; } = false;

        public void ConvertNombreArchivoToBase64String(string directorio)
        {
            if (!string.IsNullOrEmpty(directorio))
            {
                var fileName = directorio + NombreArchivo;
                this.Base64string= Utils.ConvertFileToBase64(fileName);
            }
        }
        internal Imagen() { }
        public Imagen(bool newImagen)
        {
            this.NewImagen = newImagen;
        }

        public override string ToString()
        {
            return $"{Grupo} {NombreArchivo} {NewImagen}";
        }

        public string ConvertToJson()
        {
            var obj = new { Grupo = Grupo, NombreArchivo = NombreArchivo };
            return obj.ToJson();

        }
    }

}
