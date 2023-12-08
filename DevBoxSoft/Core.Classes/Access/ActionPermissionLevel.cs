using System;

namespace DevBox.Core.Access
{
    public enum ActionSort
    {
        byName,
        byGroup,
        byId
    }
    public enum ActionListFilters { All, Allowed, Denied }
    public enum ActionPermissionLevel
    {
        None = -1,
        Allow = 0,
        PermissionRequired = 1,
        Deny = 2,
        Permitir_Autorizar = 3
    }
}
