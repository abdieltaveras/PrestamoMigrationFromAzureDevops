using DevBox.Core.Access;
using DevBox.Core.Classes.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIClient.Models;
using UIClient.Pages.Components;
using UIClient.Services;
using Action = DevBox.Core.Access.Action;

namespace UIClient.Pages.CoreSystem.ActionsManager
{
    public partial class ActionManager : BasePage
    {
        Entity SelectedEntity = new Entity();
        HashSet<Entity> entities = new HashSet<Entity>();
        [Inject] private UserManagerService userManagerService { get; set; }
        protected async override Task OnInitializedAsync()
        {            
            await base.OnInitializedAsync();
            entities = await loadEntities();
        }
        Color getActionColor(Action action)
        {
            var result = Color.Dark;
            switch (action.PermissionLevel)
            {
                default:
                case ActionPermissionLevel.None:
                    result = Color.Dark;
                    break;
                case ActionPermissionLevel.Allow:
                    result = Color.Success;
                    break;
                case ActionPermissionLevel.PermissionRequired:
                    result = Color.Warning;
                    break;
                case ActionPermissionLevel.Deny:
                    result = Color.Error;
                    break;
                case ActionPermissionLevel.Permitir_Autorizar:
                    result = Color.Secondary;
                    break;
            }
            return result;
        }
        bool fnActionComparer(Action a1, Action a2) => (a1.Value == a2.Value) && (a1.PermissionLevel == a2.PermissionLevel);
        string getActionIcon(Action action)
        {
            var result = Icons.Material.Outlined.DoubleArrow;
            switch (action.PermissionLevel)
            {
                default:
                case ActionPermissionLevel.None:
                    result = Icons.Material.Outlined.ErrorOutline;
                    break;
                case ActionPermissionLevel.Allow:
                    result = Icons.Material.Outlined.CheckCircleOutline;
                    break;
                case ActionPermissionLevel.PermissionRequired:
                    result = Icons.Material.Outlined.AdminPanelSettings;
                    break;
                case ActionPermissionLevel.Deny:
                    result = Icons.Material.Outlined.Block;
                    break;
                case ActionPermissionLevel.Permitir_Autorizar:
                    result = Icons.Material.Outlined.PermScanWifi;
                    break;
            }
            return result;
        }
        private async Task<HashSet<Entity>> loadEntities()
        {
            var result = new HashSet<Entity>();
            var groups = await userManagerService.GetUsersGroups();
            var users = await userManagerService.GetUsers();
            foreach (var group in groups.OrderBy(g => g.GroupName))
            {
                var entity = new Entity()
                {
                    EntityType = EntityType.Group,
                    ID = group.GroupID,
                    Name = group.GroupName,
                    Description = group.Description,
                    Active = true,
                    Actions = group.Actions.GroupBy(a => a.GroupName).ToDictionary(k => k.Key, k => k.Select(a => new Changeable<Action>(a, fnActionComparer)).ToList()),
                    Members = users.Where(u => !u.IsNull() && u.GroupName.Equals(group.GroupName, StringComparison.CurrentCultureIgnoreCase))
                             .Select(u => new Entity()
                             {
                                 ID = u.UserID,
                                 EntityType = EntityType.User,
                                 Name = u.UserName,
                                 Description = u.FullName,
                                 Active = u.IsActive && !u.IsDeleted,
                                 Actions = u.Actions?.GroupBy(a => a.GroupName).ToDictionary(k => k.Key, k => k.Select(a => new Changeable<Action>(a, fnActionComparer)).ToList())
                             })
                             .OrderBy(u => u.Name)
                             .ToList()
                };
                result.Add(entity);
            }
            return result;
        }
        void onTreeNodeSelect(Entity entity)
        {

        }
        void onSubActionChanged(Changeable<Action> action, SubAction subAction)
        {
            SelectedEntity.Changed = true;
            action.HasChanged = true;
            if (SelectedEntity.EntityType.Name == EntityType.Group.Name)
            {
                foreach (var member in SelectedEntity.Members)
                {
                    if (!member.Active) { continue; }
                    var actions = member.Actions.Values.SelectMany(v => v).ToArray();
                    for (int i = 0; i < actions.Count(); i++)
                    {
                        var memberAction = actions[i];
                        if (memberAction.Value.ID.Equals(action.Value.ID))
                        {
                            foreach (var sAction in memberAction.Value.SubActions)
                            {
                                if (sAction.Value.Equals(subAction.Value))
                                {
                                    sAction.PermissionLevel = subAction.PermissionLevel;
                                }
                            }
                            member.Changed = true;
                        }
                    }
                }
            }
            else
            {
                action.Value.Inherited = false;
            }

        }
        void onActionChanged(Changeable<Action> cAction)
        {
            SelectedEntity.Changed = true;
            cAction.HasChanged = true;
            var action = cAction.Value;
            action.Inherited = false;
            if (SelectedEntity.EntityType.Name == EntityType.Group.Name)
            {
                foreach (var member in SelectedEntity.Members)
                {
                    if (!member.Active) { continue; }
                    var actions = member.Actions.Values.SelectMany(v => v).ToArray();
                    for (int i = 0; i < actions.Count(); i++)
                    {
                        var memberAction = actions[i];
                        if (memberAction.Value.ID.Equals(action.ID))
                        {
                            memberAction.Value.PermissionLevel = action.PermissionLevel;
                            memberAction.Value.Inherited = true;
                            memberAction.HasChanged = true;
                            foreach (var subAction in memberAction.Value.SubActions)
                            {
                                subAction.PermissionLevel = action.SubActions[subAction.Value].PermissionLevel;
                                subAction.Inherited = true;
                            }
                            member.Changed = true;
                        }
                    }
                }
            }
        }
        async void CancelChanges() => entities = await loadEntities();
        async void SaveChanges()
        {
            var updates = new List<ActionPermission>();
            loadGroupUpdates(updates);
            loadUsersUpdates(updates);
            await userManagerService.SaveActions(updates);
            entities = await loadEntities();
        }

        private void loadUsersUpdates(List<ActionPermission> updates)
        {
            var active = entities.SelectMany(g => g.Members)
                        .Where(e => e.Active && e.Changed);
            foreach (var entidad in active)
            {
                var actions = entidad.Actions.SelectMany(kv => kv.Value);
                var changed = actions.Where(a => a.HasChanged && !a.Value.Inherited);
                var result = changed.Select(a => new ActionPermission()
                {
                    EntityID = entidad.ID,
                    ActionID = a.Value.ID,
                    ActionPermissionLevel = a.Value.PermissionLevel,
                    SubActionsValues = a.Value.SubActions.ToCSV(sa => $"{sa.Value}:{sa.PermissionLevel}", ";"),
                    ActionValue = a.Value.Value,
                    EntityName = entidad.Name
                });
                updates.AddRange(result.ToList());
            }
        }

        private void loadGroupUpdates(List<ActionPermission> updates)
        {
            var active = entities.Where(e => e.Active && e.Changed);
            foreach (var entidad in active)
            {
                var actions = entidad.Actions.SelectMany(kv => kv.Value);
                var changed = actions.Where(a => a.HasChanged && !a.Value.Inherited);
                var result = changed.Select(a => new ActionPermission()
                {
                    EntityID = entidad.ID,
                    ActionID = a.Value.ID,
                    ActionPermissionLevel = a.Value.PermissionLevel,
                    SubActionsValues = a.Value.SubActions.ToCSV(sa => $"{sa.Value}:{sa.PermissionLevel}", ";"),
                    ActionValue = a.Value.Value,
                    EntityName = entidad.Name
                });
                updates.AddRange(result.ToList());
            }
        }
    }

    class EntityType
    {
        static public EntityType None => new EntityType() { Name = "", Icon = Icons.Filled.TabUnselected };
        static public EntityType Group => new EntityType() { Name = "Grupo", Icon = Icons.Filled.Group };
        static public EntityType User => new EntityType() { Name = "Usuario", Icon = Icons.Filled.Person };
        public string Name { get; set; }
        public string Icon { get; set; }
    }
    class Entity : ITreeItemData<Entity>
    {
        public Guid ID { get; set; }
        public EntityType EntityType { get; set; } = EntityType.None;
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, List<Changeable<DevBox.Core.Access.Action>>> Actions { get; set; }
        public List<Entity> Members { get; set; } = new List<Entity>();
        public bool HasChild => Members.Count > 0;
        public string Icon { get => EntityType.Icon; }
        public bool IsExpanded { get; set; }
        public string Text { get => Name; }
        public bool Changed { get; set; }
        public bool Active { get; set; }
        public HashSet<Entity> TreeItems { get => new HashSet<Entity>(Members); }
        public override string ToString() => Name;
    }

}
