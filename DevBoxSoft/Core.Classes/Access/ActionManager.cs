using System;
using System.Collections.Generic;
using System.Text;

namespace DevBox.Core.Access
{
    public static class ActionManager
    {
        static public ActionList MergeActions(params ActionList[] actionsLists)
        {
            var result = new ActionList();
            foreach (var list in actionsLists)
            {
                foreach (var action in list)
                {
                    addAction(result, action);
                }
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
