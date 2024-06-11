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


namespace PrestamoBlazorApp.Pages.CoreSystem.Users
{
    public partial class UsersList : BasePage
    {
        [Inject] private UserManagerService userManagerService { get; set; }
        [Inject] private IDialogService Dialog { get; set; }

        private IEnumerable<CoreUser> UserList = new List<CoreUser>();
        //private List<string> columns => new List<string>() {"GroupName", "FullName", "Email", "IsActive" };
        private List<DataGridViewColumn> columns => new List<DataGridViewColumn>()
        {
        new DataGridViewColumn{ Header= "Grupo", ColumnName = "GroupName", ColumnType= DataGridViewColumnTypes.Text, ContentAligment= MudBlazor.Align.Left, HeaderAligment= MudBlazor.Align.Center},
        new DataGridViewColumn{ Header= "Usuario", ColumnName = "UserName", ColumnType= DataGridViewColumnTypes.Text, ContentAligment= MudBlazor.Align.Left, HeaderAligment= MudBlazor.Align.Center},
        new DataGridViewColumn{ Header= "Empleado", ColumnName = "FullName", ColumnType= DataGridViewColumnTypes.Text, ContentAligment= MudBlazor.Align.Left, HeaderAligment= MudBlazor.Align.Center},
        new DataGridViewColumn{ Header= "Identificaión", ColumnName = "NationalID", ColumnType= DataGridViewColumnTypes.Text, ContentAligment= MudBlazor.Align.Left, HeaderAligment= MudBlazor.Align.Center},
        new DataGridViewColumn{ Header= "Correo Electrónico", ColumnName = "Email", ColumnType= DataGridViewColumnTypes.Text, ContentAligment= MudBlazor.Align.Left, HeaderAligment= MudBlazor.Align.Center},
        new DataGridViewColumn{ Header= "Activo", ColumnName = "IsActive", ColumnType= DataGridViewColumnTypes.CheckBox, ContentAligment= MudBlazor.Align.Center, HeaderAligment= MudBlazor.Align.Center},
        new DataGridViewColumn{ Header= "Cambiar Contraseña", ColumnName = "MustChangePassword", ColumnType= DataGridViewColumnTypes.CheckBox, ContentAligment= MudBlazor.Align.Center, HeaderAligment= MudBlazor.Align.Center},
        };
        public List<DataGridViewToolbarButton> buttons => new List<DataGridViewToolbarButton>()
        {
        new DataGridViewToolbarButton(){ Color= Color.Primary, Icon=Icons.Material.Filled.AddCircle, Text="Nuevo", OnClick=btnAddClick, IsEnabled=btnAddEnabled},
        new DataGridViewToolbarButton(){ Color= Color.Secondary, Icon=Icons.Material.Filled.Edit, Text="Modificar", OnClick=btnEdtClick, IsEnabled=btnEdtEnabled},
        new DataGridViewToolbarButton(){ Color= Color.Tertiary, Icon=Icons.Material.Filled.VpnKey, Text="Debe Cambiar Contraseña", OnClick=btnChgPwdClick, IsEnabled=btnEdtEnabled},
        };
        async void btnChgPwdClick(object obj)
        {
            var usr = (CoreUser)obj;
            if (usr != null)
            {
                await userManagerService.ForceResetUserPassword(usr.UserID);
                await UpdateMustChangePassword(usr);
            }
        }

        async Task UpdateMustChangePassword(CoreUser usr)
        {
            UserList.FirstOrDefault(_usr => _usr.UserID == usr.UserID).MustChangePassword = true;
            StateHasChanged();
        }

        void btnAddClick(object obj) => showEditor(new CoreUser());
        void btnEdtClick(object obj)
        {
            var usr = (CoreUser)obj;
            if (usr!= null)
            {
                showEditor(usr);
            }
        }
        private void showEditor(CoreUser user)
        {
            var parameters = new DialogParameters();
            parameters.Add("user", user);
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            Dialog.Show<CoreUserEditor>("Editar", parameters, options);
        }
        bool btnAddEnabled(object obj) => true;
        bool btnEdtEnabled(object obj) => ((CoreUser)obj) != null;        
        public string searchString { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            UserList = await userManagerService.GetUsers();
        }
        private bool filterFunc(object obj)
        {
            var group = (CoreUser)obj;
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (group.GroupName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}
