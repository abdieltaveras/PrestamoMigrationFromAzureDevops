using Microsoft.AspNetCore.Components;
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


    }

    public class Users
    {
        public string Nombre { get; set; }
        public string Contraseña { get; set; }


        public class UserListModel
        {
            public List<Users> Users { get; set; } = new List<Users>();

            public void AddUser(Users newUser)
            {

                Users.Add(newUser);

            }
        }
        public class UserValidator
        {
            public bool ValidateUser(string Nombre, string Contraseña)
            {
                return Nombre == "usuario" && Contraseña == "contraseña";


            }


            private Users users = new Users();
            private string errorMessage;

            private void HandleValidSubmit()
            {
                UserValidator validator = new UserValidator();

                // Validar el usuario utilizando la clase UserValidator
                if (validator.ValidateUser(users.Nombre, users.Contraseña))
                {
                    // Usuario autenticado con éxito
                    // Puedes redirigir a otra página, establecer información de sesión, etc.
                    // NavigationManager.NavigateTo("/pagina-siguiente");
                }
                else
                {
                    // Autenticación fallida, puedes mostrar un mensaje de error en la interfaz de usuario.
                    errorMessage = "Credenciales incorrectas";
                }
            }

        }

    }
}






