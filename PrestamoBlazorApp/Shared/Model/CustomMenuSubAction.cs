using DevBox.Core.Access;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PrestamoBlazorApp.Shared.Model
{
    public class CustomMenuSubAction : SubAction
    {
        public string Url { get; set; }
        public override string ToString()
        {
            return Value;
        }
    }
    public class CustomMenuAction : Action
    {
        public string Url { get; set; }

        public List<CustomMenuSubAction> CustomMenuSubActions { get; set; }
        public override string ToString()
        {
            return Value;
        }
    }
    public class RootMenuAction
    {
        public List<CustomMenuAction> Actions { get; set; }
    }
}
