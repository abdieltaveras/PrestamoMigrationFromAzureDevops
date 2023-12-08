using System;
using System.Xml.Linq;

namespace DevBox.Core.Access
{
    public class Action : IComparable<Action>, IEquatable<Action>
    {
        public Guid ID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public ActionPermissionLevel PermissionLevel { get; set; }
        public SubActionList SubActions { get; set; } = new SubActionList();
        public XElement ToXml()
        {
            XElement result = new XElement("action");
            result.SetAttributeValue("id", this.ID);
            result.SetAttributeValue("displayName", this.DisplayName);
            result.SetAttributeValue("description", this.Description);
            result.SetAttributeValue("groupName", this.GroupName);            
            result.SetAttributeValue("value", this.Value);
            result.SetAttributeValue("permissionLevel", this.PermissionLevel);
            if (this.SubActions.Count > 0)
            {
                XElement subActions = new XElement("subActions");
                foreach (var item in this.SubActions)
                {
                    subActions.Add(item.ToXml());
                }
                result.Add(subActions);
            }
            return result;
        }
        public Action(XElement xml)
            : base()
        {
            Description = xml.Attribute("description").Value;
            DisplayName = xml.Attribute("displayName").Value;
            GroupName = xml.Attribute("groupName").Value;
            ID = Guid.Parse (xml.Attribute("id").Value);
            Value = xml.Attribute("value").Value;
            PermissionLevel = (ActionPermissionLevel)Enum.Parse(typeof(ActionPermissionLevel), xml.Attribute("permissionLevel").Value);
            SubActions = new SubActionList();
            if (xml.Element("subActions") != null)
            {
                foreach (var item in xml.Element("subActions").Elements())
                {
                    SubActions.Add(new SubAction(item));
                }
            }
        }
        public Action()
        {

        }
        public override string ToString()
        {
            return string.Format("{0}({2})", this.DisplayName, this.GroupName, this.PermissionLevel);
        }

        #region IComparable<Action> Members

        public int CompareTo(Action other)
        {
            return this.DisplayName.CompareTo(other.DisplayName);
        }

        #endregion

        bool IEquatable<Action>.Equals(Action other)
        {
            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }
        static public Action Empty => new Action(XElement.Parse($"<action id='{{{Guid.Empty}}}' displayName='Invalid Option' description='' groupName=''  value='' permissionLevel='Deny' />"));
    }
}
