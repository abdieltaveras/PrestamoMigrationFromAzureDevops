using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Models
{
    public class Changeable<T> : IEquatable<T>, IComparable<T>
    {
        private T oldValue;
        private readonly Func<T, T, bool> comparer;
        private T currValue;
        void setValue(T value)
        {
            oldValue = currValue;
            currValue = value;
        }
        public bool Equals(T other) => comparer(Value, other);
        public int CompareTo(T other) => Value.GetHashCode().CompareTo(other.GetHashCode());
        public T Value { get => currValue; set => setValue(value); }
        //public bool HasChanged { get => !comparer(currValue, oldValue); }
        public bool HasChanged { get; set; }
        public Changeable(T value, Func<T, T, bool> comparer)
        {
            oldValue = value;
            currValue = value;
            this.comparer = comparer;
        }
    }
}
