using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Hooks
{
    public class AddRemoveListItemHook<T>
    {
        internal List<T> _Items { get; set; }
        public AddRemoveListItemHook(List<T> items)
        {
            _Items = items;
        }

        internal void RemoveItem(T item)
        {
            _Items.Remove(item);
        }
        internal void AddItem(T item)
        {
            _Items.Add(item);
        }
    }

}
