using System;
using System.Xml.Linq;

namespace DevBox.Core.Access
{
    public class SubAction : IComparable<SubAction>, IEquatable<SubAction>
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public ActionPermissionLevel PermissionLevel { get; set; }
        public XElement ToXml()
        {
            XElement result = new XElement("subAction");
            result.SetAttributeValue("displayName", this.DisplayName);
            result.SetAttributeValue("description", this.Description);
            result.SetAttributeValue("value", this.Value);
            result.SetAttributeValue("permissionLevel", this.PermissionLevel);
            return result;
        }
        public SubAction()
        {
            this.Description = "";
            this.DisplayName = "";
            this.Value = "";
            this.PermissionLevel = ActionPermissionLevel.None;
        }
        public SubAction(XElement xml)
        {
            this.Description = xml.Attribute("description").Value;
            this.DisplayName = xml.Attribute("displayName").Value;
            this.Value = xml.Attribute("value").Value;
            this.PermissionLevel = (ActionPermissionLevel)Enum.Parse(typeof(ActionPermissionLevel), xml.Attribute("permissionLevel").Value);
        }
        public override string ToString()
        {
            string permision = "";
            switch (this.PermissionLevel)
            {
                case ActionPermissionLevel.None:
                    permision = "";
                    break;
                case ActionPermissionLevel.Allow:
                    permision = "(permitida)";
                    break;
                case ActionPermissionLevel.PermissionRequired:
                    permision = "(requiere autorización)";
                    break;
                case ActionPermissionLevel.Deny:
                    permision = "(negada)";
                    break;
                case ActionPermissionLevel.Permitir_Autorizar:
                    permision = "(puede autorizar)";
                    break;
                default:
                    break;
            }
            return string.Format("{0}{1}", this.DisplayName, permision);
        }

        public int CompareTo(SubAction other)
        {
            return string.Compare(this.Value, other.Value, true);
        }

        bool IEquatable<SubAction>.Equals(SubAction other)
        {
            return string.Equals(this.Value, other.Value, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
