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

        private IEnumerable<Users> UserList { get; set; } = new List<Users>();
        protected override void OnInitialized()
        {
            UserList= new List<Users>
            {
                new Users { Nombre = "RANDY", Contraseña = "1438" },
                new Users { Nombre = "CRISTAL ", Contraseña = "1235" },
                new Users { Nombre = "CARLOS", Contraseña = "carlos145" }
            };

            base.OnInitialized();
            
        
        }

        
          
    
      private  async Task ValidateUser( Users users)
      {

            UserList.Any(u =>
            u.Nombre == users.Nombre &&
            u.Contraseña == users.Contraseña);
      }


    }


 }

    public class Users
    {
        public string Nombre { get; set; }
        public string Contraseña { get; set; }


    }

  






