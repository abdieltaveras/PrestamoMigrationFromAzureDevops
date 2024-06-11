using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.DAL.SQLServer
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GuardarXML : Attribute
    {
        public GuardarXML()
        {

        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class GuardarEncriptado : Attribute
    {
        public bool Encryptar { get; set; }
        public GuardarEncriptado()
        {
            this.Encryptar = true;
        }
        public GuardarEncriptado(bool encriptar)
        {
            this.Encryptar = encriptar;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreOnParams : Attribute
    {
        public bool Ignorar { get; set; }
        public IgnoreOnParams()
        {
            this.Ignorar = true;
        }
        public IgnoreOnParams(bool Ignorar)
        {
            this.Ignorar = Ignorar;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class ParametroSalida : Attribute
    {
        public bool EsParametroSalida { get; set; }
        public int Tamaño { get; set; }
        public ParametroSalida()
        {
            this.EsParametroSalida = false;
        }
        public ParametroSalida(bool DeSalida, int Tamaño)
        {
            this.EsParametroSalida = DeSalida;
            this.Tamaño = Tamaño;
        }
    }
}
