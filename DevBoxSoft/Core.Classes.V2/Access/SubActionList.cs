using System.Collections.Generic;
using System.Linq;

namespace DevBox.Core.Access
{
    public class SubActionList : IList<SubAction>
    {
        List<SubAction> SubActions = new List<SubAction>();

        public int IndexOf(SubAction item)
        {
            return this.SubActions.IndexOf(item);
        }

        public void Insert(int index, SubAction item)
        {
            this.SubActions.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.SubActions.RemoveAt(index);
        }
        public SubAction this[string subActionValue]
        {
            get
            {
                var result = (SubAction)(from a in this.SubActions where (string.Compare(a.Value, subActionValue, true) == 0) select a).SingleOrDefault();
                if (result == null)
                {
                    result = new SubAction();
                }
                return result;
            }
        }
        public SubAction this[int index]
        {
            get
            {
                return this.SubActions[index];
            }
            set
            {
                this.SubActions[index] = value;
            }
        }

        public void Add(SubAction item)
        {
            this.SubActions.Add(item);
        }

        public void Clear()
        {
            this.SubActions.Clear();
        }

        public bool Contains(SubAction item)
        {
            return this.SubActions.Contains(item);
        }

        public void CopyTo(SubAction[] array, int arrayIndex)
        {
            this.SubActions.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.SubActions.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(SubAction item)
        {
            return this.SubActions.Remove(item);
        }

        public IEnumerator<SubAction> GetEnumerator()
        {
            return this.SubActions.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (this.SubActions as IEnumerable<SubAction>).GetEnumerator();
        }
    }
}
