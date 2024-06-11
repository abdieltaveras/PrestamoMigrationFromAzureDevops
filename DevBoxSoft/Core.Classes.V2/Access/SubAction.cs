using System;
using System.Xml.Linq;

namespace DevBox.Core.Access
{
    public class SubAction : IComparable<SubAction>, IEquatable<SubAction>
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public ActionPermissionLevel PermissionLevel { get; set; } = ActionPermissionLevel.None;
        public bool Inherited { get; set; }
        public XElement ToXml()
        {
            XElement result = new XElement("subAction");
            result.SetAttributeValue("displayName", DisplayName);
            result.SetAttributeValue("description", Description);
            result.SetAttributeValue("value", Value);
            result.SetAttributeValue("permissionLevel", PermissionLevel);
            result.SetAttributeValue("inherited", Inherited);
            return result;
        }
        public SubAction()
        {
            Description = "";
            DisplayName = "";
            Value = "";
            PermissionLevel = ActionPermissionLevel.None;
            Inherited = false;
        }
        public SubAction(XElement xml)
        {
            Description = xml.Attribute("description").Value;
            DisplayName = xml.Attribute("displayName").Value;
            Value = xml.Attribute("value").Value;
            PermissionLevel = (ActionPermissionLevel)Enum.Parse(typeof(ActionPermissionLevel), xml.Attribute("permissionLevel").Value);
            Inherited = (xml.Attribute("inherited") != null) ? bool.Parse(xml.Attribute("inherited").Value) : false;
        }
        public override string ToString()
        {
            string permision = "";
            switch (PermissionLevel)
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
            return string.Format("{0}{1}", DisplayName, permision);
        }

        public int CompareTo(SubAction other)
        {
            return string.Compare(Value, other.Value, true);
        }

        bool IEquatable<SubAction>.Equals(SubAction other)
        {
            return string.Equals(Value, other.Value, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
