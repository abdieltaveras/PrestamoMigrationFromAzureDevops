using DevBox.Core.Access;
using PrestamoBlazorApp.Shared.Model;
using System.Collections.Generic;

namespace PrestamoBlazorApp.Shared.Layout.Components.Menu
{
    public class MenuDictionary
    {
        public static CustomMenuAction[] MenuDictionaryData()
        {
            CustomMenuAction[] actionData = new CustomMenuAction[]
           {
            new CustomMenuAction
            {
                ID = new System.Guid( "8D8FD452-0AD9-44B2-B8BE-F49A7C36AE00"),
                Url = "/colores",
                DisplayName = "Listado de Colores",
                Description = "Listado de colores",
                GroupName = "Catalogo de Garantias",
                Value = "Listado de Colores",
                PermissionLevel = ("None").ParseEnum<ActionPermissionLevel>(),
                Inherited = false,
                CustomMenuSubActions = new List<CustomMenuSubAction>
                {
                    new CustomMenuSubAction
                    {
                        Url = "/colores",
                        DisplayName = "Agregar",
                        Description = "agregar un nuevo color",
                        Value = "AgregarColor",
                        PermissionLevel = ActionPermissionLevel.None,
                        Inherited = false
                    },
                    new CustomMenuSubAction
                    {
                        Url = "/color/edit",
                        DisplayName = "Editar",
                        Description = "Modificar un color",
                        Value = "EditarColor",
                        PermissionLevel = ActionPermissionLevel.None,
                        Inherited = false
                    },
                    new CustomMenuSubAction
                    {
                        Url = "/color/delete",
                        DisplayName = "Eliminar",
                        Description = "eliminar un color",
                        Value = "EliminarColor",
                        PermissionLevel =ActionPermissionLevel.None,
                        Inherited = false
                    }
                }
            },
            new CustomMenuAction
            {
                ID =new System.Guid( "{8D8FD452-0AD9-44B2-B8BE-F49A7C36AE01}"),
                Url = "/marcas",
                DisplayName = "Listado de Marcas y Modelos",
                Description = "ver el listado de las Marcas y Modelos",
                GroupName = "Catalogo de Garantias",
                Value = "Listado de Marcas Y Modelos  De Garantias",
                PermissionLevel =  ("None").ParseEnum<ActionPermissionLevel>(),
                Inherited = false
            },
            new CustomMenuAction
            {
                ID = new System.Guid("{8D8FD452-0AD9-44B2-B8BE-F49A7C36AE03}"),
                Url = "/garantias",
                DisplayName = "Listado de Garantias",
                Description = "Garantias",
                GroupName = "Catalogo de Garantias",
                Value = "Listado de Garantias",
                PermissionLevel = ("None").ParseEnum<ActionPermissionLevel>(),
                Inherited = false
            },
            new CustomMenuAction
            {
                ID = new System.Guid("{8D8FD452-0AD9-44B2-B8BE-F49A7C36BE01}"),
                Url = "/clientes",
                DisplayName = "Listado de clientes",
                Description = "Clientes",
                GroupName = "Clientes",
                Value = "Listado de Clientes",
                PermissionLevel =  ("None").ParseEnum<ActionPermissionLevel>(),
                Inherited = false
            },
            new CustomMenuAction
            {
                ID = new System.Guid("{8D8FD452-0AD9-44B2-B8BE-F49A7C36BE02}"),
                Url = "/cliente/crear",
                DisplayName = "Crear Cliente",
                Description = "ver el listado de Cliente",
                GroupName = "Clientes",
                Value = "CrearCliente",
                PermissionLevel = ("None").ParseEnum<ActionPermissionLevel>(),
                Inherited = false,

            },
            new CustomMenuAction
            {
                ID = new System.Guid("C56A4180-65AA-42EC-A945-5FD21DEC0001"),
                Url = "/Auth/Grupos",
                DisplayName = "Grupos",
                Description = "listado de Grupos",
                GroupName = "Configuracion",
                Value = "ListadoGrupos",
                PermissionLevel = ActionPermissionLevel.None,
                Inherited = false
            },
            new CustomMenuAction
            {
                ID = new System.Guid("C56A4180-65AA-42EC-A945-5FD21DEC0002"),
                Url = "/Auth/Usuarios",
                DisplayName = "Usuarios",
                Description = "ver la lista de usuarios",
                GroupName = "Configuracion",
                Value = "ListadoUsuario",
                PermissionLevel = ActionPermissionLevel.None,
                Inherited = false
            },new CustomMenuAction
            {
                ID = new System.Guid("C56A4180-65AA-42EC-A945-5FD21DEC0003"),
                Url = "/Auth/ActionManager",
                DisplayName = "Seguridad",
                Description = "Control de Acceso",
                GroupName = "Sistema",
                Value = "ActionManager",
                PermissionLevel = ActionPermissionLevel.None,
                Inherited = true
            }
        };
            return actionData;
        }
      public static Dictionary<string, CustomMenuAction> GetMenuDictionary()
        {
       

            var actionDictionary = new Dictionary<string, CustomMenuAction>();

            foreach (var data in MenuDictionaryData())
            {
                //var customMenuAction = new CustomMenuAction
                //{
                //    ID = new System.Guid( data.id),
                //    Url = data.url,
                //    DisplayName = data.displayName,
                //    Description = data.description,
                //    GroupName = data.groupName,
                //    Value = data.value,
                //    PermissionLevel =data.permissionLevel.ParseEnum<ActionPermissionLevel>(),
                //    Inherited = data.inherited,
                //};

                actionDictionary.Add(data.Value, data);
            }
            return actionDictionary;
        }
    }
}
