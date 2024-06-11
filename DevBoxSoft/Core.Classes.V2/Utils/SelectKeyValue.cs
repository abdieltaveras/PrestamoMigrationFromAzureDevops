using System;

namespace DevBox.Core.Classes.Utils
{
    public interface IKeyValue
    {
        object GetKey();
        object GetValue();
    }
    public class SelectKeyValue<T> :IKeyValue, IComparable<T>, IEquatable<T> where T : IKeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public object GetKey() => Key;
        public object GetValue() => Value;
        public int CompareTo(T other) => Key.CompareTo(other.GetKey());
        public bool Equals(T other) => Key.Equals(other.GetKey());
        public override string ToString() => Value;
    }
}
