using Utils = DevBox.Core.Classes.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace DevBox.Core.Access
{
    public class ActionList : ICollection<Action>, IEnumerable<Action>
    {
        static Dictionary<ActionListFilters, ActionPermissionLevel[]> actionFilters = new Dictionary<ActionListFilters, ActionPermissionLevel[]>
        {
            { ActionListFilters.All,     new ActionPermissionLevel[] { ActionPermissionLevel.None, ActionPermissionLevel.Deny, ActionPermissionLevel.Allow, ActionPermissionLevel.PermissionRequired, ActionPermissionLevel.Permitir_Autorizar } },
            { ActionListFilters.Denied,  new ActionPermissionLevel[] { ActionPermissionLevel.None, ActionPermissionLevel.Deny } },
            { ActionListFilters.Allowed, new ActionPermissionLevel[] { ActionPermissionLevel.Allow, ActionPermissionLevel.PermissionRequired, ActionPermissionLevel.Permitir_Autorizar } }
        };
        private class ActionComparer : IComparer<Action>
        {
            private ActionSort sortType = ActionSort.byName;
            public ActionComparer(ActionSort sortType)
            {
                this.sortType = sortType;
            }
            #region IComparer<Action> Members

            int IComparer<Action>.Compare(Action x, Action y)
            {
                switch (this.sortType)
                {
                    case ActionSort.byId: return x.ID.CompareTo(y.ID);
                    case ActionSort.byName: return x.DisplayName.CompareTo(y.DisplayName);
                    default:
                    case ActionSort.byGroup:
                        return string.Compare(string.Concat(x.GroupName, "_", x.DisplayName),
                                              string.Concat(y.GroupName, "_", y.DisplayName),
                                              true);
                }
            }

            #endregion
        }

        List<Action> _list = new List<Action>();

        ///ActionPermissionLevel[] permissionFilter = new ActionPermissionLevel[2]; //ActionList.actionFilters[ActionListFilters.All];
        List<Action> list
        {
            get { return _list; }
            set { _list = value; }
        }

        public ActionList Filter(ActionListFilters listFilters) =>
                new ActionList()
                {
                    list = _list.Where(a => ActionList.actionFilters[listFilters].Contains(a.PermissionLevel)).ToList()
                };
        public void Add(Action a)
        {
            setValue(a);
        }
        public void SortBy(ActionSort sortType)
        {
            this.list.Sort(new ActionComparer(sortType));
        }
        public Action this[Guid id]
        {
            get
            {
                Action result = list.Where(a => a.ID.Equals(id)).SingleOrDefault();
                return result;
            }
            set
            {
                setValue(value);
            }
        }
        public Action this[string actionValue]
        {
            get
            {
                Action result = list.Where(a => a.Value.Equals(actionValue, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
                return result;
            }
            set
            {
                setValue(value);
            }
        }
        private void setValue(Action value)
        {
            Action action = this[value.ID];
            if (action != null)
            {
                this.list.Remove(action);
            }
            this.list.Add(value);
        }
        public XElement ToXml()
        {
            XElement result = new XElement("actions");
            foreach (Action a in this.list)
            {
                result.Add(a.ToXml());
            }
            return result;
        }
        public ActionList()
        {

        }
        public ActionList(XElement xml)
        {
            list = new List<Action>();
            foreach (var item in xml.Elements("action"))
            {
                this.list.Add(new Action(item));
            }
        }

        #region IEnumerable<Action> Members

        public IEnumerator<Action> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.AsEnumerable().GetEnumerator();
        }

        #endregion

        public static ActionList Empty => new ActionList(XElement.Parse($"<actions>{Action.Empty}</actions>"));

        public int Count => ((ICollection<Action>)list).Count;

        public bool IsReadOnly => ((ICollection<Action>)list).IsReadOnly;

        public static ActionList ParseJSON(string json)
        {
            var settings = new JsonSerializerSettings();
            var actions = (ICollection<Action>)JsonConvert.DeserializeObject(json, typeof(ICollection<Action>), settings);
            var result = new ActionList();
            result.list.AddRange(actions);
            return result;
        }

        public void Clear()
        {
            ((ICollection<Action>)list).Clear();
        }

        public bool Contains(Action item)
        {
            return ((ICollection<Action>)list).Contains(item);
        }

        public void CopyTo(Action[] array, int arrayIndex)
        {
            ((ICollection<Action>)list).CopyTo(array, arrayIndex);
        }

        public bool Remove(Action item)
        {
            return ((ICollection<Action>)list).Remove(item);
        }
    }
}
