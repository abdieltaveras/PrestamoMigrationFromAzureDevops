using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevBox.Core.Classes.Configuration
{
    public class SysPolicyCategory : IComparable<SysPolicyCategory>
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<SysPolicyCategory> Categories { get; set; }
        public List<SysPolicy> Policies { get; set; }
        public override string ToString()
        {
            return string.Format("{0} ({1} pols {2} cats)", this.name, this.Policies.Count, this.Categories.Count);
        }
        public XElement ToXml()
        {
            var result = new XElement("category");
            result.SetAttributeValue("id", this.id);
            result.SetAttributeValue("name", this.name);
            result.SetAttributeValue("description", this.description);
            foreach (var pol in this.Policies)
            {
                result.Add(pol.ToXml());
            }
            foreach (var cat in this.Categories)
            {
                result.Add(cat.ToXml());
            }
            return result;
        }
        public SysPolicyCategory()
        {
            this.id = "";
            this.name = "";
            this.description = "";
            this.Categories = new List<SysPolicyCategory>();
            this.Policies = new List<SysPolicy>();
        }
        public SysPolicyCategory(string xmlText)
        {
            var xml = XElement.Parse(xmlText);
            id = xml.Attribute("id").Value;
            name = xml.Attribute("name").Value;
            description = xml.Attribute("description").Value;
            Categories = new List<SysPolicyCategory>();
            Policies = new List<SysPolicy>();
            foreach (var item in xml.Elements("policy"))
            {
                this.Policies.Add(new SysPolicy(item.ToString()));
            }
            foreach (var item in xml.Elements("category"))
            {
                this.Categories.Add(new SysPolicyCategory(item.ToString()));
            }
        }
        public void SetCategory(SysPolicyCategory category)
        {
            var idx = this.Categories.IndexOf(category);
            if (idx < 0)
            {
                this.Categories.Add(category);
            }
            else
            {
                this.Categories[idx] = category;
            }
        }
        public void SetPolicy(SysPolicy policy)
        {
            var idx = this.Policies.IndexOf(policy);
            if (idx < 0)
            {
                this.Policies.Add(policy);
            }
            else
            {
                this.Policies[idx] = policy;
            }
        }
        public SysPolicy GetPolicy(string id)
        {
            SysPolicy result = null;
            result = (from p in this.Policies where string.Compare(id, p.id, true) == 0 select p).SingleOrDefault();
            if (result == null)
            {
                foreach (var cat in this.Categories)
                {
                    result = cat.GetPolicy(id);
                    if (result != null)
                    {
                        break;
                    }
                }
            }
            return result;
        }
        public void SetPolicyValue(string id, string value)
        {
            var p = GetPolicy(id);
            p.value = value;

        }
        public int CompareTo(SysPolicyCategory other)
        {
            return string.Compare(this.id, other.id, true);
        }
    }
}
