using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Login
{
    public partial class LoginTest : BaseForCreateOrEdit
    {

        private Users users { get; set; } = new Users();

        private IEnumerable<Users> UserList { get; set; } = new List<Users>();
        protected override void OnInitialized()
        {
            UserList = new List<Users>
            {
                new Users { Nombre = "RANDY", Contraseña = "1438" },
                new Users { Nombre = "CRISTAL", Contraseña = "1235" },
                new Users { Nombre = "CARLOS", Contraseña = "carlos145" }
            };

            base.OnInitialized();


        }
        private bool ValidateUser(Users users)
        {
            return
            UserList.Any(u => u.Nombre == users.Nombre && u.Contraseña == users.Contraseña);
        }
        private async Task HandleValidSubmit()
        {
            var ValidatedUser = ValidateUser(users);
            // Validar el usuario utilizando la clase UserValidator
            if (ValidatedUser)
            {
                // Usuario autenticado con éxito
                // Puedes redirigir a otra página, establecer información de sesión, etc.
                await NavigateTo("/loginEmpresa");
                await NotifyMessageBySnackBar("Acceso concedido ", Severity.Success);

            }
            else
            {
                await NotifyMessageBySnackBar("Credenciales incorrectas", Severity.Error);
                users.Nombre = string.Empty;
                users.Contraseña = string.Empty;
                //private async Task NotImplementedMessage() => await Task.Run(() => Snackbar.Add("Opcion no implementada o revisar la url y asignarla", Severity.Warning));
                //Snackbar.Add("Opcion no implementada o revisar la url y asignarla", Severity.Warning));
            }



        }

    }
}
 
    public class Users
    {
        public string Nombre { get; set; }
        public string Contraseña { get; set; }


        //public class UserListModel
        //{
        //    public List<Users> Users { get; set; } = new List<Users>();

        //    public void AddUser(Users newUser)
        //    {

        //        Users.Add(newUser);

        //    }
        //}
        
    }







