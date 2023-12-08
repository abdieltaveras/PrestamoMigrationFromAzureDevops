using DevBox.Core.Classes.Utils;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Test1
{
    internal class AreaEducativa : IEquatable<AreaEducativa>
    {
        static string NO_EVALUALACION = "<evaluacion/>";
        public int idAreaEducativa { get; set; }
        public string areaEducativa { get; set; }
        public string Prefijo { get; set; }
        public int DiasPlazoCierreInscripcion { get; set; }
        public string Evaluacion { get; set; }
        public List<SubAreaEducativa> SubAreas { get; set; }
        public string SubAreasText
        {
            get
            {
                var result = "";
                if (this.SubAreas.Count > 0)
                {
                    result = this.SubAreas.ConcatToStr(",");
                    result = result.Remove(result.Length - 1, 1);
                }
                return result;
            }
        }
        //public string EvaluacionText
        //{
        //    get
        //    {
        //        var result = this.Evaluacion;
        //        if (string.IsNullOrEmpty(result) || this.Evaluacion == NO_EVALUALACION)
        //        {
        //            result = "No Evaluación Establecida";
        //        }
        //        else
        //        {
        //            indicador indF = new indicador();
        //            BLL.GetIndicadoresEvaluacion(this, ref indF);
        //            result = indF.ToString().Replace("\t", "");
        //        }
        //        return result;
        //    }
        //}
        public AreaEducativa()
        {
            this.idAreaEducativa = -1;
            this.areaEducativa = "";
            this.Prefijo = "";
            this.Evaluacion = NO_EVALUALACION;
            this.DiasPlazoCierreInscripcion = 5;
            this.SubAreas = new List<SubAreaEducativa>();
        }
        public override string ToString()
        {
            return areaEducativa;
        }

        internal XElement ToXml()
        {
            XElement result = new XElement("areaEducativa");
            result.SetAttributeValue("idAreaEducativa", this.idAreaEducativa);
            result.SetAttributeValue("AreaEducativa", this.areaEducativa);
            result.SetAttributeValue("SubAreas", this.SubAreas.ConcatToStr(","));
            result.SetAttributeValue("Prefijo", this.Prefijo);
            XElement eval = XElement.Parse(this.Evaluacion);
            result.Add(eval);
            return result;
        }

        public bool Equals(AreaEducativa other)
        {
            return this.idAreaEducativa.Equals(other.idAreaEducativa);
        }
    }
    public class SubAreaEducativa : IEquatable<SubAreaEducativa>
    {
        public int idSubAreaEducativa { get; set; }
        public int idAreaEducativa { get; set; }
        public string AreaEducativa { get; set; }
        public string Titulo { get; set; }
        public override string ToString()
        {
            return AreaEducativa;
        }
        //public List<ExamenAdmisionConf> GetExamenesAdmisionConf()
        //{
        //    var result = (this.idSubAreaEducativa > 0) ? BLL.GetExamenAdmisionConf(new SearchExamenAdmisionConf() { idSubAreaEducativa = this.idSubAreaEducativa })
        //                                               : new List<ExamenAdmisionConf>();
        //    return result;
        //}

        #region IEquatable<SubAreaEducativa> Members

        public bool Equals(SubAreaEducativa other)
        {
            return this.idSubAreaEducativa.Equals(other.idSubAreaEducativa);
        }

        #endregion


    }
}