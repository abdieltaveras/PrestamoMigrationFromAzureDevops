using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Base
{
    public class ButtonForToolBar<TType>
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public Color Color { get; set; }
        public Func<TType, Task> OnClick { get; set; } //= (o) => { };
        public Func<object, bool> IsEnabled { get; set; } = (o) => true;
        public bool Show { get; set; } = true;
    }
    
}
