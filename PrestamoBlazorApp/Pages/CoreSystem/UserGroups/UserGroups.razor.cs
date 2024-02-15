using DevBox.Core.Identity;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Base;
using PrestamoBlazorApp.Shared.Components.Forms;
using PrestamoBlazorApp.Shared.Layout.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Pages.CoreSystem.UserGroups
{
    public partial class UserGroups : BasePage
    {
        [Inject] private UserManagerService userManagerService { get; set; }
        [Inject] private IDialogService Dialog { get; set; }

        private IEnumerable<CoreUserGroup> Groups = new List<CoreUserGroup>();
        private List<DataGridViewColumn> columns => new List<DataGridViewColumn>()
        {
        new DataGridViewColumn{ Header= "Grupo", ColumnName = "GroupName", ColumnType= DataGridViewColumnTypes.Text, ContentAligment= MudBlazor.Align.Left, HeaderAligment= MudBlazor.Align.Center},
        new DataGridViewColumn{ Header= "Descripción", ColumnName = "Description", ColumnType= DataGridViewColumnTypes.Text, ContentAligment= MudBlazor.Align.Left, HeaderAligment= MudBlazor.Align.Center}
        };
        public List<DataGridViewToolbarButton> buttons => new List<DataGridViewToolbarButton>()
        {
        new DataGridViewToolbarButton(){ Color= Color.Primary, Icon=Icons.Material.Filled.AddCircle, Text="Nuevo", OnClick=btnAddClick, IsEnabled=btnAddEnabled},
        new DataGridViewToolbarButton(){ Color= Color.Secondary, Icon=Icons.Material.Filled.Edit, Text="Modificar", OnClick=btnEdtClick, IsEnabled=btnEdtEnabled},
        //new DataGridViewToolbarButton(){ Color= Color.Tertiary, Icon=Icons.Material.Filled.Delete, Text="Eliminar", OnClick=btnDelClick, IsEnabled=btnDelEnabled}
        };
        public string searchString { get; set; }
        void btnAddClick(object obj) => showEditor(new CoreUserGroup());
        void btnEdtClick(object obj)
        {
            var group = (CoreUserGroup)obj;
            if (group != null)
            {
                showEditor(group);
            }
        }

        private void showEditor(CoreUserGroup group)
        {
            var parameters = new DialogParameters();
            parameters.Add("usersGroup", group);
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            Dialog.Show<UserGroupEditor>("Editar", parameters, options);
        }

        bool btnAddEnabled(object obj) => true;
        bool btnEdtEnabled(object obj) => ((CoreUserGroup)obj) != null;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Groups = await userManagerService.GetUsersGroups();
        }

        private bool filterFunc(object obj)
        {
            var group = (CoreUserGroup)obj;
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (group.GroupName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}