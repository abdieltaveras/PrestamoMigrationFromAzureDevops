using DevBox.Core.Classes.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBox.Core.Access
{
    public static class ActionManager
    {
        static public ActionList MergeActions(ActionList defaultActions, ActionList groupActions, ActionList userActions = null)
        {
            var result = new ActionList();
            foreach (var action in defaultActions)
            {
                action.Inherited = false;
                action.SubActions.ForEach(sa => sa.Inherited = false);
                result.Add(action);
            }
            foreach (var action in groupActions)
            {
                var inherit = isInherited(result, action);
                action.Inherited = inherit;
                addAction(result, action);
            }
            if (userActions != null)
            {
                foreach (var action in userActions)
                {
                    addAction(result, action);
                }
            }
            return result;
        }

        private static bool isInherited(ActionList baseAction, Action newAction)
        {
            var existingAction = baseAction[newAction.ID];
            var result = (existingAction == null);
            if (existingAction != null)
            {
                result = existingAction.PermissionLevel == newAction.PermissionLevel;
            }
            return result;
        }

        static private void addAction(ActionList result, Action action)
        {
            var holder = result[action.ID];
            if (holder != null)
            {
                if (action.SubActions.Count != holder.SubActions.Count)
                {
                    action.SubActions = holder.SubActions;
                }
            }
            result.Add(action);
        }
    }
}
