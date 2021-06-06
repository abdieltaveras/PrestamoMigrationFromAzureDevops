using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;


        JsInteropUtils jsInterop { get; set; } = new JsInteropUtils();

        [Inject]
        NavigationManager navManager { get; set; }
        [Inject]
        IJSRuntime jsRuntime { get; set; }
        private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private Dictionary<MenuItem, bool> MenuItems { get; set; } = new Dictionary<MenuItem, bool>();

        //private bool expandCatalogosSubMenu { get; set; } = false;
        //private bool expandOperacionesSubMenu { get; set; } = false;
        //private bool expandGarantiasSubMenu { get; set; } = false;
        //private bool expandTerritoriosSubMenu { get; set; } = false;
        //private bool expandClientesSubMenu { get; set; } = false;

        public NavMenu()
        {
            CreateCatalogosSubMenu();
        }
        //protected override void OnInitialized()  {}
        private void ToggleNavMenu()
        {
            //jsInterop.Alert(jsRuntime,"click en toggleNavMenu");
            collapseNavMenu = !collapseNavMenu;
        }

        private void CreateCatalogosSubMenu()
        {
            MenuItems.Add(new MenuItem { CurrentText = MenuText.Catalogo_I, MenuPadreText = null }, false);
            MenuItems.Add(new MenuItem { CurrentText = MenuText.Localidades, MenuPadreText = MenuText.Catalogo_I }, false);
            MenuItems.Add(new MenuItem { CurrentText = MenuText.Territorios, MenuPadreText = MenuText.Catalogo_I }, false);
            MenuItems.Add(new MenuItem { CurrentText = MenuText.Catalogo_II, MenuPadreText = null }, false);
            MenuItems.Add(new MenuItem { CurrentText = MenuText.Garantias, MenuPadreText = MenuText.Catalogo_II }, false);
            MenuItems.Add(new MenuItem { CurrentText = MenuText.Clientes, MenuPadreText = MenuText.Catalogo_II }, false);
            MenuItems.Add(new MenuItem { CurrentText = MenuText.Prestamo, MenuPadreText = MenuText.Catalogo_II }, false);
        }
        private void navigateTo(string linkUrl)
        {
            navManager.NavigateTo(linkUrl, true);
        }

        string lastActiveMenu { get; set; }
        private void SetActiveMenu(string menuText)
        {
            if (!string.IsNullOrEmpty(lastActiveMenu))
            {
                var LastMenuItemSelected = MenuItems.Where(item => item.Key.CurrentText == lastActiveMenu).FirstOrDefault();
                ManageMenuItem(LastMenuItemSelected, false);
                var iguales = LastMenuItemSelected.Key.CurrentText == menuText;
                if (LastMenuItemSelected.Key.CurrentText == menuText)
                {
                    lastActiveMenu = string.Empty;
                    return;
                }
            }

            // para el item seleccionado
            var MenuItemSelected = MenuItems.Where(item => item.Key.CurrentText == menuText).FirstOrDefault();
            ManageMenuItem(MenuItemSelected, true);
            lastActiveMenu = MenuItemSelected.Key.CurrentText;
        }

        private void ManageMenuItem(KeyValuePair<MenuItem, bool> MenuItemSelected, bool value)
        {
            MenuItems[MenuItemSelected.Key] = value;
            var textMenuPadre = MenuItemSelected.Key.MenuPadreText;
            if (!string.IsNullOrEmpty(textMenuPadre))
            {
                var MenuPadreItem = MenuItems.Where(item => item.Key.CurrentText == textMenuPadre).FirstOrDefault();
                MenuItems[MenuPadreItem.Key] = value;
            }

        }

        private bool ExpandMenu(string menuText)
        {

            var menuItem = MenuItems.Where(item => item.Key.CurrentText == menuText).FirstOrDefault();
            var result = MenuItems[menuItem.Key];
            return result;
        }


        public static class MenuText
        {
            public static string Catalogo_I => "Catalogo I";
            public static string Catalogo_II => "Catalogo II";
            public static string Garantias => "Garantias";
            public static string Territorios => "Territorios";
            public static string Localidades => "Localidades";
            public static string Clientes => "Clientes";
            public static string Prestamo => "Prestamo";
        }

        public class MenuItem
        {
            public string MenuPadreText { get; set; }

            public string CurrentText { get; set; }

        }
    }
}
