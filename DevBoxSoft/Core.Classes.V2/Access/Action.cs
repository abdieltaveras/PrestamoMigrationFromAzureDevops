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
        public ActionPermissionLevel PermissionLevel { get; set; } = ActionPermissionLevel.None;
        public bool Inherited { get; set; }
        public SubActionList SubActions { get; set; } = new SubActionList();
        public XElement ToXml()
        {
            XElement result = new XElement("action");
            result.SetAttributeValue("id", ID);
            result.SetAttributeValue("displayName", DisplayName);
            result.SetAttributeValue("description", Description);
            result.SetAttributeValue("groupName", GroupName);
            result.SetAttributeValue("value", Value);
            result.SetAttributeValue("permissionLevel", PermissionLevel);
            result.SetAttributeValue("inherited", Inherited);
            if (SubActions.Count > 0)
            {
                XElement subActions = new XElement("subActions");
                foreach (var item in SubActions)
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
            //ID = new Guid();
            //ID = new Guid(xml.Attribute("id").Value);
            ID = Guid.Parse(xml.Attribute("id").Value);
            Value = xml.Attribute("value").Value;
            PermissionLevel = (ActionPermissionLevel)Enum.Parse(typeof(ActionPermissionLevel), xml.Attribute("permissionLevel").Value);
            Inherited = (xml.Attribute("inherited") != null) ? bool.Parse(xml.Attribute("inherited").Value) : false;
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
            return string.Format("{0}({2})", DisplayName, GroupName, PermissionLevel);
        }

        #region IComparable<Action> Members

        public int CompareTo(Action other)
        {
            return DisplayName.CompareTo(other.DisplayName);
        }

        #endregion

        bool IEquatable<Action>.Equals(Action other)
        {
            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }
        static public Action Empty => new Action(XElement.Parse($"<action id='{{{Guid.Empty}}}' displayName='Invalid Option' description='' groupName=''  value='' permissionLevel='Deny' inherited='false' />"));
    }
    public class ActionPermission
    {
        public Guid EntityID { get; set; }
        public string EntityName { get; set; }
        public Guid ActionID { get; set; }
        public string ActionValue { get; set; }
        public ActionPermissionLevel ActionPermissionLevel { get; set; }
        public string SubActionsValues { get; set; }
        public override string ToString() => $"{EntityName} {ActionValue} {ActionPermissionLevel}";
    }
}
