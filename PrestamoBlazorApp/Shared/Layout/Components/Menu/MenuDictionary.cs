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
                Inherited = false
            },
            new CustomMenuAction
            {
                ID =new System.Guid( "{8D8FD452-0AD9-44B2-B8BE-F49A7C36AE01}"),
                Url = "/marcas",
                DisplayName = "Listado de Marcas y Modelos",
                Description = "ver el listado de las Marcas y Modelos",
                GroupName = "Catalogo de Garantias",
                Value = "Listado de Marcas y Modelos",
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
                Inherited = false
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
