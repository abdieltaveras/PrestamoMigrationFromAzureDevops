using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevBox.Core.Classes.Configuration
{
    public class SysPolicy : IComparable<SysPolicy>
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string typeData { get; set; }
        public string value { get; set; }
        public override string ToString()
        {
            return string.Format("{0} ({1}) = {2})", this.name, this.type, this.value);
        }
        public XElement ToXml()
        {
            var result = new XElement("policy");
            result.SetAttributeValue("id", this.id);
            result.SetAttributeValue("name", this.name);
            result.SetAttributeValue("description", this.description);
            result.SetAttributeValue("type", this.type);
            result.SetAttributeValue("typeData", this.typeData);
            result.SetAttributeValue("value", this.value);
            return result;
        }
        public SysPolicy()
        {
            this.id = "";
            this.name = "";
            this.description = "";
            this.type = "";
            this.typeData = "";
            this.value = "";
        }
        public SysPolicy(string xmlText)
        {
            var xml = XElement.Parse(xmlText);
            this.id = xml.Attribute("id").Value;
            this.name = xml.Attribute("name").Value;
            this.description = xml.Attribute("description").Value;
            this.type = xml.Attribute("type").Value;
            this.typeData = xml.Attribute("typeData").Value;
            this.value = xml.Attribute("value").Value;
        }

        public int CompareTo(SysPolicy other)
        {
            return string.Compare(this.id, other.id, true);
        }
    }
}
